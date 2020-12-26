using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using F0.Assertions;

namespace F0.Testing
{
	public static class Test
	{
		public static AssemblyUnderTest That(Assembly assembly)
		{
			if (assembly is null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			return new AssemblyUnderTest(assembly);
		}

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

		public static AsyncEnumerableDelegateUnderTest<T> That<T>(Func<IAsyncEnumerable<T>> asyncEnumerableMethod)
		{
			if (asyncEnumerableMethod is null)
			{
				throw new ArgumentNullException(nameof(asyncEnumerableMethod));
			}

			return new AsyncEnumerableDelegateUnderTest<T>(asyncEnumerableMethod);
		}
	}
}
