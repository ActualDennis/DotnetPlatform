using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core {
    public class Faker<TInput> : IFaker<TInput> {
        public TInput Create<TInput>()
        {
            var type = typeof(TInput).GetType();

            if(type.GetConstructors().Length == 0)
            {
                var publicProps = type.GetProperties();

                foreach(var property in publicProps)
                {

                }
            }
        }
    }
}
