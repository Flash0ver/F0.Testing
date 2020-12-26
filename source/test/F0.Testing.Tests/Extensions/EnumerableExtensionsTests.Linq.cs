using System.Collections.Generic;
using System.Linq;
using F0.Extensions;
using Xunit;

namespace F0.Tests.Extensions
{
	public class EnumerableExtensionsTests
	{
		[Fact]
		public void HasExactlyOne_SequenceContainsNoElements_False()
		{
			IEnumerable<int> sequence = Enumerable.Range(0, 0);

			bool hasExactlyOne = sequence.HasExactlyOne();

			Assert.False(hasExactlyOne);
		}

		[Fact]
		public void HasExactlyOne_SequenceContainsOneSingleElement_True()
		{
			IEnumerable<int> sequence = Enumerable.Range(0, 1);

			bool hasExactlyOne = sequence.HasExactlyOne();

			Assert.True(hasExactlyOne);
		}

		[Fact]
		public void HasExactlyOne_SequenceContainsMoreThanOneElement_False()
		{
			IEnumerable<int> sequence = Enumerable.Range(0, 2);

			bool hasExactlyOne = sequence.HasExactlyOne();

			Assert.False(hasExactlyOne);
		}

		[Fact]
		public void HasExactlyOne_WithPredicate_SequenceContainsNoMatchingElement_False()
		{
			IEnumerable<int> sequence = Enumerable.Range(1, 1);

			bool hasExactlyOne = sequence.HasExactlyOne(i => i % 2 == 0);

			Assert.False(hasExactlyOne);
		}

		[Fact]
		public void HasExactlyOne_WithPredicate_SequenceContainsOneSingleMatchingElement_True()
		{
			IEnumerable<int> sequence = Enumerable.Range(1, 2);

			bool hasExactlyOne = sequence.HasExactlyOne(i => i % 2 == 0);

			Assert.True(hasExactlyOne);
		}

		[Fact]
		public void HasExactlyOne_WithPredicate_SequenceContainsMoreThanOneMatchingElement_False()
		{
			IEnumerable<int> sequence = Enumerable.Range(2, 3);

			bool hasExactlyOne = sequence.HasExactlyOne(i => i % 2 == 0);

			Assert.False(hasExactlyOne);
		}
	}
}
