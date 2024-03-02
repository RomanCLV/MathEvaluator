using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class DegreeOperator : FunctionOperator
    {
        private readonly static string _acronym = "deg";
        public new static string Acronym => _acronym;

        public DegreeOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Convert the value of the given evaluable (as radians) into degrees.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the given evaluable (as radians) into degrees.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.RadiansToDegrees(_left.Evaluate(variables));
        }
    }
}
