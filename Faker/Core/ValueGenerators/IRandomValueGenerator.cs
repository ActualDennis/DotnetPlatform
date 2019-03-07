using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core {
    public interface IRandomValueGenerator {
        /// <summary>
        /// Generates default, probably randomized value 
        /// </summary>
        /// <returns></returns>
        object GenerateValue();
    }
}
