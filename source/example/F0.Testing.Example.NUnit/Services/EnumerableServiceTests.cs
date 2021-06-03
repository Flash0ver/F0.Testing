using System;
using System.Collections.Generic;
using System.Linq;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class EnumerableServiceTests
	{
		[Test]
		public void NUnit3_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Assert.Throws<ImmediateException>(() => service.CreateSequence(true));
			StringAssert.StartsWith($"{nameof(ImmediateException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => service.CreateSequence(true),
				Throws.Exception.TypeOf<ImmediateException>()
					.With.Message.StartsWith($"{nameof(ImmediateException)}:"));
		}

		[Test]
		public void NUnit3_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			EnumerableService service = new();

			// Act
			IEnumerable<int> sequence = service.CreateSequence(false);

			// Assert
			Exception exception = Assert.Throws<IterateException>(() => sequence.Count());
			StringAssert.StartsWith($"{nameof(IterateException)}:", exception.Message);

			// ConstraintBasedAssertModel
			Assert.That(() => sequence.Count(),
				Throws.Exception.TypeOf<IterateException>()
					.With.Message.StartsWith($"{nameof(IterateException)}:"));
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(true)).ThrowsImmediately<ImmediateException>();
			StringAssert.StartsWith($"{nameof(ImmediateException)}:", exception.Message);
		}

		[Test]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(false)).ThrowsDeferred<IterateException>();
			StringAssert.StartsWith($"{nameof(IterateException)}:", exception.Message);
		}
	}
}
