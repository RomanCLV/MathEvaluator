using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

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
        /// If the evaluable is lower than -1 or greater than 1, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double sin = _left.Evaluate(variables);
            double angle;
            if (sin < -1 || sin > 1)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + sin + ')');
                }
                angle = double.NaN;
            }
            else
            {
                angle = Math.Asin(sin);
                if (MathEvaluator.AngleAreInDegrees)
                {
                    angle = Funcs.RadiansToDegrees(angle);
                }
            }
            return angle;
        }
    }
}
