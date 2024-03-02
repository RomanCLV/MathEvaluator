using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class SqrtOperator : FunctionOperator
    {
        private readonly static string _acronym = "sqrt";
        public new static string Acronym => _acronym;

        private readonly double _root;

        public SqrtOperator(IEvaluable evaluable, double root = 2.0) : base(evaluable)
        {
            _root = root;
        }

        /// <summary>
        /// Returns the Nth root of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The Nth root of the given evaluable.<br />
        /// If the root is equal to 0 or the evaluable is lower than 0, raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double value = _left.Evaluate(variables);

            if (_root == 0.0 || value < 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + _root + ", " + value + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else if (_root == 2.0)
            {
                result = Math.Sqrt(value);
            }
            else
            {
                result = Math.Pow(value, 1.0 / _root);
            }
            return result;
        }
    }
}
