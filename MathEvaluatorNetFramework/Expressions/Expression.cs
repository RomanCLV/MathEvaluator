using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Operators;
using MathEvaluatorNetFramework.Operators.Functions;

namespace MathEvaluatorNetFramework.Expressions
{
    public class Expression : IEvaluable
    {
        private IEvaluable _evaluable;

        private static readonly List<string> s_constants = new List<string>
        {
            "pi",
            "tau",
            "pau",
            "e",
            "phi"
        };

        private static readonly Dictionary<string, FunctionOperatorDetails> s_functions = new Dictionary<string, FunctionOperatorDetails>
        {
            // name - max parameters count
            { AbsoluteOperator.Acronym, AbsoluteOperator.Details },
            { ExponentialOperator.Acronym, ExponentialOperator.Details },
            { LogarithmOperator.Acronym, LogarithmOperator.Details },
            { NaperianLogarithmOperator.Acronym, NaperianLogarithmOperator.Details },
            { SqrtOperator.Acronym, SqrtOperator.Details },

            { CosineOperator.Acronym, CosineOperator.Details },
            { SineOperator.Acronym, SineOperator.Details },
            { TangentOperator.Acronym, TangentOperator.Details },

            { ArccosineOperator.Acronym, ArccosineOperator.Details },
            { ArcsineOperator.Acronym, ArcsineOperator.Details },
            { ArctangentOperator.Acronym, ArctangentOperator.Details },

            { SecantOperator.Acronym, SecantOperator.Details },
            { CosecantOperator.Acronym, CosecantOperator.Details },
            { CotangentOperator.Acronym, CotangentOperator.Details },

            { HyperbolicCosineOperator.Acronym, HyperbolicCosineOperator.Details },
            { HyperbolicSineOperator.Acronym, HyperbolicSineOperator.Details },
            { HyperbolicTangentOperator.Acronym, HyperbolicTangentOperator.Details },

            { HyperbolicSecantOperator.Acronym, HyperbolicSecantOperator.Details },
            { HyperbolicCosecantOperator.Acronym, HyperbolicCosecantOperator.Details },
            { HyperbolicCotangentOperator.Acronym, HyperbolicCotangentOperator.Details },

            { DegreeOperator.Acronym, DegreeOperator.Details },
            { RadianOperator.Acronym, RadianOperator.Details },

            { DecimalOperator.Acronym, DecimalOperator.Details },
            { CeilOperator.Acronym, CeilOperator.Details },
            { FloorOperator.Acronym, FloorOperator.Details },
            { RoundOperator.Acronym, RoundOperator.Details },

            { BinomialCoefficientOperator.Acronym, BinomialCoefficientOperator.Details }
        };

        private readonly static char[] s_specialCharacters = new char[]
        {
            '(',
            ')',
            '.',
            ',',
            '!',
            '^',
            '+',
            '-',
            '*',
            '/',
            '_',
        };

        private readonly static string[] s_invalidSequences = new string[]
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
            "^^",
            //"^.",
            "^,",
            "^!",

            "+^",
            "-^",
            "*^",
            "/^",

            "+!",
            "-!",
            "*!",
            "/!",

            "+,",
            "-,",
            "*,",
            "/,",

            //",+",
            //",-",
            ",*",
            ",/",
            ",^",
            ",!",
            ",,",
            ",)",

            "(,",
            "..",
            //"!!"
        };

        public Expression()
        {
            _evaluable = null;
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

            // first general preparation - optionnal
            if (!isExpressionCleaned)
            {
                //Console.WriteLine("Given: " + expression);
                expression = PrepareExpression(expression);
            }

            // add surround negatives with !
            expression = ManageFactorialExpression(expression);

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

            //Console.WriteLine("Expression: " + expression);
            SetCleanedExpression(expression);
        }

