using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class AsyncEnumerableServiceTests
	{
		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = Assert.Throws<AsyncImmediateException>(() => service.CreateAsynchronousSequence(true));
			StringAssert.StartsWith($"{nameof(AsyncImmediateException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => service.CreateAsynchronousSequence(true),
				Throws.Exception.TypeOf<AsyncImmediateException>()
					.With.Message.StartsWith($"{nameof(AsyncImmediateException)}:"));
		}

		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act
			IAsyncEnumerable<int> sequence = service.CreateAsynchronousSequence(false);

			// Assert
			Exception exception = Assert.ThrowsAsync<AsyncIterateException>(() => sequence.CountAsync().AsTask());
			StringAssert.StartsWith($"{nameof(AsyncIterateException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => sequence.CountAsync().AsTask(),
				Throws.Exception.TypeOf<AsyncIterateException>()
					.With.Message.StartsWith($"{nameof(AsyncIterateException)}:"));
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateAsynchronousSequence(true)).ThrowsImmediately<AsyncImmediateException>();
			StringAssert.StartsWith($"{nameof(AsyncImmediateException)}:", exception.Message);
		}

		[Test]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateAsynchronousSequence(false)).ThrowsDeferredAsync<AsyncIterateException>();
			StringAssert.StartsWith($"{nameof(AsyncIterateException)}:", exception.Message);
		}
	}
}
