using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncPattern2
{
	public enum WorkStatus
	{
		Started,
		InMiddle,
		AlmostDone,
		Finished
	}
	public class WorkSimulator
	{
		#region public Async methods

		public Task<int> WorkAsync(CancellationToken token, 
		    IProgress<WorkStatus> progress)
		{
			var tcs = new TaskCompletionSource<int>();
			progress.Report(WorkStatus.Started);
			Thread.SpinWait(Int32.MaxValue/500);
			progress.Report(WorkStatus.InMiddle);
			Thread.SpinWait(Int32.MaxValue/500);
			progress.Report(WorkStatus.AlmostDone);
			Thread.SpinWait(Int32.MaxValue/500);
			progress.Report(WorkStatus.Finished);

			try { tcs.SetResult(Int32.MaxValue); }
			catch (Exception exc) { tcs.SetException(exc); }
			return tcs.Task;
		}

		#endregion
	}
	class Program
	{
		static void CheckProgress(object sender, WorkStatus status)
		{
			Console.WriteLine("Work Status - {0}", status.ToString());
		}
		static void Main(string[] args)
		{
			var progress = new Progress<WorkStatus>();
			progress.ProgressChanged += CheckProgress;
			var workSim = new WorkSimulator();
			var cts = new CancellationTokenSource();

			workSim.WorkAsync(cts.Token, progress);

			Console.ReadKey();
		}
	}
}
