using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCSharp
{
	class MyEnumeratorMethod
	{
		private readonly  int[] _elements = {12, 13, 14, 15, 16};
		public IEnumerable<int> ReadElements()
		{
			foreach (var item in _elements)
			{
				yield return item;
			}
		}
	}
}
