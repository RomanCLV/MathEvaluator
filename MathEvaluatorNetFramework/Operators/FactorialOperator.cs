using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal class FactorialOperator : Operator
    {
        public FactorialOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the factorial of the given evaluable.<br />
        /// If the evaluable is a 0-positive integer, it use the classical factorial: <c>n!</c><br />
        /// If the evaluable is a negative integer, it can raise a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>.<br />
        /// If the evaluable is a real floating number, it can use the Gamma function depending on <see cref="MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial"/>: <c>x! = gamma(x+1)</c>.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The factorial of the given evaluable entity. It can also return <see cref="double.NaN"/> in the case where a <see cref="DomainException"/> is canceled when <see cref="MathEvaluator.RaiseDomainException"/> is false.</returns>
        /// <exception cref="DomainException"></exception>
        public override double Evaluate(params Variable[] variables)
        {
            double result;
            double evaluableResult = _left.Evaluate(variables);
            int evaluableResultInt = (int)evaluableResult;
            bool isAnInteger = (evaluableResult - evaluableResultInt) == 0.0;

            if (evaluableResult == 0.0 || evaluableResult == 1.0)
            {
                result = 1.0;
            }
            else if (evaluableResult > 0.0)
            {
                if (isAnInteger)
                {
                    result = Funcs.Factorial((uint)evaluableResult);
                }
                else
                {
                    if (MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial)
                    {
                        result = Funcs.Gamma(evaluableResult + 1.0);
                    }
                    else if (MathEvaluator.RaiseDomainException)
                    {
                        throw new DomainException("(" + evaluableResult + ")!");
                    }
                    else
                    {
                        result = double.NaN;
                    }
                }
            }
            else
            {
                if (MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial && !isAnInteger)
                {
                    result = Funcs.Gamma(evaluableResult + 1.0);
                }
                else if (MathEvaluator.RaiseDomainException)
                {
                    throw new DomainException("(" + evaluableResult + ")!");
                }
                else
                {
                    result = double.NaN;
                }
            }

            return result;
        }

        public override string ToString()
        {
            string result;
            if (_left is Addition || _left is Substraction || _left is NegativeOperator || _left is Multiplication || _left is Division || _left is FactorialOperator || _left is PowerOperator ||
                (_left is Expression e && e.Is(typeof(Addition), typeof(Substraction), typeof(NegativeOperator), typeof(Multiplication), typeof(Division), typeof(FactorialOperator), typeof(PowerOperator))))
            {
                result = '(' + _left.ToString() + ")!";
            }
            else
            {
                result = _left.ToString() + '!';
            }

            return result;
        }
    }
}
