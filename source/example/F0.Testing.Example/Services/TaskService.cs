using System;
using System.Threading.Tasks;

namespace F0.Testing.Example.Services
{
	internal sealed class TaskService
	{
		public TaskService()
		{
		}

		public Task<int> CreateOperation(bool throwSynchronously)
		{
			if (throwSynchronously)
			{
				throw new SynchronousException();
			}

			return GetAsynchronousOperation();

			static async Task<int> GetAsynchronousOperation()
			{
				await Task.Yield();

				throw new AsynchronousException();
			}
		}
	}

	internal sealed class SynchronousException : Exception
	{
		public SynchronousException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(SynchronousException)}: Throw this Exception synchronously, when the Task is retrieved.";
		}
	}

	internal sealed class AsynchronousException : Exception
	{
		public AsynchronousException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(AsynchronousException)}: Throw this Exception asynchronously, when the Task is awaited.";
		}
	}
}
