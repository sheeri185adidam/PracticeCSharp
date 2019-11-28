using System;
using System.Threading;

namespace Cancellation
{
	class Program
	{
		public class CancellationByCallback
		{
			private Thread _thread;
			public void Work()
			{
				Console.WriteLine("Beginning work on Thread {0}", 
					Thread.CurrentThread.Name);
				_thread = Thread.CurrentThread;
				try
				{
					for (int i = 1; i < 100000; ++i)
					{
						//doing work
						Thread.SpinWait(i);
						Thread.Sleep(100);
					}

					Console.WriteLine("Thread {0} finished work", 
						Thread.CurrentThread.Name);
				}
				catch (ThreadAbortException e)
				{
					Console.WriteLine("Aborting Thread {0}", 
						Thread.CurrentThread.Name);
				}
				
			}

			public void Cancel()
			{
				//abort the work thread
				_thread.Abort();
			}
		}
		public static void CancellationByPolling(object token)
		{
			var cancelationToken = (CancellationToken) token;
			for (int i = 0; (i < 1000000)
				&& (!cancelationToken.IsCancellationRequested); ++i)
			{
				//do some work
				Thread.SpinWait(i);
				Thread.Sleep(50);
				if (cancelationToken.IsCancellationRequested)
				{
					Console.WriteLine("Aborting Thread {0}",
						Thread.CurrentThread.Name);
					break;
				}
			}
		}


		static void Main(string[] args)
		{
			var cts = new CancellationTokenSource();

			//Cancellation by polling
			Console.WriteLine("Creating CancellationByPolling task");
			var thread1 = new Thread(new ParameterizedThreadStart(CancellationByPolling));
			thread1.Name = "CancellationByPolling";
			thread1.Start(cts.Token);

			//Cancellation by callback
			Console.WriteLine("Creating CancellationByCallback task");
			var obj = new CancellationByCallback();
			cts.Token.Register(obj.Cancel);
			var thread2 = new Thread(obj.Work);
			thread2.Name = "CancellationByCallback";
			thread2.Start();

			//This sleep is necessary to let above two threads to begin execution
			Thread.Sleep(1000);
			cts.Cancel();
			Console.ReadKey();
		}
	}
}
