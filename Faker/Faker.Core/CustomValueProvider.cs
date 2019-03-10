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
        
        public object GenerateValue(PropertyInfo property)
        {
            try
            {
                return Config
                    .configs
                    .Find(x => x.PropertyName == property.Name && x.PropertyType == property.PropertyType && x.TypeFor == property.DeclaringType)?
                    .Generator
                    .GenerateValue();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool HasDefinition(PropertyInfo property)
        {
            return Config.configs.Any(x => x.PropertyName == property.Name && x.PropertyType == property.PropertyType && x.TypeFor == property.DeclaringType);
        }
    }
}
