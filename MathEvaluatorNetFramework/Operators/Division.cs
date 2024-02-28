using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class Division : Operand
    {
        public Division(IEvaluable left, IEvaluable right) : base(left, right)
        {
        }

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
    }
}
