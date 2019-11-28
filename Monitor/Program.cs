using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
	class Program
	{
		static void Main(string[] args)
		{
			var threads = new List<Task>();
			var random = new Random();
			var average = 0;

			for(var i=0;i<10;++i)
				threads.Add(Task.Run(() =>
				{
					System.Threading.Monitor.Enter(random);
					var sum = 0;
					for (var i = 0; i < 50; ++i)
					{
						sum+=random.Next(100, 1000);
					}
					System.Threading.Monitor.Exit(random);

					var avg = sum / 50;
					Interlocked.Add(ref average, avg);
					Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} average = {avg}");
				}));

			try
			{
				Task.WaitAll(threads.ToArray());

				Console.WriteLine($"Total Average across all threads = {average/threads.Count}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
