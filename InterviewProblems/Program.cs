using System;

namespace InterviewProblems
{
	class Program
	{
		static void Main(string[] args)
		{
			//string input = Console.ReadLine();
			//long n = Convert.ToInt64(Console.ReadLine());

			//long result = StringTests.RepeatedString(input, n);

			var input = new int[5] { 4, 7,1,3,2};
			Sorting.InsertionSort(input, 0, 4);

			Console.WriteLine();
			foreach (var i in input)
			{
				Console.Write(i);
				Console.Write(" ");
			}
			Console.WriteLine();
		}
	}
}
