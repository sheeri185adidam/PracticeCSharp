using System;
using System.Collections.Generic;

namespace AppDomainDemoLib
{
	internal sealed class MyCachedObject
	{
		private static Random generator = new Random();
		private byte[] processedData;

		public MyCachedObject()
		{
			processedData = new byte[generator.Next(25, 70) * 1024];
		}

		public int Execute(int id)
		{
			return processedData.Length;
		}
	}

	public class Processor
	{
		private static readonly Dictionary<int, MyCachedObject> myCache = new Dictionary<int, MyCachedObject>();

		public string ProcessRequest(int id)
		{
			if (!myCache.ContainsKey(id))
			{
				myCache.Add(id, new MyCachedObject());
			}

			int cacheSize = myCache[id].Execute(id);

			return $"This is a response message from id {id} with a cache size " +
			       $"of {cacheSize} in the AppDomain named: {AppDomain.CurrentDomain.FriendlyName}";
			

		}

	}
}
