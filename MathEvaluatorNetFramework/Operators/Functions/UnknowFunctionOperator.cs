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

        /// <summary>
        /// UnknowFunctionOperator constructor.
        /// </summary>
        /// <param name="expression">The function expressed as an expression.</param>
        /// <param name="parameters">The parameters to pass to the function.</param>
        /// <exception cref="ArgumentException"></exception>
        public UnknowFunctionOperator(Expression expression, Expression[] parameters) : base(expression)
        {
            _expression = expression;
            _dependingEvaluable = parameters;
            int variablesCount = 0;

            if (_expression.DependsOnVariables(out List<string> variableNames))
            {
                variablesCount = variableNames.Count;
            }
            if (variablesCount != _dependingEvaluable.Length)
            {
                string errorMessage =
                    $"Expression {_expression.Name} required {variableNames.Count} parameters ({string.Join(", ", variableNames.ToArray())}). " +
                    $"{_dependingEvaluable.Length} parameter(s) given.";
                throw new ArgumentException(errorMessage);
            }
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public override double Evaluate(params Variable[] variables)
        {
            double[] argsResults = _dependingEvaluable.Select(evaluable => evaluable.Evaluate(variables)).ToArray();
            Variable[] vars = new Variable[0];
            if (_expression.DependsOnVariables(out List<string> variableNames))
            {
                vars = new Variable[variableNames.Count];
                for (int i = 0; i < vars.Length; i++)
                {
                    vars[i] = new Variable(variableNames[i], argsResults[i]);
                }
            }
            return _expression.Evaluate(vars);
        }

        public override string ToString()
        {
            return _expression.Name + '(' + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')';
        }
    }
}
