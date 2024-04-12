using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicCosecantOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic cosecant";
        private readonly static string _acronym = "csch";
        private readonly static string _description = "Returns the hyperbolic cosecant (defined as csch(x) = 1 / sinh(x)) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "csch(x)"
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

        public HyperbolicCosecantOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static HyperbolicCosecantOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new HyperbolicCosecantOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the hyperbolic cosecant (defined as <c>csch(x) = 1 / sinh(x)</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The hyperbolic cosecant of the given evaluable using <see cref="Funcs.Csch(double)"/>.<br />
        /// If the evaluable is equal to 0, raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.Parameters.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (value == 0.0)
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
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
                result = Funcs.Csch(value);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
