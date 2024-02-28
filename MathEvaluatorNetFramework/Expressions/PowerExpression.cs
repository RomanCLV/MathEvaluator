using MathEvaluatorNetFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class PowerExpression : IEvaluable
    {
        private readonly IEvaluable _base;
        private readonly IEvaluable _power;

        public PowerExpression(IEvaluable baseEvaluable, IEvaluable powerEvaluable)
        {
            _base = baseEvaluable;
            _power = powerEvaluable;
        }

        /// <summary>
        /// Evaluate two evaluable entities as base^power.
        /// </summary>
        /// <param name="variables"></param>
        /// <returns>If base and power are equal to 0, throw a <see cref="DomainException"/> if <see cref="MathEvaluator.RaiseDomainException"/> is <c>true</c>, else return <see cref="double.NaN"/>. Else return base^power using <see cref="Math.Pow(double, double)"/>.</returns>
        /// <exception cref="DomainException"></exception>
        public double Evaluate(params Variable[] variables)
        {
            double baseResult = _base.Evaluate(variables);
            double powerResult = _power.Evaluate(variables);
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
