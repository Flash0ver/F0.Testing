using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class AsyncEnumerableServiceTests
	{
		[Fact]
		public void xUnit_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = Assert.Throws<AsyncImmediateException>(() => service.CreateAsynchronousSequence(true));
			Assert.StartsWith($"{nameof(AsyncImmediateException)}:", exception.Message);
		}

		[Fact]
		public async Task xUnit_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act
			IAsyncEnumerable<int> sequence = service.CreateAsynchronousSequence(false);

			// Assert
			Exception exception = await Assert.ThrowsAsync<AsyncIterateException>(() => sequence.CountAsync().AsTask());
			Assert.StartsWith($"{nameof(AsyncIterateException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateAsynchronousSequence(true)).ThrowsImmediately<AsyncImmediateException>();
			Assert.StartsWith($"{nameof(AsyncImmediateException)}:", exception.Message);
		}

		[Fact]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			var service = new AsyncEnumerableService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateAsynchronousSequence(false)).ThrowsDeferredAsync<AsyncIterateException>();
			Assert.StartsWith($"{nameof(AsyncIterateException)}:", exception.Message);
		}
	}
}
