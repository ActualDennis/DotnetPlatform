using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.Additional_value_generators {
    public class AlwaysOneValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            return "1"; 
        }
    }
}
