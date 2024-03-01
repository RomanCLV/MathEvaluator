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
            string expression = "2(2+(3.5-1.5))!";
            double expected = 48.0;
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
            string expression = "2((2+(3.5-1.5))!)+5";
            double expected = 53.0;
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
        public void Test_Factorial_11()
        {
            string expression = "2!^3!";
            double expected = 40320.0;
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
        public void Test_Factorial_12()
        {
            string expression = "2!^(3!)";
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
        public void Test_Factorial_13()
        {
            string expression = "(5!)/(5!)";
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
        public void Test_Factorial_14()
        {
            string expression = "5!/5!";
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
        public void Test_Factorial_15()
        {
            string expression = "170!";
            double expected = Funcs.Factorial(170);
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
        public void Test_Factorial_16()
        {
            string expression = "171!";
            double expected = double.PositiveInfinity;
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
        public void Test_Factorial_17()
        {
            string expression = "-0!";
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
        public void Test_Factorial_18()
        {
            string expression = "(-1)!";
            try
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = false;
                _ = MathEvaluator.Evaluate(expression);
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
            finally
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = true;
            }
        }

        [TestMethod]
        public void Test_Factorial_19()
        {
            string expression = "(-1)!";
            try
            {
                _ = MathEvaluator.Evaluate(expression);
                throw new InvalidOperationException("Shoul have raise a DomainException");
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
        public void Test_Factorial_20()
        {
            string expression = "0.5!";
            try
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = false;
                _ = MathEvaluator.Evaluate(expression);
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
            finally
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = true;
            }
        }

        [TestMethod]
        public void Test_Factorial_21()
        {
            string expression = "0.5!";
            double expected = 0.8862269254528;
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
            Assert.AreEqual(expected, result, 0.000001);
        }

        [TestMethod]
        public void Test_Factorial_22()
        {
            string expression = "(-0.9)!";
            try
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = false;
                _ = MathEvaluator.Evaluate(expression);
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
            finally
            {
                MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = true;
            }
        }

        [TestMethod]
        public void Test_Factorial_23()
        {
            string expression = "(-0.9)!";
            double expected = 9.5135076986687;
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
            Assert.AreEqual(expected, result, 0.000001);
        }

        [TestMethod]
        public void Test_Factorial_24()
        {
            string expression = "3!!";
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
            Assert.AreEqual(expected, result, 0.000001);
        }
    }
}
