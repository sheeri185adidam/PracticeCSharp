using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCSharp
{
	public class MyList<T> : IEnumerable<T>
	{
		private Node _head = null;

		public void Add(T t)
		{
			var node = new Node();
			node._data = t;
			node._next = _head;
			_head = node;
		}

		public void Pop()
		{
			_head = _head._next;
		}
		public class Node
		{
			public T _data;
			public Node _next = null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new MyListEnum<T>(_head);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator) this.GetEnumerator();
		}
	}

	class MyListEnum<T> : IEnumerator<T>
	{
		private MyList<T>.Node _node = null;
		private MyList<T>.Node _head = null;
		public MyListEnum(MyList<T>.Node head)
		{
			_node = new MyList<T>.Node();
			_node._next = head;
			_head = head;
		}

		public T Current
		{
			get
			{
				if (_node == null)
					throw new InvalidOperationException();
				return _node._data;
			}
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		bool IEnumerator.MoveNext()
		{
			if (_node._next
				!= null)
			{
				_node = _node._next;
				return true;
			}
			return false;
		}

		void IEnumerator.Reset()
		{
			_node = _head;
		}

		void IDisposable.Dispose()
		{
		}
	}
}
