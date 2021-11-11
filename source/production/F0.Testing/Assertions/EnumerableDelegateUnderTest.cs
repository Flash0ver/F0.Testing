using System;
using System.Collections.Generic;
using F0.Exceptions;

namespace F0.Assertions
{
	public sealed class EnumerableDelegateUnderTest<T>
	{
		private readonly Func<IEnumerable<T>> enumerableMethod;

		internal EnumerableDelegateUnderTest(Func<IEnumerable<T>> enumerableMethod)
		{
			this.enumerableMethod = enumerableMethod ?? throw new ArgumentNullException(nameof(enumerableMethod));
		}

		public TException ThrowsImmediately<TException>()
			where TException : Exception
		{
			Exception? exception = CaptureException();

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsImmediately), typeof(TException).FullName!, "(No exception was thrown when retrieving the iterator)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsImmediately), typeof(TException).FullName!, exception.GetType().FullName!);
			}

			return (exception as TException)!;
		}

		public TException ThrowsDeferred<TException>()
			where TException : Exception
		{
			IEnumerable<T>? iterator;

			try
			{
				iterator = enumerableMethod();
			}
			catch (Exception e)
			{
				iterator = null;

				AssertionFailedException.Throw(nameof(ThrowsDeferred), typeof(TException).FullName!, $"(An exception was thrown before iteration over the sequence: '{e.GetType()}')");
			}

			Exception? exception = CaptureException(iterator);

			if (exception is null)
			{
				AssertionFailedException.Throw(nameof(ThrowsDeferred), typeof(TException).FullName!, "(No exception was thrown during iteration over the sequence)");
			}
			else if (exception.GetType() != typeof(TException))
			{
				AssertionFailedException.Throw(nameof(ThrowsDeferred), typeof(TException).FullName!, exception.GetType().FullName!);
			}

			return (exception as TException)!;
		}

		private Exception? CaptureException()
		{
			Exception? exception;

			try
			{
				_ = enumerableMethod();
				exception = null;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}

		private static Exception? CaptureException(IEnumerable<T> iterator)
		{
			Exception? exception;

			try
			{
				foreach (T element in iterator)
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
