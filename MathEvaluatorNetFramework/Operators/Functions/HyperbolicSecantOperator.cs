using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicSecantOperator : FunctionOperator
    {
        private readonly static string _acronym = "sech";
        public new static string Acronym => _acronym;

        public HyperbolicSecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic secant of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic secant of the given evaluable using <see cref="Funcs.Sech(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Sech(_left.Evaluate(variables));
        }
    }
}
