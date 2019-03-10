using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class IntValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return (int)new Random().Next(0, int.MaxValue);
        }
    }
}
