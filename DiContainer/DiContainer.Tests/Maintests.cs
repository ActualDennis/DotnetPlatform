using DiContainer.Core;
using DiContainer.Tests.TestData;
using DiContainer.Tests.TestData.Generics;
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
        public void BasicTest()
        { 
            config.RegisterSingleton<IProduct, FirstProduct>();
            config.RegisterTransient<IUser, User>();

            provider = new DependencyProvider(config);
            
            var item = provider.Resolve<IProduct>();

            Assert.That(item != null);
        }

        [Test]
        public void ManyTest()
        {
            config.RegisterSingleton<IProduct, FirstProduct>();
            config.RegisterSingleton<IProduct, SecondProduct>();
            config.RegisterTransient<IUser, User>();

            provider = new DependencyProvider(config);

            var items = provider.Resolve<IEnumerable<IProduct>>();

            Assert.That(items.Count() == 2);
        }

        [Test]
        public void GenericsTest()
        { 
            provider = new DependencyProvider(config);

            var item = provider.Resolve<IService<SomeRepository>>();

            Assert.That(item != null);
        }

    }
}