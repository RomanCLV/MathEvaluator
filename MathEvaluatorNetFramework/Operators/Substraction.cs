using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal class Substraction : Operator2
    {
        public Substraction(IEvaluable left, IEvaluable right) : base(left, right)
        {
        }

        /// <summary>
        /// Evaluate the substraction between the two given evaluable entities.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The substraction between the two given evaluable entities.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return _left.Evaluate(variables) - _right.Evaluate(variables);
        }

        public override string ToString()
        {
            return _left.ToString() + '-' + _right.ToString();
        }
    }
}
