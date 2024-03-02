using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArccosineOperator : FunctionOperator
    {
        private readonly static string _acronym = "acos";
        public new static string Acronym => _acronym;

        public ArccosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the arccosine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose cosine is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.AngleAreInDegrees"/> to return the angle in degrees or in radians.<br />
        /// If the evaluable is lower than -1 or greater than 1, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double cos = _left.Evaluate(variables);
            double angle;
            if (cos < -1 || cos > 1)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + cos + ')');
                }
                angle = double.NaN;
            }
            else
            {
                angle = Math.Acos(cos);
                if (MathEvaluator.AngleAreInDegrees)
                {
                    angle = Funcs.RadiansToDegrees(angle);
                }
            }
            return angle;
        }
    }
}
