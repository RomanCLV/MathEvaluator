using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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

namespace MathEvaluatorNetFramework
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
            { GammaOperator.Acronym, GammaOperator.Details },

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
            { SignedDecimalOperator.Acronym, SignedDecimalOperator.Details },
            { CeilOperator.Acronym, CeilOperator.Details },
            { FloorOperator.Acronym, FloorOperator.Details },
            { RoundOperator.Acronym, RoundOperator.Details },

            { BinomialCoefficientOperator.Acronym, BinomialCoefficientOperator.Details },
            { SumOperator.Acronym, SumOperator.Details },
            { ProductOperator.Acronym, ProductOperator.Details },
            { IntegralOperator.Acronym, IntegralOperator.Details },
        };

        private static readonly List<string> s_reservedNames = BuildReservedNames();

        private static List<string> BuildReservedNames()
        {
            List<string> reservedNames = new List<string>(s_constants.Count + s_functions.Count);
            reservedNames.AddRange(s_constants);
            reservedNames.AddRange(s_functions.Keys);
            return reservedNames;
        }

        public static IReadOnlyList<string> ReservedNames => s_reservedNames;


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

        public string Name
        {
            get;
        }

        public Expression()
        {
            _evaluable = null;
            Name = string.Empty;
        }

        public Expression(string name) : this()
        {
            Name = name;
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
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Return the current object.</returns>
        public Expression Set(string expression)
        {
            return InternalSet(expression, false);
        }

        /// <summary>
        /// Internal method to set an expression and prepare it before the process.
        /// </summary>
        /// <param name="expression">The expression to prepare.</param>
        /// <param name="isExpressionCleaned">If the current is obtained by another expression that already have been prepared.</param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The current object.</returns>
        private Expression InternalSet(string expression, bool isExpressionCleaned)
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
            return this;
        }

        /// <summary>
        /// Prepare the expression before the process.
        /// </summary>
        /// <exception cref="FormatException"></exception>
        private string PrepareExpression(string expression)
        {
            expression = string.Join("", expression.Trim().ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

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

            char lastChar = expression[expression.Length - 1];
            if (IsCharOperandSymbol(lastChar) || lastChar == ',' || lastChar == '^')
            {
                throw new FormatException("Invalid format exception: Expression can not ends with: " + lastChar);
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

            expression = expression.Replace("(+", "(");

            // remove useless ()
            expression = RemoveUselessOpenCloseParenthesis(expression);

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

        private string RemoveUselessOpenCloseParenthesis(string expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int length = expression.Length;

            for (int i = 0; i < length; i++)
            {
                if (expression[i] == '(')
                {
                    if (i < length - 1 && expression[i + 1] == ')')
                    {
                        // case: ()

                        // if function call, let it. Else Remove it
                        if (i > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            int j = i - 1;
                            while (j >= 0)
                            {
                                if (!char.IsLetter(expression[j]) && expression[j] != '_')
                                {
                                    break;
                                }
                                sb.Insert(0, expression[j]);
                                j--;
                            }
                            string word = sb.ToString();
                            if (s_functions.ContainsKey(word) || MathEvaluator.ExpressionsManager.Contains(word))
                            {
                                stringBuilder.Append(expression[i]);
                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        stringBuilder.Append(expression[i]);
                    }
                }
                else
                {
                    stringBuilder.Append(expression[i]);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Check the count of coma.
        /// </summary>
        /// <exception cref="FormatException"></exception>
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

                            if (s_functions.ContainsKey(word) || MathEvaluator.ExpressionsManager.Contains(word))
                            {
                                string content = GetParenthesisContent(expression, i + 1);
                                sb = new StringBuilder();

                                List<int> indexs = FindSplitIndex(content, ',');
                                CheckFunctionArgsCount(word, indexs.Count + 1);
                                if (indexs.Count > 0)
                                {
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

        /// <summary>
        /// Check the count of dot.
        /// </summary>
        /// <exception cref="FormatException"></exception>
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

        /// <summary>
        /// Add the multiply symbol (*) where it's missing.
        /// </summary>
        /// <exception cref="FormatException"></exception>
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

                            if (s_constants.Contains(word) || (!s_functions.ContainsKey(word) && !MathEvaluator.ExpressionsManager.Contains(word)))
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
                            throw new FormatException("Call function " + word + " without ()");
                        }
                        isReadingAWord = false;
                        isReadingANumber = false;
                    }
                }
            }

            if (isReadingAWord)
            {
                string word = sb.ToString();
                if (s_functions.ContainsKey(word) || MathEvaluator.ExpressionsManager.Contains(word))
                {
                    throw new FormatException("Call function " + word + " without ()");
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
                            if (parenthesisCount == -1)
                            {
                                break;
                            }
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
                        parenthesisCount = 1;
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
                        else if (parenthesisCount == 1 && IsCharOperandSymbol(expression[j]))
                        {
                            //if (expression[j] == '-')
                            //{
                            //    j++;
                            //    while (j < expression.Length && (char.IsDigit(expression[j]) || char.IsLetter(expression[j]) || expression[j] == '_'))
                            //    {
                            //        j++;
                            //    }
                            //}
                            j--;
                            break;
                        }
                        else if (parenthesisCount == 1 && expression[j] == '!')
                        {
                            j--;
                            break;
                        }
                        j++;
                    }
                    if (parenthesisCount == 1)
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
                    if (parenthesisCount >= 0)
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
                                if (prevC == '/' || prevC == '*' || prevC == ',')
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

        /// <summary>
        /// Set the current Expression with a cleaned string expression.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void SetCleanedExpression(string expression)
        {
            if (double.TryParse(expression.Replace('.', ','), out double d) || double.TryParse(expression, out d))
            {
                if (expression[0] == '-' && d == 0.0)
                {
                    _evaluable = new NegativeOperator(new ValueOperator(0.0));
                }
                else
                {
                    _evaluable = new ValueOperator(d);
                }
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
                new NegativeOperator(new Expression().InternalSet(expression.Substring(1), true)) :
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

                Expression expLeft = new Expression().InternalSet(left, true);

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

                Expression expLeft = new Expression().InternalSet(left, true);
                Expression expRight = new Expression().InternalSet(right, true);

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
            if (s_functions.ContainsKey(func))
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
            else
            {
                if (MathEvaluator.ExpressionsManager.Contains(func))
                {
                    if (MathEvaluator.ExpressionsManager.Get(func).DependsOnVariables(out List<string> variables))
                    {
                        if (argsCount < variables.Count)
                        {
                            throw new ArgumentException("Too few arguments in " + func + "()");
                        }
                        else if (argsCount > variables.Count)
                        {
                            throw new ArgumentException("Too many arguments in " + func + "()");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Try to associate a word with a constant or a mathematical builtin functions or a permanent expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>An <see cref="IEvaluable"/> or <see cref="null"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentException"></exception>
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
                    string funcContent = GetParenthesisContent(expression, indexParenthesis + 1);
                    string[] argsSplitted = GetParenthesisContentSplitted(funcContent);
                    if (s_functions.ContainsKey(func))
                    {
                        if (func == AbsoluteOperator.Acronym)
                        {
                            evaluable = AbsoluteOperator.Create(argsSplitted);
                        }
                        else if (func == ExponentialOperator.Acronym)
                        {
                            evaluable = ExponentialOperator.Create(argsSplitted);
                        }
                        else if (func == LogarithmOperator.Acronym)
                        {
                            evaluable = LogarithmOperator.Create(argsSplitted);
                        }
                        else if (func == NaperianLogarithmOperator.Acronym)
                        {
                            evaluable = NaperianLogarithmOperator.Create(argsSplitted);
                        }
                        else if (func == SqrtOperator.Acronym)
                        {
                            evaluable = SqrtOperator.Create(argsSplitted);
                        }
                        else if (func == GammaOperator.Acronym)
                        {
                            evaluable = GammaOperator.Create(argsSplitted);
                        }
                        else if (func == CosineOperator.Acronym)
                        {
                            evaluable = CosineOperator.Create(argsSplitted);
                        }
                        else if (func == SineOperator.Acronym)
                        {
                            evaluable = SineOperator.Create(argsSplitted);
                        }
                        else if (func == TangentOperator.Acronym)
                        {
                            evaluable = TangentOperator.Create(argsSplitted);
                        }
                        else if (func == ArccosineOperator.Acronym)
                        {
                            evaluable = ArccosineOperator.Create(argsSplitted);
                        }
                        else if (func == ArcsineOperator.Acronym)
                        {
                            evaluable = ArcsineOperator.Create(argsSplitted);
                        }
                        else if (func == ArctangentOperator.Acronym)
                        {
                            evaluable = ArctangentOperator.Create(argsSplitted);
                        }
                        else if (func == SecantOperator.Acronym)
                        {
                            evaluable = SecantOperator.Create(argsSplitted);
                        }
                        else if (func == CosecantOperator.Acronym)
                        {
                            evaluable = CosecantOperator.Create(argsSplitted);
                        }
                        else if (func == CotangentOperator.Acronym)
                        {
                            evaluable = CotangentOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicCosineOperator.Acronym)
                        {
                            evaluable = HyperbolicCosineOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicSineOperator.Acronym)
                        {
                            evaluable = HyperbolicSineOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicTangentOperator.Acronym)
                        {
                            evaluable = HyperbolicTangentOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicSecantOperator.Acronym)
                        {
                            evaluable = HyperbolicSecantOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicCosecantOperator.Acronym)
                        {
                            evaluable = HyperbolicCosecantOperator.Create(argsSplitted);
                        }
                        else if (func == HyperbolicCotangentOperator.Acronym)
                        {
                            evaluable = HyperbolicCotangentOperator.Create(argsSplitted);
                        }
                        else if (func == DegreeOperator.Acronym)
                        {
                            evaluable = DegreeOperator.Create(argsSplitted);
                        }
                        else if (func == RadianOperator.Acronym)
                        {
                            evaluable = RadianOperator.Create(argsSplitted);
                        }
                        else if (func == DecimalOperator.Acronym)
                        {
                            evaluable = DecimalOperator.Create(argsSplitted);
                        }
                        else if (func == CeilOperator.Acronym)
                        {
                            evaluable = CeilOperator.Create(argsSplitted);
                        }
                        else if (func == FloorOperator.Acronym)
                        {
                            evaluable = FloorOperator.Create(argsSplitted);
                        }
                        else if (func == RoundOperator.Acronym)
                        {
                            evaluable = RoundOperator.Create(argsSplitted);
                        }
                        else if (func == BinomialCoefficientOperator.Acronym)
                        {
                            evaluable = BinomialCoefficientOperator.Create(argsSplitted);
                        }
                        else if (func == SumOperator.Acronym)
                        {
                            evaluable = SumOperator.Create(argsSplitted);
                        }
                        else if (func == ProductOperator.Acronym)
                        {
                            evaluable = ProductOperator.Create(argsSplitted);
                        }
                        else if (func == IntegralOperator.Acronym)
                        {
                            evaluable = IntegralOperator.Create(argsSplitted);
                        }
                        else
                        {
                            throw new NotImplementedException("Function " + expression + " not implemented");
                        }
                    }
                    else
                    {
                        if (MathEvaluator.ExpressionsManager.Contains(func))
                        {
                            Expression exp = MathEvaluator.ExpressionsManager.Get(func);
                            Expression[] parameters = new Expression[0];
                            if (exp.DependsOnVariables(out List<string> variableNames))
                            {
                                if (argsSplitted.Length != variableNames.Count)
                                {
                                    string errorMessage =
                                        $"Expression {func} required {variableNames.Count} parameters ({string.Join(", ", variableNames.ToArray())}). " +
                                        $"{argsSplitted.Length} parameter(s) given: {funcContent}";
                                    throw new ArgumentException(errorMessage);
                                }
                                parameters = argsSplitted.Select(arg => new Expression().InternalSet(arg, true)).ToArray();
                            }
                            evaluable = new UnknowFunctionOperator(exp, parameters);
                        }
                    }
                }
            }
            return evaluable;
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <param name="variables">The variables used in the expression.</param>
        /// <returns>The result of the given expression.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exceptions.NotDefinedException"></exception>
        /// <exception cref="Exceptions.DomainException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        public double Evaluate(params Variable[] variables)
        {
            if (_evaluable == null)
            {
                throw new InvalidOperationException("Expression not set.");
            }
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

        /// <summary>
        /// Build the name of the expressions depending on its variables.<br />
        /// If the expression is named f and depends on variables x and y, the default result is f(x, y).<br />
        /// </summary>
        /// <param name="separator">Separator to use between each variable.</param>
        /// <returns>The expression's name depending variables.</returns>
        public string GetNameWithVariables(string separator = ", ")
        {
            string result = Name;
            if (DependsOnVariables(out List<string> variables))
            {
                result += '(' + string.Join(separator, variables.ToArray()) + ')';
            }
            return result;
        }

        internal bool Is(Type t)
        {
            return _evaluable.GetType() == t;
        }

        internal bool Is(params Type[] types)
        {
            return types.Any(t => Is(t));
        }

        public override string ToString()
        {
            return _evaluable.ToString();
        }
    }
}
