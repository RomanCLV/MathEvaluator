using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class NaperianLogarithmOperator : FunctionOperator
    {
        private readonly static string _fullname = "naperian logarithm";
        private readonly static string _acronym = "ln";
        private readonly static string _description = "Returns the Naperian logarithm of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "ln(x)"
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

        public NaperianLogarithmOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static NaperianLogarithmOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new NaperianLogarithmOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the Naperian logarithm of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The Naperian logarithm of the given evaluable.<br />
        /// If the evaluable is equal to 0, returns <see cref="double.NegativeInfinity"/>.<br />
        /// If the evaluable is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/> returns <see cref="double.Nan"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);
            if (value < 0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (value == 0.0)
            {
                result = double.NegativeInfinity;
            }
            else
            {
                result = Math.Log(value);
            }
            return result;
        }
    }
}
