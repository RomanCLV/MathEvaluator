using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework
{
    public static class Funcs
    {
        public const uint MAX_FACTORIAL_N = 170;
        public const double MAX_GAMMA_X = 170.624376950001;

        private static readonly List<double> s_factorials = new List<double>(171)
        {
            1.0,
            1.0
        };

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

        public static double Gamma(double x)
        { 
            // https://jamesmccaffrey.wordpress.com/2021/12/27/the-gamma-function-implemented-using-csharp/
            return (x > MAX_GAMMA_X) ? double.PositiveInfinity : Math.Exp(LogGamma(x));
        }

        public static double LogGamma(double x)
        {
            // Lanczos approximation g=5, n=7
            double[] coef = new double[7]
            {
                1.000000000190015,
                76.18009172947146,
                -86.50532032941677,
                24.01409824083091,
                -1.231739572450155,
                0.1208650973866179e-2,
                -0.5395239384953e-5
            };

            double LogSqrtTwoPi = 0.91893853320467274178;

            if (x < 0.5) // Gamma(z) = Pi / (Sin(Pi*z))* Gamma(1-z))
            {
                return Math.Log(Math.PI / Math.Sin(Math.PI * x)) - LogGamma(1.0 - x);
            }

            double xx = x - 1.0;
            double b = xx + 5.5; // g + 0.5
            double sum = coef[0];

            for (int i = 1; i < coef.Length; ++i)
            {
                sum += coef[i] / (xx + i);
            }

            return (LogSqrtTwoPi + Math.Log(sum) - b) + (Math.Log(b) * (xx + 0.5));
        }
    }
}
