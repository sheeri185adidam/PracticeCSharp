using System;

namespace EqualsVersusOperator__
{
	public class Author
	{
		public Author(string name, int age)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Age = age;
		}

		public string Name { get; }
		public int Age { get; }

		public override bool Equals(object other)
		{
			if (other is null)
			{
				return false;
			}

			if (!(other is Author otherAuthor))
			{
				return false;
			}

			return this.Equals(otherAuthor);
		}

		protected bool Equals(Author other)
		{
			return string.Equals(Name, other.Name) && Age == other.Age;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Age;
			}
		}

		public static bool operator==(Author left, Author right)
		{
			return !(left is null) && left.Equals(right);
		}

		public static bool operator !=(Author left, Author right)
		{
			return !(left == right);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			string str1 = new string("manik");
			string str2 = new string("manik");

			Console.WriteLine($"str1 == str2: {str1==str2}");
			Console.WriteLine($"str1.Equals(str2): {str1.Equals(str2)}");

			var author1 = new Author("manik", 34);
			var author2 = new Author("manik", 34);

			Console.WriteLine($"author1 == author2: {author1 == author2}");
			Console.WriteLine($"author1.Equals(author2): {author1.Equals(author2)}");
		}
	}
}
