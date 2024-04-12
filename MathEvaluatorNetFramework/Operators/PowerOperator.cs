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
        /// <returns>If base and power are equal to 0, throw a <see cref="DomainException"/> if <see cref="MathEvaluator.Parameters.RaiseDomainException"/> is <c>true</c>, else return <see cref="double.NaN"/>. Else return base^power using <see cref="Math.Pow(double, double)"/>.</returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double baseResult = _left.Evaluate(variables);
            double powerResult = _right.Evaluate(variables);
            double result;
            if (baseResult == 0.0 && powerResult == 0.0)
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
                {
                    throw new DomainException("0^0");
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (double.IsInfinity(powerResult))
            {
                if (baseResult < 0.0)
                {
                    result = double.NegativeInfinity;
                }
                else if (baseResult > 0.0)
                {
                    result = double.PositiveInfinity;
                }
                else
                {
                    result = 0.0;
                }
            }
            else
            {
                result = Math.Pow(baseResult, powerResult);
            }
            return result;
        }

        public override string ToString()
        {
            string result;
            if (_left is Addition || _left is Substraction || _left is NegativeOperator || _left is Multiplication || _left is Division || _left is FactorialOperator || _left is PowerOperator ||
                (_left is Expression el && el.Is(typeof(Addition), typeof(Substraction), typeof(NegativeOperator), typeof(Multiplication), typeof(Division), typeof(FactorialOperator), typeof(PowerOperator))))
            {
                result = '(' + _left.ToString() + ')';
            }
            else
            {
                result = _left.ToString();
            }
            result += '^';
            if (_right is Addition || _right is Substraction || _right is NegativeOperator || _right is Multiplication || _right is Division || _right is FactorialOperator || _right is PowerOperator ||
                (_right is Expression er && er.Is(typeof(Addition), typeof(Substraction), typeof(NegativeOperator), typeof(Multiplication), typeof(Division), typeof(FactorialOperator), typeof(PowerOperator))))
            {
                result += '(' + _right.ToString() + ')';
            }
            else
            {
                result += _right.ToString();
            }
            return result;
        }
    }
}
