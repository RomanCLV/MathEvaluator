using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicSecantOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic secant";
        private readonly static string _acronym = "sech";
        private readonly static string _description = "Returns the hyperbolic secant (defined as sech(x) = 1 / cosh(x)) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "sech(x)"
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

        public HyperbolicSecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic secant (defined as <c>sech(x) = 1 / cosh(x)</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic secant of the given evaluable using <see cref="Funcs.Sech(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Sech(_left.Evaluate(variables));
        }
    }
}
