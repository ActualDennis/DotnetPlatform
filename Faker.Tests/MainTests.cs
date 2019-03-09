using Faker.Tests;
using NUnit.Framework;
using System;

namespace Tests {
    public class Tests {

        Faker.Core.Faker faker; 

        [SetUp]
        public void Setup()
        {
            faker = new Faker.Core.Faker();
        }

        [Test]
        public void BasicTest()
        {
            var result = new testClass1();

            Assert.DoesNotThrow(() => result = faker.Create<testClass1>());

            Assert.True(result != null, "Faker created null object.");

            Assert.True(result.firstList != null, "Failed to initialize firstList");

            Assert.True(result.unsupportedList != null, "Failed to initialize unsupportedList");

            Assert.True(result.firstList.Count == 1, "Failed to add value to firstList");
        }

        [Test]
        public void ReferenceTypesTest()
        {
            var result = new testClass2();

            Assert.DoesNotThrow(() => result = faker.Create<testClass2>());

            Assert.True(result != null, "Faker created null object.");

            Assert.AreNotEqual(null, result.refType);

            Assert.AreNotEqual(null, result.list);
        }

        [Test]
        public void CircularDependenciesTest()
        {
            Assert.Throws(typeof(ArgumentException), () => faker.Create<testClass5>());
            Assert.Throws(typeof(ArgumentException), () => faker.Create<testClass4>());
        }

        [Test]
        public void NestedTest()
        {
            var result = new testClass7();

            Assert.DoesNotThrow(() => result = faker.Create<testClass7>());

            Assert.AreNotEqual(null, result.field.value);
        }
    }
}