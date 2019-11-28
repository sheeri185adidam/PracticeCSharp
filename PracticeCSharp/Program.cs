using System;
using System.Runtime.InteropServices;

namespace PracticeCSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			Int64 time = 131611356934580000;
			var data = BitConverter.GetBytes(time);
			var ftime = new FILETIME
			{
				dwLowDateTime = BitConverter.ToInt32(data, 0),
				dwHighDateTime = BitConverter.ToInt32(data, 4)
			};

			Console.WriteLine($"FileTime - Low : {ftime.dwLowDateTime}, Hight : {ftime.dwHighDateTime}");
			var fileTime = (((long)ftime.dwHighDateTime) << 32) + ftime.dwLowDateTime;
			var systemTime = DateTime.FromFileTime(fileTime);

			Console.ReadLine();
		}
	}
}
