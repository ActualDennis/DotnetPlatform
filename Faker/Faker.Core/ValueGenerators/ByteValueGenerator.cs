using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class ByteValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return (byte)new Random().Next(0, byte.MaxValue);
        }
    }
}
