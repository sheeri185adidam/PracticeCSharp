using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LockStatement
{
	class Program
	{
		private static Stack<string> _stack = new Stack<string>();
		private static readonly Random _random = new Random();
		private static System.IO.StreamWriter _fileStream;
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[_random.Next(s.Length)]).ToArray());
		}

		private static void WriterThread()
		{
			while (true)
			{
				lock (_stack)
				{
					var s = RandomString(5);
					_stack.Push(s);
					//_fileStream.WriteLine("Writer: {0}", s);
				}

				Thread.Yield();
			}
		}

		private static void ReaderThread()
		{
			while (true)
			{
				lock (_stack)
				{
					if (_stack.Count > 0)
					{
						var s = _stack.Pop();
						//_fileStream.WriteLine("Reader: {0}", s);
					}
				}

				Thread.Yield();
			}
		}
		static void Main(string[] args)
		{
			_fileStream = System.IO.File.AppendText("output.txt");
			
			var writer = new Thread(WriterThread);
			var reader = new Thread(ReaderThread);

			writer.Start();
			reader.Start();

			Console.ReadKey();
			_fileStream.Close();
		}
	}
}
