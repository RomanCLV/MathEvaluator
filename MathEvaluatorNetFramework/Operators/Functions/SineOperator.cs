using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class SineOperator : FunctionOperator
    {
        private readonly static string _fullname = "sine";
        private readonly static string _acronym = "sin";
        private readonly static string _description = "Returns the sine of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "sin(x)"
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

        public SineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static SineOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new SineOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the sine of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The sine of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            return Math.Sin(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
        }
    }
}
