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

		private static string CreateErrorMessage(string assertionName, string expectedText, string actualText)
		{
			string errorMessage = $"'{assertionName}' failed."
				+ Environment.NewLine + $"   Expected: {expectedText}"
				+ Environment.NewLine + $"   Actual:   {actualText}";
			return errorMessage;
		}
	}
}
