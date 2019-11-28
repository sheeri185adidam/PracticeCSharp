using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
namespace PracticeCSharp
{
	public class ResourceBase : IDisposable
	{
		private bool _disposed = false;

		#region IDisposable members
		public void Dispose()
		{
			Console.WriteLine("ResourceBase::Dispose() method called");
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		protected virtual void Dispose(bool disposing)
		{
			Console.WriteLine("ResourceBase::Dispose(bool) method called");
			if (_disposed)
				return;

			if (disposing)
			{
				// release managed resources here
			}

			// release unmanaged resources here

			_disposed = true;
		}

		// finalize
		~ResourceBase()
		{
			Console.WriteLine("~ResourceBase() method called");
			Dispose(false);
		}
	}

	// deriving from IDisposable class
	public sealed class ResourceDerived : ResourceBase
	{
		private bool _disposed = false;
		private readonly TextReader _reader = null;

		public ResourceDerived(string path)
		{
			_reader = new StreamReader(path);	
		}

		public void Print()
		{
			if (_reader != null)
			{
				//Console.WriteLine(_reader.ReadToEnd() + " ----SOME UNMANAGED DATA");
			}
		}
		protected override void Dispose(bool disposing)
		{
			Console.WriteLine("ResourceDerived::Dispose(bool) method called");
			if (_disposed)
				return;

			if (disposing)
			{
				// release managed resources here
				ReleaseManagedResources();
			}

			// release unmanaged resources here
			ReleaseUnmanagedResources();
			_disposed = true;

			base.Dispose(disposing);
		}

		// Is this Finalizer needed ?? - MS
		~ResourceDerived()
		{
			Console.WriteLine("~ResourceDerived() method called");
			Dispose(false);
			base.Dispose(false);
		}

		private void ReleaseManagedResources()
		{
			Console.WriteLine("ReleaseManagedResources() method called");
			_reader?.Dispose();
		}

		private void ReleaseUnmanagedResources()
		{
			Console.WriteLine("ReleaseUnmanagedResources() method called");
		}
	}
}
