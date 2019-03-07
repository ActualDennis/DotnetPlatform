using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListDateTimeValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<DateTime>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(DateTime));
            returnList.Add((DateTime)generatedValue);
            return returnList;
        }
    }
}
