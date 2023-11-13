using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace CalcFraction.Pages
{
    public class CalcView : ComponentBase
    {
        public string expression { get; set; }

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

        public double Calculate(string input)
        {
            string output = GetExpression(input);
            double result = Counting(output);
            return result;
        }

        public string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

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
            Stack<double> temp = new Stack<double>();

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
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }

        public string DoubleToNormalFraction()
        {
            var numeric = Calculate(expression);
            //Разбиваем число на целую и дробную часть
            var numericArray = numeric.ToString().Split(new[] { CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator }, StringSplitOptions.None);
            var wholeStr = numericArray[0];
            var fractionStr = "0";
            if (numericArray.Length > 1)
                fractionStr = numericArray[1];

            //Получаем степень десятки, на которую нужно умножить число, чтобы дробь стала целым 
            var power = fractionStr.Length;

            //Получаем целую часть числителя и знаменатель
            long whole = long.Parse(wholeStr) * 10;
            long denominator = 10;
            for (int i = 1; i < power; i++)
            {
                denominator = denominator * 10;
                whole = whole * 10;
            }

            //получаем числитель
            var numerator = long.Parse(fractionStr);
            numerator = numerator + whole;


            //Ищем общий знаменатель и делим на него
            var index = 2;
            while (index < denominator / 2) //Если дошли до половины, то там его нет. Тут вообще можно брать наименьшее из числителя и знаменателя
            {
                if (numerator % index == 0 && denominator % index == 0)
                {
                    numerator = numerator / index;
                    denominator = denominator / index;
                    index = 1; //При i++ будет увеличен до 2х
                }
                index++;
            }

            return $"{numerator}/{denominator}";
        }

        
    }
}
