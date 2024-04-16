using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class UnknowFunctionOperator : FunctionNOperator
    {
        private readonly IEvaluable[] _dependingEvaluable;
        private readonly Expression _expression;

        public UnknowFunctionOperator(Expression expression, Expression[] parameters) : base(expression)
        {
            _expression = expression;
            _dependingEvaluable = parameters;
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public override double Evaluate(params Variable[] variables)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _expression.Name + '(' + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')';
        }
    }
}
