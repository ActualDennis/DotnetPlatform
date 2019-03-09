using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Tests {

    public class testClass6 {
        public testClass5 field { get; set; }
    }

    public class testClass5 {
        public testClass6 field { get; set; }
    }
}
