using System;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
	        var numbers = new int[10] { 34, 45, 50, 60, 32, 1, 345, 321, 568, 93};
	        var selected = from num in numbers where num > 50 select num;
			foreach (var sel in selected)
			{
				Console.WriteLine(sel);
			}
        }
    }
}
