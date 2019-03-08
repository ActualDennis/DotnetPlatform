using System;
using System.Collections.Generic;
using System.Reflection;

namespace Faker.Core {
    public class Faker {

        private Stack<Type> dtoDependencies { get; set; } = new Stack<Type>();

        public TInput Create<TInput>()
        {
            try
            {
                if (IsDto(typeof(TInput)))
                {
                    var result = (TInput)CreateDto(typeof(TInput));

                    TryFillProperties(ref result);

                    return result;
                }
                else
                {
                    throw new ArgumentException("Parameter: TInput");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        private object CreateDto(Type typeOfDto)
        {
            var constructors = typeOfDto.GetConstructors();

            if(constructors.Length == 0)
            {
                throw new ArgumentException("typeofDto");
            }

            var constructor = constructors[0];
            var parameters = constructors[0].GetParameters();
            var parametersArray = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; ++i)
            {
                var propertyType = parameters[i].GetType();

                var generatedValue = DefaultValuesProvider.GenerateValue(propertyType);

                if (generatedValue == null)
                {
                    if (propertyType.IsValueType)
                    {
                        generatedValue = Activator.CreateInstance(propertyType);
                    }
                }

                parametersArray[i] = generatedValue;
            }

            return constructor.Invoke(parametersArray);
        }

        private bool TryFillProperties<TInput>(ref TInput input)
        {
            try
            {

                var t = input.GetType();

                var publicProperties = t.GetProperties();

                foreach (var property in publicProperties)
                {
                    var propertyType = property.GetMethod.ReturnType;

                    if (IsDto(propertyType))
                    {
                        var result = TryFillProperties(ref propertyType);

                        if (!result)
                            return false;

                        continue;
                    }

                    var generatedValue = DefaultValuesProvider.GenerateValue(propertyType);

                    //if property of value type is set to null, it'll get default value.

                    property.SetValue(input, generatedValue);
                }

                return true;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        private bool IsDto(Type t)
        {
            if (DefaultValuesProvider.IsSupportedType(t))
                return false;

            if (ContainsCircularDependencies(t))
            {
                throw new ArgumentException("t : type did contain circular dependencies.");
            }

            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach(var field in fields)
            {
                if (!DefaultValuesProvider.IsSupportedType(field.FieldType))
                {
                    dtoDependencies.Push(t);

                    try
                    {
                        if (!IsDto(field.GetType()))
                            return false;
                    }
                    catch (ArgumentException)
                    {
                        throw;
                    }

                    dtoDependencies.Pop();
                }
            }

            return true;
        }

        private bool ContainsCircularDependencies(Type t)
        {
            foreach(var dependency in dtoDependencies)
            {
                if (dependency == t)
                    return true;
            }

            return false;
        }
    }
}
