using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F0.Testing.Example.Services
{
	internal sealed class AsyncEnumerableService
	{
		public AsyncEnumerableService()
		{
		}

		public IAsyncEnumerable<int> CreateAsynchronousSequence(bool throwImmediately)
		{
			if (throwImmediately)
			{
				throw new AsyncImmediateException();
			}

			return GetAsynchronousSequenceEnumerator();

			static async IAsyncEnumerable<int> GetAsynchronousSequenceEnumerator()
			{
				await Task.Yield();
				yield return 240;

				throw new AsyncIterateException();
			}
		}
	}

	internal sealed class AsyncImmediateException : Exception
	{
		public AsyncImmediateException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(AsyncImmediateException)}: Throw this Exception when the asynchronous iterator is retrieved.";
		}
	}

	internal sealed class AsyncIterateException : Exception
	{
		public AsyncIterateException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(AsyncIterateException)}: Throw this Exception when the asynchronous iterator is enumerated.";
		}
	}
}
