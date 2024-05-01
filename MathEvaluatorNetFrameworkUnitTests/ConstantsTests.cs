using MathEvaluatorNetFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MathEvaluatorNetFrameworkUnitTests
{
    [TestClass]
    public class ConstantsTests
    {
        private const double EPSILON = 0.000001;

        [TestMethod]
        public void Test_Pi_1()
        {
            string expression = "pi";
            double expected = Funcs.PI;
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
        public void Test_Pi_2()
        {
            string expression = "2*pi";
            double expected = Funcs.TAU;
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
        public void Test_Pi_3()
        {
            string expression = "3*pi/2";
            double expected = Funcs.PAU;
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
        public void Test_Pi_4()
        {
            string expression = "-(pi)";
            double expected = -Funcs.PI;
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
        public void Test_Pi_5()
        {
            string expression = "-pi";
            double expected = -Funcs.PI;
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
        public void Test_Pi_6()
        {
            string expression = "2-pi";
            double expected = 2.0 - Funcs.PI;
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
        public void Test_Pi_7()
        {
            string expression = "-2-pi";
            double expected = -2.0 - Funcs.PI;
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
        public void Test_Pi_8()
        {
            string expression = "-(2-pi)";
            double expected = -(2.0 - Funcs.PI);
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
        public void Test_E_1()
        {
            string expression = "e";
            double expected = Funcs.E;
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
        public void Test_E_2()
        {
            string expression = "e^pi";
            double expected = Math.Exp(Funcs.PI);
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
        public void Test_E_3()
        {
            string expression = "-e^-pi";
            double expected = -Math.Exp(-Funcs.PI);
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
        public void Test_E_4()
        {
            string expression = "-e^pi";
            double expected = -Math.Exp(Funcs.PI);
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
        public void Test_E_5()
        {
            string expression = "-e*-pi";
            double expected = -Funcs.E * -Funcs.PI;
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
        public void Test_Tau_1()
        {
            string expression = "tau";
            double expected = Funcs.TAU;
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
        public void Test_Tau_2()
        {
            string expression = "-tau";
            double expected = -Funcs.TAU;
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
        public void Test_Pau_1()
        {
            string expression = "pau";
            double expected = Funcs.PAU;
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
        public void Test_Pau_2()
        {
            string expression = "-pau";
            double expected = -Funcs.PAU;
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
        public void Test_Variable_1()
        {
            string expression = "-a";
            double a = 5.0;
            double expected = -a;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable("a", a));
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Variable_2()
        {
            string expression = "-_a";
            double a = 5.0;
            double expected = -a;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable("_a", a));
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
