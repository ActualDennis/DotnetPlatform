using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class FloatDoubleValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return (float)new Random().NextDouble();
        }
    }
}
