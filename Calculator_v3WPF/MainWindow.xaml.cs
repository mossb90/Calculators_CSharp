using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_v3WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] charTokens = null;                         //array of characters in user inputted expression
        Stack<double> numbers = new Stack<double>();      //stack to manage numbers in expression
        Stack<char> operators = new Stack<char>();        //stack to manage operators in expression
        string operatorsString = "+-*/x";
        double num1;
        double num2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClick_AppendNum(object sender, RoutedEventArgs e)
        {
            object tag = ((FrameworkElement)sender).Tag;
            EntryBox.Text += (string)tag;
        }

        private void btnClick_AppendOperator(object sender, RoutedEventArgs e)
        {
            object tag = ((FrameworkElement)sender).Tag;
            Expression.Text += EntryBox.Text + (string)tag;
            EntryBox.Text = "";
        }

        private void btnClick_RemoveChar(object sender, RoutedEventArgs e)
        {
            EntryBox.Text = EntryBox.Text.Remove(EntryBox.Text.Length - 1, 1);
        }

        private void btnClick_Equals(object sender, RoutedEventArgs e)
        {
            Expression.Text = Expression.Text + EntryBox.Text;
            EntryBox.Text = "";
            charTokens = GetExpression(Expression.Text.ToString());
            Expression.Text = Expression.Text + "=";
            EntryBox.Text = CalculateResult(charTokens).ToString();
        }

        private void btnClick_AllClear(object sender, RoutedEventArgs e)
        {
            EntryBox.Text = "";
            Expression.Text = "";
        }

        private static char[] GetExpression(string expression)
        {
            // set of allowable characters for the expression string
            string allowableCharacters = "0123456789+-*/x=() ";

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
        private double CalculateResult(char[] charTokens)
        {
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
                        numbers.Push(num);
                        numString = "";
                    }
                }
                // manage characters that are operators
                else if (operatorsString.Contains(charTokens[i].ToString()))
                {
                    if ((operators.Count == 0) || (operators.Peek() == '('))
                    {
                        operators.Push(charTokens[i]);
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
            return numbers.Pop();

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
