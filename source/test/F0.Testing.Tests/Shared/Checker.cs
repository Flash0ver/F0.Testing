using System;
using Xunit;

namespace F0.Tests.Shared
{
	internal static class Checker
	{
		internal static void CheckExceptionMessage(Exception actualException, string assertionName, string expectedText, string actualText)
		{
			string expectedMessage = CreateErrorMessage(assertionName, expectedText, actualText);
			Assert.Equal(expectedMessage, actualException.Message);
		}

		internal static void CheckExceptionMessage(Exception actualException, string assertionName, string expectedText, string actualText, Exception innerException)
		{
			string expectedMessage = CreateErrorMessage(assertionName, expectedText, actualText, innerException);
			Assert.Equal(expectedMessage, actualException.Message);
		}

		private static string CreateErrorMessage(string assertionName, string expectedText, string actualText)
		{
			string errorMessage = $"'{assertionName}' failed."
				+ Environment.NewLine + $"   Expected: {expectedText}"
				+ Environment.NewLine + $"   Actual:   {actualText}";
			return errorMessage;
		}

		private static string CreateErrorMessage(string assertionName, string expectedText, string actualText, Exception innerException)
		{
			string errorMessage = $"'{assertionName}' failed. ({innerException.Message})"
				+ Environment.NewLine + $"   Expected: {expectedText}"
				+ Environment.NewLine + $"   Actual:   {actualText}"
				+ Environment.NewLine + $"   Inner --> {innerException}";
			return errorMessage;
		}
	}
}
