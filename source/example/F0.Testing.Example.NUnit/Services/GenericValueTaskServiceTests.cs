using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class GenericValueTaskServiceTests
	{
		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith($"{nameof(SynchronousGenericAllocationFreeException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => service.CreateOperation(true),
				Throws.Exception.TypeOf<SynchronousGenericAllocationFreeException>()
					.With.Message.StartsWith($"{nameof(SynchronousGenericAllocationFreeException)}:"));
		}

		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act
			ValueTask<int> operation = service.CreateOperation(false);

			// Assert
			Exception exception = Assert.ThrowsAsync<AsynchronousGenericAllocationFreeException>(() => operation.AsTask());
			StringAssert.StartsWith($"{nameof(AsynchronousGenericAllocationFreeException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => operation.AsTask(),
				Throws.Exception.TypeOf<AsynchronousGenericAllocationFreeException>()
					.With.Message.StartsWith($"{nameof(AsynchronousGenericAllocationFreeException)}:"));
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousGenericAllocationFreeException>();
			StringAssert.StartsWith($"{nameof(SynchronousGenericAllocationFreeException)}:", exception.Message);
		}

		[Test]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new GenericValueTaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousGenericAllocationFreeException>();
			StringAssert.StartsWith($"{nameof(AsynchronousGenericAllocationFreeException)}:", exception.Message);
		}
	}
}
