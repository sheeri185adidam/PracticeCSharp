using System;
using System.Threading;

namespace ThreadPool
{
	public class Work1
	{
		public void Do(object arg)
		{
			string s = (string) arg;
			Console.WriteLine("Thread {0} executed with argument {1}",
				Thread.CurrentThread.Name, s);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var work = new Work1();
				var arg = "Work1WorkArgumentIsMe";
				System.Threading.ThreadPool.QueueUserWorkItem(work.Do, arg);
			}
			catch(NotSupportedException e)
			{
				Console.WriteLine("Failed to queue user work item to threadpool - {0}",
					e.Message);
			}

			Console.ReadKey();
		}
	}
}
