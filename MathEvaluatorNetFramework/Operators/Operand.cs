using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal abstract class Operand : IEvaluable
    {
        protected readonly IEvaluable _left;
        protected readonly IEvaluable _right;

        public Operand(IEvaluable left, IEvaluable right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Evaluate two evaluable entity.
        /// </summary>
        /// <param name="variables"></param>
        /// <returns>The result</returns>
        public abstract double Evaluate(params Variable[] variables);
    }
}
