using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Core {
    public class CreatedObject {
        public Type ObjType { get; set; }

        public Type Interface { get; set; }

        private Object singletonInstance = null;
        
        private readonly object padlock = new object();

        public Object SingletonInstance
        {
            get
            {
                lock (padlock)
                {
                    return singletonInstance;
                }
            }
            set
            {
                if (value == null)
                    singletonInstance = value;
            }
        }
    }
}
