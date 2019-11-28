using System;

namespace InterviewProblems
{
	public class StringTests
	{
		// Complete the RepeatedString function below.
		public static long RepeatedString(string input, long n)
		{
			long count = 0;
			var remainingChars = n;

			while (remainingChars > 0)
			{
				foreach (var character in input)
				{
					if (character == 'a')
					{
						count++;
					}

					remainingChars--;
					if (remainingChars == 0)
					{
						return count;
					}
				}
			}

			return count;
		}
	}
}