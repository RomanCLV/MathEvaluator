using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArctangentOperator : FunctionOperator
    {
        private readonly static string _fullname = "arctangent";
        private readonly static string _acronym = "atan";
        private readonly static string _description = "Returns the arctangent of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "atan(x)"
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

        public ArctangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static ArctangentOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new ArctangentOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the arctangent of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose tangent is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.AngleAreInDegrees"/> to return the angle in degrees or in radians.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = Math.Atan(_left.Evaluate(variables));
            return MathEvaluator.AngleAreInDegrees ? Funcs.RadiansToDegrees(angle) : angle;
        }
    }
}
