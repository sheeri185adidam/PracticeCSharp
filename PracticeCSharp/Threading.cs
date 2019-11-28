using System;

namespace PracticeCSharp
{
	public class Work
	{
		public string Execute()
		{
			return "I am the master";
		}
	}

	public class TestThreading
	{
		public void ThreadReturningValues()
		{
			var worker = new System.ComponentModel.BackgroundWorker();
			worker.DoWork+=new System.ComponentModel.DoWorkEventHandler(DoWork);
			worker.RunWorkerCompleted +=
				new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);

			var work = new Work();
			worker.RunWorkerAsync(work);
		}
		private void DoWork(
			object sender,
			System.ComponentModel.DoWorkEventArgs e)
		{
			var work = (Work)e.Argument;
			// Return the value through the Result property.  
			e.Result = work.Execute();
		}

		private void WorkComplete(
			object sender,
			System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			var returnValue = (string)e.Result;
			Console.WriteLine("The thread execution returned - {0}", returnValue);
		}
	}
}
