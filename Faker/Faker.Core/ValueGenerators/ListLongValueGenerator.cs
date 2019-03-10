using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListLongValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<long>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(long));
            returnList.Add((long)generatedValue);
            return returnList;
        }
}
}
