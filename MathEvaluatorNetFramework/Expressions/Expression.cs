using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Operators;

namespace MathEvaluatorNetFramework.Expressions
{
    public class Expression : IEvaluable
    {
        private IEvaluable _evaluable;
        private readonly List<Variable> _variables;

        public Expression()
        {
            _evaluable = null;
            _variables = new List<Variable>();
        }

        /// <summary>
        /// Expression constructor with a given expression.
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
        /// <exception cref="FormatException"></exception>
        public Expression(string expression) : this()
        {
            InternalSet(expression, false);
        }

        private Expression(string expression, bool isExpressionCleaned) : this()
        {
            InternalSet(expression, isExpressionCleaned);
        }

        /// <summary>
        /// Define the current Expression with the given expression.
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
        /// <exception cref="FormatException"></exception>
        public void Set(string expression)
        {
            InternalSet(expression, false);
        }

        private void InternalSet(string expression, bool isExpressionCleaned)
        {
            _evaluable = null;
            _variables.Clear();

            // first general preparation - optionnal
            if (!isExpressionCleaned)
            {
                Console.WriteLine("Given: " + expression);
                expression = PrepareExpression(expression);
            }

            // add surround negatives with ^
            expression = ManagePowerExpression(expression);

            // add surround negatives with ()
            expression = ManageNegativeExpression(expression);
            bool haveChanged;
            do
            {
                // remove surrounding ()
                expression = RemoveGlobalParenthesis(expression, out haveChanged);
            } while (haveChanged);

            // last general preparation to be sure...
            expression = PrepareExpression(expression);

            Console.WriteLine("Expression: " + expression);
            SetCleanedExpression(expression);
        }

        private string PrepareExpression(string expression)
        {
            expression = string.Join("", expression.Trim().ToLower().Replace(",", ".").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            int parenthesisCount = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    parenthesisCount++;
                }
                else if (expression[i] == ')')
                {
                    parenthesisCount--;
                }
                if (parenthesisCount < 0)
                {
                    throw new FormatException("Invalid format exception: Too many ')' or too few '('");
                }
            }
            if (parenthesisCount > 0)
            {
                throw new FormatException("Invalid format exception: Too many '(' or too few ')'");
            }


            while (expression.Contains("+-"))
            {
                expression = expression.Replace("+-", "-");
            }
            while (expression.Contains("-+"))
            {
                expression = expression.Replace("-+", "-");
            }
            while (expression.Contains("--"))
            {
                expression = expression.Replace("--", "+");
            }
            while (expression.Contains("*+"))
            {
                expression = expression.Replace("*+", "*");
            }
            while (expression.Contains("/+"))
            {
                expression = expression.Replace("/+", "/");
            }

            expression = expression
                .Replace("*.", "*0.")
                .Replace("/.", "/0.")
                .Replace("+.", "+0.")
                .Replace("-.", "-0.");

            string[] invalidSequences = new string[]
            {
                //"(+",
                //"(-",
                "(*",
                "(/",

                "+)",
                "-)",
                "*)",
                "/)",

                "++",
                //"+-",
                "+*",
                "+/",

                //"-+",
                //"--",
                "-*",
                "-/",

                //"*+",
                //"*-",
                "**",
                "*/",

                //"/+",
                //"/-",
                "/*",
                "//",

                "^+",
                //"^-",
                "^*",
                "^/",

                "+^",
                "-^",
                "*^",
                "/^",

                "^^",
                "^.",
                "^!",
                "..",
                //"!!"
            };

            foreach (string invalidSequence in invalidSequences)
            {
                if (expression.Contains(invalidSequence))
                {
                    throw new FormatException("Invalid sequence: " + invalidSequence);
                }
            }

            char[] specialCharacters = new char[]
            {
                '(',
                ')',
                '.',
                '!',
                '^',
                '+',
                '-',
                '*',
                '/',
                '_',
            };


            foreach (char c in expression)
            {
                if (!char.IsDigit(c) && !char.IsLetter(c) && !specialCharacters.Contains(c))
                {
                    throw new FormatException("Invalid character: " + c);
                }
            }

            // remove useless point
            if (expression.EndsWith("."))
            {
                expression = expression.Substring(0, expression.Length - 1);
            }

            expression = expression
                .Replace("(+", "(")
                .Replace("()", "");

