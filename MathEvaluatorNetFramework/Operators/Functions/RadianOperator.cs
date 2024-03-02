using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class RadianOperator : FunctionOperator
    {
        private readonly static string _acronym = "rad";
        public new static string Acronym => _acronym;

        public RadianOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Convert the value of the given evaluable (as degrees) into radians.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the given evaluable (as degrees) into radians.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.DegreesToRadians(_left.Evaluate(variables));
        }
    }
}
