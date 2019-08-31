using System;
using System.Collections.Generic;

namespace F0.Testing.Example.Services
{
	internal sealed class EnumerableService
	{
		public EnumerableService()
		{
		}

		public IEnumerable<int> CreateSequence(bool throwImmediately)
		{
			if (throwImmediately)
			{
				throw new ImmediateException();
			}

			return GetSequenceEnumerator();

			IEnumerable<int> GetSequenceEnumerator()
			{
				yield return 240;

				throw new IterateException();
			}
		}
	}

	internal sealed class ImmediateException : Exception
	{
		public ImmediateException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(ImmediateException)}: Throw this Exception when the iterator is retrieved.";
		}
	}

	internal sealed class IterateException : Exception
	{
		public IterateException()
			: base(CreateMessage()) { }

		private static string CreateMessage()
		{
			return $"{nameof(IterateException)}: Throw this Exception when the iterator is enumerated.";
		}
	}
}
