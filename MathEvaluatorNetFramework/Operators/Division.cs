using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class Division : Operator2
    {
        public Division(IEvaluable left, IEvaluable right) : base(left, right)
        {
        }

        /// <summary>
        /// Evaluate the division between the two given evaluable entities.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The division between the two given evaluable entities.<br />
        /// If the denominator equals to 0, then if <see cref="MathEvaluator.RaiseDivideByZeroException"/> is true, raise a <see cref="DivideByZeroException"/>.
        /// If the numerator is not equals to 0, then returns <see cref="double.NegativeInfinity"/> or <see cref="double.PositiveInfinity"/>.<br />
        /// If the numerator and the denominator are equal to 0, then if <see cref="MathEvaluator.RaiseDomainException"/> is true, raise a <see cref="DomainException"/> else return <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double right = _right.Evaluate(variables);
            if (right == 0.0 && MathEvaluator.RaiseDivideByZeroException)
            {
                throw new DivideByZeroException();
            }
            double left = _left.Evaluate(variables);
            double result;
            if (right == 0.0 && left == 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException("0/0");
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = left / right;
            }

            return result;
        }

        public override string ToString()
        {
            string result;
            if (_left is Addition || _left is Substraction || _left is NegativeOperator || 
                (_left is Expression el && el.Is(typeof(Addition), typeof(Substraction), typeof(NegativeOperator))))
            {
                result = '(' + _left.ToString() + ')';
            }
            else
            {
                result = _left.ToString();
            }
            result += '/';
            if (_right is Addition || _right is Substraction || _right is NegativeOperator ||
                (_right is Expression er && er.Is(typeof(Addition), typeof(Substraction), typeof(NegativeOperator))))
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
