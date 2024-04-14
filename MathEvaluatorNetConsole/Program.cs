﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MathEvaluatorNetFramework;

namespace MathEvaluatorNetFrameworkConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKey consoleKey;
            do
            {
                Console.Clear();
                DisplayMenu();

                consoleKey = Console.ReadKey(true).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.NumPad1:
                        TestMathInterpreter();
                        break;

                    case ConsoleKey.NumPad2:
                        Settings();
                        break;

                    case ConsoleKey.NumPad3:
                        PermanentVariables();
                        break;
                }

            } while (consoleKey != ConsoleKey.Escape);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("--- Math interpreter console menu ---\n");
            Console.WriteLine($"1. Write and evaluate an expression");
            Console.WriteLine($"2. Settings");
            Console.WriteLine($"3. Permanent variables");
            Console.WriteLine($"\nPress a key to select an option");
            Console.WriteLine($"\nPress ESCAPE to exit");
            Console.WriteLine();
        }

        #region CHOICE 1 : Write and evaluate an expression

        private static void TestMathInterpreter()
        {
            Expression expression = null;

            Console.WriteLine("--- Math evalution ---\n");
            Console.Write("Expression: ");
            string input = Console.ReadLine();
            Console.WriteLine();

            try
            {
                expression = MathEvaluator.Expression(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }

            if (expression != null)
            {
                Variable[] vars = new Variable[0];

                if (expression.DependsOnVariables(out List<string> variables))
                {
                    vars = SetVariables(variables);
                }

                try
                {
                    double result = expression.Evaluate(vars);
                    DisplayTestResult(expression, result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                }
            }

            Console.ReadKey(true);
        }

        private static Variable[] SetVariables(List<string> variables)
        {
            Console.WriteLine("The expression depends on: " + string.Join(", ", variables));

            List<string> variablesToSet = new List<string>(variables.Count);
            foreach (string name in variables)
            {
                if (MathEvaluator.VariableManager.Contains(name))
                {
                    Console.WriteLine($"{name}: {MathEvaluator.VariableManager.Get(name)}");
                }
                else
                {
                    variablesToSet.Add(name);
                }
            }

            Variable[] vars = new Variable[variablesToSet.Count];
            if (vars.Length > 0)
            {
                for (int i = 0; i < vars.Length; i++)
                {
                    Console.WriteLine();
                    double value = InputNumber(variablesToSet[i] + ": ");
                    vars[i] = new Variable(variablesToSet[i], value);
                }
            }

            return vars;
        }

        private static double InputNumber(string message)
        {
            double d;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (double.TryParse(input, out d) || double.TryParse(input.Replace('.', ','), out d))
                {
                    break;
                }
                Console.WriteLine("Wrong entry\n");
            } while (true);

            return d;
        }

        private static void DisplayTestResult(Expression expression, double result)
        {
            string exp = expression.ToString();
            Console.Write($"\n{exp} = ");

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

        #endregion

        #region CHOICE 2 : Settings

        private static void DisplaySettingsMenu()
        {
            Console.WriteLine("--- Settings menu ---\n");
            Console.WriteLine($"0. Set default settings\n");
            Console.WriteLine($"1. Raise divide by zero exception                          " + MathEvaluator.Parameters.RaiseDivideByZeroException);
            Console.WriteLine($"2. Raise domain exception                                  " + MathEvaluator.Parameters.RaiseDomainException);
            Console.WriteLine($"3. Use Gamma function for non natural integer factorial    " + MathEvaluator.Parameters.UseGammaFunctionForNonNaturalIntegerFactorial);
            Console.WriteLine($"4. Angle unit                                              " + (MathEvaluator.Parameters.AngleAreInDegrees ? "Degree" : "Radian"));
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
                        MathEvaluator.Parameters.Reset();
                        break;

                    case ConsoleKey.NumPad1:
                        MathEvaluator.Parameters.RaiseDivideByZeroException = !MathEvaluator.Parameters.RaiseDivideByZeroException;
                        break;

                    case ConsoleKey.NumPad2:
                        MathEvaluator.Parameters.RaiseDomainException = !MathEvaluator.Parameters.RaiseDomainException;
                        break;

                    case ConsoleKey.NumPad3:
                        MathEvaluator.Parameters.UseGammaFunctionForNonNaturalIntegerFactorial = !MathEvaluator.Parameters.UseGammaFunctionForNonNaturalIntegerFactorial;
                        break;

                    case ConsoleKey.NumPad4:
                        MathEvaluator.Parameters.AngleAreInDegrees = !MathEvaluator.Parameters.AngleAreInDegrees;
                        break;
                }

            } while (consoleKey != ConsoleKey.Escape && consoleKey != ConsoleKey.Enter);

        }

        #endregion

        #region CHOICE 3 : Permanent variables

        private static void PermanentVariables()
        {
            ConsoleKey consoleKey;
            do
            {
                Console.Clear();
                DisplayPermanentVariablesMenu();
                consoleKey = Console.ReadKey(true).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.NumPad1:
                        DisplayPermanentVariables();
                        break;

                    case ConsoleKey.NumPad2:
                        CreateVariable();
                        break;

                    case ConsoleKey.NumPad3:
                        UpdateVariable();
                        break;

                    case ConsoleKey.NumPad4:
                        DeleteVariable();
                        break;

                    case ConsoleKey.NumPad5:
                        MathEvaluator.VariableManager.Clear();
                        break;
                }

            } while (consoleKey != ConsoleKey.Escape);
        }

        private static void DisplayPermanentVariablesMenu()
        {
            Console.WriteLine("--- Permanent variables menu ---\n");
            Console.WriteLine($"1. Display permanent variables");
            Console.WriteLine($"2. Create variable");
            Console.WriteLine($"3. Update variable");
            Console.WriteLine($"4. Delete variable");
            Console.WriteLine($"5. Delete all variables");
            Console.WriteLine($"\nPress ENTER/ESCAPE to exit");
            Console.WriteLine();
        }

        private static void DisplayPermanentVariables()
        {
            List<string> variables = MathEvaluator.VariableManager.GetExistingVariables().ToList();

            if (variables.Count == 0)
            {
                Console.WriteLine("No permanent variable registered");
            }
            else
            {
                uint max_name_length = (uint)variables.Max(v => v.Length) + 10;
                Console.WriteLine("Permanent variables:\n");

                WriteLine(new string[2] { "Variable", "Value" }, max_name_length);
                Console.WriteLine();
                foreach (string variable in variables)
                {
                    WriteLine(new string[2] { variable, MathEvaluator.VariableManager.Get(variable).ToString() }, max_name_length);
                }
            }
            Console.WriteLine();
            Console.ReadKey(true);
        }

        private static void WriteLine(string[] cells, uint spaceBetweenCell)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Console.Write(cells[i]);
                if (i < cells.Length - 1)
                {
                    int spaceToWrite = (int)spaceBetweenCell - cells[i].Length;
                    for (int j = 0; j < spaceToWrite; j++)
                    {
                        Console.Write(' ');
                    }
                }
            }
            Console.Write('\n');
        }

        private static string InputName(string message, bool checkName = true)
        {
            string name;
            Console.WriteLine(message);
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();

                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("The name can not be empty\n");
                    name = null;
                    continue;
                }
                else
                {
                    name = name.ToLower();
                    if (checkName)
                    {
                        if (char.IsDigit(name[0]))
                        {
                            Console.WriteLine("The name can not start with a number\n");
                            name = null;
                            continue;
                        }

                        foreach (char c in name)
                        {
                            if (!char.IsLetter(c) && c != '_')
                            {
                                Console.WriteLine($"Invalid character '{c}'");
                                name = null;
                                break;
                            }
                        }

                        if (Expression.ReservedNames.Contains(name))
                        {
                            Console.WriteLine($"{name} is a reserved key word");
                            name = null;
                        }
                    }
                }
                Console.WriteLine();
            } while (name == null);
            return name;
        }

        private static void CreateVariable()
        {
            string name = InputName("Input the name of the variable to create (only letters, digits or _)\n");
            Console.WriteLine();

            if (MathEvaluator.VariableManager.Contains(name))
            {
                Console.WriteLine($"The variable \"{name}\" already exists.");
            }
            else
            {
                Console.WriteLine("Input the value of " + name);
                double value = InputNumber("Value: ");
                if (MathEvaluator.VariableManager.Create(name, value))
                {
                    Console.WriteLine($"{name} created");
                }
                else
                {
                    Console.WriteLine($"{name} not created");
                }
            }
            Console.ReadKey(true);
        }

        private static void UpdateVariable()
        {
            string name = InputName("Input the name of the variable tp update\n");
            Console.WriteLine();

            if (MathEvaluator.VariableManager.Contains(name))
            {
                Console.WriteLine("Input the new value of " + name);
                double value = InputNumber("Value: ");
                if (MathEvaluator.VariableManager.Update(name, value))
                {
                    Console.WriteLine($"{name} updated");
                }
                else
                {
                    Console.WriteLine($"{name} not updated");
                }
            }
            else
            {
                Console.WriteLine($"The variable \"{name}\" not exists.");
            }
            Console.ReadKey(true);
        }

        private static void DeleteVariable()
        {
            string name = InputName("Input the name of the variable to delete\n");
            Console.WriteLine();

            if (MathEvaluator.VariableManager.Contains(name))
            {
                if (MathEvaluator.VariableManager.Delete(name))
                {
                    Console.WriteLine($"{name} deleted");
                }
                else
                {
                    Console.WriteLine($"{name} not deleted");
                }
            }
            else
            {
                Console.WriteLine($"The variable \"{name}\" not exists.");
            }
            Console.ReadKey(true);
        }

        #endregion
    }
}
