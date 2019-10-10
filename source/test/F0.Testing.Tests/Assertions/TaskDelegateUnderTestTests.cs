using System;
using System.Threading.Tasks;
using F0.Assertions;
using F0.Exceptions;
using F0.Tests.Shared;
using Xunit;

namespace F0.Tests.Assertions
{
	public class TaskDelegateUnderTestTests
	{
		[Fact]
		public void Ctor_MethodUnderTest_CannotBeNull()
		{
			Assert.Throws<ArgumentNullException>("asyncMethod", () => new TaskDelegateUnderTest(null));
		}

		[Fact]
		public void ThrowsSynchronously_TesteeDoesNotThrowAnyExceptionSynchronously_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(SuccessfullyCompleteSynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "F0.Tests.Shared.DerivedException", "(No exception was observed synchronously)");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeDoesNotThrowAnyExceptionAsynchronously_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(SuccessfullyCompleteAsynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "F0.Tests.Shared.DerivedException", "(No exception was observed synchronously)");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsSynchronouslyExactException_PassedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = assertor.ThrowsSynchronously<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowSynchronously)}", exception.Message);
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsSynchronouslyDerivedException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "F0.Tests.Shared.BaseException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsSynchronouslyDifferentException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "System.ArgumentException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsAsynchronouslyExactException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "F0.Tests.Shared.DerivedException", "(No exception was observed synchronously)");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsAsynchronouslyDerivedException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "F0.Tests.Shared.BaseException", "(No exception was observed synchronously)");
		}

		[Fact]
		public void ThrowsSynchronously_TesteeThrowsAsynchronouslyDifferentException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsSynchronously<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsSynchronously", "System.ArgumentException", "(No exception was observed synchronously)");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeDoesNotThrowAnyExceptionSynchronously_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(SuccessfullyCompleteSynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "F0.Tests.Shared.DerivedException", "(No exception was observed asynchronously)");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeDoesNotThrowAnyExceptionAsynchronously_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(SuccessfullyCompleteAsynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "F0.Tests.Shared.DerivedException", "(No exception was observed asynchronously)");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsSynchronouslyExactException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "F0.Tests.Shared.DerivedException", "(An exception was thrown synchronously: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsSynchronouslyDerivedException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "F0.Tests.Shared.BaseException", "(An exception was thrown synchronously: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsSynchronouslyDifferentException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowSynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "System.ArgumentException", "(An exception was thrown synchronously: 'F0.Tests.Shared.DerivedException')");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsAsynchronouslyExactException_PassedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = await assertor.ThrowsAsynchronously<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowAsynchronously)}", exception.Message);
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsAsynchronouslyDerivedException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "F0.Tests.Shared.BaseException", "F0.Tests.Shared.DerivedException");
		}

		[Fact]
		public async Task ThrowsAsynchronously_TesteeThrowsAsynchronouslyDifferentException_FailedAssertion()
		{
			var assertor = new TaskDelegateUnderTest(ThrowAsynchronously);

			Exception exception = await Assert.ThrowsAsync<AssertionFailedException>(() => assertor.ThrowsAsynchronously<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsAsynchronously", "System.ArgumentException", "F0.Tests.Shared.DerivedException");
		}

		private static Task SuccessfullyCompleteSynchronously()
		{
			return Task.CompletedTask;
		}

		private static async Task SuccessfullyCompleteAsynchronously()
		{
			await Task.Yield();
		}

		private static Task ThrowSynchronously()
		{
			throw new DerivedException(nameof(ThrowSynchronously));
		}

		private static async Task ThrowAsynchronously()
		{
			await Task.Yield();

			throw new DerivedException(nameof(ThrowAsynchronously));
		}

		private static void CheckExceptionMessage(Exception actualException, string assertionName, string expectedText, string actualText)
		{
			string expectedMessage = CreateErrorMessage(assertionName, expectedText, actualText);
			Assert.Equal(expectedMessage, actualException.Message);
		}

		private static string CreateErrorMessage(string assertionName, string expectedText, string actualText)
		{
			string errorMessage = $"'{assertionName}' failed."
				+ Environment.NewLine + $"   Expected: {expectedText}"
				+ Environment.NewLine + $"   Actual:   {actualText}";
			return errorMessage;
		}
	}
}
