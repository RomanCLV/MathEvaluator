using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal abstract class FunctionOperator : Operator
    {
        public static string Acronym { get; }

        public FunctionOperator(IEvaluable evaluable) : base(evaluable)
        {
        }
    }
}
