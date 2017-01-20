using System;
using System.Collections.Generic;

namespace ExtendedFizzBuzz
{
    class Program
    {
        /*
         *  This Program extends the classic "FizzBuzz program.
         *  It allows the user to specify how high the program will count, and customize the replacements.
         *  As such, multiples of 3 may not be replaced by "Fizz", and multiples of 5 may not be replaced with "Buzz", etc.
         */ 

        private static int number;
        private static bool repeat = false;
        private static bool negative = false;
        private static SortedDictionary<int, string> dict = new SortedDictionary<int, string>();

        public static void Main(string[] args)
        {
            Console.WriteLine("This program extends the classic \"FizzBuzz\" program, but is coded significantly different.");
            Console.WriteLine("If you are not familiar with the classic \"FizzBuzz\" program, please familiarize yourself before continuing.");
            Console.WriteLine();
            Console.WriteLine("First, enter the upper bound for printed numbers.");
            Console.WriteLine("Ex: Entering \"1000\" will have the program print the numbers 1 - 1000.");
            Console.WriteLine();
            Console.WriteLine("Note: You may enter a negative number. If you do, the program will count down from -1.");
            Console.WriteLine();
            Console.WriteLine();

            do
            {
                SetNumber();

                Console.WriteLine();
                Console.WriteLine();

                CompileDictionary();

                Console.WriteLine();
                Console.WriteLine();

                DictionaryFizzBuzz();

                Console.WriteLine();
                Console.WriteLine("Program Complete");
                Console.WriteLine();
                Console.WriteLine();

                negative = false;
                dict.Clear();

                Console.WriteLine("You may now enter a new upper bound for printed numbers, starting the program over.");
                Console.WriteLine();
                Console.WriteLine();
            } while (true);
        }

        private static void SetNumber()
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    number = int.Parse(Console.ReadLine());

                    if (number == 0)
                    {
                        throw new OverflowException();
                    }
                    else if (number < 0)
                    {
                        number = number * -1;
                        negative = true;
                    }

                    done = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Unable to parse to an integer");
                    Console.WriteLine("You must enter a valid integer without any punctuation.");
                    Console.WriteLine("Please try again.");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a nonzero number between -2147483647 and +2147483647.");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        private static void CompileDictionary()
        {
            Console.WriteLine("Now please enter the replacement values and strings, separated by an equals sign.");
            if (!repeat)
            {
                Console.WriteLine("Ex: If you wanted to replace multiples of 7 with the word \"Ting\", enter \"7 = Ting\".");
                repeat = true;
                Console.WriteLine();
                Console.WriteLine("Note: All true replacements will execute for a given number.");
                Console.WriteLine("Ex: Entering \"3 = Fizz\",  \"7 = Ting\", and \"21 = FizzTing\" will result in 21 being replaced by \"FizzTingFizzTing\".");
            }
            Console.WriteLine("After each entry, click Enter. When finished, type \"Done\" and click Enter.");
            Console.WriteLine();
            Console.WriteLine();

            bool done = false;
            do
            {
                string input = Console.ReadLine();

                if (input == "Done")
                {
                    done = true;
                }
                else
                {
                    try
                    {
                        int equalsSignPosition = input.IndexOf("=");
                        string tempNumber = input.Substring(0, equalsSignPosition);
                        tempNumber = tempNumber.Trim();

                        string word = input.Substring(equalsSignPosition + 1, input.Length - equalsSignPosition - 1);
                        word = word.Trim();

                        int key = int.Parse(tempNumber);
                        if(key == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        if (key > number)
                        {
                            throw new OverflowException();
                        }

                        dict.Add(key, word);
                    }
                    catch(ArgumentOutOfRangeException) //This applies to the input.Substring method, and will be triggered if the user enters a string that doesn't contain an equals siggn
                    {
                        Console.WriteLine("Unable to locate an equals sign in the entered string");
                        Console.WriteLine("Please try again.");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch (FormatException) //This applies to the int.Parse() method
                    {
                        Console.WriteLine("Unable to parse entry to the left of the equals sign to an integer");
                        Console.WriteLine("You must enter a valid integer without any punctuation.");
                        Console.WriteLine("Please try again.");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch (OverflowException) //This applies to the int.Parse() method
                    {
                        Console.WriteLine("You may not enter an integer greater than the maximum counting value entered above ({1}).", number);
                        Console.WriteLine("Please try again.");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch(DivideByZeroException) //This is manually thrown if the customer tries to enter a key of 0
                    {
                        Console.WriteLine("You may not enter a replacement value for 0 because it will trigger a DivideByZeroException later");
                        Console.WriteLine("Please try again.");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch (ArgumentException) //This applies to the dict.Add() method
                    {
                        Console.WriteLine("You have already entered a replacement for this number. You may not enter a 2nd replacement.");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            } while (!done);
        }

        private static void DictionaryFizzBuzz()
        {
            int i = 1;
            while (i <= number)
            {
                string output = "";
                foreach (int key in dict.Keys)
                {
                    if (i % key == 0)
                    {
                        output = output + dict[key];
                    }
                }

                if (output == "")
                {
                    if (negative)
                    {
                        output = (i * -1).ToString();
                    }
                    else
                    {
                        output = i.ToString();
                    }
                }

                Console.WriteLine(output);

                i++;
            }
        }
    }
}
