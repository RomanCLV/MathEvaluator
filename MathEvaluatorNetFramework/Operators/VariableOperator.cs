using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class VariableOperator : IEvaluable
    {
        private readonly string _variableName;

        public VariableOperator(string variableName)
        {
            _variableName = variableName;
        }

        public double Evaluate(params Variable[] variables)
        {
            Variable variable = null;
            double value;
            foreach (var v in variables)
            {
                if (v.Name == _variableName)
                {
                    variable = v;
                    break;
                }
            }

            if (variable == null || variable.Value == null)
            {
                if (MathEvaluator.VariablesManager.Contains(_variableName))
                {
                    value = MathEvaluator.VariablesManager.Get(_variableName);
                }
                else
                {
                    throw new NotDefinedException(_variableName);
                }
            }
            else
            {
                value = (double)variable.Value;
            }
            return value;
        }

        public bool DependsOnVariables(out List<string> variables)
        {
            variables = new List<string>(1) { _variableName };
            return true;
        }

        public override string ToString()
        {
            return _variableName;
        }
    }
}
