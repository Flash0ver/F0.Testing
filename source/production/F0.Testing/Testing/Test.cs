using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

		public static TaskDelegateUnderTest That(Func<Task> asyncMethod)
		{
			if (asyncMethod is null)
			{
				throw new ArgumentNullException(nameof(asyncMethod));
			}

			return new TaskDelegateUnderTest(asyncMethod);
		}

		public static NonGenericValueTaskDelegateUnderTest That(Func<ValueTask> asynchronousMethod)
		{
			if (asynchronousMethod is null)
			{
				throw new ArgumentNullException(nameof(asynchronousMethod));
			}

			return new NonGenericValueTaskDelegateUnderTest(asynchronousMethod);
		}

		public static GenericValueTaskDelegateUnderTest<T> That<T>(Func<ValueTask<T>> asynchronousMethod)
		{
			if (asynchronousMethod is null)
			{
				throw new ArgumentNullException(nameof(asynchronousMethod));
			}

			return new GenericValueTaskDelegateUnderTest<T>(asynchronousMethod);
		}
	}
}
