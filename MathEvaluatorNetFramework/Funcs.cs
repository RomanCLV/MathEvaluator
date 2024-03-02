using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework
{
    public static class Funcs
    {
        public const uint MAX_FACTORIAL_N = 170;
        public const double MAX_GAMMA_X = 170.624376950001;
        public const double MAX_EXP_X = 709.782712893384;

        private const double PI_DIV_180 = Math.PI / 180.0;
        private const double _180_DIV_PI = 180.0 / Math.PI;

        private const int s_logGamma_coefs = 7;
        // Lanczos approximation g=5, n=7
        private static readonly double[] s_logGamma_coef = new double[s_logGamma_coefs]
        {
                1.000000000190015,
                76.18009172947146,
                -86.50532032941677,
                24.01409824083091,
                -1.231739572450155,
                0.1208650973866179e-2,
                -0.5395239384953e-5
        };

        private const double LOG_SQRT_TWO_PI = 0.91893853320467274178;


        private const double IS_MULTIPLE_EPSILON = 0.000000001;

        private static readonly List<double> s_factorials = new List<double>(171)
        {
            1.0,
            1.0
        };

        /// <summary>
        /// Compute the factorial of the given integer, noted n!.
        /// </summary>
        /// <returns>If <paramref name="n"/> is greater than <see cref="MAX_FACTORIAL_N"/>, returns <see cref="double.PositiveInfinity"/>, else returns the factorial of n.</returns>
        public static double Factorial(uint n)
        {

            double result;
            if (n > MAX_FACTORIAL_N)
            {
                result = double.PositiveInfinity;
            }
            else
            {
                if (n >= s_factorials.Count)
                {
                    for (int i = s_factorials.Count; i <= n; i++)
                    {
                        s_factorials.Add(i * s_factorials[i - 1]);
                    }
                }
                result = s_factorials[(int)n];
            }
            return result;
        }

        /// <summary>
        /// Apply the Gamma function to the specified number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>
        /// If <paramref name="x"/> is a real positive number or a negative non-integer, returns Gamma(<paramref name="x"/>). However, if <paramref name="x"/> is greater than <see cref="MAX_GAMMA_X"/> returns <see cref="double.PositiveInfinity"/>.<br />
        /// If <paramref name="x"/> is a negative integer or 0, raises a a <see cref="DomainException"/> depending on <see cref="MathEvaluator.RaiseDomainException"/>, else returns <see cref="double.NaN"/>.
        /// </returns>
        public static double Gamma(double x)
        {
            // https://jamesmccaffrey.wordpress.com/2021/12/27/the-gamma-function-implemented-using-csharp/
            return (x > MAX_GAMMA_X) ? double.PositiveInfinity : Math.Exp(LogGamma(x));
        }

        public static double LogGamma(double x)
        {
            if (x < 0.5) // Gamma(z) = Pi / (Sin(Pi*z))* Gamma(1-z))
            {
                return Math.Log(Math.PI / Math.Sin(Math.PI * x)) - LogGamma(1.0 - x);
            }

            double xx = x - 1.0;
            double b = xx + 5.5; // g + 0.5
            double sum = s_logGamma_coef[0];

            for (int i = 1; i < s_logGamma_coefs; ++i)
            {
                sum += s_logGamma_coef[i] / (xx + i);
            }

            return (LOG_SQRT_TWO_PI + Math.Log(sum) - b) + (Math.Log(b) * (xx + 0.5));
        }

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees">Degrees to convert.</param>
        /// <returns>The value in radians of the given degrees.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * PI_DIV_180;
        }

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        /// <param name="radians">Radians to convert.</param>
        /// <returns>The value in degrees of the given radians.</returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * _180_DIV_PI;
        }

        /// <summary>
        /// Indicates if the given <paramref name="value"/> is a multiple of <paramref name="multiple"/> where the epsilon value is <see cref="IS_MULTIPLE_EPSILON"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="multiple">The multiple.</param>
        /// <returns>True if it exists an integer n that verify <paramref name="value"/> = n * <paramref name="multiple"/> (+/- <see cref="IS_MULTIPLE_EPSILON"/>), else returns false.</returns>
        internal static bool IsMultiple(double value, double multiple)
        {
            int quotient = (int)(value / multiple);
            double rest = value - quotient * multiple;
            return rest > -IS_MULTIPLE_EPSILON && rest < IS_MULTIPLE_EPSILON;
        }

        /// <summary>
        /// Returns the secant (defined as <c>sec(x) = 1 / cos(x)</c>) of the specified angle.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>
        /// If cos(<paramref name="angle"/>)=0, then returns <see cref="double.NaN"/>, else returns the secant of <paramref name="angle"/>.<br />
        /// If <paramref name="angle"/> is equal to <see cref="double.PositiveInfinity"/>, or <see cref="double.NegativeInfinity"/>, or <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.
        /// </returns>
        public static double Sec(double angle)
        {
            double result;
            if (double.IsInfinity(angle) || double.IsNaN(angle))
            {
                result = double.NaN;
            }
            else
            {
                double cos = Math.Cos(angle);
                result = cos == 0.0 ? double.NaN : 1.0 / cos;
            }
            return result;
        }

        /// <summary>
        /// Returns the cosecant (defined as <c>csc(x) = 1 / sin(x)</c>) of the specified angle.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>
        /// If sin(<paramref name="angle"/>)=0, then returns <see cref="double.NaN"/>, else returns the cosecant of <paramref name="angle"/>.<br />
        /// If <paramref name="angle"/> is equal to <see cref="double.PositiveInfinity"/>, or <see cref="double.NegativeInfinity"/>, or <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.
        /// </returns>
        public static double Csc(double angle)
        {
            double result;
            if (double.IsInfinity(angle) || double.IsNaN(angle))
            {
                result = double.NaN;
            }
            else
            {
                double sin = Math.Sin(angle);
                result = sin == 0.0 ? double.NaN : 1.0 / sin;
            }
            return result;
        }

        /// <summary>
        /// Returns the cotangent (defined as <c>cot(x) = 1 / tan(x)</c>) of the specified angle.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>
        /// If tan(<paramref name="angle"/>)=0, then returns <see cref="double.NaN"/>, else returns the cotangent of <paramref name="angle"/>.<br />
        /// If <paramref name="angle"/> is equal to <see cref="double.PositiveInfinity"/>, or <see cref="double.NegativeInfinity"/>, or <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.
        /// </returns>
        public static double Cot(double angle)
        {
            double result;
            if (double.IsInfinity(angle) || double.IsNaN(angle))
            {
                result = double.NaN;
            }
            else
            {
                double tan = Math.Tan(angle);
                result = tan == 0.0 ? double.NaN : 1.0 / tan;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic cosine (defined as <c>cosh(x) = (exp(x) + exp(-x))/2</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic cosine of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>, returns <see cref="double.PositiveInfinity"/>.<br />
        /// If <paramref name="x"/> is greater than <see cref="MAX_EXP_X"/> or lower than -<see cref="MAX_EXP_X"/>, returns <see cref="double.PositiveInfinity"/>.
        /// </returns>
        public static double Cosh(double x)
        {
            double result;
            if (double.IsNaN(x))
            {
                result = double.NaN;
            }
            else if (double.IsInfinity(x) || x > MAX_EXP_X || x < -MAX_EXP_X)
            {
                result = double.PositiveInfinity;
            }
            else
            {
                result = (Math.Exp(x) + Math.Exp(-x)) / 2.0;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic sine (defined as <c>sinh(x) = (exp(x) - exp(-x))/2</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic sine of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or is greater than <see cref="MAX_EXP_X"/>, returns <see cref="double.PositiveInfinity"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.NegativeInfinity"/> or is lower than -<see cref="MAX_EXP_X"/>, returns <see cref="double.NegativeInfinity"/>.<br />
        /// </returns>
        public static double Sinh(double x)
        {
            double result;
            if (double.IsNaN(x))
            {
                result = double.NaN;
            }
            else if (double.IsPositiveInfinity(x) || x > MAX_EXP_X)
            {
                result = double.PositiveInfinity;
            }
            else if (double.IsNegativeInfinity(x) || x < -MAX_EXP_X)
            {
                result = double.NegativeInfinity;
            }
            else
            {
                result = (Math.Exp(x) - Math.Exp(-x)) / 2.0;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic tangent (defined as <c>tanh(x) = sinh(x) / cosh(x)</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic tangent of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or is greater than <see cref="MAX_EXP_X"/>, returns 1.<br />
        /// If <paramref name="x"/> is <see cref="double.NegativeInfinity"/> or is lower than -<see cref="MAX_EXP_X"/>, returns -1.<br />
        /// </returns>
        public static double Tanh(double x)
        {
            double result;
            if (double.IsNaN(x))
            {
                result = double.NaN;
            }
            else if (double.IsPositiveInfinity(x) || x > MAX_EXP_X)
            {
                result = 1.0;
            }
            else if (double.IsNegativeInfinity(x) || x < -MAX_EXP_X)
            {
                result = -1.0;
            }
            else
            {
                double expx = Math.Exp(x);
                double expmx = Math.Exp(-x);
                result = (expx - expmx) / (expx + expmx);
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic secant (defined as <c>sech(x) = 1 / cosh(x)</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic secant of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/>, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>, returns 0.<br />
        /// If <paramref name="x"/> is greater than <see cref="MAX_EXP_X"/> or lower than -<see cref="MAX_EXP_X"/>, returns 0.
        /// </returns>
        public static double Sech(double x)
        {
            double result;
            if (double.IsNaN(x))
            {
                result = double.NaN;
            }
            else if (double.IsInfinity(x) || x > MAX_EXP_X || x < -MAX_EXP_X)
            {
                result = 0.0;
            }
            else
            {
                result = 2.0 / (Math.Exp(x) + Math.Exp(-x));
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic secant (defined as <c>sech(x) = 1 / cosh(x)</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic secant of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/> or equal to 0, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>, returns 0.<br />
        /// If <paramref name="x"/> is greater than <see cref="MAX_EXP_X"/> or lower than -<see cref="MAX_EXP_X"/>, returns 0.
        /// </returns>
        public static double Csch(double x)
        {
            double result;
            if (double.IsNaN(x) || x == 0.0)
            {
                result = double.NaN;
            }
            else if (double.IsInfinity(x) || x > MAX_EXP_X || x < -MAX_EXP_X)
            {
                result = 0.0;
            }
            else
            {
                result = 2.0 / (Math.Exp(x) - Math.Exp(-x));
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic cotangent (defined as <c>coth(x) = cosh(x) / sinh(x)</c>) of the specified numbers.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>
        /// The hyperbolic tangent of the specified numbers.<br />
        /// If <paramref name="x"/> is <see cref="double.NaN"/> or equal to 0, returns <see cref="double.NaN"/>.<br />
        /// If <paramref name="x"/> is <see cref="double.PositiveInfinity"/> or is greater than <see cref="MAX_EXP_X"/>, returns 1.<br />
        /// If <paramref name="x"/> is <see cref="double.NegativeInfinity"/> or is lower than -<see cref="MAX_EXP_X"/>, returns -1.<br />
        /// </returns>
        public static double Coth(double x)
        {
            double result;
            if (double.IsNaN(x) || x == 0.0)
            {
                result = double.NaN;
            }
            else if (double.IsPositiveInfinity(x) || x > MAX_EXP_X)
            {
                result = 1.0;
            }
            else if (double.IsNegativeInfinity(x) || x < -MAX_EXP_X)
            {
                result = -1.0;
            }
            else
            {
                double expx = Math.Exp(x);
                double expmx = Math.Exp(-x);
                result = (expx + expmx) / (expx - expmx);
            }
            return result;
        }

        /// <summary>
        /// Compute the binomial coefficient, written (n k) and pronounced "n choose k".
        /// </summary>
        /// <param name="k">The number of ways you can pick k items.</param>
        /// <param name="n">The number of a set of n items.</param>
        /// <returns>
        /// The binomial coefficient "n choose k".<br />
        /// If <paramref name="k"/> or <paramref name="n"/> is lower than 0, returns <see cref="double.NaN"/>.
        /// </returns>
        public static double BinomialCoefficient(int k, int n)
        {
            double result;
            if (k < 0 || n < 0)
            {
                result = double.NaN;
            }
            else if (k > n)
            {
                result = 0.0;
            }
            else if (k == 0 || k == n)
            {
                result = 1.0;
            }
            else if (k == 1 || k == n - 1)
            {
                result = n;
            }
            else
            {
                result = 1.0;
                for (int i = 1; i <= k; i++)
                {
                    result *= n - (k - i);
                    result /= i;
                }
            }
            return result;
        }
    }
}
