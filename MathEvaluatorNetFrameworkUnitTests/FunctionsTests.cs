using MathEvaluatorNetFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFrameworkUnitTests
{
    [TestClass]
    public class FunctionsTests
    {
        private const double EPSILON = 0.000001;

        [TestMethod]
        public void Test_Exponential_1()
        {
            string expression = "exp(1)";
            double expected = Math.E;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Complexe_1()
        {
            string expression = "sqrt(log(5, 6), sqrt(2, 3))";
            double expected = Math.Pow(Math.Log(5, 6), 1.0 / Math.Pow(2, 1.0 / 3.0));
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Complexe_2()
        {
            string expression = "log(1)+sqrt(5,3)";
            double expected = Math.Log10(1) + Math.Pow(5, 1.0 / 3.0);
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Complexe_3()
        {
            string expression = "(1+((3!+2)/2))!^(2cos(3(5!)))";
            double expected = 14400;
            double result = 0.0;

            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_4()
        {
            string expression = "exp(-((x/2)^2+(y/2)^2))";
            Variable x = new Variable("x", 0.5);
            Variable y = new Variable("y", 2.25);
            double expected = Math.Exp(-(Math.Pow((double)x.Value / 2.0, 2) + Math.Pow((double)y.Value / 2.0, 2)));
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable[] { x, y });
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Complexe_5()
        {
            string expression = "x^2+2x+1";
            double expected = 9;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable[] { new Variable("x", 2) });
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }
    }
}
