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

        public FunctionOperator(IEvaluable evaluable) : base(evaluable)
        {
        }
    }
}
