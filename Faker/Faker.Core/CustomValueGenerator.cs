using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core {
    public class CustomValueGenerator {
        public IRandomValueGenerator Generator { get; set; }

        public Type TypeFor { get; set; }

        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }
    }
}
