using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class PowerExpression : IEvaluable
    {
        private readonly IEvaluable _base;
        private readonly IEvaluable _power;

        public PowerExpression(IEvaluable baseEvaluable, IEvaluable powerEvaluable)
        {
            _base = baseEvaluable;
            _power = powerEvaluable;
        }

        public double Evaluate(params Variable[] variables)
        {
            return Math.Pow(_base.Evaluate(variables), _power.Evaluate(variables));
        }
    }
}
