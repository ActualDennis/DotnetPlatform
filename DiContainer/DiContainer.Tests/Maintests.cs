using DiContainer.Core;
using DiContainer.Tests.TestData;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    public class Tests {
        DiConfiguration config;
        DependencyProvider provider;
        [SetUp]
        public void Setup()
        {
            config = new DiConfiguration();
        }

        [Test]
        public void Basic()
        {
            config.RegisterSingleton<IProduct, FirstProduct>();
            config.RegisterSingleton<IProduct, SecondProduct>();
            config.RegisterTransient<IUser, User>();

            provider = new DependencyProvider(config);

            var items = provider.Resolve<IEnumerable<IProduct>>();

            Assert.That(items.Count() == 2);
        }
    }
}