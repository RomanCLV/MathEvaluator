using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArctangentOperator : FunctionOperator
    {
        private readonly static string _acronym = "atan";
        public new static string Acronym => _acronym;

        public ArctangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the tangent of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose tangent is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.AngleAreInDegrees"/> to return the angle in degrees or in radians.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = Math.Atan(_left.Evaluate(variables));
            return MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle;
        }
    }
}
