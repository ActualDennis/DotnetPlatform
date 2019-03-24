using System;
using System.Collections.Generic;
using System.Text;

namespace DiContainer.Core {
    public class ContainerEntity {
        public Type InterfaceType { get; set; }

        public List<Implementation> Implementations { get; set; }
    }
}
