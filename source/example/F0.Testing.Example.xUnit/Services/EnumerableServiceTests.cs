using System;
using System.Collections.Generic;
using System.Linq;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class EnumerableServiceTests
	{
		[Fact]
		public void xUnit_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			var service = new EnumerableService();

			// Act and Assert
			Exception exception = Assert.Throws<ImmediateException>(() => service.CreateSequence(true));
			Assert.StartsWith($"{nameof(ImmediateException)}:", exception.Message);
		}

		[Fact]
		public void xUnit_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			var service = new EnumerableService();

			// Act
			IEnumerable<int> sequence = service.CreateSequence(false);

			// Assert
			Exception exception = Assert.Throws<IterateException>(() => sequence.Count());
			Assert.StartsWith($"{nameof(IterateException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			var service = new EnumerableService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(true)).ThrowsImmediately<ImmediateException>();
			Assert.StartsWith($"{nameof(ImmediateException)}:", exception.Message);
		}

		[Fact]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			var service = new EnumerableService();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(false)).ThrowsDeferred<IterateException>();
			Assert.StartsWith($"{nameof(IterateException)}:", exception.Message);
		}
	}
}
