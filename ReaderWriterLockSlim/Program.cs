using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLockSlim
{
	public class SynchronizedCache<TKey, TValue>
	{
		private readonly System.Threading.ReaderWriterLockSlim _lock = new System.Threading.ReaderWriterLockSlim();
		private readonly IDictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

		public TValue this[TKey key]
		{
			get
			{
				_lock.EnterReadLock();
				try
				{
					if (!_dictionary.TryGetValue(key, out var value))
					{
						throw new InvalidOperationException($"key {key} doesn't exist");
					}

					return value;
				}
				finally
				{
					_lock.ExitReadLock();
				}
			}

			set
			{
				_lock.EnterWriteLock();
				_dictionary[key] = value;
				_lock.ExitWriteLock();
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			try
			{
				value = this[key];
				return true;
			}
			catch (Exception)
			{
				value = default(TValue);
				return false;
			}
		}

		public void SetValue(TKey key, TValue value)
		{
			this[key] = value;
		}
	}

	class Program
	{
		private static readonly SynchronizedCache<int,string> Cache = new SynchronizedCache<int, string>();
		private static readonly Random Random = new Random();
		private static readonly IList<Task> Readers = new List<Task>();
		private static readonly IList<Task> Writers = new List<Task>();

		static async Task ReaderTask(string name)
		{
			while (true)
			{
				lock (Random)
				{
					var index = Random.Next(1, 100);
					if (Cache.TryGetValue(index, out var value))
					{
						Console.WriteLine(
							$"Reader({name}) {Thread.CurrentThread.ManagedThreadId}, cache[{index}]={value}");
					}
					else
					{
						Console.WriteLine($"Reader({name}) {Thread.CurrentThread.ManagedThreadId}");
					}
				}

				await Task.Delay(1000);
			}
		}

		static async Task WriterTask(string name)
		{
			while (true)
			{
				lock (Random)
				{
					var index = Random.Next(1, 100);
					var value = Random.Next(100, 1000);
					Cache[index] = value.ToString();
					Console.WriteLine(
						$"Writer({name}) {Thread.CurrentThread.ManagedThreadId}, cache[{index}]={Cache[index]}");
				}

				await Task.Delay(500);
			}
		}

		private static async Task Main(string[] args)
		{
			Console.WriteLine($"Main thread {Thread.CurrentThread.ManagedThreadId}");
			await Task.WhenAll( // readers
				ReaderTask("Reader 1"),
				ReaderTask("Reader 2"),
				ReaderTask("Reader 3"),
				ReaderTask("Reader 4"),

				// writers
				WriterTask("Writer 1"),
				WriterTask("Writer 2"));
		}
	}
}
