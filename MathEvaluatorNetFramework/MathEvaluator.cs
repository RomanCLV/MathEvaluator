using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework
{
    public static class MathEvaluator
    {
        public static class Parameters
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
            /// Indicates to the trigonometrics functions if the angle has to be interpreted in degrees or in radians.
            /// </summary>
            public static bool AngleAreInDegrees { get; set; } = true;
        }

        public static class VariableManager
        {
            private readonly static List<Variable> s_variables = new List<Variable>();

            /// <summary>
            /// Try to create a new permanent variable.
            /// </summary>
            /// <param name="name">Name of the variable.</param>
            /// <param name="value">Value of the variable.</param>
            /// <param name="updateVariableIfAlreadyExists">If the variable already exists, update it by calling <see cref="Update(string, double, bool)"/>.</param>
            /// <returns><c>True</c> if the variable had been created (or updated), else <c>False</c>.</returns>
            public static bool Create(string name, double value = 0.0, bool updateVariableIfAlreadyExists = true)
            {
                bool added = false;
                Variable variable = s_variables.Find(v => v.Name == name);
                if (variable == null)
                {
                    try
                    {
                        variable = new Variable(name, value);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    }
                    if (variable != null)
                    {
                        s_variables.Add(variable);
                        added = true;
                    }
                }
                else
                {
                    if (updateVariableIfAlreadyExists)
                    {
                        variable.Value = value;
                        added = true;
                    }
                }
                return added;
            }

            /// <summary>
            /// Try to update a variable.
            /// </summary>
            /// <param name="name">Name of the variable.</param>
            /// <param name="value">New value of the variable.</param>
            /// <param name="createVariableIfNotExists">If the variable not exists, then create it by calling <see cref="Create(string, double, bool)"/>.</param>
            /// <returns></returns>
            public static bool Update(string name, double value, bool createVariableIfNotExists = true)
            {
                bool updated = false;
                Variable variable = s_variables.Find(v => v.Name == name);
                if (variable == null)
                {
                    if (createVariableIfNotExists)
                    {
                        variable = new Variable(name, value);
                        s_variables.Add(variable);
                        updated = true;
                    }
                }
                else
                {
                    variable.Value = value;
                    updated = true;
                }
                return updated;
            }

            /// <summary>
            /// Delete a permanent variable.
            /// </summary>
            /// <param name="name">Name of the variable.</param>
            /// <returns><c>True</c> if the variable exists and has been removed, else <c>false</c>.</returns>
            public static bool Delete(string name)
            {
                bool deleted = false;
                Variable variable = s_variables.Find(v => v.Name == name);
                if (variable != null)
                {
                    deleted = s_variables.Remove(variable);
                }
                return deleted;
            }

            /// <summary>
            /// Determine if a variable exists.
            /// </summary>
            /// <param name="name">Name of the variable.</param>
            /// <returns><c>True</c> if the variable exists, else <c>false</c>.</returns>
            public static bool Contains(string name)
            {
                return s_variables.Any(v => v.Name == name);
            }

            /// <summary>
            /// Get the names list of all existing variables.
            /// </summary>
            /// <returns>The list with all variable's name.</returns>
            public static IEnumerable<string> GetExistingVariables()
            {
                return s_variables.Select(v => v.Name);
            }

            /// <summary>
            /// Get the value of a variable.
            /// </summary>
            /// <param name="name">The name of the variable.</param>
            /// <returns>The value of the variable.</returns>
            /// <exception cref="NotDefinedVariableException"></exception>
            public static double Get(string name)
            {
                Variable variable = s_variables.Find(v => v.Name == name);
                return variable == null ? throw new NotDefinedVariableException(name) : (double)variable.Value;
            }

            public static void Clear()
            {
                s_variables.Clear();
            }
        }

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
