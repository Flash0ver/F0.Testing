using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
			Func<IEnumerable<int>> param = null;
			Assert.Throws<ArgumentNullException>("enumerableMethod", () => Test.That(param));

			EnumerableDelegateUnderTest<int> asserter = Test.That(() => Enumerable.Empty<int>());
			Assert.NotNull(asserter);
		}

		[Fact]
		public void CheckAsserterForTask()
		{
			Func<Task> param = null;
			Assert.Throws<ArgumentNullException>("asyncMethod", () => Test.That(param));

			TaskDelegateUnderTest asserter = Test.That(() => Task.CompletedTask);
			Assert.NotNull(asserter);
		}

		[Fact]
		public void CheckAsserterForValueTask()
		{
			Func<ValueTask> param = null;
			Assert.Throws<ArgumentNullException>("asynchronousMethod", () => Test.That(param));

			NonGenericValueTaskDelegateUnderTest asserter = Test.That(() => new ValueTask());
			Assert.NotNull(asserter);
		}

		[Fact]
		public void CheckAsserterForValueTaskT()
		{
			Func<ValueTask<int>> param = null;
			Assert.Throws<ArgumentNullException>("asynchronousMethod", () => Test.That(param));

			GenericValueTaskDelegateUnderTest<int> asserter = Test.That(() => new ValueTask<int>());
			Assert.NotNull(asserter);
		}
	}
}
