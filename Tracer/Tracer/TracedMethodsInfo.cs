using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tracer {
    /// <summary>
    /// Class contains all info about methods in specific thread.
    /// </summary>
    public class TracedMethodsInfo {

        /// <summary>
        /// Stack of methods. Used for basic operations w/ methods
        /// </summary>
        private Stack<MethodMetadata> methodsStack { get; set; } = new Stack<MethodMetadata>();
         
        /// <summary>
        /// Represents methods of last-stackframe-level, used by serializers
        /// </summary>
        public List<MethodMetadata> Methods { get; private set; } = new List<MethodMetadata>();

        public void StartMeasure(MethodBase methodToMeasure)
        {
            var method = new MethodMetadata(methodToMeasure);

            if (methodsStack.Count != 0)
            {
                methodsStack.Peek().NewInnerMethod(method);
            }
            else
            {// this is last-stackframe-level method
                Methods.Add(method);
            }

            methodsStack.Push(method);

            method.StartMeasure();
        }

        public void StopMeasure()
        {
            methodsStack.Peek().StopMeasure();
            methodsStack.Pop();
        }
    }
}
