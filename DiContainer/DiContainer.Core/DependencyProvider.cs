using DiContainer.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DiContainer.Core {
    public class DependencyProvider {
        private DiConfiguration m_Configuration { get; set; }

        private List<CreatedObject> CreatedObjects { get; set; }

        public DependencyProvider(DiConfiguration config)
        {
            m_Configuration = config;
            CreatedObjects = new List<CreatedObject>();
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)ResolveCore(typeof(TInterface));
        }

        public Object ResolveCore(Type interfaceType)
        {
            bool IsEnumerable = false;

            if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                interfaceType = interfaceType.GetGenericArguments()[0];
                IsEnumerable = true;
            }

            var entity =
                m_Configuration
                .Configuration
                .Find(x => x.InterfaceType == interfaceType);

            if (entity == null)
                throw new InvalidOperationException($"Dependency {interfaceType.ToString()} was not registered in container.");

            var result = new List<Object>();

            foreach (var impl in entity.Implementations)
            {
                var constructorDependencies = GetConstructorDependencies(impl.ImplType);

                if (constructorDependencies.Count().Equals(0))
                {
                    result.Add(CreateObjectCore(null, impl.ImplType, interfaceType));
                    continue;
                }

                var parametersToPass = new object[constructorDependencies.Count()];

                for(int dependency = 0; dependency < constructorDependencies.Count(); ++dependency)
                {
                    parametersToPass[dependency] = ResolveCore(constructorDependencies[dependency]);
                }

                result.Add(CreateObjectCore(parametersToPass, impl.ImplType, interfaceType));
            }

            if (IsEnumerable)
                return result.ListObjToEnumerableType(interfaceType);
            else
                return result.First();
        }

        private Object CreateObjectCore(object[] constructorParams, Type t, Type interfaceType)
        {
            var lifeTime = m_Configuration.GetObjectLifeTime(t);

            if (lifeTime == ObjLifetime.Transient)
            {
                return CreateObjInternal(constructorParams, t, interfaceType, lifeTime);
            }
            else if (lifeTime == ObjLifetime.Singleton)
            {
                if (IsObjectCreated(t))
                    return GetCreatedObject(t);

                return CreateObjInternal(constructorParams, t, interfaceType, lifeTime);
            }

            throw new NotImplementedException();
        }

        private Object CreateObjInternal(object[] constructorParams, Type t, Type interfaceType, ObjLifetime lifetime)
        {
            object createdObj;

            if (constructorParams == null)
            {
                createdObj = Activator.CreateInstance(t);
            }
            else
            {
                createdObj = GetConstructor(t).Invoke(constructorParams);
            }

            CreatedObjects.Add(new CreatedObject()
            {
                ObjType = t,
                Interface = interfaceType,
                SingletonInstance = (lifetime == ObjLifetime.Singleton) ? createdObj : null
            });

            return createdObj;
        }

        private bool IsObjectCreated(Type t)
        {
            return CreatedObjects.Find(x => x.ObjType == t) != null;
        }

        private Object GetCreatedObject(Type t)
        {
            return CreatedObjects.Find(x => x.ObjType == t);
        }

        private List<Type> GetConstructorDependencies(Type classType)
        {
            var constructor = GetConstructor(classType);

            if (constructor == null)
                return null;

            return constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToList();
        }

        private ConstructorInfo GetConstructor(Type classType)
        {
            var constructors = classType.GetConstructors();
            return constructors.Length == 0 ? null : constructors[0];
        }
    }
}
