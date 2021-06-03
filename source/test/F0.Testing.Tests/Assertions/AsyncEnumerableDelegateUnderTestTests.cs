using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Assertions;
using F0.Exceptions;
using F0.Tests.Shared;
using Xunit;

namespace F0.Tests.Assertions
{
	public class AsyncEnumerableDelegateUnderTestTests
	{
		[Fact]
		public void Ctor_MethodUnderTest_CannotBeNull()
		{
			Assert.Throws<ArgumentNullException>("asyncEnumerableMethod", () => new AsyncEnumerableDelegateUnderTest<object>(null));
		}

		[Fact]
		public void ThrowsImmediately_TesteeDoesNotThrowAnyExceptionSynchronously_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(GetItemsSynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Shared.DerivedException", "(No exception was thrown when retrieving the asynchronous iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeDoesNotThrowAnyExceptionAsynchronously_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(GetItemsAsync);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Shared.DerivedException", "(No exception was thrown when retrieving the asynchronous iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyExactException_PassedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = assertor.ThrowsImmediately<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowImmediately)}", exception.Message);
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyDerivedException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Shared.BaseException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyDifferentException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "System.ArgumentException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredAsynchronouslyExactException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Shared.DerivedException", "(No exception was thrown when retrieving the asynchronous iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredAsynchronouslyDerivedException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Shared.BaseException", "(No exception was thrown when retrieving the asynchronous iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredAsynchronouslyDifferentException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsImmediately", "System.ArgumentException", "(No exception was thrown when retrieving the asynchronous iterator)");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeDoesNotThrowAnyExceptionSynchronously_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(GetItemsSynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "F0.Tests.Shared.DerivedException", "(No exception was thrown during iteration over the asynchronous stream)");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeDoesNotThrowAnyExceptionAsynchronously_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(GetItemsAsync);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "F0.Tests.Shared.DerivedException", "(No exception was thrown during iteration over the asynchronous stream)");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsImmediatelyExactException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "F0.Tests.Shared.DerivedException", "(An exception was thrown before iteration over the asynchronous stream: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsImmediatelyDerivedException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "F0.Tests.Shared.BaseException", "(An exception was thrown before iteration over the asynchronous stream: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsImmediatelyDifferentException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowImmediately);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "System.ArgumentException", "(An exception was thrown before iteration over the asynchronous stream: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsDeferredAsynchronouslyExactException_PassedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = await assertor.ThrowsDeferredAsync<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowDeferredAsync)}", exception.Message);
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsDeferredAsynchronouslyDerivedException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "F0.Tests.Shared.BaseException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public async Task ThrowsDeferredAsync_TesteeThrowsDeferredAsynchronouslyDifferentException_FailedAssertion()
		{
			AsyncEnumerableDelegateUnderTest<int> assertor = new(ThrowDeferredAsync);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsDeferredAsync<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			Checker.CheckExceptionMessage(exception, "ThrowsDeferredAsync", "System.ArgumentException", "F0.Tests.Shared.DerivedException");
		}

		private static IAsyncEnumerable<int> GetItemsSynchronously()
		{
			return AsyncEnumerable.Empty<int>();
		}

		private static async IAsyncEnumerable<int> GetItemsAsync()
		{
			await Task.Yield();

			yield break;
		}

		private static IAsyncEnumerable<int> ThrowImmediately()
		{
			throw new DerivedException(nameof(ThrowImmediately));
		}

		private static async IAsyncEnumerable<int> ThrowDeferredAsync()
		{
			await Task.Yield();
			yield return 240;

			throw new DerivedException(nameof(ThrowDeferredAsync));
		}
	}
}
