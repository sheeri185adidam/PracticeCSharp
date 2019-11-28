using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCSharp
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, 
		AllowMultiple = true)]
	public class MyAttribute:System.Attribute
	{
		public string Author { get; set; } = null;

		public string Version { get; set; } = null;
	}

	[MyAttribute(Author = "Manik Sheeri", Version = "1.0")]
	public class SampleClass
	{
		
	}
}
