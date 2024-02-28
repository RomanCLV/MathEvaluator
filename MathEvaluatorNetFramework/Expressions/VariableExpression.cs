using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Expressions
{
    internal class VariableExpression : IEvaluable
    {
        private readonly string _variableName;

        public VariableExpression(string variableName)
        {
            _variableName = variableName;
        }

        public double Evaluate(params Variable[] variables)
        {
            Variable variable = null;
            foreach (var v in variables)
            {
                if (v.Name == _variableName)
                {
                    variable = v;
                }
            }

            if (variable == null || variable.Value == null)
            {
                throw new NotDefinedVariableException(_variableName);
            }
            return (double)variable.Value;
        }
    }
}
