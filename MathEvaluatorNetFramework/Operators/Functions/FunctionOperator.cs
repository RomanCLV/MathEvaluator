using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal abstract class FunctionOperator : Operator
    {
        public static string FullName { get; } = "?";
        public static string Acronym { get; } = "?";
        public static string Description { get; } = "No description";
        public static IReadOnlyList<string> Usages { get; } = new List<string>();
        public static uint MinArg { get; } = 0;
        public static uint MaxArg { get; } = 0;
        public static FunctionOperatorDetails Details { get; } = null;

        public static FunctionOperator Create(string[] args)
        {
            throw new InvalidOperationException("Cannot call this method.");
        }

        public FunctionOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public override abstract string ToString();
    }

    internal abstract class FunctionNOperator : FunctionOperator
    {
        public FunctionNOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public override bool DependsOnVariables(out List<string> variables)
        {
            IEvaluable[] evaluables = GetDependingEvaluables();
            bool result = false;
            bool subResult;
            variables = new List<string>();

            foreach (IEvaluable evaluable in evaluables)
            {
                subResult = evaluable.DependsOnVariables(out List<string> subVariables);
                if (subResult)
                {
                    foreach (string subVar in subVariables)
                    {
                        if (!variables.Contains(subVar))
                        {
                            variables.Add(subVar);
                        }
                    }
                }
                result |= subResult;
            }

            return result;
        }

        protected abstract IEvaluable[] GetDependingEvaluables();
    }
}
