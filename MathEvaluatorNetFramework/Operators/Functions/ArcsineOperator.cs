using MathEvaluatorNetFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArcsineOperator : FunctionOperator
    {
        private readonly static string _acronym = "asin";
        public new static string Acronym => _acronym;

        public ArcsineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the arcsine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose sine is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.AngleAreInDegrees"/> to return the angle in degrees or in radians.<br />
        /// If the evaluable is lower than -1 or greater than 1, it will raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/> or it will return <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            double result;
            if (angle < -1 || angle > 1)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + angle + ')');
                }
                result = double.NaN;
            }
            else
            {
                result = Math.Asin(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
            }
            return result;
        }
    }
}
