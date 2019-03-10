using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class DateTimeValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return DateTime.Now;
        }
    }
}
