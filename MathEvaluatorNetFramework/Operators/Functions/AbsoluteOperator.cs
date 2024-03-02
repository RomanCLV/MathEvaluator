using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class AbsoluteOperator : FunctionOperator
    {
        private readonly static string _acronym = "abs";
        public new static string Acronym => _acronym;

        public AbsoluteOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Returns the absolute value of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The absolute value of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Math.Abs(_left.Evaluate(variables));
        }
    }
}
