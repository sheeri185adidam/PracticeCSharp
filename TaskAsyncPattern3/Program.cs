
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncPattern3
{
	class Program {
public
  static async Task<int> Fibonacci(CancellationToken token, int N) {
	return await Task.Run(() => {
	  if (N <= 0)
		throw new ArgumentException("Invalid N");
	  if (N > 1000)
		throw new ArgumentOutOfRangeException("Invalid N");
	  if (token.IsCancellationRequested)
		throw new OperationCanceledException("Cancellation requested");

	  int sum1 = 1;
	  int sum2 = 1;
	  for (int i = 3; i <= N; ++i) {
		if (token.IsCancellationRequested)
		  throw new OperationCanceledException("Cancellation requested");
		sum2 += sum1;
		sum1 = sum2 - sum1;
	  }

	  return sum2;
	});
  }

		public static Task PrintString(string str)
		{
			return Task.Run(() => {
				Thread.Sleep(5000);
				Console.WriteLine("PrintString called with argument - {0}", str);
			});
		}

		public static async void TestAwait()
		{
			Console.WriteLine("TestAwait stage 1");
			await PrintString("manik");
			Console.WriteLine("TestAwait stage 2");
			await PrintString("sheeri");
			Console.WriteLine("TestAwait done");
		}
  static void Main(string[] args) {
	int N = 0;

	  TestAwait();
	  Console.ReadKey();
	  return;
	while (true) {
	  Console.Write("Enter N for Fibonacci Calculation : ");
	  var number = Console.ReadLine();
	  if (int.TryParse(number, out N)) {
		var cts = new CancellationTokenSource();
		var work = Fibonacci(cts.Token, N);
		Console.WriteLine("Press 'c' = Cancel");
		Console.WriteLine("Press 'q' = Quit program");
		var key = Console.ReadKey().Key;
		switch (key) {
		case ConsoleKey.C:
		  cts.Cancel();
		  break;
		case ConsoleKey.Q:
		  return;
		}
	  } else {
		Console.WriteLine("Invalid value for N!");
	  }

	  Console.WriteLine();
	}
  }
}
} // namespace TaskAsyncPattern3
