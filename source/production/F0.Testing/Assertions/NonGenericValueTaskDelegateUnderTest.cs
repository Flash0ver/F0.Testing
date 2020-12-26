using System;
using System.Threading.Tasks;
using F0.Exceptions;

namespace F0.Assertions
{
	public sealed class NonGenericValueTaskDelegateUnderTest
	{
		private readonly Func<ValueTask> asynchronousMethod;

		internal NonGenericValueTaskDelegateUnderTest(Func<ValueTask> asynchronousMethod)
		{
			this.asynchronousMethod = asynchronousMethod ?? throw new ArgumentNullException(nameof(asynchronousMethod));
		}

		public TException ThrowsSynchronously<TException>()
			where TException : Exception
		{
			Exception exception = CaptureExceptionSynchronously();

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsSynchronously), typeof(TException).FullName, "(No exception was observed synchronously)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsSynchronously), typeof(TException).FullName, exception.GetType().FullName);
			}

			return exception as TException;
		}

		public async ValueTask<TException> ThrowsAsynchronously<TException>()
			where TException : Exception
		{
			ValueTask valueTask;

			try
			{
				valueTask = asynchronousMethod();
			}
			catch (Exception e)
			{
				valueTask = default;

				AssertionFailedException.Throw(nameof(ThrowsAsynchronously), typeof(TException).FullName, $"(An exception was thrown synchronously: '{e.GetType()}')");
			}

			Exception exception = await CaptureExceptionAsynchronously(valueTask);

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsAsynchronously), typeof(TException).FullName, "(No exception was observed asynchronously)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsAsynchronously), typeof(TException).FullName, exception.GetType().FullName);
			}

			return exception as TException;
		}

		private Exception CaptureExceptionSynchronously()
		{
			Exception exception;

			try
			{
				_ = asynchronousMethod();
				exception = null;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}

		private static async ValueTask<Exception> CaptureExceptionAsynchronously(ValueTask valueTask)
		{
			Exception exception;

			try
			{
				await valueTask;
				exception = null;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}
	}
}
