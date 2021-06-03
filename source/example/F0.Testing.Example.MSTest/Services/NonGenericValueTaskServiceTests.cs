using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class NonGenericValueTaskServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = Assert.ThrowsException<SynchronousNonGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousNonGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public async Task MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act
			ValueTask operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsExceptionAsync<AsynchronousNonGenericAllocationFreeException>(() => operation.AsTask());
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousNonGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousNonGenericAllocationFreeException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousNonGenericAllocationFreeException)}:");
		}

		[TestMethod]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousNonGenericAllocationFreeException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousNonGenericAllocationFreeException)}:");
		}
	}
}
