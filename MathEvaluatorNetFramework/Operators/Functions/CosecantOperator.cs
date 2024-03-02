using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class CosecantOperator : FunctionOperator
    {
        private readonly static string _acronym = "csc";
        public new static string Acronym => _acronym;

        public CosecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the cosecant (defined as csc(x)=1/sin(x)) of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The cosecant of the given evaluable.<br />
        /// If the evaluable is a multiple of PI (or 180°), raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double angle = _left.Evaluate(variables);
            double rad = MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle;

            if (Funcs.IsMultiple(rad, Math.PI))
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
                result = 1.0 / Math.Sin(rad);
            }
            return result;
        }
    }
}
