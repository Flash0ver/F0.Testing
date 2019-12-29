using System;
using System.Threading.Tasks;

namespace F0.Testing.Example.Services
{
	internal sealed class GenericValueTaskService
	{
		public GenericValueTaskService()
		{
		}

		public ValueTask<int> CreateOperation(bool throwSynchronously)
		{
			if (throwSynchronously)
			{
				throw new SynchronousGenericAllocationFreeException();
			}

			return GetAsynchronousOperation();

			static async ValueTask<int> GetAsynchronousOperation()
			{
				await Task.Yield();

				throw new AsynchronousGenericAllocationFreeException();
			}
		}
	}

	internal sealed class SynchronousGenericAllocationFreeException : Exception
	{
		public SynchronousGenericAllocationFreeException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(SynchronousGenericAllocationFreeException)}: Throw this Exception synchronously, when the ValueTask<TResult> is retrieved.";
		}
	}

	internal sealed class AsynchronousGenericAllocationFreeException : Exception
	{
		public AsynchronousGenericAllocationFreeException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(AsynchronousGenericAllocationFreeException)}: Throw this Exception asynchronously, when the ValueTask<TResult> is awaited.";
		}
	}
}
