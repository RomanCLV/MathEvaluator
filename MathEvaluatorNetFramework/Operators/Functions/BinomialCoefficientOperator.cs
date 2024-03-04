using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class BinomialCoefficientOperator : FunctionNOperator
    {
        private readonly static string _fullname = "binomial coefficient";
        private readonly static string _acronym = "binc";
        private readonly static string _description = "Returns the binomial coefficient, written (n k) and pronounced \"n choose k\" of the two given evaluable where they must be equal to 0-positive integer.";
        private readonly static string[] _usages = new string[1]
        {
            "binc(k, n)"
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

        private readonly IEvaluable _n;
        private readonly IEvaluable[] _dependingEvaluable;

        public BinomialCoefficientOperator(IEvaluable k, IEvaluable n) : base(k)
        {
            _n = n;
            _dependingEvaluable = new IEvaluable[2]
            {
                _left,
                _n
            };
        }

        public new static BinomialCoefficientOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new BinomialCoefficientOperator(new Expression(args[0]), new Expression(args[1]));
        }

        /// <summary>
        /// Evaluate the binomial coefficient, written (n k) and pronounced "n choose k" of the two given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The binomial coefficient "n choose k" of the two given evaluable.<br />
        /// If one of the evaluables is not an integer or is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double k = _left.Evaluate(variables);
            double n = _n.Evaluate(variables);

            int ki = (int)k;
            int ni = (int)n;

            if (k <= 0 || n <= 0 || k - ki != 0.0 || n - ni != 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + k + ", " + n + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Funcs.BinomialCoefficient(ki, ni);
            }
            return result;
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }
    }
}
