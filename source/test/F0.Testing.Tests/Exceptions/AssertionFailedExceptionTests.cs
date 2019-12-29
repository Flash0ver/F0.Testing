using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using F0.Exceptions;
using Xunit;

namespace F0.Tests.Exceptions
{
	public class AssertionFailedExceptionTests
	{
		[Fact]
		public void Throw()
		{
			Exception exception = Assert.Throws<AssertionFailedException>(() => AssertionFailedException.Throw("1", "2", "3"));

			string message = "'1' failed."
				+ Environment.NewLine + "   Expected: 2"
				+ Environment.NewLine + "   Actual:   3";
			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public void Constructor_Parameterless()
		{
			var exception = new AssertionFailedException();

			Assert.Equal($"Exception of type '{typeof(AssertionFailedException)}' was thrown.", exception.Message);
			Assert.Null(exception.InnerException);
		}

		[Fact]
		public void Constructor_Message()
		{
			string message = "240";
			var exception = new AssertionFailedException(message);

			Assert.Equal(message, exception.Message);
			Assert.Null(exception.InnerException);
		}

		[Fact]
		public void Constructor_InnerException()
		{
			Exception innerException = new ArgumentException("F0");
			var exception = new AssertionFailedException("240", innerException);

			Assert.Equal("240", exception.Message);
			Assert.Same(innerException, exception.InnerException);
		}

		[Fact]
		public void Constructor_Deserialization_And_TypeIsMarkedAsSerializable()
		{
			var original = new AssertionFailedException("240", new ArgumentException("F0"));

			AssertionFailedException roundtrip = RoundTrip(original);

			Assert.NotSame(original, roundtrip);
			Assert.Equal("240", roundtrip.Message);
			Assert.IsType<ArgumentException>(roundtrip.InnerException);
			Assert.Equal("F0", roundtrip.InnerException.Message);
		}

		private static AssertionFailedException RoundTrip(AssertionFailedException exception)
		{
			using Stream stream = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();

			formatter.Serialize(stream, exception);
			stream.Seek(0, SeekOrigin.Begin);

			return (AssertionFailedException)formatter.Deserialize(stream);
		}
	}
}
