using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncPattern
{
	public class AyncFileStream : IDisposable
	{
		#region private members

		private bool _disposed = false;
		private FileStream _fileStream = null;

        #endregion // private members

        #region public Async methods

        public Task<int> ReadAsync(byte[] buffer, 
            CancellationToken token,
			IProgress<long> progress)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			return ReadAsyncInternal(buffer, token, progress);
		}

		public Task WriteAsync(byte [] buffer, 
		    CancellationToken token,
			IProgress<long> progress)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			return WriteAsyncInternal(buffer, token, progress);
		}

		#endregion

		#region constructor

		public AyncFileStream(string path)
		{
			if (path == null)
				throw new System.ArgumentException("Parameter is null");
			_fileStream = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		}

		#endregion

		#region public methods

		public void Read(byte[] buffer)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			_fileStream.Read(buffer, 0, Convert.ToInt32(_fileStream.Length));
		}

		public void Write(byte[] buffer)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			_fileStream.Write(buffer, 0, buffer.Length);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region protected methods

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				if (_fileStream == null) return;
				_fileStream.Flush();
				_fileStream.Close();
				_fileStream.Dispose();
				_fileStream = null;
			}

			_disposed = true;
		}

		#endregion

		#region private Async helpers

		private async Task<int> ReadAsyncInternal(byte [] buffer,
		    CancellationToken token,
			IProgress<long> progress)
		{
			return await _fileStream.ReadAsync(buffer,
			    0, 
			    Convert.ToInt32(_fileStream.Length), 
			    token);
		}

		private async Task WriteAsyncInternal(byte [] buffer, 
		    CancellationToken token,
			IProgress<long> progress)
		{
			await _fileStream.WriteAsync(buffer, 0, buffer.Length, token);
		}

		#endregion
		
	}

	class Program
	{
		static void Main(string[] args)
		{
			const string text = 
			    @"This is a sample text to test async feature of C#";
			var writeBuffer = System.Text.Encoding.ASCII.GetBytes(text);
			var readBuffer = new byte[1024];

			using (var stream = new AyncFileStream("sample.txt"))
			{
				stream.WriteAsync(writeBuffer, 
				    CancellationToken.None, 
				    null).Wait();
			}

			using (var stream = new AyncFileStream("sample.txt"))
			{
				stream.ReadAsync(readBuffer, 
				    CancellationToken.None, 
				    null).Wait();
				Console.WriteLine(Encoding.UTF8.GetString(readBuffer));
			}

			Console.ReadKey();
		}
	}
}
