using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class DegreeOperator : FunctionOperator
    {
        private readonly static string _fullname = "degree";
        private readonly static string _acronym = "deg";
        private readonly static string _description = "Convert the value of the given evaluable (as radians) into degrees.";
        private readonly static string[] _usages = new string[1]
        {
            "deg(x)"
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

        public DegreeOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static DegreeOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new DegreeOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Convert the value of the given evaluable (as radians) into degrees.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the given evaluable (as radians) into degrees.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.RadiansToDegrees(_left.Evaluate(variables));
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