        private string PrepareExpression(string expression)
        {
            expression = string.Join("", expression.Trim().ToLower()/*.Replace(",", ".")*/.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrEmpty(expression) || string.IsNullOrWhiteSpace(expression))
            {
                throw new FormatException("Empty expression");
            }

            if (expression.Length == 1 && s_specialCharacters.Contains(expression[0]))
            {
                throw new FormatException("Invalid expression: " + expression[0]);
            }

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

            // remove useless point
            if (expression.EndsWith("."))
            {
                expression = expression.Substring(0, expression.Length - 1);
            }

            foreach (string invalidSequence in s_invalidSequences)
            {
                if (expression.Contains(invalidSequence))
                {
                    throw new FormatException("Invalid sequence: " + invalidSequence);
                }
            }

            foreach (char c in expression)
            {
                if (!char.IsDigit(c) && !char.IsLetter(c) && !s_specialCharacters.Contains(c))
                {
                    throw new FormatException("Invalid character: " + c);
                }
            }

            expression = expression
                .Replace("(+", "(")
                .Replace("()", "");

            // remove useless point: .(   .)   .+
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == '.' && s_specialCharacters.Contains(expression[i + 1]))
                {
                    string left = expression.Substring(0, i);
                    string right = expression.Substring(i + 1, expression.Length - 1 - i);
                    expression = left + right;
                }
            }

            CheckComaCount(expression);

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

            CheckDotCount(expression);

            expression = AddMultiplieBetweenVariables(expression);

            return expression;
        }

        private void CheckDotCount(string expression)
        {
            bool isReadingANumber = false;
            bool foundDot = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in expression)
            {
                if (isReadingANumber)
                {
                    if (char.IsDigit(c))
                    {
                        sb.Append(c);
                        continue;
                    }
                    else
                    {
                        if (c == '.')
                        {
                            sb.Append(c);
                            if (foundDot)
                            {
                                throw new FormatException("Invalid format: " + sb.ToString());
                            }
                            foundDot = true;
                        }
                        else
                        {
                            isReadingANumber = false;
                            sb.Clear();
                        }
                    }
                }
                else
                {
                    foundDot = c == '.';
                    isReadingANumber = foundDot || char.IsDigit(c);
                    sb.Append(c);
                }
            }
        }

        private void CheckComaCount(string expression)
        {
            if (!expression.Contains(','))
            {
                return;
            }

            bool isReadingAWord = false;
            StringBuilder sb = new StringBuilder();
            char c;

            for (int i = 0; i < expression.Length; i++)
            {
                c = expression[i];

                if (char.IsLetter(c))
                {
                    if (!isReadingAWord)
                    {
                        sb = new StringBuilder();
                    }
                    sb.Append(c);
                    isReadingAWord = true;
                }
                else
                {
                    if (char.IsDigit(c))
                    {
                        if (isReadingAWord)
                        {
                            sb.Append(c);
                            continue;
                        }
                    }
                    else if (c == '_')
                    {
                        if (isReadingAWord)
                        {
                            sb.Append(c);
                            continue;
                        }
                        else
                        {
                            isReadingAWord = true;
                            sb.Clear();
                            sb.Append(c);
                            continue;
                        }
                    }
                    else if (c == '(')
                    {
                        if (isReadingAWord)
                        {
                            string word = sb.ToString();
                            sb = new StringBuilder();

                            if (s_functions.ContainsKey(word))
                            {
                                string content = GetParenthesisContent(expression, i + 1);
                                sb = new StringBuilder();

                                List<int> indexs = FindSplitIndex(content, ',');
                                if (indexs.Count > 0)
                                {
                                    CheckFunctionArgsCount(word, indexs.Count + 1);

                                    string[] contentSplitted = GetParenthesisContentSplitted(content, indexs);

                                    foreach (string subContent in contentSplitted)
                                    {
                                        CheckComaCount(subContent);
                                    }
                                    i += content.Length + 2;
                                }
                                else
                                {
                                    i += content.Length + 1;
                                }
                            }
                        }
                    }
                    else if (c == ',')
                    {
                        throw new FormatException("Invalid character: ,");
                    }
                    isReadingAWord = false;
                }
            }
        }

        private string GetParenthesisContent(string expression, int startIndex)
        {
            StringBuilder sb = new StringBuilder();
            int j = startIndex;
            int parenthesisCount = 1;
            while (j < expression.Length)
            {
                if (expression[j] == '(')
                {
                    parenthesisCount++;
                }
                else if (expression[j] == ')')
                {
                    parenthesisCount--;
                    if (parenthesisCount == 0)
                    {
                        break;
                    }
                }
                sb.Append(expression[j]);
                j++;
            }

            return sb.ToString();
        }

        private string[] GetParenthesisContentSplitted(string expression, List<int> indexs = null)
        {
            if (indexs == null)
            {
                indexs = FindSplitIndex(expression, ',');
            }
            int argCount = indexs.Count + 1;

            string[] expressionSplitted = new string[argCount];

            if (indexs.Count == 0)
            {
                expressionSplitted[indexs.Count] = expression;
            }
            else
            {
                while (indexs.Count > 0)
                {
                    int index = indexs.Last();
                    string s = expression.Substring(index + 1, expression.Length - index - 1);
                    expression = expression.Substring(0, index);
                    expressionSplitted[indexs.Count] = s;
                    indexs.RemoveAt(indexs.Count - 1);

                    if (indexs.Count == 0)
                    {
                        expressionSplitted[indexs.Count] = expression;
                    }
                }
            }

            return expressionSplitted;
        }

        private string AddMultiplieBetweenVariables(string expression)
        {
            bool isReadingANumber = false;
            bool isReadingAWord = false;
            char c;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < expression.Length; i++)
            {
                c = expression[i];
                if (char.IsDigit(c))
                {
                    if (isReadingAWord)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        if (!isReadingANumber)
                        {
                            isReadingANumber = true;
                        }
                    }
                }
                else if (char.IsLetter(c))
                {
                    if (isReadingAWord)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        if (isReadingANumber)
                        {
                            expression = expression.Insert(i, "*");
                            i++;
                            isReadingANumber = false;
                        }
                        isReadingAWord = true;
                        sb = new StringBuilder();
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c == '_')
                    {
                        if (isReadingANumber)
                        {
                            expression = expression.Insert(i, "*");
                            i++;
                            isReadingANumber = false;
                        }
                        if (!isReadingAWord)
                        {
                            isReadingAWord = true;
                            sb = new StringBuilder();
                        }
                        sb.Append(c);
                    }
                    else if (c == '(')
                    {
                        if (isReadingAWord)
                        {
                            string word = sb.ToString();
                            sb = new StringBuilder();

                            if (s_constants.Contains(word) || !s_functions.ContainsKey(word))
                            {
                                expression = expression.Insert(i, "*");
                                i++;
                            }
                        }
                        isReadingANumber = false;
                        isReadingAWord = false;
                    }
                    else
                    {
                        string word = sb.ToString();
                        sb = new StringBuilder();
                        if (s_functions.ContainsKey(word))
                        {
                            throw new FormatException("Use " + word + " without ()");
                        }
                        isReadingAWord = false;
                        isReadingANumber = false;
                    }
                }
            }

            if (isReadingAWord)
            {
                string word = sb.ToString();
                if (s_functions.ContainsKey(word))
                {
                    throw new FormatException("Use " + word + " without ()");
                }
            }
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
                        else if (parenthesisCount == 0 && expression[j] == '!')
                        {
                            j--;
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

        private string ManageFactorialExpression(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '!')
                {
                    int parenthesisCount = 0;
                    if (i == expression.Length - 1)
                    {
                        expression += ')';
                        parenthesisCount--;
                    }
                    else if (expression[i + 1] != ')')
                    {
                        expression = expression.Insert(i + 1, ")");
                        parenthesisCount--;
                    }
                    else
                    {
                        continue;
                    }
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
                        else if (parenthesisCount == -1 && !char.IsDigit(expression[j]) && !char.IsLetter(expression[j]) && expression[j] != '.' && expression[j] != '_')
                        {
                            break;
                        }
                        j--;
                    }
                    if (parenthesisCount == 0)
                    {
                        expression = '(' + expression;
                        i++;
                    }
                    else if (parenthesisCount == -1)
                    {
                        expression = expression.Insert(j + 1, "(");
                        i++;
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
                _evaluable = new ValueOperator(d);
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
                _evaluable = TryFindFunctionOrVariable(expression);
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

        private void CheckFunctionArgsCount(string func, int argsCount)
        {
            if (argsCount < s_functions[func].MinArg)
            {
                throw new ArgumentException("Too few arguments in " + func + "()");
            }
            else if (argsCount > s_functions[func].MaxArg)
            {
                throw new ArgumentException("Too many arguments in " + func + "()");
            }
        }

        private IEvaluable TryFindFunctionOrVariable(string expression)
        {
            IEvaluable evaluable = null;
            if (s_constants.Contains(expression))
            {
                switch (expression)
                {
                    case "pi":
                        evaluable = new ValueOperator(Funcs.PI);
                        break;
                    case "tau":
                        evaluable = new ValueOperator(Funcs.TAU);
                        break;
                    case "pau":
                        evaluable = new ValueOperator(Funcs.PAU);
                        break;
                    case "e":
                        evaluable = new ValueOperator(Funcs.E);
                        break;
                    case "phi":
                        evaluable = new ValueOperator(Funcs.PHI);
                        break;
                    default:
                        throw new NotImplementedException("Constant " + expression + " not implemented");
                }
            }
            else
            {
                int indexParenthesis = expression.IndexOf('(');
                if (indexParenthesis == -1)
                {
                    evaluable = new VariableOperator(expression);
                }
                else
                {
                    string func = expression.Substring(0, indexParenthesis);
                    if (s_functions.ContainsKey(func))
                    {
                        string funcContent = GetParenthesisContent(expression, indexParenthesis + 1);
                        string[] argsSplitter = GetParenthesisContentSplitted(funcContent);

                        if (func == AbsoluteOperator.Acronym)
                        {
                            evaluable = AbsoluteOperator.Create(argsSplitter);
                        }
                        else if (func == ExponentialOperator.Acronym)
                        {
                            evaluable = ExponentialOperator.Create(argsSplitter);
                        }
                        else if (func == LogarithmOperator.Acronym)
                        {
                            evaluable = LogarithmOperator.Create(argsSplitter);
                        }
                        else if (func == NaperianLogarithmOperator.Acronym)
                        {
                            evaluable = NaperianLogarithmOperator.Create(argsSplitter);
                        }
                        else if (func == SqrtOperator.Acronym)
                        {
                            evaluable = SqrtOperator.Create(argsSplitter);
                        }
                        else if (func == CosineOperator.Acronym)
                        {
                            evaluable = CosineOperator.Create(argsSplitter);
                        }
                        else if (func == SineOperator.Acronym)
                        {
                            evaluable = SineOperator.Create(argsSplitter);
                        }
                        else if (func == TangentOperator.Acronym)
                        {
                            evaluable = TangentOperator.Create(argsSplitter);
                        }
                        else if (func == ArccosineOperator.Acronym)
                        {
                            evaluable = ArccosineOperator.Create(argsSplitter);
                        }
                        else if (func == ArcsineOperator.Acronym)
                        {
                            evaluable = ArcsineOperator.Create(argsSplitter);
                        }
                        else if (func == ArctangentOperator.Acronym)
                        {
                            evaluable = ArctangentOperator.Create(argsSplitter);
                        }
                        else if (func == SecantOperator.Acronym)
                        {
                            evaluable = SecantOperator.Create(argsSplitter);
                        }
                        else if (func == CosecantOperator.Acronym)
                        {
                            evaluable = CosecantOperator.Create(argsSplitter);
                        }
                        else if (func == CotangentOperator.Acronym)
                        {
                            evaluable = CotangentOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicCosineOperator.Acronym)
                        {
                            evaluable = HyperbolicCosineOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicSineOperator.Acronym)
                        {
                            evaluable = HyperbolicSineOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicTangentOperator.Acronym)
                        {
                            evaluable = HyperbolicTangentOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicSecantOperator.Acronym)
                        {
                            evaluable = HyperbolicSecantOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicCosecantOperator.Acronym)
                        {
                            evaluable = HyperbolicCosecantOperator.Create(argsSplitter);
                        }
                        else if (func == HyperbolicCotangentOperator.Acronym)
                        {
                            evaluable = HyperbolicCotangentOperator.Create(argsSplitter);
                        }
                        else if (func == DegreeOperator.Acronym)
                        {
                            evaluable = DegreeOperator.Create(argsSplitter);
                        }
                        else if (func == RadianOperator.Acronym)
                        {
                            evaluable = RadianOperator.Create(argsSplitter);
                        }
                        else if (func == DecimalOperator.Acronym)
                        {
                            evaluable = DecimalOperator.Create(argsSplitter);
                        }
                        else if (func == CeilOperator.Acronym)
                        {
                            evaluable = CeilOperator.Create(argsSplitter);
                        }
                        else if (func == FloorOperator.Acronym)
                        {
                            evaluable = FloorOperator.Create(argsSplitter);
                        }
                        else if (func == RoundOperator.Acronym)
                        {
                            evaluable = RoundOperator.Create(argsSplitter);
                        }
                        else if (func == BinomialCoefficientOperator.Acronym)
                        {
                            evaluable = BinomialCoefficientOperator.Create(argsSplitter);
                        }
                        else
                        {
                            throw new NotImplementedException("Function " + expression + " not implemented");
                        }
                    }
                }
            }
            return evaluable;
        }

        public double Evaluate(params Variable[] variables)
        {
            if (_evaluable == null)
            {
                throw new InvalidOperationException("Expression not set.");
            }
            // verifier que les variables donnees n'ont pas des noms réservés
            return _evaluable.Evaluate(variables);
        }

        public bool DependsOnVariables(out List<string> variables)
        {
            if (_evaluable == null)
            {
                throw new InvalidOperationException("Expression not set.");
            }
            return _evaluable.DependsOnVariables(out variables);
        }
    }
}
