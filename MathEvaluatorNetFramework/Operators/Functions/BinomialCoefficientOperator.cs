using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class BinomialCoefficientOperator : FunctionOperator
    {
        private readonly static string _acronym = "binc";
        public new static string Acronym => _acronym;

        private IEvaluable _k => _left;
        private IEvaluable _n;

        public BinomialCoefficientOperator(IEvaluable evaluableK, IEvaluable evaluableN) : base(evaluableK)
        {
            _n = evaluableN;
        }

        /// <summary>
        /// Returns the binomial coefficient, written (n k) and pronounced "n choose k" of the two given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>
        /// The binomial coefficient "n choose k" of the two given evaluable.<br />
        /// If one of the evaluables is not an integer or is lower than 0, raises a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, or returns <see cref="double.NaN"/>.
        /// </returns>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double k = _k.Evaluate(variables);
            double n = _n.Evaluate(variables);

            int ki = (int)k;
            int ni = (int)n;

            if (k <= 0 || n <= 0 || k - ki != 0.0 || n - ni != 0.0)
            {
                if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException(_acronym + '(' + k + ", " + n + ')');
                }
                else
                {
                    result = double.NaN;
                }
            }
            else
            {
                result = Funcs.BinomialCoefficient(ki, ni);
            }
            return result;
        }
    }
}
