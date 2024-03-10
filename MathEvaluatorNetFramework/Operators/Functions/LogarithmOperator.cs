using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class LogarithmOperator : FunctionNOperator
    {
        private readonly static string _fullname = "logarithm";
        private readonly static string _acronym = "log";
        private readonly static string _description = "Returns the logarithm of the given evaluable with the given base. If no base specified, base 10 is used.";
        private readonly static string[] _usages = new string[2]
        {
            "log(x)",
            "log(x, b)"
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

        private readonly IEvaluable _logBase;
        private readonly IEvaluable[] _dependingEvaluable;

        /// <summary>
        /// LogarithmOperator constructor that use a logBase of 10.
        /// </summary>
        public LogarithmOperator(IEvaluable evaluable) : this(evaluable, new ValueOperator(10.0))
        {
        }

        /// <summary>
        /// LogarithmOperator constructor that use a specified logBase (must be 0-positive).
        /// </summary>
        public LogarithmOperator(IEvaluable evaluable, IEvaluable logBase) : base(evaluable)
        {
            _logBase = logBase;
            _dependingEvaluable = new IEvaluable[2]
            {
                _left,
                _logBase
            };
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public new static LogarithmOperator Create(string[] args)
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
                new LogarithmOperator(new Expression(args[0])) :
                new LogarithmOperator(new Expression(args[0]), new Expression(args[1]));
        }

        /// <summary>
        /// Evaluate the logarithm of the given evaluable with the given base.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The logarithm of the given evaluable with the given base.<br />
        /// If the logarithm base or the evaluable is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/> returns <see cref="double.Nan"/>.<br />
        /// If the logarithm base is equal to 0, if the evaluable is equal to 0, raises a <see cref="DomainException"/> or returns <see cref="double.NaN"/>, else return 1.<br />
        /// If the logarithm base is greater than 0, if the evaluable is equal to 0, returns <see cref="double.NegativeInfinity"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            double logBase = _logBase.Evaluate(variables);

            if (logBase < 0 || value < 0 || (logBase == 0.0 && value == 0.0))
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + logBase + ", " + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (logBase == 0.0)
            {
                result = 1.0;
            }
            else if (value == 0.0)
            {
                result = double.NegativeInfinity;
            }
            else if (logBase == 10.0)
            {
                result = Math.Log10(value);
            }
            else
            {
                result = Math.Log(value, logBase);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')'; ;
        }
    }
}
