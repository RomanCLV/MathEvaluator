using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class NegativeExpression : IEvaluable
    {
        private readonly IEvaluable _evaluable;

        public NegativeExpression(IEvaluable expression)
        {
            _evaluable = expression;
        }

        public NegativeExpression(string expression) : this(new Expression(expression))
        {
        }

        internal NegativeExpression(string expression, bool isCleanedExpression) : this(new Expression(expression, isCleanedExpression))
        {
        }

        public double Evaluate(params Variable[] variables)
        {
            return -_evaluable.Evaluate();
        }

        public double Evaluate()
        {
            return -_evaluable.Evaluate();
        }
    }
}
