using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class ShortValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return new Random().Next(0, short.MaxValue);
        }
    }
}
