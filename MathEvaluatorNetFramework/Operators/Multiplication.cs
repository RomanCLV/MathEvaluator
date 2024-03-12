using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal class Multiplication : Operator2
    {
        public Multiplication(IEvaluable left, IEvaluable right) : base(left, right)
        {
        }

        /// <summary>
        /// Evaluate the multiplication between the two given evaluable entities.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The multiplication between the two given evaluable entities.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return _left.Evaluate(variables) * _right.Evaluate(variables);
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
            result += '*';
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
