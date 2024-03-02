using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class FloorOperation : FunctionOperator
    {
        private readonly static string _acronym = "floor";
        public new static string Acronym => _acronym;

        public FloorOperation(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Returns the value of the largest integer less than or equal to the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the largest integer less than or equal to the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Math.Floor(_left.Evaluate(variables));
        }
    }
}
