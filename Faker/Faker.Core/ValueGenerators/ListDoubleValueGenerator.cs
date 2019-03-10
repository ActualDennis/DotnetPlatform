using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core.ValueGenerators {
    class ListDoubleValueGenerator : IRandomValueGenerator {
        public object GenerateValue()
        {
            var returnList = new List<double>();
            var generatedValue = DefaultValuesProvider.GenerateValue(typeof(double));
            returnList.Add((double)generatedValue);
            return returnList;
        }
    }
}
