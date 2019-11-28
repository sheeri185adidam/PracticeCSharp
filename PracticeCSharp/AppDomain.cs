using System;

namespace PracticeCSharp
{
	class MyAppDomain
	{
		public static void CreateDomain()
		{
			Console.WriteLine("Creating new AppDomain.");

			/*configuring app domain*/
			var domainSetup = new AppDomainSetup();
			domainSetup.ApplicationBase = System.IO.Directory.GetCurrentDirectory();
			
			/*creating app domain*/
			var domain = AppDomain.CreateDomain("MyDomain", null, domainSetup);

			Console.WriteLine("Child domain: " + domain.FriendlyName);
			
			/*Load Assembly*/
			var assembly = domain.Load("SimplePrint");
			var type = assembly.GetType("SimplePrint.Program");
			var method = type.GetMethod("Print");
			object obj = Activator.CreateInstance(type);
			method.Invoke(obj, null);
			
			/*unload app domain*/
			AppDomain.Unload(domain);
		}
	}
}
