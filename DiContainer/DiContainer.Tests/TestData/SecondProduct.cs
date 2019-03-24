using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Tests.TestData {
    public class SecondProduct : IProduct {
        public IUser user { get; set; }

        public SecondProduct(IUser user)
        {
            this.user = user;
        }
        public void Sell()
        {
            //do something
        }
    }
}
