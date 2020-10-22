using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using F0.Exceptions;
using F0.Extensions;

namespace F0.Assertions
{
	public sealed class AssemblyUnderTest
	{
		private readonly Assembly assembly;

		internal AssemblyUnderTest(Assembly assembly)
		{
			this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
		}

		public TAttribute HasAttribute<TAttribute>()
			where TAttribute : Attribute
		{
			TAttribute attribute = null;

			try
			{
				attribute = assembly.GetCustomAttribute<TAttribute>();
			}
			catch (AmbiguousMatchException ex)
			{
				string expectedMessage = $"{typeof(TAttribute)}";
				string actualMessage = $"(Multiple custom attributes of the same specified type are applied to assembly '{assembly.FullName}')";
				AssertionFailedException.Throw(nameof(HasAttribute), expectedMessage, actualMessage, ex);
			}

			if (attribute is null)
			{
				string expectedMessage = $"{typeof(TAttribute)}";
				string actualMessage = $"(A custom attribute of the specified type is not applied to assembly '{assembly.FullName}')";
				AssertionFailedException.Throw(nameof(HasAttribute), expectedMessage, actualMessage);
			}

			return attribute;
		}

		public IEnumerable<TAttribute> HasAttributes<TAttribute>()
			where TAttribute : Attribute
		{
			IEnumerable<TAttribute> attributes = assembly.GetCustomAttributes<TAttribute>();

			if (!attributes.Any())
			{
				string expectedMessage = $"{typeof(TAttribute)}";
				string actualMessage = $"(No custom attributes of the specified type are applied to assembly '{assembly.FullName}')";
				AssertionFailedException.Throw(nameof(HasAttributes), expectedMessage, actualMessage);
			}

			if (attributes.HasExactlyOne())
			{
				string expectedMessage = $"{typeof(TAttribute)}";
				string actualMessage = $"(Only one custom attribute of the specified type is applied to assembly '{assembly.FullName}')";
				AssertionFailedException.Throw(nameof(HasAttributes), expectedMessage, actualMessage);
			}

			return attributes;
		}

		public TAttribute[] HasAttributes<TAttribute>(int expectedCount)
			where TAttribute : Attribute
		{
			if (expectedCount < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, $"The {nameof(expectedCount)} argument cannot be a negative number.");
			}

			IEnumerable<TAttribute> attributes = assembly.GetCustomAttributes<TAttribute>();

			TAttribute[] array = attributes.ToArray();

			int actualLength = array.Length;
			if (expectedCount != actualLength)
			{
				string expectedMessage = $"Number of {typeof(TAttribute)}: {expectedCount} (Target: '{assembly.FullName}')";
				string actualMessage = $"Number of {typeof(TAttribute)}: {actualLength} (Target: '{assembly.FullName}')";
				AssertionFailedException.Throw($"{nameof(HasAttributes)}({nameof(Int32)})", expectedMessage, actualMessage);
			}

			return array;
		}

		public TAttribute[] HasAttributes<TAttribute>(long expectedLongCount)
			where TAttribute : Attribute
		{
			if (expectedLongCount < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(expectedLongCount), expectedLongCount, $"The {nameof(expectedLongCount)} argument cannot be a negative number.");
			}

			IEnumerable<TAttribute> attributes = assembly.GetCustomAttributes<TAttribute>();

			TAttribute[] array = attributes.ToArray();

			long actualLongLength = array.LongLength;
			if (expectedLongCount != actualLongLength)
			{
				string expectedMessage = $"Total number of {typeof(TAttribute)}: {expectedLongCount} (Target: '{assembly.FullName}')";
				string actualMessage = $"Total number of {typeof(TAttribute)}: {actualLongLength} (Target: '{assembly.FullName}')";
				AssertionFailedException.Throw($"{nameof(HasAttributes)}({nameof(Int64)})", expectedMessage, actualMessage);
			}

			return array;
		}

		public Version HasAssemblyVersion(Version expected)
		{
			Version actual = assembly.GetName().Version;

			if (!actual.Equals(expected))
			{
				string expectedMessage = expected is null ? "null" : expected.ToString();
				string actualMessage = actual.ToString();
				AssertionFailedException.Throw(nameof(HasAssemblyVersion), expectedMessage, actualMessage);
			}

			return actual;
		}
	}
}
