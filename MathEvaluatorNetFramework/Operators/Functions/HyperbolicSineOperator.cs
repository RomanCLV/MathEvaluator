using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicSineOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic sine";
        private readonly static string _acronym = "sinh";
        private readonly static string _description = "Returns the hyperbolic sine (defined as cosh(x) = (exp(x) - exp(-x)) / 2) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "sinh(x)"
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

        public HyperbolicSineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static HyperbolicSineOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new HyperbolicSineOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the hyperbolic sine (defined as <c>cosh(x) = (exp(x) - exp(-x)) / 2</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic sine of the given evaluable using <see cref="Funcs.Sinh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Sinh(_left.Evaluate(variables));
        }
    }
}
