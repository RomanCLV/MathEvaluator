using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicCosineOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic cosine";
        private readonly static string _acronym = "cosh";
        private readonly static string _description = "Returns the hyperbolic cosine (defined as cosh(x) = (exp(x) + exp(-x)) / 2) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "cosh(x)"
        };
        private readonly static uint _minArg = 1;
        private readonly static uint _maxArg = 1;
        private readonly static FunctionOperatorDetails _details = new FunctionOperatorDetails(_fullname, _acronym, _description, _minArg, _maxArg, _usages);

        public new static string FullName => _fullname;
        public new static string Acronym => _acronym;
        public new static string Description => _description;
        public new static IReadOnlyList<string> Usages => _usages;
        public new static uint MinArg => _minArg;
        public new static uint MaxArg => _maxArg;
        public new static FunctionOperatorDetails Details => _details;

        public HyperbolicCosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic cosine (defined as <c>cosh(x) = (exp(x) + exp(-x)) / 2</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic cosine of the given evaluable using <see cref="Funcs.Cosh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Cosh(_left.Evaluate(variables));
        }
    }
}
