using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class AsyncEnumerableServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			AsyncEnumerableService service = new();

			// Act and Assert
			Exception exception = Assert.ThrowsException<AsyncImmediateException>(() => service.CreateAsynchronousSequence(true));
			StringAssert.StartsWith(exception.Message, $"{nameof(AsyncImmediateException)}:");
		}

		[TestMethod]
		public async Task MSTestV2_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			AsyncEnumerableService service = new();

			// Act
			IAsyncEnumerable<int> sequence = service.CreateAsynchronousSequence(false);

			// Assert
			Exception exception = await Assert.ThrowsExceptionAsync<AsyncIterateException>(() => sequence.CountAsync().AsTask());
			StringAssert.StartsWith(exception.Message, $"{nameof(AsyncIterateException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheAsyncIteratorIsRetrieved()
		{
			// Arrange
			AsyncEnumerableService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateAsynchronousSequence(true)).ThrowsImmediately<AsyncImmediateException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(AsyncImmediateException)}:");
		}

		[TestMethod]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumeratedAsynchronously()
		{
			// Arrange
			AsyncEnumerableService service = new();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateAsynchronousSequence(false)).ThrowsDeferredAsync<AsyncIterateException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(AsyncIterateException)}:");
		}
	}
}
