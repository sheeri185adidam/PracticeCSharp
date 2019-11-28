using System;
using System.ComponentModel;

namespace BackgroundWorker2
{
	class Program
	{
		class BackgroundTask<TResult, TArg>
		{
			public delegate TResult Work(TArg arg);

			private readonly Work _work;
			//private readonly TArg _workArg;
			private TResult _workResult;
			private readonly System.ComponentModel.BackgroundWorker _worker;
			public BackgroundTask(Work func, TArg arg)
			{
				_work = func;
				//_workArg = arg;
				_worker = new System.ComponentModel.BackgroundWorker();
				_worker.DoWork += 
					new System.ComponentModel.DoWorkEventHandler(Execute);
				_worker.ProgressChanged  +=
					new System.ComponentModel.ProgressChangedEventHandler(ProgressChanged);
				_worker.RunWorkerCompleted += 
					new System.ComponentModel.RunWorkerCompletedEventHandler(Completed);
				_worker.RunWorkerAsync(arg);
			}

			public bool IsBusy()
			{
				return _worker.IsBusy;
			}

			public TResult Result()
			{
				while (_worker.IsBusy)
				{
					System.Threading.Thread.Sleep(100);
				}
				return _workResult;
			}
			private void Execute(object sender, DoWorkEventArgs e)
			{
				e.Result = _work((TArg)e.Argument);
			}

			private void ProgressChanged(object sender, ProgressChangedEventArgs e)
			{

			}

			private void Completed(object sender, AsyncCompletedEventArgs e)
			{

			}
		}

		static int CalculateFibonacci(int n)
		{
			if (n <= 0) return 0;
			if (n <= 2) return 1;
			return CalculateFibonacci(n - 1) 
				+ CalculateFibonacci(n - 2);
		}
		static void Main(string[] args)
		{
			var task = new BackgroundTask<int, int>(CalculateFibonacci, 10);
			Console.WriteLine("Background task result = {0}", task.Result());
			Console.ReadKey();
		}
	}
}
