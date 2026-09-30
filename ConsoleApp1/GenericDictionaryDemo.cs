using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GenericDictionaryDemo
    {

        public static void Main()
        {
            Dictionary<int, string> di = new Dictionary<int, string>();

            di.Add(101, "Avijit");
            di.Add(102, "Komal");
            di.Add(103, "Shaheen");
            di.Add(104, "Anubhaw");
            di.Add(106, "Anmol");
            di.Add(107, "Surbhi");

            foreach (var item in di)
            {
                Console.WriteLine($"Employee Id : {item.Key} & Employee Name : {item.Value}");
                
            }

        }
    }
}
