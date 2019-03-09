using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Data {

    public class Bar {
        public Bar()
        {
            
        }

        public string str { get; set; }

        public List<char> x { get; set; }

        public char foo { get; set; }
    }

    public class Foo {
        public Foo()
        {

        }

        public string str { get; set; }
        public int integer { get; set; }
        public Bar bar { get; set; }
        public DateTime _time { get; set; }

        public int x { get; set; }
        public List<long> longs { get; set; }
    }
}
