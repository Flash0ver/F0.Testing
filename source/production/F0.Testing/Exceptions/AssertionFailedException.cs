using System;
using System.Runtime.Serialization;

namespace F0.Exceptions
{
	[Serializable]
	public class AssertionFailedException : Exception
	{
		private const string dotnet_test_error_message_indentation = "   ";

		internal static void Throw(string assertionName, string expectedMessage, string actualMessage)
		{
			string message = $"'{assertionName}' failed."
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Expected: {expectedMessage}"
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Actual:   {actualMessage}";
			throw new AssertionFailedException(message);
		}

		internal static void Throw(string assertionName, string expectedMessage, string actualMessage, Exception innerException)
		{
			string message = $"'{assertionName}' failed. ({innerException.Message})"
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Expected: {expectedMessage}"
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Actual:   {actualMessage}"
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Inner --> {innerException.GetType()}: {innerException.Message}";
			throw new AssertionFailedException(message, innerException);
		}

		public AssertionFailedException()
		{ }

		public AssertionFailedException(string message)
			: base(message)
		{ }

		public AssertionFailedException(string message, Exception inner)
			: base(message, inner)
		{ }

		protected AssertionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}
}
