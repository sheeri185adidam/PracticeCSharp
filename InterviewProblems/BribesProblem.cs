using System;

namespace InterviewProblems
{
	public class BribesProblem
	{
		// Complete the minimumBribes function below.
		static void MinimumBribes(int[] q)
		{
			// let's validate the bribes
			int inputLength = q.Length;
			int bribeValidation = 0;
			int bribesCount = 0;

			for (var index = 1; index <= inputLength; ++index)
			{
				var expected = index;
				var actual = q[index - 1];
				if (actual == expected)
				{
					continue;
				}
				else if (actual > expected)
				{
					if ((actual - expected) > 2)
					{
						bribeValidation = -1;
						break;
					}

					bribesCount += actual - expected;
					bribeValidation += (actual - expected);
				}
				else
				{
					bribeValidation -= (expected - actual);
				}
			}

			if (bribeValidation != 0)
			{
				Console.WriteLine("Too chaotic");
				return;
			}
			else
			{
				Console.WriteLine(bribesCount);
			}
		}

		public static void Test()
		{
			int t = Convert.ToInt32(Console.ReadLine());

			for (int tItr = 0; tItr < t; tItr++)
			{
				int n = Convert.ToInt32(Console.ReadLine());
				int[] q = Array.ConvertAll(Console.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp));
				MinimumBribes(q);
			}
		}
	}
}