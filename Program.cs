using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCSharp
{
	public class StorageBlock
	{
		public int BlockIndex { get; set; }
	}


	class Program
	{
		private static ConcurrentDictionary<string, StorageBlock> _blocks =
			new ConcurrentDictionary<string, StorageBlock>();

		IDictionary<string, int> StorageSnapshot()
		{
			var dictionary = _blocks.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.BlockIndex);
			return dictionary;
		}

		static void Main(string[] args)
		{
			

		}
	}
}
