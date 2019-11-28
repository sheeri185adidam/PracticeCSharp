namespace InterviewProblems
{
	public class Sorting
	{
		public static void InsertionSort(int[] input, int low, int high)
		{
			for (var i = low + 1; i <=high; ++i)
			{
				for (var j = i - 1; j >= 0; --j)
				{
					if (input[j] > input[j + 1])
					{
						var tmp = input[j];
						input[j] = input[j + 1];
						input[j + 1] = tmp;
					}
				}
			}
		}
	}
}