using Faker.Core.ValueGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core {
    public class DefaultValuesProvider {
        static DefaultValuesProvider()
        {
            TypesGenerators = new Dictionary<Type, IRandomValueGenerator>()
            {
                {typeof(int), new IntValueGenerator()},
                {typeof(long), new LongValueGenerator()},
                {typeof(short), new ShortValueGenerator()},
                {typeof(float), new FloatDoubleValueGenerator()},
                {typeof(double), new FloatDoubleValueGenerator()},
                {typeof(string), new StringValueGenerator()},
                {typeof(DateTime), new DateTimeValueGenerator()},
                {typeof(byte), new ByteValueGenerator() },
                {typeof(List<int>), new ListIntValueGenerator()},
                {typeof(List<long>), new ListLongValueGenerator()},
                {typeof(List<short>), new ListShortValueGenerator()},
                {typeof(List<float>), new ListFloatValueGenerator()},
                {typeof(List<double>), new ListDoubleValueGenerator()},
                {typeof(List<string>), new ListStringValueGenerator()},
                {typeof(List<DateTime>), new ListDateTimeValueGenerator()},
                {typeof(List<byte>), new ListByteValueGenerator()}
            };

            SimpleTypes = new List<Type>()
            {
                typeof(int),
                typeof(long),
                typeof(short),
                typeof(float),
                typeof(double),
                typeof(string),
                typeof(DateTime),
                typeof(byte)
            };

            ListTypes = new List<Type>()
            {
                typeof(List<int>),
                typeof(List<long>),
                typeof(List<short>),
                typeof(List<float>),
                typeof(List<double>),
                typeof(List<string>),
                typeof(List<DateTime>),
                typeof(List<byte>)
            };
        }
        public static Dictionary<Type, IRandomValueGenerator> TypesGenerators { get; set; }

        public static List<Type> SimpleTypes { get; set; }

        public static List<Type> ListTypes { get; set; }

        public static object GenerateValue(Type typeOfValue)
        {
            if (TypesGenerators.ContainsKey(typeOfValue))
                return TypesGenerators[typeOfValue].GenerateValue();
            else
                return null;

        }

        public static bool IsSupportedType(Type t)
        {
            foreach(var type in SimpleTypes)
            {
                if (t == type)
                    return true;
            }

            foreach (var type in ListTypes)
            {
                if (t == type)
                    return true;
            }

            return false;
        }
    }
}
