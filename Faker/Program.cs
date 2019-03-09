using Faker.Data;
using System;

namespace Faker.Core {
    class Program {
        static void Main(string[] args)
        {
            try
            {
                var faker = new Faker();
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
