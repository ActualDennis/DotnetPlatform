using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class StringValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var stringArray = new byte[54];

            new Random().NextBytes(stringArray);

            return Encoding.UTF8.GetString(stringArray);
        }
    }
}
