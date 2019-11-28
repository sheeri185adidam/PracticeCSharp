using System;

namespace StringManipulations
{
	class Program
	{
		static void Main(string[] args)
		{
			// strint.format
			var formatted = string.Format("My age is {0}", 34);
			Console.WriteLine(formatted);

			//string.concat
			var concat = string.Concat(formatted, ".", "My name is Manik.");
			Console.WriteLine($"Concatenated String: {concat} Original String: {formatted}");
		}
	}
}
