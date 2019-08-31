using System;
using System.Runtime.Serialization;

namespace F0.Exceptions
{
	[Serializable]
	public class AssertionFailedException : Exception
	{
		internal static void Throw(string assertionName, string expectedMessage, string actualMessage)
		{
			const string dotnet_test_error_message_indentation = "   ";

			string message = $"'{assertionName}' failed."
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Expected: {expectedMessage}"
				+ Environment.NewLine + dotnet_test_error_message_indentation + $"Actual:   {actualMessage}";
			throw new AssertionFailedException(message);
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
