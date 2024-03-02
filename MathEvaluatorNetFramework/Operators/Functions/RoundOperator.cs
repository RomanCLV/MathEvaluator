using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class RoundOperator : FunctionOperator
    {
        private readonly static string _acronym = "round";
        public new static string Acronym => _acronym;

        private readonly int _precision;

        public RoundOperator(IEvaluable evaluable, int precision) : base(evaluable)
        {
            _precision = precision;
        }

        /// <summary>
        /// Round the given evaluable to the nearest integer (if the given precision is lower or equal to 0) or round the given evaluable to the specified number of fractional digits (depending of the precision).
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The absolute value of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return (_precision <= 0) ? Math.Round(_left.Evaluate(variables)) : Math.Round(_left.Evaluate(variables), _precision);
        }
    }
}
