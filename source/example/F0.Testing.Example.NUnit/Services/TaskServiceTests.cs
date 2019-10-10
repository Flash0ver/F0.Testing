using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class TaskServiceTests
	{
		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith($"{nameof(SynchronousException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => { _ = service.CreateOperation(true); },
				Throws.Exception.TypeOf<SynchronousException>()
					.With.Message.StartsWith($"{nameof(SynchronousException)}:"));
		}

		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act
			Task<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = Assert.ThrowsAsync<AsynchronousException>(() => operation);
			StringAssert.StartsWith($"{nameof(AsynchronousException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => operation,
				Throws.Exception.TypeOf<AsynchronousException>()
					.With.Message.StartsWith($"{nameof(AsynchronousException)}:"));
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousException>();
			StringAssert.StartsWith($"{nameof(SynchronousException)}:", exception.Message);
		}

		[Test]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedTaskIsAwaited()
		{
			// Arrange
			var service = new TaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousException>();
			StringAssert.StartsWith($"{nameof(AsynchronousException)}:", exception.Message);
		}
	}
}
