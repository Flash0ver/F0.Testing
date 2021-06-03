using System;
using System.Collections.Generic;
using System.Linq;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class EnumerableServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Assert.ThrowsException<ImmediateException>(() => service.CreateSequence(true));
			StringAssert.StartsWith(exception.Message, $"{nameof(ImmediateException)}:");
		}

		[TestMethod]
		public void MSTestV2_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			EnumerableService service = new();

			// Act
			IEnumerable<int> sequence = service.CreateSequence(false);

			// Assert
			Exception exception = Assert.ThrowsException<IterateException>(() => sequence.Count());
			StringAssert.StartsWith(exception.Message, $"{nameof(IterateException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheIteratorIsRetrieved()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(true)).ThrowsImmediately<ImmediateException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(ImmediateException)}:");
		}

		[TestMethod]
		public void Explicitly_AssertThatExceptionsForMethodIteratorsSurface_WhenTheReturnedSequenceIsEnumerated()
		{
			// Arrange
			EnumerableService service = new();

			// Act and Assert
			Exception exception = Test.That(() => service.CreateSequence(false)).ThrowsDeferred<IterateException>();
			StringAssert.StartsWith(exception.Message, $"{nameof(IterateException)}:");
		}
	}
}
