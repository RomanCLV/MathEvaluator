using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicCosineOperator : FunctionOperator
    {
        private readonly static string _acronym = "cosh";
        public new static string Acronym => _acronym;

        public HyperbolicCosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic cosine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic cosine of the given evaluable using <see cref="Funcs.Cosh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Cosh(_left.Evaluate(variables));
        }
    }
}
