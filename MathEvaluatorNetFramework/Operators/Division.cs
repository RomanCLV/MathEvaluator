using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal class Division : Operand
    {
        public Division(IEvaluable left, IEvaluable right) : base(left, right)
        {
        }

        public override double Evaluate(params Variable[] variables)
        {
            return _left.Evaluate(variables) / _right.Evaluate(variables);
        }
    }
}
