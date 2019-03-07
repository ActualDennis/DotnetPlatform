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
                {typeof(int), new IntLongValueGenerator()},
                {typeof(long), new IntLongValueGenerator()},
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
        }
        public static Dictionary<Type, IRandomValueGenerator> TypesGenerators { get; set; }

        public static object GenerateValue(Type typeOfValue)
        {
            if (TypesGenerators.ContainsKey(typeOfValue))
                return TypesGenerators[typeOfValue].GenerateValue();
            else
                return null;

        }
    }
}
