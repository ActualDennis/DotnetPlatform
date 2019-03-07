using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class UnrecognizedValueGenerator<T> {
        public object GenerateValue<T>()
        {
            return default(T);
        }
    }
}
