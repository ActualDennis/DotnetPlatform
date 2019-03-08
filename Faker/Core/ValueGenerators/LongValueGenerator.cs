using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class LongValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return (long)new Random().Next(0, int.MaxValue);
        }
    }
}
