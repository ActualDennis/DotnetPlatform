using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Test {
    public class Foo {
        public Foo()
        {

        }

        public string str { get; set; }
        public int integer { get; set; }
        public Foo b { get; set; }
        public DateTime _time { get; set; }

        public int x { get; set; }
        public List<long> longs { get; set; }
    }
}
