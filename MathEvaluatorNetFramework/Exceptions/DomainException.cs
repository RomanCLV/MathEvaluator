using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Exceptions
{
    public class DomainException : InvalidOperationException
    {
        public string InvalidOperation { get; }

        public DomainException(string invalidOperation) : base("Invalid domain operation: " + invalidOperation)
        {
            InvalidOperation = invalidOperation;
        }
    }
}
