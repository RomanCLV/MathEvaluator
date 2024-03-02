using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicSineOperator : FunctionOperator
    {
        private readonly static string _acronym = "sinh";
        public new static string Acronym => _acronym;

        public HyperbolicSineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic sine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic sine of the given evaluable using <see cref="Funcs.Sinh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Sinh(_left.Evaluate(variables));
        }
    }
}
