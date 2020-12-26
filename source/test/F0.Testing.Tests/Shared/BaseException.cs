using System;

namespace F0.Tests.Shared
{
	public class BaseException : Exception
	{
		public BaseException(string message)
			: base(message)
		{ }
	}
}
