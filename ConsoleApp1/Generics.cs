using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Generic
    {

        public static string CompareValues<T>(T value1, T value2)
            where T : IComparable<T>
        {
            int result = value1.CompareTo(value2);

            if (result > 0)
                return $"{value1} is greater than {value2}";
            else if (result < 0)
                return $"{value1} is less than {value2}";
            else
                return $"{value1} is equal to {value2}";
        }

        static void Main()
        {
            Console.WriteLine(CompareValues(10, 20));
            Console.WriteLine(CompareValues(50, 25));

            Console.WriteLine(CompareValues("Apple", "Banana"));
            Console.WriteLine(CompareValues("Cat", "Cat"));

            Console.WriteLine(CompareValues(15.5, 12.2));

            Console.ReadLine();
        }
    }
}
