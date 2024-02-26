using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathEvaluatorNetFramework;

namespace MathEvaluatorNetFrameworkUnitTests
{
    [TestClass]
    public class MathEvaluatorNetFrameworkUnitTests
    {
        [TestMethod]
        public void Test_Addition_1()
        {
            string expression = "2 + 10";
            double expected = 12.0;
            double result = 0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Addition_2()
        {
            string expression = "2 + 010";
            double expected = 12.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Addition_3()
        {
            string expression = "2 + 0 + 4+1";
            double expected = 7.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Addition_4()
        {
            string expression = "-8+12.5";
            double expected = 4.5;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Negative_1()
        {
            string expression = "-(5)";
            double expected = -5.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Negative_2()
        {
            string expression = "-(5+3)";
            double expected = -8.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Negative_3()
        {
            string expression = "-(-5+3)";
            double expected = 2.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Negative_4()
        {
            string expression = "-(-5+3)";
            double expected = 2.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Negative_5()
        {
            string expression = "-(-(-5))";
            double expected = -5.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Substraction_1()
        {
            string expression = "8-3";
            double expected = 5.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Substraction_2()
        {
            string expression = "8-10";
            double expected = -2.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Substraction_3()
        {
            string expression = "-8-10";
            double expected = -18.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Multiplication_1()
        {
            string expression = "5*2";
            double expected = 10.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Multiplication_2()
        {
            string expression = "5*2*3.5";
            double expected = 35.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Multiplication_3()
        {
            string expression = "5*-3";
            double expected = -15.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Multiplication_4()
        {
            string expression = "-4*3";
            double expected = -12.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Multiplication_5()
        {
            string expression = "-4*-5";
            double expected = 20.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_1()
        {
            string expression = "6/2";
            double expected = 3.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_2()
        {
            string expression = "(5/2)/.25";
            double expected = 10.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_3()
        {
            string expression = "9.3/-3";
            double expected = -3.1;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_4()
        {
            string expression = "-4/3";
            double expected = -4.0/3.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_5()
        {
            string expression = "-4/-5";
            double expected = 0.8;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_6()
        {
            string expression = "5/(2/.25)";
            double expected = 0.625;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_7()
        {
            string expression = "-5/(2/.25)";
            double expected = -0.625;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_8()
        {
            string expression = "-5/(2/-.25)";
            double expected = 0.625;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_9()
        {
            string expression = "-5/(-2/-.25)";
            double expected = -0.625;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_10()
        {
            string expression = "-5/-(-2/-.25)";
            double expected = 0.625;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Division_11()
        {
            string expression = "8/2/0.25";
            double expected = 16;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_1()
        {
            string expression = "-8*2+10-5";
            double expected = -11.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_2()
        {
            string expression = "(5 + 2)(2(3 / 4) + 3 *.5)";
            double expected = 21.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_3()
        {
            string expression = "-(5 + 2)(2(3 / 4) + 3 *.5)";
            double expected = -21.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
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
            string expression = "-(5 + 1)(-2(-3 / 4) + 3 *.5)";
            double expected = -18.0;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_5()
        {
            string expression = "-(5 + 1)(-2.-(3 / 4) + 3 *.5)";
            double expected = 7.5;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Complexe_6()
        {
            string expression = "-(5 + 1)(-2-(-3 / 4) + 3 *.5)";
            double expected = -1.5;
            double result = 0.0;
            try
            {
                result = MathEvaluator.Evaluate(expression);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetType().Name + ": " + ex.Message);
            }
            Assert.AreEqual(expected, result);
        }
    }
}
