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

        /// <summary>
        /// Variable constructor with a <c>null</c> value;
        /// </summary>
        /// <param name="name">The name of the variable. It can not be reserved names : see <see cref="Expression.ReservedNames"/>.</param>
        /// <exception cref="ArgumentException"></exception>
        internal Variable(string name)
        {
            if (Expression.ReservedNames.Contains(name))
            {
                throw new ArgumentException("A variable can not use an Expression reserved keywords.");
            }

            Name = name;
            Value = null;
        }

        /// <summary>
        /// Variable constructor.
        /// </summary>
        /// <param name="name">The name of the variable. It can not be reserved names : see <see cref="Expression.ReservedNames"/>.</param>
        /// <param name="value">The value of the variable</param>
        /// <exception cref="ArgumentException"></exception>
        public Variable(string name, double value) : this(name)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
