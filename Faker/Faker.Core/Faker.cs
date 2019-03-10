using System;
using System.Collections.Generic;
using System.Reflection;

namespace Faker.Core {
    public class Faker {
        public Faker(FakerConfig config)
        {
            customValueProvider = new CustomValueProvider(config);
        }

        private Stack<Type> dtoDependencies { get; set; } = new Stack<Type>();

        private CustomValueProvider customValueProvider { get; set; }

        public TInput Create<TInput>()
        {
            try
            {
                if (!AnyCircularDependencies(typeof(TInput)))
                {
                    var result = CreateDto(typeof(TInput));

                    TryFillProperties(ref result, typeof(TInput));

                    return (TInput)result;
                }
                else
                {
                    throw new ArgumentException($"Parameter: {nameof(TInput).ToString()}, type did contain circular dependencies.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        /// <exception cref="ArgumentException">Throws if type doesn't have a contructor</exception>
        /// <summary>
        /// Creates dto using constructor.
        /// </summary>
        /// <param name="typeOfDto"></param>
        /// <returns></returns>
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

        private bool TryFillProperties(ref object input,Type typeOfObject)
        {
            try
            {

                var t = typeOfObject;

                var publicProperties = t.GetProperties();

                foreach (var property in publicProperties)
                {
                    var propertyType = property.GetMethod.ReturnType;

                    if (customValueProvider.HasDefinition(property))
                    {
                        property.SetValue(input, customValueProvider.GenerateValue(property));
                        continue;
                    }

                    if (IsDto(propertyType))
                    {
                            
                        object nestedDto;

                        try
                        {
                            nestedDto = CreateDto(property.GetMethod.ReturnType);
                            var result = TryFillProperties(ref nestedDto, property.GetMethod.ReturnType);
                        }
                        catch (Exception)
                        {

                            nestedDto = null;
                        }
                        
                        property.SetValue(input, nestedDto);

                        continue;
                    }


                    var generatedValue = DefaultValuesProvider.GenerateValue(propertyType);

                    if(generatedValue == null)
                    {
                        try
                        {
                            generatedValue = Activator.CreateInstance(propertyType);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Type {propertyType.ToString()} is not a DTO and does not have parameterless constructor. Set this field to null.");
                        }
                    }

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

            if (t.Assembly.IsMicrosoftAssembly())
                return false;

            return true;
        }

        /// <summary>
        /// Checks if type has circular dependencies.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool AnyCircularDependencies(Type t)
        {
            if (ContainsCircularDependencies(t))
                return true;

            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (!DefaultValuesProvider.IsSupportedType(field.FieldType))
                {
                    if (!t.Assembly.IsMicrosoftAssembly())
                    {
                        dtoDependencies.Push(t);

                        if (AnyCircularDependencies(field.FieldType))
                            return true;

                        dtoDependencies.Pop();
                    }
                }
            }

            return false;
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
