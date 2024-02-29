using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathEvaluatorNetFramework;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFrameworkUnitTests
{
    [TestClass]
    public class FactorialTests
    {
        [TestMethod]
        public void Test_Factorial_1()
        {
            string expression = "0!";
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
        public void Test_Factorial_2()
        {
            string expression = "1!";
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
        public void Test_Factorial_3()
        {
            string expression = "3!";
            double expected = 6.0;
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
        public void Test_Factorial_4()
        {
            string expression = "5!*6";
            double expected = 720.0;
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
        public void Test_Factorial_6()
        {
            string expression = "5!(6)";
            double expected = 720.0;
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
        public void Test_Factorial_7()
        {
            string expression = "5!6";
            double expected = 720.0;
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
        public void Test_Factorial_8()
        {
            string expression = "(5+1)!";
            double expected = 720.0;
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
        public void Test_Factorial_9()
        {
            string expression = "-1!";
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
        public void Test_Factorial_10()
        {
            string expression = "(-1)!";
            try
            {
                MathEvaluator.RaiseDomainException = false;
                _ = MathEvaluator.Evaluate(expression);
                MathEvaluator.RaiseDomainException = true;
                throw new InvalidOperationException("Should have raise a DomainException");
            }
            catch (DomainException)
            {
                Console.WriteLine("Result: DomainException");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
        }

        [TestMethod]
        public void Test_Factorial_11()
        {
            string expression = "(-1)!";
            double expected = double.NaN;
            double result = 0.0;
            try
            {
                result  = MathEvaluator.Evaluate(expression);
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
