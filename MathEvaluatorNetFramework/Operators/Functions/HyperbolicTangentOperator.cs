using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicTangentOperator : FunctionOperator
    {
        private readonly static string _acronym = "tanh";
        public new static string Acronym => _acronym;

        public HyperbolicTangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic tangent of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic tangent of the given evaluable using <see cref="Funcs.Tanh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Tanh(_left.Evaluate(variables));
        }
    }
}
