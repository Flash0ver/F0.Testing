using System;
using System.Collections.Generic;
using F0.Assertions;

namespace F0.Testing
{
	public static class Test
	{
		public static EnumerableDelegateUnderTest<T> That<T>(Func<IEnumerable<T>> enumerableMethod)
		{
			if (enumerableMethod is null)
			{
				throw new ArgumentNullException(nameof(enumerableMethod));
			}

			return new EnumerableDelegateUnderTest<T>(enumerableMethod);
		}
	}
}
