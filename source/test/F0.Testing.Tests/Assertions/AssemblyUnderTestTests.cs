using System;
using System.Collections.Generic;
using System.Reflection;
using F0.Assertions;
using F0.Exceptions;
using F0.Testing.TestNamespace;
using F0.Tests.Shared;
using Xunit;

namespace F0.Tests.Assertions
{
	public class AssemblyUnderTestTests
	{
		private const string assemblyDisplayName = "F0.Testing.TestAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

		private static readonly AssemblyUnderTest assertor = new(typeof(TestClass).Assembly);

		[Fact]
		public void Ctor_AssemblyUnderTest_CannotBeNull()
		{
			Assert.Throws<ArgumentNullException>("assembly", () => new AssemblyUnderTest(null));
		}

		[Fact]
		public void HasAttribute_NoSuchAttributeIsFound_FailAssertion()
		{
			static TestAttribute Act()
			{
				return assertor.HasAttribute<TestAttribute>();
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(Act);
			Checker.CheckExceptionMessage(exception, "HasAttribute", "F0.Testing.TestNamespace.TestAttribute", GetNotFoundMessage());
		}

		[Fact]
		public void HasAttribute_SingleAttributeIsFound_PassAssertion()
		{
			AssemblyTitleAttribute attribute = assertor.HasAttribute<AssemblyTitleAttribute>();

			Assert.NotNull(attribute);
			Assert.Equal("F0.Testing.TestAssembly", attribute.Title);
		}

		[Fact]
		public void HasAttribute_SingleAttributeIsAppliedToAssembly_PassAssertion()
		{
			AssemblyDescriptionAttribute attribute = assertor.HasAttribute<AssemblyDescriptionAttribute>();

			Assert.NotNull(attribute);
			Assert.Equal("TestDescription", attribute.Description);
		}

		[Fact]
		public void HasAttribute_MoreThanOneOfTheRequestedAttributesIsFound_FailAssertion()
		{
			static TestMultiAttribute Act()
			{
				return assertor.HasAttribute<TestMultiAttribute>();
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(Act);

			AmbiguousMatchException inner = new("Multiple custom attributes of the same type found.");
			Checker.CheckExceptionMessage(exception, "HasAttribute", "F0.Testing.TestNamespace.TestMultiAttribute", GetMultipleFoundMessage(), inner);
		}

		[Fact]
		public void HasAttribute_Versioning()
		{
			static Attribute AssemblyVersion()
			{
				return assertor.HasAttribute<AssemblyVersionAttribute>();
			}

			AssertionFailedException assemblyVersion = Assert.Throws<AssertionFailedException>(AssemblyVersion);
			AssemblyFileVersionAttribute assemblyFileVersion = assertor.HasAttribute<AssemblyFileVersionAttribute>();
			AssemblyInformationalVersionAttribute assemblyInformationalVersion = assertor.HasAttribute<AssemblyInformationalVersionAttribute>();

			Checker.CheckExceptionMessage(assemblyVersion, "HasAttribute", "System.Reflection.AssemblyVersionAttribute", GetNotFoundMessage());
			Assert.Equal("1.2.3.4", assemblyFileVersion.Version);
			Assert.Equal("1.2.3-beta1+204ff0a", assemblyInformationalVersion.InformationalVersion);
		}

		[Fact]
		public void HasAttributes_NoSuchAttributesAreFound_FailAssertion()
		{
			static IEnumerable<TestAttribute> Act()
			{
				return assertor.HasAttributes<TestAttribute>();
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(Act);
			Checker.CheckExceptionMessage(exception, "HasAttributes", "F0.Testing.TestNamespace.TestAttribute", GetNoneFoundMessage());
		}

		[Fact]
		public void HasAttributes_SingleAttributeIsAppliedToAssembly_FailAssertion()
		{
			static IEnumerable<AssemblyTrademarkAttribute> Act()
			{
				return assertor.HasAttributes<AssemblyTrademarkAttribute>();
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(Act);
			Checker.CheckExceptionMessage(exception, "HasAttributes", "System.Reflection.AssemblyTrademarkAttribute", GetOneFoundMessage());
		}

		[Fact]
		public void HasAttributes_MoreThanOneOfTheRequestedAttributesIsFound_PassAssertion()
		{
			IEnumerable<TestMultiAttribute> attributes = assertor.HasAttributes<TestMultiAttribute>();

			Assert.Collection(attributes,
				first =>
				{
					Assert.Equal("First", first.Parameter);
				},
				second =>
				{
					Assert.Equal("Second", second.Parameter);
				});
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-2)]
		[InlineData(Int32.MinValue)]
		public void HasAttributes_WithExpectedCount_TheExpectedCountCannotBeNegative_FailAssertion(int expectedCount)
		{
			static TestMultiAttribute[] Act(int count)
			{
				return assertor.HasAttributes<TestMultiAttribute>(count);
			}

			string expectedMessage = new ArgumentOutOfRangeException("expectedCount", expectedCount, "The expectedCount argument cannot be a negative number.").Message;

			ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>("expectedCount", () => Act(expectedCount));
			Assert.Equal(expectedMessage, exception.Message);
			Assert.Equal(expectedCount, exception.ActualValue);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(4)]
		[InlineData(Int32.MaxValue)]
		public void HasAttributes_WithExpectedCount_NumberOfAttributesFoundIsNotEqualToTheExpectedCount_FailAssertion(int expectedCount)
		{
			static TestMultiAttribute[] Act(int count)
			{
				return assertor.HasAttributes<TestMultiAttribute>(count);
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(() => Act(expectedCount));
			Checker.CheckExceptionMessage(exception, "HasAttributes(Int32)", GetMessage<TestMultiAttribute>(expectedCount), GetMessage<TestMultiAttribute>(2));
		}

		[Fact]
		public void HasAttributes_WithExpectedCount_NumberOfAttributesFoundIsEqualToTheExpectedCount_PassAssertion()
		{
			TestMultiAttribute[] attributes = assertor.HasAttributes<TestMultiAttribute>(2);

			Assert.Collection(attributes,
				first =>
				{
					Assert.Equal("First", first.Parameter);
				},
				second =>
				{
					Assert.Equal("Second", second.Parameter);
				});
		}

		[Theory]
		[InlineData(-1L)]
		[InlineData(-2L)]
		[InlineData(Int64.MinValue)]
		public void HasAttributes_WithExpectedLongCount_TheExpectedLongCountCannotBeNegative_FailAssertion(long expectedLongCount)
		{
			static TestMultiAttribute[] Act(long longCount)
			{
				return assertor.HasAttributes<TestMultiAttribute>(longCount);
			}

			string expectedMessage = new ArgumentOutOfRangeException("expectedLongCount", expectedLongCount, "The expectedLongCount argument cannot be a negative number.").Message;

			ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>("expectedLongCount", () => Act(expectedLongCount));
			Assert.Equal(expectedMessage, exception.Message);
			Assert.Equal(expectedLongCount, exception.ActualValue);
		}

		[Theory]
		[InlineData(0L)]
		[InlineData(1L)]
		[InlineData(3L)]
		[InlineData(4L)]
		[InlineData(Int64.MaxValue)]
		public void HasAttributes_WithExpectedLongCount_NumberOfAttributesFoundIsNotEqualToTheExpectedLongCount_FailAssertion(long expectedLongCount)
		{
			static TestMultiAttribute[] Act(long longCount)
			{
				return assertor.HasAttributes<TestMultiAttribute>(longCount);
			}

			AssertionFailedException exception = Assert.Throws<AssertionFailedException>(() => Act(expectedLongCount));
			Checker.CheckExceptionMessage(exception, "HasAttributes(Int64)", GetMessage<TestMultiAttribute>(expectedLongCount), GetMessage<TestMultiAttribute>(2L));
		}

		[Fact]
		public void HasAttributes_WithExpectedLongCount_NumberOfAttributesFoundIsEqualToTheExpectedLongCount_PassAssertion()
		{
			TestMultiAttribute[] attributes = assertor.HasAttributes<TestMultiAttribute>(2L);

			Assert.Collection(attributes,
				first =>
				{
					Assert.Equal("First", first.Parameter);
				},
				second =>
				{
					Assert.Equal("Second", second.Parameter);
				});
		}

		[Fact]
		public void HasAssemblyVersion_ExpectedAssemblyVersion()
		{
			Version expected = new("1.0.0.0");
			Version actual = assertor.HasAssemblyVersion(expected);

			Assert.Equal(new Version(1, 0, 0, 0), actual);
			Assert.NotSame(expected, actual);
		}

		[Theory]
		[InlineData("1.0")]
		[InlineData("1.0.0")]
		[InlineData("1.0.0.1")]
		public void HasAssemblyVersion_UnexpectedAssemblyVersion(string input)
		{
			Version version = new(input);

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.HasAssemblyVersion(version));
			Checker.CheckExceptionMessage(exception, "HasAssemblyVersion", input, "1.0.0.0");
		}

		[Fact]
		public void HasAssemblyVersion_NoAssemblyVersion()
		{
			Version nullVersion = null;

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.HasAssemblyVersion(nullVersion));
			Checker.CheckExceptionMessage(exception, "HasAssemblyVersion", "null", "1.0.0.0");
		}

		[Fact]
		public void HasAssemblyVersion_DefaultAssemblyVersion()
		{
			Version defaultVersion = new();

			Exception exception = Assert.Throws<AssertionFailedException>(() => assertor.HasAssemblyVersion(defaultVersion));
			Checker.CheckExceptionMessage(exception, "HasAssemblyVersion", defaultVersion.ToString(), "1.0.0.0");
		}

		private static string GetNotFoundMessage()
		{
			return $"(A custom attribute of the specified type is not applied to assembly '{assemblyDisplayName}')";
		}

		private static string GetMultipleFoundMessage()
		{
			return $"(Multiple custom attributes of the same specified type are applied to assembly '{assemblyDisplayName}')";
		}

		private static string GetNoneFoundMessage()
		{
			return $"(No custom attributes of the specified type are applied to assembly '{assemblyDisplayName}')";
		}

		private static string GetOneFoundMessage()
		{
			return $"(Only one custom attribute of the specified type is applied to assembly '{assemblyDisplayName}')";
		}

		private static string GetMessage<T>(int count)
		{
			return $"Number of {typeof(T)}: {count} (Target: '{assemblyDisplayName}')";
		}

		private static string GetMessage<T>(long longCount)
		{
			return $"Total number of {typeof(T)}: {longCount} (Target: '{assemblyDisplayName}')";
		}
	}
}
