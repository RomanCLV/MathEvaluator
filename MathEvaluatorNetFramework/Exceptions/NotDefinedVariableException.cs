using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Exceptions
{
    internal class NotDefinedVariableException : Exception
    {
        public NotDefinedVariableException(Variable variable) : base("The variable " + variable.Name + " is not defined.")
        {
        }
    }
}
