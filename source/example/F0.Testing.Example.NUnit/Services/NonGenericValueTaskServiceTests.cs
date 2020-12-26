using System;
using System.Threading.Tasks;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class NonGenericValueTaskServiceTests
	{
		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new NonGenericValueTaskService();

			// Act and Assert
			Exception exception = Assert.Throws<SynchronousNonGenericAllocationFreeException>(() => { _ = service.CreateOperation(true); });
			StringAssert.StartsWith($"{nameof(SynchronousNonGenericAllocationFreeException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => service.CreateOperation(true),
				Throws.Exception.TypeOf<SynchronousNonGenericAllocationFreeException>()
					.With.Message.StartsWith($"{nameof(SynchronousNonGenericAllocationFreeException)}:"));
		}

		[Test]
		public void NUnit3_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new NonGenericValueTaskService();

			// Act
			ValueTask operation = service.CreateOperation(false);

			// Assert
			Exception exception = Assert.ThrowsAsync<AsynchronousNonGenericAllocationFreeException>(() => operation.AsTask());
			StringAssert.StartsWith($"{nameof(AsynchronousNonGenericAllocationFreeException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => operation.AsTask(),
				Throws.Exception.TypeOf<AsynchronousNonGenericAllocationFreeException>()
					.With.Message.StartsWith($"{nameof(AsynchronousNonGenericAllocationFreeException)}:"));
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheAsynchronousOperationIsRetrieved()
		{
			// Arrange
			var service = new NonGenericValueTaskService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateOperation(true)).ThrowsSynchronously<SynchronousNonGenericAllocationFreeException>();
			StringAssert.StartsWith($"{nameof(SynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}

		[Test]
		public async Task Explicitly_AssertThatExceptionsForAsynchronousMethodsSurface_WhenTheReturnedValueTaskIsAwaited()
		{
			// Arrange
			var service = new NonGenericValueTaskService();

			// Act and Assert
			Exception exception = await Test.That(() => service.CreateOperation(false)).ThrowsAsynchronously<AsynchronousNonGenericAllocationFreeException>();
			StringAssert.StartsWith($"{nameof(AsynchronousNonGenericAllocationFreeException)}:", exception.Message);
		}
	}
}
