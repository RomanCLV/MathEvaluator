using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArccosineOperator : FunctionOperator
    {
        private readonly static string _fullname = "arccosine";
        private readonly static string _acronym = "acos";
        private readonly static string _description = "Returns the arccosine of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "acos(x)"
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

        public ArccosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static ArccosineOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new ArccosineOperator(new Expression(args[0]));
        }

        /// <summary>
        /// Evaluate the arccosine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose cosine is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.AngleAreInDegrees"/> to return the angle in degrees or in radians.<br />
        /// If the evaluable is lower than -1 or greater than 1, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double cos = _left.Evaluate(variables);
            double angle;
            if (cos < -1 || cos > 1)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + cos + ')');
                }
                angle = double.NaN;
            }
            else
            {
                angle = Math.Acos(cos);
                if (MathEvaluator.AngleAreInDegrees)
                {
                    angle = Funcs.RadiansToDegrees(angle);
                }
            }
            return angle;
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
