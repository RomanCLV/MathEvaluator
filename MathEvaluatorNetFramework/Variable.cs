using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework
{
    public class Variable
    {
        public string Name { get; }

        public double? Value { get; set; }

        internal Variable(string name)
        {
            Name = name;
            Value = null;
        }

        public Variable(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
