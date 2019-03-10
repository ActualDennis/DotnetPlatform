using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Faker.Core {
    public class CustomValueProvider {
        public CustomValueProvider(FakerConfig config)
        {
            Config = config;
        }

        private FakerConfig Config { get; set; }
        
        public object GenerateValue(string propertyName, Type propertyType, Type declaringType)
        {
            try
            {
                return Config
                    .configs
                    .Find(x => x.PropertyName == propertyName && x.PropertyType == propertyType && x.TypeFor == declaringType)?
                    .Generator
                    .GenerateValue();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool HasDefinition(string propertyName, Type propertyType , Type declaringType)
        {
            return Config.configs.Any(x => x.PropertyName == propertyName && x.PropertyType == propertyType && x.TypeFor == declaringType);
        }
    }
}
