using MathEvaluatorNetFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MathEvaluatorNetFrameworkUnitTests
{
    [TestClass]
    public class PowerTests
    {
        [TestMethod]
        public void Test_Power_1()
        {
            string expression = "2^3";
            double expected = 8.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_2()
        {
            string expression = "2^0";
            double expected = 1.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_3()
        {
            string expression = "(1+3)^(1+1)";
            double expected = 16.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_4()
        {
            string expression = "2(1+3)^(1+1)";
            double expected = 32.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_5()
        {
            string expression = "(2(1+3))^(1+1)";
            double expected = 64.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_6()
        {
            string expression = "(-2)^3";
            double expected = -8.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_7()
        {
            string expression = "(-2)^4";
            double expected = 16.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_8()
        {
            string expression = "-2^4";
            double expected = -16.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_9()
        {
            string expression = "0-2^4";
            double expected = -16.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_10()
        {
            string expression = "(0-2)^3";
            double expected = -8.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_11()
        {
            string expression = "(1-1)^3";
            double expected = double.PositiveInfinity;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_12()
        {
            string expression = "(2^2)^3";
            double expected = 64.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_13()
        {
            string expression = "2^(2^3)";
            double expected = 256.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_14()
        {
            string expression = "2^2^3";
            double expected = 64.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }
    }
}
