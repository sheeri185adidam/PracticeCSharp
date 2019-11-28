using System;
using System.Threading;

namespace AutoResetEvent
{
	class Program
	{
		private static EventWaitHandle _autoResetEvent;
		private static EventWaitHandle _manualResetEvent;
		
		private static void ThreadProc(object handle)
		{
			var waitHandle = (EventWaitHandle) handle;

			Console.WriteLine("Thread {0} waiting for handle", Thread.CurrentThread.Name);
			waitHandle.WaitOne();
			Console.WriteLine("Thread {0} got the handle", Thread.CurrentThread.Name);

		}

		static void Main(string[] args)
		{
			_autoResetEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			_manualResetEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

			for (int i = 0; i < 5; ++i)
			{
				var thread = new Thread(new ParameterizedThreadStart(ThreadProc));
				thread.Name = "(auto reset event) Thread" + i;
				thread.Start(_autoResetEvent);
				Console.ReadLine();
				_autoResetEvent.Set();
			}


			for (int i = 0; i < 5; ++i)
			{
				var thread = new Thread(new ParameterizedThreadStart(ThreadProc));
				thread.Name = "(manual reset event) Thread" + i;
				thread.Start(_manualResetEvent);
			}

			Console.ReadLine();
			_manualResetEvent.Set();

		}
	}
}
