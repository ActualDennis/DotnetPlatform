﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Tests.TestData {
    public class User : IUser {
        public int Balance { get; set; } = 512;
        public int GetBalance() => Balance;

        public User()
        {

        }
    }
}
