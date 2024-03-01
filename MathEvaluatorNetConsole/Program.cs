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
        private const uint SETTINGS_CODE = 2;

        public static void Main(string[] args)
        {
            uint option;
            do
            {
                Console.Clear();
                DisplayMenu();
                option = SelectOption(0, 2);
                Execute(option);
            } while (option != EXIT_CODE);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("--- Math interpreter console menu ---\n");
            Console.WriteLine($"{EVAL_CODE}. Eval expression");
            Console.WriteLine($"{SETTINGS_CODE}. Settings");
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

                case SETTINGS_CODE:
                    Settings();
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
            Console.ReadKey(true);
        }

        private static void DisplaySettingsMenu()
        {
            Console.WriteLine("--- Settings menu ---\n");
            Console.WriteLine($"0. Set default settings\n");
            Console.WriteLine($"1. Raise divide by zero exception                          " + MathEvaluator.RaiseDivideByZeroException);
            Console.WriteLine($"2. Raise domain exception                                  " + MathEvaluator.RaiseDomainException);
            Console.WriteLine($"3. Use Gamma function for non natural integer factorial    " + MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial);
            Console.WriteLine($"\nPress a key to change the setting value");
            Console.WriteLine($"\nPress ENTER/ESCAPE to exit");
            Console.WriteLine();
        }

        private static void Settings()
        {
            ConsoleKey consoleKey;
            do
            {
                Console.Clear();
                DisplaySettingsMenu();
                consoleKey = Console.ReadKey(true).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.NumPad0:
                        MathEvaluator.Reset();
                        break;

                    case ConsoleKey.NumPad1:
                        MathEvaluator.RaiseDivideByZeroException = !MathEvaluator.RaiseDivideByZeroException;
                        break;

                    case ConsoleKey.NumPad2:
                        MathEvaluator.RaiseDomainException = !MathEvaluator.RaiseDomainException;
                        break;

                    case ConsoleKey.NumPad3:
                        MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial = !MathEvaluator.UseGammaFunctionForNonNaturalIntegerFactorial;
                        break;
                }

            } while (consoleKey != ConsoleKey.Escape && consoleKey != ConsoleKey.Enter);
            
        }
    }
}
