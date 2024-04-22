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

        [TestMethod]
        public void Test_Sum_1()
        {
            string expression = "sum(1, _, 1, 10)";
            double expected = 10.0;
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
        public void Test_Sum_2()
        {
            string expression = "sum(n, n, 1, 10)";
            double expected = 55.0;
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
        public void Test_Sum_3()
        {
            string expression = "sum(n^2, n, 1, 10)";
            double expected = 385.0;
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
        public void Test_Sum_4()
        {
            string expression = "sum(n, n, 1, 10, 2)";
            double expected = 25.0;
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
        public void Test_Sum_5()
        {
            string expression = "sum(n, n, 0, 10, 2)";
            double expected = 30.0;
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
        public void Test_Sum_6()
        {
            string expression = "sum(n, n, 0, 10, 2)";
            double expected = 30.0;
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
        public void Test_Sum_7()
        {
            string expression = "sum(n, n, 1, a)";
            double expected = 55.0;
            double result = 0.0;
            Variable a = new Variable("a", 10);
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable[] { a });
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Sum_8()
        {
            string expression = "sum(n, n, 1, n) + n";
            double expected = 20.0;
            double result = 0.0;
            Variable a = new Variable("n", 5);
            try
            {
                result = MathEvaluator.Evaluate(expression, new Variable[] { a });
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result, EPSILON);
        }

        [TestMethod]
        public void Test_Sum_9()
        {
            string expression = "sum(n, n, 1, 2, .1)";
            double expected = 16.5;
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
        public void Test_Product_1()
        {
            string expression = "prod(n, n, 1, 5)";
            double expected = 120.0;
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
        public void Test_Product_2()
        {
            string expression = "prod(n, n, 1, 2, .1)";
            double expected = 67.04425728;
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
        public void Test_Integral_1()
        {
            string expression = "int(1,_,0,5)";
            double expected = 5;
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
        public void Test_Integral_2()
        {
            string expression = "int(x,x,0,2)";
            double expected = 2.0;
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
        public void Test_Integral_3()
        {
            string expression = "int(x^2,x,0,1, 0.001)";
            double expected = 1.0/3.0;
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
        public void Test_Integral_4()
        {
            string expression = "int(e^(-(x^2)),x,-100,100)";
            double expected = Math.Sqrt(Math.PI);
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

        //[TestMethod]
        //public void Test_Integral_5()
        //{
        //    string expression = "int(e^(-x^2),x,-100,100)";
        //    double expected = Math.Sqrt(Math.PI);
        //    double result = 0.0;
        //    try
        //    {
        //        result = MathEvaluator.Evaluate(expression);
        //        Console.WriteLine("Result: " + result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.GetType().Name + ": " + ex.Message);
        //    }
        //    Assert.AreEqual(expected, result, EPSILON);
        //}
    }
}
