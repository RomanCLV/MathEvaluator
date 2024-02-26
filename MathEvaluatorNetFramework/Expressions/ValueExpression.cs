using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class ValueExpression : IEvaluable
    {
        private readonly double _value;

        public ValueExpression(double value)
        {
            _value = value;
        }

        public double Evaluate(params Variable[] variables)
        {
            return _value;
        }

        public double Evaluate()
        {
            return _value;
        }
    }
}
