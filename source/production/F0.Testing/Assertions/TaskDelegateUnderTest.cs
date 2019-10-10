using System;
using System.Threading.Tasks;
using F0.Exceptions;

namespace F0.Assertions
{
	public sealed class TaskDelegateUnderTest
	{
		private readonly Func<Task> asyncMethod;

		internal TaskDelegateUnderTest(Func<Task> asyncMethod)
		{
			this.asyncMethod = asyncMethod ?? throw new ArgumentNullException(nameof(asyncMethod));
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

		public async Task<TException> ThrowsAsynchronously<TException>()
			 where TException : Exception
		{
			Task task;

			try
			{
				task = asyncMethod();
			}
			catch (Exception e)
			{
				task = null;

				AssertionFailedException.Throw(nameof(ThrowsAsynchronously), typeof(TException).FullName, $"(An exception was thrown synchronously: '{e.GetType()}')");
			}

			Exception exception = await CaptureExceptionAsynchronously(task);

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
				_ = asyncMethod();
				exception = null;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}

		private static async Task<Exception> CaptureExceptionAsynchronously(Task task)
		{
			Exception exception;

			try
			{
				await task;
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
