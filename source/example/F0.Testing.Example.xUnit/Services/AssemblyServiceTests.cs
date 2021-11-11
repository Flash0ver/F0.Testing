using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using F0.Testing.Example.Services;
using Xunit;

namespace F0.Testing.Example.xUnit.Services
{
	public class AssemblyServiceTests
	{
		[Fact]
		public void xUnit_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			GuidAttribute? attribute = assembly.GetCustomAttribute<GuidAttribute>();
			Assert.NotNull(attribute);
			Assert.Equal("c1b49028-a6f6-4e34-a579-08458219330c", attribute.Value);
		}

		[Fact]
		public void xUnit_AssertThat_MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = assembly.GetCustomAttributes<InternalsVisibleToAttribute>();
			Assert.Collection(attributes,
				first =>
				{
					Assert.Equal("F0.Testing.Example.MSTest", first.AssemblyName);
				},
				second =>
				{
					Assert.Equal("F0.Testing.Example.NUnit", second.AssemblyName);
				},
				third =>
				{
					Assert.Equal("F0.Testing.Example.xUnit", third.AssemblyName);
				});
		}

		[Fact]
		public void xUnit_AssertThat_AssemblyVersionIsAsExpected()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			Version? assemblyVersion = assembly.GetName().Version;
			Assert.Equal(new Version(1, 0, 0, 0), assemblyVersion);
		}

		[Fact]
		public void Explicitly_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			GuidAttribute attribute = Test.That(assembly).HasAttribute<GuidAttribute>();
			Assert.Equal("c1b49028-a6f6-4e34-a579-08458219330c", attribute.Value);
		}

		[Fact]
		public void Explicitly_AssertThat_MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = Test.That(assembly).HasAttributes<InternalsVisibleToAttribute>();
			Assert.Collection(attributes,
				first =>
				{
					Assert.Equal("F0.Testing.Example.MSTest", first.AssemblyName);
				},
				second =>
				{
					Assert.Equal("F0.Testing.Example.NUnit", second.AssemblyName);
				},
				third =>
				{
					Assert.Equal("F0.Testing.Example.xUnit", third.AssemblyName);
				});
		}

		[Fact]
		public void Explicitly_AssertThat_AssemblyVersionIsAsExpected()
		{
			// Arrange
			AssemblyService service = new();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			_ = Test.That(assembly).HasAssemblyVersion(new Version(1, 0, 0, 0));
		}
	}
}
