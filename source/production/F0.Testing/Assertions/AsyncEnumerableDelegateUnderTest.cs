using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Exceptions;

namespace F0.Assertions
{
	public sealed class AsyncEnumerableDelegateUnderTest<T>
	{
		private readonly Func<IAsyncEnumerable<T>> asyncEnumerableMethod;

		internal AsyncEnumerableDelegateUnderTest(Func<IAsyncEnumerable<T>> asyncEnumerableMethod)
		{
			this.asyncEnumerableMethod = asyncEnumerableMethod ?? throw new ArgumentNullException(nameof(asyncEnumerableMethod));
		}

		public TException ThrowsImmediately<TException>()
			 where TException : Exception
		{
			Exception exception = CaptureException();

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsImmediately), typeof(TException).FullName, "(No exception was thrown when retrieving the asynchronous iterator)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsImmediately), typeof(TException).FullName, exception.GetType().FullName);
			}

			return exception as TException;
		}

		public async Task<TException> ThrowsDeferredAsync<TException>()
			where TException : Exception
		{
			IAsyncEnumerable<T> asyncIterator;

			try
			{
				asyncIterator = asyncEnumerableMethod();
			}
			catch (Exception e)
			{
				asyncIterator = null;

				AssertionFailedException.Throw(nameof(ThrowsDeferredAsync), typeof(TException).FullName, $"(An exception was thrown before iteration over the asynchronous stream: '{e.GetType()}')");
			}

			Exception exception = await CaptureExceptionAsync(asyncIterator);

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsDeferredAsync), typeof(TException).FullName, "(No exception was thrown during iteration over the asynchronous stream)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsDeferredAsync), typeof(TException).FullName, exception.GetType().FullName);
			}

			return exception as TException;
		}

		private Exception CaptureException()
		{
			Exception exception;

			try
			{
				_ = asyncEnumerableMethod();
				exception = null;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}

		private static async Task<Exception> CaptureExceptionAsync(IAsyncEnumerable<T> asyncIterator)
		{
			Exception exception;

			try
			{
				await foreach (T element in asyncIterator)
				{
					_ = element;
				}
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
