using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Exceptions
{
    public class NotDefinedException : InvalidOperationException
    {
        public NotDefinedException(string variableName) : base(variableName + " is not defined or its value null.")
        {
        }
    }
}
