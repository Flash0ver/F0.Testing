using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class TaskServiceTests
	{
		[Fact]
		public void xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousException>(() => { _ = service.CreateOperation(true); });
			Assert.StartsWith($"{nameof(SynchronousException)}:", exception.Message);
		}

		[Fact]
		public async Task xUnit_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act
			Task<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsAsync<AsynchronousException>(() => operation);
			Assert.StartsWith($"{nameof(AsynchronousException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousException>();
			Assert.StartsWith($"{nameof(SynchronousException)}:", exception.Message);
		}

		[Fact]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousException>();
			Assert.StartsWith($"{nameof(AsynchronousException)}:", exception.Message);
		}
	}
}
