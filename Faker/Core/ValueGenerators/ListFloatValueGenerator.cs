using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListFloatValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<float>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(float));
            returnList.Add((float)generatedValue);
            return returnList;
        }
    }
}
