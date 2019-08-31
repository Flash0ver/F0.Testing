using System;
using System.Linq;
using F0.Assertions;
using F0.Testing;
using Xunit;

namespace F0.Tests.Testing
{
	public class TestTests
	{
		[Fact]
		public void CheckAsserterForEnumerable()
		{
			Assert.Throws<ArgumentNullException>("enumerableMethod", () => Test.That<int>(null));

			EnumerableDelegateUnderTest<int> asserter = Test.That(() => Enumerable.Empty<int>());
			Assert.NotNull(asserter);
		}
	}
}
