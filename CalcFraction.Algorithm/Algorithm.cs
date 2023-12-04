using System.Text.RegularExpressions;

namespace Calculator.Algorithm
{
    public class Algorithm
    {


        public struct Fraction
        {
            public int N;
            public int D;

            public Fraction(int numerator, int denominator)
            {
                N = numerator;
                D = denominator;
            }

            public Fraction(int num) : this(num, 1)
            {
            }

            public override string ToString()
            {
                return string.Format("{0}/{1}", N, D);
            }

            public static Fraction operator +(Fraction f1, Fraction f2)
            {
                return new Fraction(f1.N * f2.D + f2.N * f1.D, f1.D * f2.D).Normalization();
            }

            public static Fraction operator *(Fraction f1, Fraction f2)
            {
                return new Fraction(f1.N * f2.N, f1.D * f2.D).Normalization();
            }

            public static Fraction operator -(Fraction f1, Fraction f2)
            {
                return new Fraction(f1.N * f2.D - f2.N * f1.D, f1.D * f2.D).Normalization();
            }

            public static Fraction operator -(Fraction f1)
            {
                return new Fraction(-f1.N, f1.D).Normalization();
            }

            public static Fraction operator /(Fraction f1, Fraction f2)
            {
                return new Fraction(f1.N * f2.D, f1.D * f2.N).Normalization();
            }

            public Fraction Normalization()
            {
                var n = Math.Abs(N);
                var d = Math.Abs(D);
                var nod = NOD(n, d);
                var sign = Math.Sign(N * D);
                return new Fraction(sign * n / nod, d / nod);
            }

            static int NOD(int a, int b)
            {
                while (a > 0 && b > 0)
                    if (a > b)
                        a %= b;
                    else
                        b %= a;

                return a + b;
            }
        }
    }
}