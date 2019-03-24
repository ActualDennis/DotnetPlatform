using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Tests.TestData.Generics {
    public class SomeRepository : IRepository {
        public void AddTo(int index) => throw new NotImplementedException();
        public void DeleteAt(int index) => throw new NotImplementedException();
    }
}
