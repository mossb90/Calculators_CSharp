using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator_v2
{
    /* Console calculator application.  Includes the capability to process long expressions with several
       operators and ( ) and evaluates the expression using proper order of operation rules. 
     */
    class Program
    {
        /* Pseudo code plan 
            - receive expression from user
            - check validity of expression (no letters or symbols other than +, -, *, /, = )
            - expand expression into character array tokens
            - make 2 stacks:  numbers and operators
            - if token == ' ' (blank), skip
            - if token >= '0' and <= '9', 
                -- build a string of digits and convert into integer 
                -- push integer into number stack
            - if '(' push onto operators stack
            - if ')' solve expression between ( ) and push results onto number stack
            - if +, -, *, /, check to see if operator on top of stack is higher precident than current operator,
                -- if true, apply operator on top of stack to top two values on number stack & add result to numbers stack
                -- if false, push token onto operator stack
            - once all tokens have been examined, apply remaining operations to remaining numbers

            Anticipated Methods:
            - HasPrecidence(char op1, char op2)
            - SolveOp(int value1, int value2, char op)
        */

        /*  Challenges Presented & Overcome (YAY!)
            - Programming the HasPrecedence to drive order of operations from left to right
                -- HasPrecedence actually needed to report higher OR equal precedence
            - Managing decimal results that may occur dividing integers

         */

        static void Main(string[] args)
        {

            char[] charTokens = null;                         //array of characters in user inputted expression
            Stack<double> numbers = new Stack<double>();            //stack to manage numbers in expression
            Stack<char> operators = new Stack<char>();        //stack to manage operators in expression
            string calculateAnother;
            string operatorsString = "+-*/x";
            double num1;
            double num2;


            Console.WriteLine("\nWelcome to C# Calculator Chaos! (Full Mathematical Expression Edition)\n");

            // Do-While loop gives users the opportunity complete multiple calculations without having
            // to restart the application
            do
            {

                charTokens = GetExpression();

                if (charTokens == null)
                {
                    Console.WriteLine("Not a valid expression!  Try again\n");
                    charTokens = GetExpression();
                    if (charTokens == null)
                    {
                        Console.WriteLine("Still not a valid input!  Exiting calculator");
                        Environment.Exit(1);
                    }
                }

                string numString = "";
                for (int i = 0; i < charTokens.Length; i++)
                {
                    // manage characters that are digits
                    if (charTokens[i] >= '0' && charTokens[i] <= '9')
                    {
                        numString = numString + charTokens[i];
                        // handles when last character of an integer
                        if ((i < (charTokens.Length - 1)) && !(charTokens[i + 1] >= '0' && charTokens[i + 1] <= '9') ||
                            (i == charTokens.Length - 1))
                        {
                            double num = double.Parse(numString);
                            //Console.WriteLine("int being pushed: " + num);
                            numbers.Push(num);
                            //Console.WriteLine("push successful for :" + num);
                            numString = "";
                        }
                    }
                    // manage characters that are operators
                    else if (operatorsString.Contains(charTokens[i].ToString()))
                    {
                        if ((operators.Count == 0) || (operators.Peek() == '('))
                        {
                            operators.Push(charTokens[i]);
                            //Console.WriteLine("push successful for:" + charTokens[i]);
                        }
                        else if (HasPrecedence(operators.Peek(), charTokens[i]))
                        {
                            num2 = numbers.Pop();
                            num1 = numbers.Pop();
                            numbers.Push(SolveOp(num1, num2, operators.Pop()));
                            //apply operator on stack to top two numbers on numbers stack
                            operators.Push(charTokens[i]);
                        }
                        else
                        {
                            operators.Push(charTokens[i]);
                        }

                    }
                    // manage parenthesis characters in the math expression
                    else if (charTokens[i] == '(')
                    {
                        operators.Push(charTokens[i]);
                    }
                    else if (charTokens[i] == ')')
                    {
                        num2 = numbers.Pop();
                        num1 = numbers.Pop();
                        numbers.Push(SolveOp(num1, num2, operators.Pop()));

                        if (operators.Peek() == '(')
                        {
                            operators.Pop();
                        }
                    }

                }

                while ((operators.Count > 0)) 
                {
                    if (operators.Peek() == '(')
                    {
                        operators.Pop();
                    }
                    else
                    {
                        num2 = numbers.Pop();
                        num1 = numbers.Pop();
                        numbers.Push(SolveOp(num1, num2, operators.Pop()));
                    }
                }

                Console.WriteLine("result = " + numbers.Pop());


                Console.WriteLine();
                Console.Write("Calculate more?  (Type C to continue or any other key to quit): ");
                calculateAnother = Console.ReadLine();
                Console.WriteLine();

            } while ((calculateAnother == "C") || (calculateAnother == "c"));

            Console.WriteLine("Have a great day!");
        }


        // GetNumbers collects two numbers from user console input that will be used for the math operation
        private static char[] GetExpression()
        {
            // set of allowable characters for the expression string
            string allowableCharacters = "0123456789+-*/x=() ";

            Console.Write("Enter math expression you would like solved (may only include integers, +, -, *, /, ) , and ( ): \n\n");
            string expression = Console.ReadLine();
            char[] tokens = expression.ToCharArray();

            foreach (char token in tokens)
            {
                if (!allowableCharacters.Contains(token.ToString()))
                {
                    return null;
                }
            }

            return tokens;
        }

        private static bool HasPrecedence(char op1, char op2)
        {
            if (((op1 == '*') || (op1 == '/') || (op1 == 'x')) && ((op2 == '+') || (op2 == '-')))
                return true;
            else if (((op1 == '*') || (op1 == '/') || (op1 == 'x')) && ((op2 == '*') || (op2 == '/') || (op2 == 'x')))
                return true;
            else if (((op1 == '+') || (op1 == '-')) && ((op2 == '+') || (op2 == '-')))
                return true; 
            else return false;
        }

        private static double SolveOp(double value1, double value2, char op)
        {
            double result = 0;
            switch (op)
            {
                case '+':
                    result = (value1 + value2);
                    break;

                case '-':
                    result = (value1 - value2);
                    break;

                case '*':
                case 'x':
                    result = (value1 * value2);
                    break;

                case '/':
                    result = (value1 / value2);
                    break;
            }
            return result;


        }
    }
}