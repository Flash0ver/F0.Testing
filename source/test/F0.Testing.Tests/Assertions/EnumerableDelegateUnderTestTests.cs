﻿using System;
using System.Collections.Generic;
using System.Linq;
using F0.Assertions;
using F0.Exceptions;
using Xunit;

namespace F0.Tests.Assertions
{
	public class EnumerableDelegateUnderTestTests
	{
		[Fact]
		public void Ctor_MethodUnderTest_CannotBeNull()
		{
			Assert.Throws<ArgumentNullException>("enumerableMethod", () => new EnumerableDelegateUnderTest<object>(null));
		}

		[Fact]
		public void ThrowsImmediately_TesteeDoesNotThrowAnyException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(GetSequence);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Assertions.DerivedException", "(No exception was thrown when retrieving the iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyExactException_PassedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = assertor.ThrowsImmediately<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowImmediately)}", exception.Message);
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyDerivedException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Assertions.BaseException", "F0.Tests.Assertions.DerivedException");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsImmediatelyDifferentException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "System.ArgumentException", "F0.Tests.Assertions.DerivedException");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredExactException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Assertions.DerivedException", "(No exception was thrown when retrieving the iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredDerivedException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "F0.Tests.Assertions.BaseException", "(No exception was thrown when retrieving the iterator)");
		}

		[Fact]
		public void ThrowsImmediately_TesteeThrowsDeferredDifferentException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsImmediately<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsImmediately", "System.ArgumentException", "(No exception was thrown when retrieving the iterator)");
		}

		[Fact]
		public void ThrowsDeferred_TesteeDoesNotThrowAnyException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(GetSequence);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "F0.Tests.Assertions.DerivedException", "(No exception was thrown during iteration over the sequence)");
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsImmediatelyExactException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<DerivedException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "F0.Tests.Assertions.DerivedException", "(An exception was thrown before iteration over the sequence: 'F0.Tests.Assertions.DerivedException')");
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsImmediatelyDerivedException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "F0.Tests.Assertions.BaseException", "(An exception was thrown before iteration over the sequence: 'F0.Tests.Assertions.DerivedException')");
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsImmediatelyDifferentException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowImmediately);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "System.ArgumentException", "(An exception was thrown before iteration over the sequence: 'F0.Tests.Assertions.DerivedException')");
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsDeferredExactException_PassedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = assertor.ThrowsDeferred<DerivedException>();

			Assert.IsType<DerivedException>(exception);
			Assert.Equal($"{nameof(ThrowDeferred)}", exception.Message);
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsDeferredDerivedException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<BaseException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "F0.Tests.Assertions.BaseException", "F0.Tests.Assertions.DerivedException");
		}

		[Fact]
		public void ThrowsDeferred_TesteeThrowsDeferredDifferentException_FailedAssertion()
		{
			var assertor = new EnumerableDelegateUnderTest<int>(ThrowDeferred);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.ThrowsDeferred<ArgumentException>());

			Assert.IsType<AssertionFailedException>(exception);
			CheckExceptionMessage(exception, "ThrowsDeferred", "System.ArgumentException", "F0.Tests.Assertions.DerivedException");
		}

		public static IEnumerable<int> GetSequence()
		{
			return Enumerable.Empty<int>();
		}

		private static IEnumerable<int> ThrowImmediately()
		{
			throw new DerivedException(nameof(ThrowImmediately));
		}

		private static IEnumerable<int> ThrowDeferred()
		{
			yield return 240;

			throw new DerivedException(nameof(ThrowDeferred));
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

	public class BaseException : Exception
	{
		public BaseException(string message)
			: base(message)
		{ }
	}

	public class DerivedException : BaseException
	{
		public DerivedException(string message)
			: base(message)
		{ }
	}
}