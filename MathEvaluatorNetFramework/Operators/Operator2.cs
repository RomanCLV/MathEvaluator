using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal abstract class Operator2 : Operator
    {
        protected readonly IEvaluable _right;

        public Operator2(IEvaluable left, IEvaluable right) : base(left)
        {
            _right = right;
        }
    }
}
