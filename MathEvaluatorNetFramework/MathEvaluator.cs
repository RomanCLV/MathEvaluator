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
        /// <exception cref="NotDefinedVariableException"></exception>
        public static double Evaluate(string expression, params Variable[] variables)
        {
            return new Expression(expression).Evaluate(variables);
        }
    }
}
