using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Attributes
{
	[Obsolete("This class is not supported.")]
	internal class ObsoleteClass
	{

	}

	public class MyAttribute : Attribute
	{
		public MyAttribute() { }
		public string Author { get; set; }
	}

	[MyAttribute(Author="manik sheeri")]
	class SampleClass
	{
	}

	class Program
	{
		static void Main(string[] args)
		{
			var obsolete = new ObsoleteClass();

			var attribute =
				typeof(SampleClass).GetCustomAttributes().FirstOrDefault(attr => attr is MyAttribute) as MyAttribute;
			Console.WriteLine($"Sample Class Author: {attribute?.Author}");
		}
	}
}
