using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal class NegativeOperator : Operator
    {
        public NegativeOperator(IEvaluable expression) : base(expression)
        {
        }

        /// <summary>
        /// Evaluate the negation of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable.</param>
        /// <returns>The negation of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return -_left.Evaluate();
        }

        public override string ToString()
        {
            string result = "-";
            if (_left is Multiplication || _left is Division || _left is Addition || _left is Substraction || _left is NegativeOperator ||
                (_left is Expression e && e.Is(typeof(Multiplication), typeof(Division), typeof(Addition), typeof(Substraction), typeof(NegativeOperator))))
            {
                result += '(' + _left.ToString() + ')';
            }
            else
            {
                result += _left.ToString();
            }
            return result;
        }
    }
}
