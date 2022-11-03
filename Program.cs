using System.Security;

namespace ConsoleApp7
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Введите числитель первой дроби через = ");
                var numOfFirstFraction = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите знаменатель первой дроби через = ");
                var denOfFirstFraction = int.Parse(Console.ReadLine()!);
            
                Console.WriteLine("Введите операцию (+, -, /, *) = ");
                var operation = Console.ReadLine();
            
                Console.WriteLine("Введите числитель второй дроби через = ");
                var numOfSecondFraction = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите знаменатель второй дроби через = ");
                var denOfSecondFraction = int.Parse(Console.ReadLine()!);
            
                var f1 = new Fraction(numOfFirstFraction, denOfFirstFraction);
                var f2 = new Fraction(numOfSecondFraction, denOfSecondFraction);
                var fractionList = new List<Fraction>()
                {
                    f1, f2
                };

                var f3 = operation switch
                {
                    "+" => f1 + f2,
                    "-" => f1 - f2,
                    "*" => f1 * f2,
                    "/" => f1 / f2,
                    _ => throw new Exception("Unknown operation")
                };
                fractionList.Sort();

                Console.WriteLine("{0} {1} {2} = {3}", f1, operation, f2, f3);
                Console.WriteLine("Sorted: {0}", string.Join(", ", fractionList.ToArray()));
                
                var compareTo = f1.CompareTo(f2);
                Console.WriteLine("Compared: {0}", compareTo);
                
                //массив дробей
                Random random = new Random();
                var array = new Fraction[10];
                Console.WriteLine("Initial Array");
                for (var i = 0; i < array.Length; i++)
                {
                    var fraction = new Fraction(random.Next(0, 100), random.Next(1, 100));
                    array[i] = fraction;
                    Console.WriteLine(array[i].ToString("F3"));
                }
                //отсортированный массив дробей
                Console.WriteLine("Sorted Array");
                array = array.OrderByDescending(f => f).ToArray();
                for (var i = 0; i < array.Length; i++)
                {
                    Console.WriteLine(array[i].Normalization().ToString("F3"));
                }

                // сложение дробей в массиве
                // Fraction? sum = null;
                // foreach (var item in array)
                // {
                //     sum += item;
                // }
                // Console.WriteLine(sum);
                
                Console.ReadLine();
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case DivideByZeroException:
                        Console.WriteLine("Ошибка вычислений: Знаменатель должен быть положительным");
                        break;
                    default:
                        throw;
                }
            }
        }
    }
 
    public struct Fraction : IComparable<Fraction>
    {
        private readonly int _n;
        private readonly int _d;
 
        public Fraction(int numerator, int denominator)
        {
            _n = numerator;
            _d = denominator;
        }
 
        public string ToString(string f3)
        {
            return $"{_n}/{_d}";
        }
 
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._n * f2._d + f2._n * f1._d, f1._d * f2._d).Normalization();
        }
 
        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._n * f2._n, f1._d * f2._d).Normalization();
        }
 
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._n * f2._d - f2._n * f1._d, f1._d * f2._d).Normalization();
        }
 
        public static Fraction operator -(Fraction f1)
        {
            return new Fraction(-f1._n, f1._d).Normalization();
        }
 
        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._n * f2._d, f1._d * f2._n).Normalization();
        }

        public Fraction Normalization()
        {
            var n = Math.Abs(_n);
            var d = Math.Abs(_d);
            var nod = Nod(n, d);
            var sign = Math.Sign(_n*_d);
            return new Fraction(sign * n / nod, d / nod);
        }

        private static int Nod(int a, int b)
        {
            while (a > 0 && b > 0)
                if (a > b)
                    a %= b;
                else
                    b %= a;
 
            return a + b;
        }

        public int CompareTo(Fraction other)
        {
            var nComparison = _n.CompareTo(other._n);
            return nComparison != 0 ? nComparison : _d.CompareTo(other._d);
        }
    }
}