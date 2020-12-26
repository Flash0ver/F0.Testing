using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class TaskServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Assert.ThrowsException<SynchronousException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousException)}:");
		}

		[TestMethod]
		public async Task MSTestV2_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act
			Task<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = await Assert.ThrowsExceptionAsync<AsynchronousException>(() => operation);
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(SynchronousException)}:");
		}

		[TestMethod]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(AsynchronousException)}:");
		}
	}
}
