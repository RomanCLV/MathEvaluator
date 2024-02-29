using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework;

namespace MathEvaluatorNetFrameworkConsole
{
    internal class Program
    {
        private const uint EXIT_CODE = 0;
        private const uint EVAL_CODE = 1;

        public static void Main(string[] args)
        {
            uint option;
            do
            {
                Console.Clear();
                DisplayMenu();
                option = SelectOption(EXIT_CODE, 1);
                Execute(option);

                if (option != EXIT_CODE)
                {
                    Console.ReadKey(true);
                }
            } while (option != EXIT_CODE);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("--- Math interpreter console menu ---\n");
            Console.WriteLine($"{EVAL_CODE}. Eval expression");
            Console.WriteLine($"{EXIT_CODE}. Exit");
            Console.WriteLine();
        }

        private static uint SelectOption(uint min, uint max)
        {
            string input;
            uint d;
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                if (!uint.TryParse(input, out d) || d < min || d > max)
                {
                    Console.WriteLine($"\nWrong entry. Please select option {min} to {max}.");
                    input = null;
                }
            } while (input == null);
            Console.WriteLine();
            return d;
        }

        private static void Execute(uint option)
        {
            switch (option)
            {
                case EVAL_CODE:
                    TestMathInterpreter();
                    break;
            }
        }

        private static void TestMathInterpreter()
        {
            Console.WriteLine("--- Math evalution ---\n");
            Console.Write("Expression: ");
            string expression = Console.ReadLine();
            Console.WriteLine();
            double result;
            try
            {
                result = MathEvaluator.Evaluate(expression);
                Console.Write($"Result: ");

                if (double.IsPositiveInfinity(result))
                {
                    Console.WriteLine("+infinity");
                }
                else if (double.IsNegativeInfinity(result))
                {
                    Console.WriteLine("-infinity");
                }
                else if (double.IsNaN(result))
                {
                    Console.WriteLine("NaN");
                }
                else
                {
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}

