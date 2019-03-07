using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    public class ListIntValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<int>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(int));
            returnList.Add((int)generatedValue);
            return returnList;
        }
    }
}
