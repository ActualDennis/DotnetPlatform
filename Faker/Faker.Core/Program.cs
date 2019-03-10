using Faker.Core.Additional_value_generators;
using Faker.Data;
using System;

namespace Faker.Core {
    class Program {
        static void Main(string[] args)
        {
            try
            {
                var config = new FakerConfig();
                config.Add<Foo, string, AlwaysOneValueGenerator>(foo => foo.str);
                var faker = new Faker(config);
                var result = faker.Create<Foo>();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Failed to create dto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
