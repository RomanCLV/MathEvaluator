using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class DecimalOperator : FunctionOperator
    {
        private readonly static string _acronym = "dec";
        public new static string Acronym => _acronym;

        public DecimalOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Returns the decimal part value of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The decimal part value of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.DecimalPart(_left.Evaluate(variables));
        }
    }
}
