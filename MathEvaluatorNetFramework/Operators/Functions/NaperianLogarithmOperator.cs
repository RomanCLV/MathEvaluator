using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class NaperianLogarithmOperator : FunctionOperator
    {
        private readonly static string _acronym = "ln";
        public new static string Acronym => _acronym;

        public NaperianLogarithmOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the Naperian logarithm of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The Naperian logarithm of the given evaluable.<br />
        /// If the evaluable is equal to 0, returns <see cref="double.NegativeInfinity"/>.<br />
        /// If the evaluable is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/> returns <see cref="double.Nan"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (value < 0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (value == 0.0)
            {
                result = double.NegativeInfinity;
            }
            else
            {
                result = Math.Log(value);
            }
            return result;
        }
    }
}
