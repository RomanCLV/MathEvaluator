using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class FloorOperator : FunctionOperator
    {
        private readonly static string _fullname = "floor";
        private readonly static string _acronym = "floor";
        private readonly static string _description = "Returns the value of the largest integer less than or equal to the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "floor(x)"
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

        public FloorOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static FloorOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new FloorOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evalute the value of the largest integer less than or equal to the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the largest integer less than or equal to the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Math.Floor(_left.Evaluate(variables));
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
