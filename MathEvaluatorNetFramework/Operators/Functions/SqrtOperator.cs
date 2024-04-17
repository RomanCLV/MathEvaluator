using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class SqrtOperator : FunctionNOperator
    {

        private readonly static string _fullname = "square root";
        private readonly static string _acronym = "sqrt";
        private readonly static string _description = "Returns the Nth root of the given evaluable. If no root specified, root 2 is used.";
        private readonly static string[] _usages = new string[2]
        {
            "sqrt(x)",
            "sqrt(x, r)"
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

        private readonly IEvaluable _root;
        private readonly IEvaluable[] _dependingEvaluable;

        public SqrtOperator(IEvaluable evaluable) : this(evaluable, new ValueOperator(2.0))
        {
        }

        public SqrtOperator(IEvaluable evaluable, IEvaluable root) : base(evaluable)
        {
            _root = root;
            _dependingEvaluable = new IEvaluable[2]
            {
                _left,
                _root
            };
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public new static SqrtOperator Create(string[] args)
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
                new SqrtOperator(new Expression().Set(args[0])) :
                new SqrtOperator(new Expression().Set(args[0]), new Expression().Set(args[1]));
        }

        /// <summary>
        /// Returns the Nth root of the given evaluable. If no root specified, root 2 is used.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The Nth root of the given evaluable.<br />
        /// If the root is equal to 0 or the evaluable is lower than 0, raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.Parameters.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            double root = _root.Evaluate(variables);

            if (root == 0.0 || value < 0.0)
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + root + ", " + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (root == 2.0)
            {
                result = Math.Sqrt(value);
            }
            else
            {
                result = Math.Pow(value, 1.0 / root);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')'; ;
        }
    }
}
