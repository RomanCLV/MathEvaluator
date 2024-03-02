using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicCosecantOperator : FunctionOperator
    {
        private readonly static string _acronym = "csch";
        public new static string Acronym => _acronym;

        public HyperbolicCosecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic cotangent of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The hyperbolic cotangent of the given evaluable using <see cref="Funcs.Coth(double)"/>.<br />
        /// If the evaluable is equal to 0, raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (value == 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + "(0)");
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Funcs.Coth(value);
            }
            return result;
        }
    }
}
