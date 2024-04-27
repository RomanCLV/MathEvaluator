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

        private readonly string _stringValue;

        public ValueOperator(double value)
        {
            _value = value;
            _stringValue = null;
        }

        public ValueOperator(double value, string stringValue) : this(value)
        {
            _stringValue = stringValue;
        }

        public double Evaluate(params Variable[] variables)
        {
            return _value;
        }

        public bool DependsOnVariables(out List<string> variables)
        {
            variables = new List<string>(0);
            return false;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(_stringValue) ? _value.ToString().Replace(',', '.') : _stringValue;
        }
    }
}