            // remove useless point: .(   .)   .+
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == '.' && specialCharacters.Contains(expression[i + 1]))
                {
                    string left = expression.Substring(0, i);
                    string right = expression.Substring(i + 1, expression.Length - 1 - i);
                    expression = left + right;
                }
            }

            // add * if required: 2( -> 2*(   |   )2 -> )*2
            // add 0 if required: .2 -> 0.2
            for (int i = 0; i < 10; i++)
            {
                string iStr = i.ToString();
                expression = expression
                    .Replace(")" + iStr, ")*" + iStr)
                    .Replace(iStr + "(", iStr + "*(");

                for (int j = 1; j < expression.Length - 1; j++)
                {
                    if (expression[j] == '.' && !char.IsDigit(expression[j - 1]))
                    {
                        expression = expression.Insert(j, "0");
                    }
                }
            }

            expression = expression
                .Replace(")(", ")*(")
                .Replace("!(", "!*(");

            return expression;
        }

        private string ManagePowerExpression(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '^')
                {
                    int parenthesisCount = 0;
                    int j = i - 1;

                    while (j >= 0)
                    {
                        if (expression[j] == '(')
                        {
                            parenthesisCount++;
                        }
                        else if (expression[j] == ')')
                        {
                            parenthesisCount--;
                        }
                        else if (parenthesisCount == 0 && IsCharOperandSymbol(expression[j]))
                        {
                            j++;
                            break;
                        }
                        j--;
                    }

                    if (j == -1)
                    {
                        j = 0;
                    }

                    if (parenthesisCount == 0)
                    {
                        while (IsCharOperandSymbol(expression[j]))
                        {
                            j++;
                        }
                        expression = expression.Insert(j, "(");
                        i++;
                    }
                    else
                    {
                        continue;
                    }

                    j = i + 1;

                    while (j < expression.Length)
                    {
                        if (expression[j] == '(')
                        {
                            parenthesisCount++;
                        }
                        else if (expression[j] == ')')
                        {
                            parenthesisCount--;
                        }
                        else if (parenthesisCount == 0 && IsCharOperandSymbol(expression[j]))
                        {
                            break;
                        }
                        j++;
                    }
                    if (parenthesisCount == 0)
                    {
                        if (j == expression.Length)
                        {
                            expression += ')';
                        }
                        else
                        {
                            expression = expression.Insert(j + 1, ")");
                        }
                    }
                }
            }
            return expression;
        }

        private bool IsCharOperandSymbol(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private string ManageNegativeExpression(string expression)
        {
            bool hasFoundMinus = false;
            int parenthesisCount = 0;
            int parenthesisCountMinusFound = 0;
            char c;
            for (int i = 0; i < expression.Length; i++)
            {
                c = expression[i];
                if (hasFoundMinus)
                {
                    if (!char.IsDigit(c))
                    {
                        if (c == '(')
                        {
                            parenthesisCount++;
                        }
                        else if (c == ')')
                        {
                            parenthesisCount--;

                            if (parenthesisCount == parenthesisCountMinusFound)
                            {
                                expression = expression.Insert(i, ")");
                                parenthesisCount--;
                                i++;
                                hasFoundMinus = false;
                            }
                        }
                        else if (c == '.')
                        {
                            continue;
                        }
                        else
                        {
                            if (parenthesisCount == parenthesisCountMinusFound + 1)
                            {
                                expression = expression.Insert(i, ")");
                                parenthesisCount--;
                                i++;
                                hasFoundMinus = false;
                            }
                        }
                    }
                }
                else
                {
                    if (c == '(')
                    {
                        parenthesisCount++;
                    }
                    else if (c == ')')
                    {
                        parenthesisCount--;
                    }
                    else if (c == '-')
                    {
                        parenthesisCountMinusFound = parenthesisCount;
                        if (i == 0)
                        {
                            expression = expression.Insert(0, "(");
                            parenthesisCount++;
                            hasFoundMinus = true;
                            i++;
                        }
                        else
                        {
                            char prevC = expression[i - 1];
                            if (prevC != '(')
                            {
                                if (prevC == '/' || prevC == '*')
                                {
                                    expression = expression.Insert(i, "(");
                                    i++;
                                }
                                else
                                {
                                    expression = expression.Insert(i, "+(");
                                    i += 2;
                                }
                                parenthesisCount++;
                                hasFoundMinus = true;
                            }
                        }
                    }
                }
            }
            if (hasFoundMinus && parenthesisCount <= 1)
            {
                expression += ")";
            }
            expression = expression.Replace(")(", ")*(");
            return expression;
        }

        private string RemoveGlobalParenthesis(string expression, out bool haveChanged)
        {
            haveChanged = false;
            if (expression.StartsWith("(") && expression.EndsWith(")"))
            {
                int parenthesisCount = 0;
                for (int i = 0; parenthesisCount < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        parenthesisCount++;
                    }
                    else if (expression[i] == ')')
                    {
                        parenthesisCount--;
                    }
                    if (parenthesisCount == 0)
                    {
                        if (i == expression.Length - 1)
                        {
                            haveChanged = true;
                            expression = expression.Substring(1, expression.Length - 2);
                        }
                        break;
                    }
                }
            }
            return expression;
        }

        private void SetCleanedExpression(string expression)
        {
            if (double.TryParse(expression.Replace('.', ','), out double d) || double.TryParse(expression, out d))
            {
                _evaluable = new ValueExpression(d);
            }

            if (_evaluable == null && expression.Contains('+'))
            {
                _evaluable = CheckAddition(expression);
            }
            if (_evaluable == null && expression.Contains('-'))
            {
                _evaluable = CheckSubstraction(expression);
            }
            if (_evaluable == null && expression.Contains('*'))
            {
                _evaluable = CheckMultiplication(expression);
            }
            if (_evaluable == null && expression.Contains('/'))
            {
                _evaluable = CheckDivision(expression);
            }
            if (_evaluable == null && expression.Contains('^'))
            {
                _evaluable = CheckPower(expression);
            }
            if (_evaluable == null && expression.Contains('!'))
            {
                _evaluable = CheckFactorial(expression);
            }
            if (_evaluable == null)
            {
                throw new NotSupportedException(expression);
            }
        }

        private IEvaluable CheckAddition(string expression)
        {
            return CheckOperand(expression, '+', "addition", (l, r) => new Addition(l, r));
        }

        private IEvaluable CheckSubstraction(string expression)
        {
            return expression[0] == '-' ?
                new NegativeOperator(new Expression(expression.Substring(1), true)) :
                CheckOperand(expression, '-', "substraction", (l, r) => new Substraction(l, r));
        }

        private IEvaluable CheckMultiplication(string expression)
        {
            return CheckOperand(expression, '*', "multiplication", (l, r) => new Multiplication(l, r));
        }

        private IEvaluable CheckDivision(string expression)
        {
            return CheckOperand(expression, '/', "division", (l, r) => new Division(l, r));
        }

        private IEvaluable CheckPower(string expression)
        {
            return CheckOperand(expression, '^', "power", (b, p) => new PowerOperator(b, p));
        }

        private IEvaluable CheckFactorial(string expression)
        {
            return CheckOperand(expression, '!', "factorial", (e) => new FactorialOperator(e));
        }

        private List<int> FindSplitIndex(string expression, char c)
        {
            int parenthesisCount;
            int index;
            int i;
            List<int> indexToTest = new List<int>(expression.Length);
            List<int> validIndexs = new List<int>(expression.Length);

            for (i = 0; i < expression.Length; i++)
            {
                if (expression[i] == c)
                {
                    indexToTest.Add(i);
                }
            }

            while (indexToTest.Count > 0)
            {
                parenthesisCount = 0;
                index = indexToTest.First();
                indexToTest.RemoveAt(0);

                for (i = 0; i < index; i++)
                {
                    if (expression[i] == '(')
                    {
                        parenthesisCount++;
                    }
                    else if (expression[i] == ')')
                    {
                        parenthesisCount--;
                    }
                }

                if (parenthesisCount == 0)
                {
                    validIndexs.Add(index);
                }
            }
            return validIndexs;
        }

        private void CheckEmptyExpression(string expression, string operationName, string expressionName)
        {
            if (expression.Length == 0)
            {
                throw new FormatException("Invalid " + operationName + " format: " + expressionName + " expression empty");
            }
        }

        private IEvaluable CheckOperand(string expression, char c, string operandName, Func<IEvaluable, IEvaluable> func)
        {
            //int index = FindSplitIndex(expression, c);
            List<int> validIndexs = FindSplitIndex(expression, c);
            int index = validIndexs.Count > 0 ? validIndexs.Last() : -1;
            if (index > 0)
            {
                string left = expression.Substring(0, index);

                CheckEmptyExpression(left, operandName, "left");

                Expression expLeft = new Expression(left, true);

                return func(expLeft);
            }
            return null;
        }

        private IEvaluable CheckOperand(string expression, char c, string operandName, Func<IEvaluable, IEvaluable, IEvaluable> func)
        {
            //int index = FindSplitIndex(expression, c);
            List<int> validIndexs = FindSplitIndex(expression, c);
            int index = validIndexs.Count > 0 ? validIndexs.Last() : -1;
            if (index > 0)
            {
                string left = expression.Substring(0, index);
                string right = expression.Substring(index + 1);

                CheckEmptyExpression(left, operandName, "left");
                CheckEmptyExpression(right, operandName, "right");

                Expression expLeft = new Expression(left, true);
                Expression expRight = new Expression(right, true);

                return func(expLeft, expRight);
            }
            return null;
        }

        // TODO: liste des mots clés réservés (math func, constantes pi, e, phi, tau)
        public double Evaluate(params Variable[] variables)
        {
            if (_evaluable == null)
            {
                throw new InvalidOperationException("Current evaluation not set.");
            }
            // verifier que les variables donnees n'ont pas des noms réservés
            return _evaluable.Evaluate(variables);
        }
    }
}
