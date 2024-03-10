using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicCotangentOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic cotangent";
        private readonly static string _acronym = "coth";
        private readonly static string _description = "Returns the hyperbolic cotangent (defined as coth(x) = cosh(x) / sinh(x)) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "coth(x)"
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

        public HyperbolicCotangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static HyperbolicCotangentOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new HyperbolicCotangentOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the hyperbolic cotangent (defined as <c>coth(x) = cosh(x) / sinh(x)</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The hyperbolic cotangent of the given evaluable using <see cref="Funcs.Coth(double)"/>.<br />
        /// If the evaluable is equal to 0, raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (value == 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + "(0)");
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Funcs.Coth(value);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
