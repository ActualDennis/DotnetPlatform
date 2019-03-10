using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListByteValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<byte>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(byte));
            returnList.Add((byte)generatedValue);
            return returnList;
        }
    }
}
