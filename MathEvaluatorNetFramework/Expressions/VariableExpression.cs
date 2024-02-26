using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class VariableExpression : IEvaluable
    {
        private readonly Variable _variable;

        public VariableExpression(Variable variable)
        {
            _variable = variable;
        }

        public double Evaluate(params Variable[] variables)
        {
            if (_variable.Value == null)
            {
                throw new NotDefinedVariableException(_variable);
            }
            return (double)_variable.Value;
        }

        public double Evaluate()
        {
            if (_variable.Value == null)
            {
                throw new NotDefinedVariableException(_variable);
            }
            return (double)_variable.Value;
        }
    }
}
