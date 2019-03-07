using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListShortValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<short>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(short));
            returnList.Add((short)generatedValue);
            return returnList;
        }
    }
}
