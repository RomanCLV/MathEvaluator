using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Exceptions
{
    public class NotDefinedVariableException : InvalidOperationException
    {
        public NotDefinedVariableException(string variableName) : base("The variable " + variableName + " is not defined or its value null.")
        {
        }
    }
}
