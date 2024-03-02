using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class SecantOperator : FunctionOperator
    {
        private readonly static string _acronym = "sec";
        public new static string Acronym => _acronym;

        public SecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the secant (defined as sec(x)=1/cos(x)) of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The secant of the given evaluable.<br />
        /// If the evaluable is a multiple of PI/2 (or 90°), raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double angle = _left.Evaluate(variables);
            double rad = MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle;

            if (Funcs.IsMultiple(rad, Math.PI / 2.0))
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + (MathEvaluator.AngleAreInDegrees ? angle.ToString() + '°' : rad.ToString()) + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = 1.0 / Math.Cos(rad);
            }
            return result;
        }
    }
}
