using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class CeilOperator : FunctionOperator
    {
        private readonly static string _acronym = "ceil";
        public new static string Acronym => _acronym;

        public CeilOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Returns the smallest integer value greater than or equal to the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The smallest integer value greater than or equal to the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Math.Ceiling(_left.Evaluate(variables));
        }
    }
}
