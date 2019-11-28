using System;
using System.Threading.Tasks;

namespace TaskApp
{
	class Program
	{
		public static void Work(int i)
		{
			Console.WriteLine("Task {0} finished", i);
		}
		static void Main(string[] args)
		{
			var taskWithoutResult = Task.Run(
				() =>
				{
					Console.WriteLine("Task finished");
				});

			var taskWithResult = Task<string>.Run(() =>
			{
				return "SampleString";
			});

			Console.WriteLine("Task returned - {0}", taskWithResult.Result);
			Console.ReadKey();
		}
	}
}
