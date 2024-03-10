using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators
{
    internal abstract class Operator2 : Operator
    {
        protected readonly IEvaluable _right;

        protected abstract string Symbol { get; }

        public Operator2(IEvaluable left, IEvaluable right) : base(left)
        {
            _right = right;
        }

        public override bool DependsOnVariables(out List<string> variables)
        {
            bool leftResult = _left.DependsOnVariables(out List<string> leftVars);
            bool rightResult = _right.DependsOnVariables(out List<string> rightVars);
            bool result = leftResult || rightResult;

            variables = new List<string>(leftVars);
            if (result)
            {
                foreach (string rightVar in rightVars)
                {
                    if (!variables.Contains(rightVar))
                    {
                        variables.Add(rightVar);
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            return EvaluableToString(_left) + Symbol + EvaluableToString(_right);
        }
    }
}
