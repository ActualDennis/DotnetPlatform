using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListStringValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<string>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(string));
            returnList.Add((string)generatedValue);
            return returnList;
        }
    }
}
