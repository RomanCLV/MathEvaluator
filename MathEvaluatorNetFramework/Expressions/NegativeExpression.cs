using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class NegativeExpression : IEvaluable
    {
        private readonly Expression _expression;

        public NegativeExpression(Expression expression)
        {
            _expression = expression;
        }

        public NegativeExpression(string expression) : this(new Expression(expression))
        {
        }

        internal NegativeExpression(string expression, bool isCleanedExpression) : this(new Expression(expression, isCleanedExpression))
        {
        }

        public double Evaluate(params Variable[] variables)
        {
            return -_expression.Evaluate();
        }

        public double Evaluate()
        {
            return -_expression.Evaluate();
        }
    }
}
