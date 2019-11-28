﻿using System;
using System.Collections;

namespace PracticeCSharp
{
	public class Person
	{
		public Person(string fName, string lName)
		{
			this._firstName = fName;
			this._lastName = lName;
		}

		public string _firstName;
		public string _lastName;
	}
	public class People : IEnumerable
	{
		private Person[] _people;
		public People(Person[] pArray)
		{
			_people = new Person[pArray.Length];
			for (var i = 0; i < pArray.Length; i++)
			{
				_people[i] = pArray[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator) GetEnumerator();
		}

		public PeopleEnum GetEnumerator()
		{
			return new PeopleEnum(_people);
		}
	}

	public class PeopleEnum : IEnumerator
	{
		public Person[] _people;
		int position = -1;

		public PeopleEnum(Person[] list)
		{
			_people = list;
		}

		public bool MoveNext()
		{
			position++;
			return (position < _people.Length);
		}

		public void Reset()
		{
			position = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public Person Current
		{
			get
			{
				try
				{
					return _people[position];
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
