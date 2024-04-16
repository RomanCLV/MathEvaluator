using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public static class VariablesManager
        {
            private readonly static List<Variable> s_variables = new List<Variable>();

            /// <summary>
            /// Try to create a new permanent variable.
            /// </summary>
            /// <param name="name">Name of the variable.</param>
            /// <param name="value">Value of the variable.</param>
            /// <param name="updateVariableIfAlreadyExists">If the variable already exists, update it by calling <see cref="Update(string, double, bool)"/>.</param>
            /// <returns><c>True</c> if the variable has been created (or updated), else (variable or expression with this name already exists) <c>False</c>.</returns>
            public static bool Create(string name, double value = 0.0, bool updateVariableIfAlreadyExists = true)
            {
                bool added = false;
                Variable variable = s_variables.Find(v => v.Name == name);
                if (variable == null)
                {
                    if (!ExpressionsManager.Contains(name))
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
                }
                else
                {
                    if (updateVariableIfAlreadyExists)
                    {
                        added = Update(name, value, false);
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
            /// <returns><c>True</c> if the variable has been updated (or created), else <c>False</c>.</returns>
            public static bool Update(string name, double value, bool createVariableIfNotExists = true)
            {
                bool updated = false;
                Variable variable = s_variables.Find(v => v.Name == name);
                if (variable == null)
                {
                    if (createVariableIfNotExists)
                    {
                        updated = Create(name, value, false);
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
            /// Get a list with the names of all existing variables.
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
            /// <exception cref="NotDefinedException"></exception>
            public static double Get(string name)
            {
                Variable variable = s_variables.Find(v => v.Name == name);
                return variable == null ? throw new NotDefinedException(name) : (double)variable.Value;
            }

            public static void Clear()
            {
                s_variables.Clear();
            }
        }

        public static class ExpressionsManager
        {
            private readonly static List<Expression> s_expressions = new List<Expression>();

            /// <summary>
            /// Try to create a new permanent expression.
            /// </summary>
            /// <param name="name">Name of the expression.</param>
            /// <param name="expression">Value of the expression.</param>
            /// <param name="updateVariableIfAlreadyExists">If the variable already exists, update it by calling <see cref="Update(string, string, bool)"/>.</param>
            /// <returns><c>True</c> if the expression has been created (or updated), else (variable or expression with this name already exists) <c>False</c>.</returns>
            public static bool Create(string name, string expression, bool updateVariableIfAlreadyExists = true)
            {
                bool added = false;
                Expression exp = s_expressions.Find(e => e.Name == name);
                if (exp == null)
                {
                    if (!VariablesManager.Contains(name))
                    {
                        try
                        {
                            exp = new Expression(expression, name);
                            s_expressions.Add(exp);
                            added = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                        }
                    }
                }
                else
                {
                    if (updateVariableIfAlreadyExists)
                    {
                        added = Update(name, expression, false);
                    }
                }
                return added;
            }

            /// <summary>
            /// Try to update a expression.
            /// </summary>
            /// <param name="name">Name of the expression.</param>
            /// <param name="expression">New value of the expression.</param>
            /// <param name="createExpressionIfNotExists">If the expression not exists, then create it by calling <see cref="Create(string, string, bool)"/>.</param>
            /// <returns><c>True</c> if the expression has been updated (or created), else <c>False</c>.</returns>
            public static bool Update(string name, string expression, bool createExpressionIfNotExists = true)
            {
                bool updated = false;
                Expression exp = s_expressions.Find(e => e.Name == name);
                if (exp == null)
                {
                    if (createExpressionIfNotExists)
                    {
                        updated = Create(name, expression, false);
                    }
                }
                else
                {
                    try
                    {
                        exp.Set(expression);
                        updated = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    }
                }
                return updated;
            }

            /// <summary>
            /// Delete a permanent expression.
            /// </summary>
            /// <param name="name">Name of the expression.</param>
            /// <returns><c>True</c> if the expression exists and has been removed, else <c>false</c>.</returns>
            public static bool Delete(string name)
            {
                bool deleted = false;
                Expression exp = s_expressions.Find(e => e.Name == name);
                if (exp != null)
                {
                    deleted = s_expressions.Remove(exp);
                }
                return deleted;
            }

            /// <summary>
            /// Determine if an expression exists.
            /// </summary>
            /// <param name="name">Name of the expression.</param>
            /// <returns><c>True</c> if the expression exists, else <c>false</c>.</returns>
            public static bool Contains(string name)
            {
                return s_expressions.Any(e => e.Name == name);
            }

            /// <summary>
            /// Get a list with the name of all existing expressions.
            /// </summary>
            /// <returns>The list with all expression's name.</returns>
            public static IEnumerable<string> GetExistingExpressions()
            {
                return s_expressions.Select(exp => exp.Name);
            }

            /// <summary>
            /// Get the value of a variable.
            /// </summary>
            /// <param name="name">The name of the variable.</param>
            /// <returns>The value of the variable.</returns>
            /// <exception cref="NotDefinedException"></exception>
            public static Expression Get(string name)
            {
                Expression expression = s_expressions.Find(v => v.Name == name);
                return expression ?? throw new NotDefinedException(name);
            }

            public static void Clear()
            {
                s_expressions.Clear();
            }
        }

        public static bool IsReservedKeyWord(string keyWord)
        {
            return MathEvaluatorNetFramework.Expression.ReservedNames.Contains(keyWord);
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
        /// <param name="variables">The variables used in the expression.</param>
        /// <returns>The result of the given expression.</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotDefinedException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        public static double Evaluate(string expression, params Variable[] variables)
        {
            return new Expression(expression).Evaluate(variables);
        }

        /// <summary>
        /// Evaluate a given expression.
        /// </summary>
        /// <param name="expression">The epxression to evaluate.</param>
        /// <param name="variables">The variables used in the expression.</param>
        /// <returns>The result of the given expression.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotDefinedException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        public static double Evaluate(Expression expression, params Variable[] variables)
        {
            return expression.Evaluate(variables);
        }
    }
}
