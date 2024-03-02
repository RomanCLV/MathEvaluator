using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ExponentialOperator : FunctionOperator
    {
        private readonly static string _acronym = "exp";
        public new static string Acronym => _acronym;

        public ExponentialOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the exponential of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The exponential of the given evaluable.<br />
        /// If the evaluable is greater than <see cref="Funcs.MAX_EXP_X"/>, returns <see cref="double.PositiveInfinity"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double value = _left.Evaluate(variables);
            return value > Funcs.MAX_EXP_X ? double.PositiveInfinity : Math.Exp(value);
        }
    }
}
