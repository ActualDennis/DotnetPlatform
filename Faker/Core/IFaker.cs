using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.Core {
    public interface IFaker<T> {
        T Create<T>();
    }
}
