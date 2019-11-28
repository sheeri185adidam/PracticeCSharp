using System;
using System.Linq;
using System.Threading;
using static System.Threading.Interlocked;

namespace Interlocked
{
	class Program
	{
		private static int _sync = 1;
		private static string [] _buffer;
		private static Int32 _index = 0;
		private static readonly UInt32 _size = (1024<<10);
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
				while (CompareExchange(ref _sync, 0, 1) == 0)
				{; }

				if (_index < _size)
				{
					var s = RandomString(5);
					_buffer[_index++] = s;
					_fileStream.WriteLine("Writer: {0}",s);
				}

				Exchange(ref _sync, 1);

				Thread.Sleep(10);
			}
		}

		private static void ReaderThread()
		{
			while (true)
			{
				while (CompareExchange(ref _sync, 0, 1) == 0)
				{; }

				if (_index > 0)
				{
					var s = _buffer[_index--];
					_fileStream.WriteLine("Reader: {0}", s);
				}

				Exchange(ref _sync, 1);
				Thread.Sleep(10);
			}
		}
		static void Main(string[] args)
		{;
			_fileStream = System.IO.File.AppendText("output.txt");
			_buffer = new string[_size]; //1MB
			_index = 0;
			Exchange(ref _sync, 1);

			var writer = new Thread(WriterThread);
			var reader = new Thread(ReaderThread);

			writer.Start();
			reader.Start();

			//Console.WriteLine("Reader and Writer thread both exited successfully");
			Console.ReadKey();
			_fileStream.Close();
		}
	}
}
