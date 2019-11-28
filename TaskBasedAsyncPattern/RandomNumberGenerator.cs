using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsyncPattern
{
	public class RandomNumberGenerator
	{
		public static Task GenerateNumbers(int count, int interval, CancellationToken cancellationToken, 
			IProgress<double> progress)
		{
			return Task.Run(async () =>
			{
				if (count < 0)
				{
					throw new InvalidOperationException(nameof(count));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}

				var random = new Random();
				var index = 0;
				while (index++ < count)
				{
					Console.WriteLine($"ThreadId = {Thread.CurrentThread.ManagedThreadId}");
					var number = random.Next(1, count);
					//Console.WriteLine($"Random Number: {number}");

					if (cancellationToken.IsCancellationRequested)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}

					var completion = ((double) ((double )index / (double)count) * 100);
					progress?.Report(completion);
					await Task.Delay(interval, cancellationToken);
				}
			}, cancellationToken).ContinueWith(t =>
			{
				Console.WriteLine($"Task State {t.IsCompletedSuccessfully}");
			}, TaskContinuationOptions.OnlyOnCanceled);
		}

		public static async Task Test1()
		{
			var tokenSource = new CancellationTokenSource();
			var progress = new Progress<double>();
			progress.ProgressChanged += (s, e) =>
			{
				Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
				Console.WriteLine($"{e}%");
			};

			try
			{
				var task = RandomNumberGenerator.GenerateNumbers(10000, 200, tokenSource.Token, progress);
				var command = Console.ReadLine() ?? "none";

				if (command.Contains("cancel") && !task.IsCompleted)
				{
					tokenSource.Cancel();
				}

				await task;
			}
			catch (OperationCanceledException e)
			{
				Console.WriteLine($"Task was cancelled");
			}
			finally
			{
				tokenSource.Dispose();
			}
		}
	}
}