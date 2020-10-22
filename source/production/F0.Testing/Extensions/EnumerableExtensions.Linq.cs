using System;
using System.Collections.Generic;

namespace F0.Extensions
{
	internal static class EnumerableExtensions
	{
		internal static bool HasExactlyOne<T>(this IEnumerable<T> source)
		{
			bool hasOne = false;

			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (hasOne)
					{
						hasOne = false;
						break;
					}
					else
					{
						hasOne = true;
					}
				}
			}

			return hasOne;
		}

		internal static bool HasExactlyOne<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			bool hasOne = false;

			foreach (T element in source)
			{
				if (!predicate(element))
				{
					continue;
				}

				if (hasOne)
				{
					hasOne = false;
					break;
				}
				else
				{
					hasOne = true;
				}
			}

			return hasOne;
		}
	}
}
