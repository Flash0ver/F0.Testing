using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class NonGenericValueTaskServiceTests
	{
		[Fact]
		public void xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousNonGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			Assert.StartsWith($"{nameof(SynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public async Task xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act
			ValueTask operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsAsync<AsynchronousNonGenericAllocationFreeException>(() => operation.AsTask());
			Assert.StartsWith($"{nameof(AsynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousNonGenericAllocationFreeException>();
			Assert.StartsWith($"{nameof(SynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			NonGenericValueTaskService service = new();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousNonGenericAllocationFreeException>();
			Assert.StartsWith($"{nameof(AsynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}
	}
}
