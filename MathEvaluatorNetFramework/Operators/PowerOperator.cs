using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class PowerOperator : Operator2
    {
        public PowerOperator(IEvaluable baseEvaluable, IEvaluable powerEvaluable) : base(baseEvaluable, powerEvaluable)
        {
        }

        /// <summary>
        /// Evaluate two evaluable entities as base^power.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>If base and power are equal to 0, throw a <see cref="DomainException"/> if <see cref="MathEvaluator.RaiseDomainException"/> is <c>true</c>, else return <see cref="double.NaN"/>. Else return base^power using <see cref="Math.Pow(double, double)"/>.</returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double baseResult = _left.Evaluate(variables);
            double powerResult = _right.Evaluate(variables);
            double result;
            if (baseResult == 0.0 && powerResult == 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException("0^0");
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Math.Pow(baseResult, powerResult);
            }
            return result;
        }
    }
}
