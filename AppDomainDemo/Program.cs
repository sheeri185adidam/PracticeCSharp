using System;
using System.Diagnostics;

namespace AppDomainDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcessRequestBatch();
			Console.WriteLine("Completed the batch press enter to continue");
			Console.ReadLine();
		}

		private static void ProcessRequestBatch()
		{
			var watch = new Stopwatch();
			var assembly = $"C:\\workspace\\c#\\PracticeCSharp\\AppDomainLib\\bin\\Debug\\AppDomainDemoLib.dll";
			watch.Start();
			var domain = AppDomain.CreateDomain("ProcessingDomain");
			domain.Load(assembly);
			var processor = domain.CreateInstanceAndUnwrap(assembly, "AppDomainDemoLib.Processor");
			var type = processor.GetType();
			var method = type.GetMethod(@"ProcessRequest");
			var methodParameters = new object[1];
			for (int index = 0; index < 10000; index++)
			{
				int id = index % 5000;
				methodParameters[0] = id;
				if (method != null) Console.WriteLine(method.Invoke(type, methodParameters));
			}

			
			AppDomain.Unload(domain);

			watch.Stop();

			Console.WriteLine("The processing took {0} ms to complete.", watch.ElapsedMilliseconds);
		}
	}
}
