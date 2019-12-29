using System;
using System.Threading.Tasks;

namespace F0.Testing.Example.Services
{
	internal sealed class NonGenericValueTaskService
	{
		public NonGenericValueTaskService()
		{
		}

		public ValueTask CreateOperation(bool throwSynchronously)
		{
			if (throwSynchronously)
			{
				throw new SynchronousNonGenericAllocationFreeException();
			}

			return GetAsynchronousOperation();

			static async ValueTask GetAsynchronousOperation()
			{
				await Task.Yield();

				throw new AsynchronousNonGenericAllocationFreeException();
			}
		}
	}

	internal sealed class SynchronousNonGenericAllocationFreeException : Exception
	{
		public SynchronousNonGenericAllocationFreeException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(SynchronousNonGenericAllocationFreeException)}: Throw this Exception synchronously, when the ValueTask is retrieved.";
		}
	}

	internal sealed class AsynchronousNonGenericAllocationFreeException : Exception
	{
		public AsynchronousNonGenericAllocationFreeException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(AsynchronousNonGenericAllocationFreeException)}: Throw this Exception asynchronously, when the ValueTask is awaited.";
		}
	}
}
