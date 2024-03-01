using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class CosineOperator : FunctionOperator
    {
        private readonly static string _acronym = "cos";
        public new static string Acronym => _acronym;

        public CosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the cosine of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The cosine of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            return Math.Cos(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
        }
    }
}
