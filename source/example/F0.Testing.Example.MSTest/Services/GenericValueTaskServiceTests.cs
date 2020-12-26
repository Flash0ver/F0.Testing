using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class GenericValueTaskServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Assert.ThrowsException<SynchronousGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public async Task MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act
			ValueTask<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsExceptionAsync<AsynchronousGenericAllocationFreeException>(() => operation.AsTask());
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousGenericAllocationFreeException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousGenericAllocationFreeException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousGenericAllocationFreeException)}:");
		}
	}
}
