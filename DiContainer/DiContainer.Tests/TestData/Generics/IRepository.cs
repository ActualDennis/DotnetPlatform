using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Tests.TestData.Generics {
    public interface IRepository {
        void AddTo(int index);

        void DeleteAt(int index);
    }
}
