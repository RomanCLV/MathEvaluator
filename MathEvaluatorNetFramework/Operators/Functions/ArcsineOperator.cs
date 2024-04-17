using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class ArcsineOperator : FunctionOperator
    {
        private readonly static string _fullname = "arcsine";
        private readonly static string _acronym = "asin";
        private readonly static string _description = "Returns the arcsine of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "asin(x)"
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

        public ArcsineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static ArcsineOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new ArcsineOperator(new Expression().Set(args[0]));
        }

        /// <summary>
        /// Evaluate the arcsine of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// Returns the angle whose sine is the specified evaluable.<br />
        /// Set <see cref="MathEvaluator.Parameters.AngleAreInDegrees"/> to return the angle in degrees or in radians.<br />
        /// If the evaluable is lower than -1 or greater than 1, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.Parameters.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double sin = _left.Evaluate(variables);
            double angle;
            if (sin < -1 || sin > 1)
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + sin + ')');
                }
                angle = double.NaN;
            }
            else
            {
                angle = Math.Asin(sin);
                if (MathEvaluator.Parameters.AngleAreInDegrees)
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
