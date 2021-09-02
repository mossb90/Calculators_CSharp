using System;
using System.Collections;

namespace Calculator1
{
    /* Console calculator application.  Processes two doubles and includes options to add, subtract
     * multiply or divide.  Allows a second chance when non-numbers are inputted. 
     */
    class Program
    {
        static void Main(string[] args)
        {
         
            Double[] num = new Double[2];
            string operation;
            string calculateAnother;

            Console.WriteLine("Welcome to C# Calculator Chaos! \n");

            // Do-While loop gives users the opportunity complete multiple calculations without having
            // to restart the application
            do
            {
                try
                {
                    GetNumbers(num);
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a valid input!  Try again\n");
                    try
                    {
                        GetNumbers(num);
                    }
                    catch (Exception)
                    {
                       Console.WriteLine("Still not a valid input!  Exiting calculator");
                       Environment.Exit(1);
                    }
                }

                Console.WriteLine("\nPlease select the type of calculation you wish to complete");
                Console.WriteLine("Input one of the following  +  -  x  /  ");
                operation = Console.ReadLine();
                Console.WriteLine();

                // Runs appropriate method depending on math operation selected
                switch(operation)
                {
                    case "+":
                        AddNumbers(num);
                        break;

                    case "-":
                        SubtractNumbers(num);
                        break;

                    case "*":
                        MultiplyNumbers(num);
                        break;

                    case "x":
                        MultiplyNumbers(num);
                        break;

                    case "/":
                        DivideNumbers(num);
                        break;

                    default:
                        Console.WriteLine("Invalid mathematical operator entered.");
                        break;
                }
            

                Console.WriteLine();
                Console.Write("Calculate more?  (Type C to continue or any other key to quit): ");
                calculateAnother = Console.ReadLine();
                Console.WriteLine();

            } while ((calculateAnother == "C") || (calculateAnother == "c"));

             Console.WriteLine("Have a great day!");
        }

        
        // GetNumbers collects two numbers from user console input that will be used for the math operation
        private static Double[] GetNumbers(Double[ ] num)
        { 
            Console.Write("Enter first number: ");
            num[0] = Double.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            num[1] = Double.Parse(Console.ReadLine());
           
            return num;
        }
        // AddNumbers performs the add operation on two numbers stored in addend and sends results to console
        private static void AddNumbers(Double[ ] addend)
        {
            double result = 0;
            result = addend[0] + addend[1];
            Console.WriteLine($"Result: {addend[0]} + {addend[1]} = {result}");
        }

        // SubtractNumbers performs the subtraction operation on two numbers stored in num and sends results to console
        private static void SubtractNumbers(Double[] num)
        {
            double result = 0;
            result = num[0] - num[1];
            Console.WriteLine($"Result: {num[0]} - {num[1]} = {result}");
        }

        // MultiplyNumbers performs the multiplication operation on two numbers stored in factor and sends results to console
        private static void MultiplyNumbers(Double[] factor)
        {
            double result = 0;
            result = factor[0] * factor[1];
            Console.WriteLine($"Result: {factor[0]} x {factor[1]} = {result}");
        }


        // Divide Numbers performs the division operation on two numbers stored in num and sends results to console
        private static void DivideNumbers(Double[] num)
        {
            double result = 0;
            if (num[1] == 0){
                Console.WriteLine("You cannot divide by zero! Exiting calculator now.\n");
                Environment.Exit(1);
                
            } else {
                result = num[0] / num[1];
                Console.WriteLine($"Result: {num[0]} / {num[1]} = {result}");
            }
            
        }

    }
}
