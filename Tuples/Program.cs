using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuples
{
    internal class Program
    {
        private static (string name, string addres) FunctionReturningTuple()
        {
            return ("manik", "las vegas");
        }

        public static void Main(string[] args)
        {
            var tuple = FunctionReturningTuple();
            Console.WriteLine($"Name: {tuple.name}, Address: {tuple.addres}");
            Console.ReadKey();
        }
    }
}
