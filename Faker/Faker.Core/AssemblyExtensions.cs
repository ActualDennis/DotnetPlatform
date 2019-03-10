using System;
using System.Linq;
using System.Reflection;

namespace Faker.Core {
    public static class AssemblyExtensions {
        public static bool IsMicrosoftAssembly(this Assembly assembly)
        {
            return assembly.GetName().GetPublicKeyToken()
                   .SequenceEqual(typeof(int).Assembly.GetName().GetPublicKeyToken());
        }
    }
}
