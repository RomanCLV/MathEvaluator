using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class RoundOperator : FunctionNOperator
    {
        private readonly static string _fullname = "round";
        private readonly static string _acronym = "round";
        private readonly static string _description = "Round the given evaluable to the nearest integer (if the given precision is equal to 0) or round the given evaluable to the specified number of fractional digits (depending on the given precision).";
        private readonly static string[] _usages = new string[2]
        {
            "round(x)",
            "round(x, p)",
        };
        private readonly static uint _minArg = 1;
        private readonly static uint _maxArg = 2;
        private readonly static FunctionOperatorDetails _details = new FunctionOperatorDetails(_fullname, _acronym, _description, _minArg, _maxArg, _usages);

        public new static string FullName => _fullname;
        public new static string Acronym => _acronym;
        public new static string Description => _description;
        public new static IReadOnlyList<string> Usages => _usages;
        public new static uint MinArg => _minArg;
        public new static uint MaxArg => _maxArg;
        public new static FunctionOperatorDetails Details => _details;

        private readonly IEvaluable _precision;
        private readonly IEvaluable[] _dependingEvaluable;

        /// <summary>
        /// <see cref="RoundOperator"/> constructor that use a 0-digit precision.
        /// </summary>
        public RoundOperator(IEvaluable evaluable) : this(evaluable, new ValueOperator(0.0))
        {
        }

        /// <summary>
        /// <see cref="RoundOperator"/> constructor that use a specified digit precision.
        /// </summary>
        public RoundOperator(IEvaluable evaluable, IEvaluable precision) : base(evaluable)
        {
            _precision = precision;
            _dependingEvaluable = new IEvaluable[2]
            {
                _left,
                _precision
            };
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public new static RoundOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }

            return args.Length == 1 ?
                new RoundOperator(new Expression().Set(args[0])) :
                new RoundOperator(new Expression().Set(args[0]), new Expression().Set(args[1]));
        }

        /// <summary>
        /// Round the given evaluable to the nearest integer (if the given precision is equal to 0) or round the given evaluable to the specified number of fractional digits (depending on the given precision).
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The absolute value of the given evaluable.<br />
        /// If the precision is not an integer or is negative, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.Parameters.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double value = _left.Evaluate(variables);
            double precision = _precision.Evaluate(variables);
            int iPrecition = (int)precision;
            double result;

            if (precision < 0 || precision - iPrecition != 0.0)
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + value + ", " + precision + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = iPrecition == 0 ? Math.Round(value) : Math.Round(value, iPrecition);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')'; ;
        }
    }
}
