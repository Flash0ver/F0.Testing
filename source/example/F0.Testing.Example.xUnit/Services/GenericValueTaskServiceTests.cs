using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class GenericValueTaskServiceTests
	{
		[Fact]
		public void xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			Assert.StartsWith($"{nameof(SynchronousGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public async Task xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act
			ValueTask<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsAsync<AsynchronousGenericAllocationFreeException>(() => operation.AsTask());
			Assert.StartsWith($"{nameof(AsynchronousGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousGenericAllocationFreeException>();
			Assert.StartsWith($"{nameof(SynchronousGenericAllocationFreeException)}:", exception.Message);
		}

		[Fact]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousGenericAllocationFreeException>();
			Assert.StartsWith($"{nameof(AsynchronousGenericAllocationFreeException)}:", exception.Message);
		}
	}
}
