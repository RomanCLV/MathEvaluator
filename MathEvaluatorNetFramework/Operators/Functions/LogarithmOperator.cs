using MathEvaluatorNetFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class LogarithmOperator : FunctionOperator
    {
        private readonly static string _acronym = "ln";
        public new static string Acronym => _acronym;

        private double _logBase;

        public LogarithmOperator(IEvaluable evaluable, double logBase = 10.0) : base(evaluable)
        {
            _logBase = logBase;
        }

        /// <summary>
        /// Evaluate the logarithm of the given evaluable with the given base.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The logarithm of the given evaluable with the given base.<br />
        /// If the logarithm base or the evaluable is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/> returns <see cref="double.Nan"/>.<br />
        /// If the logarithm base is equal to 0, if the evaluable is equal to 0, raises a <see cref="DomainException"/> or returns <see cref="double.NaN"/>, else return 1.<br />
        /// If the logarithm base is greater than 0, if the evaluable is equal to 0, returns <see cref="double.NegativeInfinity"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (_logBase < 0 || value < 0 || (_logBase == 0.0 && value == 0.0))
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + _logBase + ", " + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (_logBase == 0.0)
            {
                result = 1.0;
            }
            else if (value == 0.0)
            {
                result = double.NegativeInfinity;
            }
            else if (_logBase == 10.0)
            {
                result = Math.Log10(value);
            }
            else
            {
                result = Math.Log(value, _logBase);
            }
            return result;
        }
    }
}
