using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Expressions;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework
{
    public static class MathEvaluator
    {
        public static void Reset()
        {
            RaiseDivideByZeroException = true;
            RaiseDomainException = true;
            UseGammaFunctionForNonNaturalIntegerFactorial = true;
        }

        /// <summary>
        /// Whether or not to raise an <see cref="DivideByZeroException"/> if we try to divide by zero.
        /// </summary>
        public static bool RaiseDivideByZeroException { get; set; } = true;

        /// <summary>
        /// Whether or not to raise an <see cref="DomainException"/> if we try to apply an operation with a invalid domain value.<br />
        /// Examples:<br />
        /// 0^0<br />
        /// 0/0<br />
        /// sqrt(x) with x lower than 0<br />
        /// ln(x) with x lower or equal than 0<br />
        /// </summary>
        public static bool RaiseDomainException { get; set; } = true;

        /// <summary>
        /// Whether or not to use the gamma function if the factorial of a floating number or a negative number is applying. If not, a <see cref="DomainException"/> can be raised depending on <see cref="RaiseDomainException"/>.<br />
        /// Examples:<br />
        /// 5!      -> classic factorial<br />
        /// 5.25!   -> Use the gamma function or can raise a <see cref="DomainException"/> depending on <see cref="RaiseDomainException"/><br />
        /// (-0.5)! -> Use the gamma function or can raise a <see cref="DomainException"/> depending on <see cref="RaiseDomainException"/><br />
        /// (-n)! where n is a positive integer -> Raise a <see cref="DomainException"/> depending on <see cref="RaiseDomainException"/><br />
        /// </summary>
        public static bool UseGammaFunctionForNonNaturalIntegerFactorial { get; set; } = true;

        /// <summary>
        /// Create a new Expression with the given expression.
        /// </summary>
        /// <param name="expression">
        /// The literral expression.<br/>
        /// All values used with trignometric functions are in degrees.<br/>
        /// Examples:<br/>
        /// 5+3<br/>
        /// cos(45)*sin(45)<br/>
        /// x+2<br/>
        /// exp(x)+y^2<br/>
        /// e^(-((x^2+y^2)/2))<br/>
        /// </param>
        /// <returns>The new expression</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static Expression Expression(string expression)
        {
            return new Expression(expression);
        }

        /// <summary>
        /// Evaluate a given expression.
        /// </summary>
        /// <param name="expression">
        /// The literral expression.<br/>
        /// All values used with trignometric functions are in degrees.<br/>
        /// Examples:<br/>
        /// 5+3<br/>
        /// cos(45)*sin(45)<br/>
        /// x+2<br/>
        /// exp(x)+y^2<br/>
        /// e^(-((x^2+y^2)/2))<br/>
        /// </param>
        /// <returns>The result of the given expression.</returns>
        /// <param name="variables">The values of the used variables in the expression.</param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotDefinedVariableException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        public static double Evaluate(string expression, params Variable[] variables)
        {
            return new Expression(expression).Evaluate(variables);
        }
    }
}
