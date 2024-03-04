using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ExponentialOperator : FunctionOperator
    {
        private readonly static string _fullname = "exponential";
        private readonly static string _acronym = "exp";
        private readonly static string _description = "Returns the exponential of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "exp(x)"
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

        public ExponentialOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static ExponentialOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new ExponentialOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the exponential of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The exponential of the given evaluable.<br />
        /// If the evaluable is greater than <see cref="Funcs.MAX_EXP_X"/>, returns <see cref="double.PositiveInfinity"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double value = _left.Evaluate(variables);
            return value > Funcs.MAX_EXP_X ? double.PositiveInfinity : Math.Exp(value);
        }
    }
}
