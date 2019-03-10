using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Faker.Core {
    public class FakerConfig {
        public FakerConfig()
        {
            configs = new List<CustomValueGenerator>();
        }

        public List<CustomValueGenerator> configs { get; set; }
        
        public void Add<TObject, TField, TRandomValueGenerator>(Expression<Func<TObject, object>> expression) where TRandomValueGenerator : IRandomValueGenerator
        {
            if (expression == null)
                throw new ArgumentNullException("expression", "expression is null.");

            var memberExpression = expression.Body as MemberExpression;

            if(memberExpression == null)
                throw new ArgumentNullException("expression does not contain a member reference", "memberExpression is null.");

            configs.Add(new CustomValueGenerator()
            {
                Generator = (IRandomValueGenerator)Activator.CreateInstance(typeof(TRandomValueGenerator)),
                TypeFor = typeof(TObject),
                PropertyName = memberExpression.Member.Name,
                PropertyType = ((PropertyInfo)memberExpression.Member).PropertyType
            });
        }

    }
}
