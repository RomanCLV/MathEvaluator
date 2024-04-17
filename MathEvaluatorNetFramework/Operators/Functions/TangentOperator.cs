using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class TangentOperator : FunctionOperator
    {
        private readonly static string _fullname = "tangent";
        private readonly static string _acronym = "tan";
        private readonly static string _description = "Returns the tangent of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "tan(x)"
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

        public TangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static TangentOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new TangentOperator(new Expression().Set(args[0]));
        }

        /// <summary>
        /// Evaluate the tangent of the given evaluable. Set <see cref="MathEvaluator.Parameters.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The tangent of the given evaluable.<br />
        /// If the evaluable is a multiple of PI/2 (or 90°), raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.Parameters.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double angle = _left.Evaluate(variables);
            double rad = MathEvaluator.Parameters.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle;

            if (Funcs.IsMultiple(rad, Math.PI / 2.0))
            {
                if (MathEvaluator.Parameters.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + (MathEvaluator.Parameters.AngleAreInDegrees ? angle.ToString() + '°' : rad.ToString()) + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Math.Tan(rad);
            }
            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
