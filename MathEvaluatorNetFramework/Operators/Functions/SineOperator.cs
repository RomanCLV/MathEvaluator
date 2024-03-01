using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class SineOperator : FunctionOperator
    {
        private readonly static string _acronym = "sin";
        public new static string Acronym => _acronym;

        public SineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the sine of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The sine of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            return Math.Sin(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
        }
    }
}
