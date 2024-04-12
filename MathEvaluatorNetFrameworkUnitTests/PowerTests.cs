using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathEvaluatorNetFramework;
using MathEvaluatorNetFramework.Exceptions;

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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
                Console.WriteLine("Result: " + result);
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
            double expected = 0.0;
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
        public void Test_Power_12()
        {
            string expression = "(2^2)^3";
            double expected = 64.0;
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
        public void Test_Power_13()
        {
            string expression = "2^(2^3)";
            double expected = 256.0;
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
        public void Test_Power_14()
        {
            string expression = "2^2^3";
            double expected = 64.0;
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
        public void Test_Power_15()
        {
            string expression = "-(1+2)^3";
            double expected = -27.0;
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
        public void Test_Power_16()
        {
            string expression = "1-(1+2)^(2+1)";
            double expected = -26.0;
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
        public void Test_Power_17()
        {
            string expression = "-((1+1)^(4))";
            double expected = -16.0;
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
        public void Test_Power_18()
        {
            string expression = "-((2+1-2)^(2-2))";
            double expected = -1.0;
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
        public void Test_Power_19()
        {
            string expression = "(2+1-3)^(2-2)";
            try
            {
                _ = MathEvaluator.Evaluate(expression);
                throw new InvalidOperationException("Shoud have raise a DomainException");
            }
            catch (DomainException domainExpection)
            {
                Console.WriteLine("Result: DomainException: " + domainExpection.InvalidOperation);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
        }

        [TestMethod]
        public void Test_Power_20()
        {
            string expression = "(2+1-3)^(2-2)";
            double expected = double.NaN;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDomainException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDomainException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_21()
        {
            string expression = "2^(4.3/0)";
            double expected = double.PositiveInfinity;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_22()
        {
            string expression = "-2^(4.3/0)";
            double expected = double.NegativeInfinity;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_23()
        {
            string expression = "2^(4.3/0)";
            try
            {
                _ = MathEvaluator.Evaluate(expression);
                throw new InvalidOperationException("Should have raise a DivideByZeroException.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Result: DivideByZeroException");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
        }

        [TestMethod]
        public void Test_Power_24()
        {
            string expression = "0^(1/0)";
            double expected = 0.0;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_25()
        {
            string expression = "-0^(1/0)";
            double expected = 0.0;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_26()
        {
            string expression = "(1/0)^(1/0)";
            double expected = double.PositiveInfinity;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Power_27()
        {
            string expression = "(-1/0)^(1/0)";
            double expected = double.NegativeInfinity;
            double result = 0.0;
            try
            {
                MathEvaluator.Parameters.RaiseDivideByZeroException = false;
                result = MathEvaluator.Evaluate(expression);
                MathEvaluator.Parameters.RaiseDivideByZeroException = true;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }
    }
}
