using System;
using System.Threading.Tasks;

namespace TaskBasedAsyncPattern
{
	public static class TaskExtensions
	{
		public static async Task<T> RetryOnFault<T>(Func<T> func, int retries)
		{
			return await Task.Run(() =>
			{
				for (var retry = 0; retry < retries; retry++)
				{
					try
					{
						return func();
					}
					catch(Exception e)
					{
						if (retry == retries - 1)
							throw;
					}
				}

				return default;
			});
		}

	}
}