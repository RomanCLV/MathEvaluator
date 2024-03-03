using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal class ValueOperator : IEvaluable
    {
        private readonly double _value;

        public ValueOperator(double value)
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
