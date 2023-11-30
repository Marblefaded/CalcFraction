using Microsoft.AspNetCore.Components;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace CalcFraction.Pages
{
    public class CalcView : ComponentBase
    {
        public string expression { get; set; }
        public string fractions { get; set; }
        public string answer { get; set; }
        public List<string> iterations { get; set; } = new List<string>();
        public bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
        public bool IsOperator(char с)
        {
            if (("+-/*^()".IndexOf(с) != -1))
                return true;
            return false;
        }
        public byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;
            }
        }
        public double Sqrt(double number)
        {
            return Math.Sqrt(number);
        }

        public string Calculate(string input)
        {
            if (Regex.IsMatch(input, @"[^0-9\,\+\-\*\/\(\)\^\s]"))
            {
                return answer = "You have entered a string containing letters or invalid characters";
            }
            else
            {
                iterations.Clear();
                string output = GetExpression(input);
                double result = Counting(output);
                answer = result.ToString();
                return answer;
            }
        }

        public string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }
                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output;
        }
        public double Counting(string input)
        {
            double result = 0;
            Stack<double> temp = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a));
                    i--;
                }
                else if (IsOperator(input[i]))
                {
                    if (input[i] != '^')
                    {

                        if (input[i] != '-')
                        {
                            double a = temp.Pop();
                            if (temp.Count == 0)
                            {
                                result = 0 - a;
                            }
                            else
                            {
                                double b = temp.Pop();
                                switch (input[i])
                                {
                                    case '+':
   
                                        result = b + a;
                                        iterations.Add($"{b} + {a} = {DoubleToNormalFraction(result)}");
                                        break;
                                    case '*':
                                        result = b * a;
                                        iterations.Add($"{b} * {a} = {DoubleToNormalFraction(result)}");
                                        break;
                                    case '/':
                                        result = b / a;
                                        iterations.Add($"{b} / {a} = {DoubleToNormalFraction(result)}");
                                        break;
                                }
                            }
                            
                        }
                        else
                        {
                            double a = temp.Pop();
                            if (temp.Count == 0)
                            {
                                switch (input[i])
                                {
                                    case '-':
                                        result = 0 - a;
                                        iterations.Add($"-{a} = {DoubleToNormalFraction(result)}");
                                        break;
                                }
                            }
                            else
                            {
                                double b = temp.Pop();
                                switch (input[i])
                                {
                                    case '-':
                                        result = b - a;
                                        iterations.Add($"{b} - {a} = {DoubleToNormalFraction(result)}");
                                        break;
                                }
                            }

                        }
                    }
                    else
                    {
                        double b = temp.Pop();
                        switch (input[i])
                        {
                            case '^':
                                result = Math.Sqrt(b);
                                iterations.Add($"sqrt({b})");
                                break;
                        }

                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }

        public string DoubleToNormalFraction(double numeric)
        {
            numeric = Math.Round(numeric, 5);
            var numericArray = numeric.ToString().Split(new[] { CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator }, StringSplitOptions.None);

            var wholeStr = numericArray[0];
            var fractionStr = "0";

            if (numericArray.Length > 1)
            {
                fractionStr = numericArray[1];
            }

            var wholeNumber = long.Parse(wholeStr);
            var numerator = long.Parse(fractionStr);
            var denominator = Math.Pow(10, fractionStr.Length);

            var denominatorLong = (long)denominator;

            var gcd = GCD(numerator, denominatorLong);

            numerator /= gcd;
            denominatorLong /= gcd;

            if (numerator == 0)
            {
                if (numeric >= 0)
                {
                    return fractions = $"{wholeNumber}";
                }
                else
                {
                    return fractions = $"{wholeNumber}";
                }
            }
            else if (wholeNumber == 0)
            {
                if (numeric >= 0)
                {
                    return fractions = $"{numerator}/{denominatorLong}";
                }
                else
                {
                    return fractions = $"-{numerator}/{denominatorLong}";
                }
            }
            else
            {
                if (numeric >= 0)
                {
                    return fractions = $"{wholeNumber}({numerator}/{denominatorLong})";
                }
                else
                {
                    return fractions = $"{wholeNumber}({numerator}/{denominatorLong})";
                }
            }
        }

        private static long GCD(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }




    }
}