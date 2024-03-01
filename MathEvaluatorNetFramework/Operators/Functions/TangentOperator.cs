using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class TangentOperator : FunctionOperator
    {
        private readonly static string _acronym = "tan";
        public new static string Acronym => _acronym;

        public TangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the tangent of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The tangent of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            return Math.Tan(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
        }
    }
}
