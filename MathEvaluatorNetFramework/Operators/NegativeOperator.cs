using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class NegativeOperator : Operator
    {
        public NegativeOperator(IEvaluable expression) : base(expression)
        {
        }

        /// <summary>
        /// Evaluate the negation of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable.</param>
        /// <returns>The negation of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return -_left.Evaluate();
        }
    }
}
