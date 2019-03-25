using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiContainer.Core {
    public class DiConfiguration {

        public DiConfiguration()
        {
            Configuration = new List<ContainerEntity>();
        }

        public List<ContainerEntity> Configuration { get; private set; }

        public void RegisterTransient(Type interfaceType, Type implementationType)
        {
            if (interfaceType.IsValueType || implementationType.IsValueType)
                throw new InvalidOperationException("Both implementation and interface should be reference types.");

            RegisterCore(interfaceType, implementationType, ObjLifetime.Transient);
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            if (interfaceType.IsValueType || implementationType.IsValueType)
                throw new InvalidOperationException("Both implementation and interface should be reference types.");

            RegisterCore(interfaceType, implementationType, ObjLifetime.Singleton);
        }


        public void RegisterTransient<TInterface, TImplementation>() 
            where TInterface : class
            where TImplementation : class
        {
            RegisterCore(typeof(TInterface), typeof(TImplementation),ObjLifetime.Transient);
        }

        public void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class 
            where TImplementation : class
        {
            RegisterCore(typeof(TInterface), typeof(TImplementation), ObjLifetime.Singleton);
        }

        public ObjLifetime GetObjectLifeTime(Type implType)
        {
            return (from conf in Configuration from impl in conf.Implementations where impl.ImplType == implType select impl.LifeTime).First();
        }

        private void RegisterCore(Type interfaceType, Type implementationType,  ObjLifetime lifetime)
        {
            if (implementationType.GetInterfaces().FirstOrDefault(x => x.Name == interfaceType.Name) == null)
                throw new InvalidOperationException($"Type {implementationType.ToString()} is not assignable from {interfaceType.ToString()}");

            if (implementationType.IsAbstract || implementationType.IsInterface)
                throw new InvalidOperationException($"Type {implementationType.ToString()} couldn't be abstract or interface.");

            var entity =
                from x in Configuration
                where x.InterfaceType == interfaceType
                select x;

            if (entity.Count().Equals(0))
            {
                Configuration.Add(new ContainerEntity()
                {
                    Implementations = new List<Implementation>()
                    {
                        new Implementation()
                        {
                            ImplType = implementationType,
                            LifeTime = lifetime
                        }
                    },
                    InterfaceType = interfaceType
                });

                return;
            }

            if (entity.First().Implementations.FirstOrDefault(x => x.ImplType == implementationType) != null)
                return;

            entity.First().Implementations.Add(new Implementation() { ImplType = implementationType, LifeTime = lifetime });
        }
    }
}
