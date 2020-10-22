using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using F0.Testing.Example.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F0.Testing.Example.MSTest.Services
{
	[TestClass]
	public class AssemblyServiceTests
	{
		[TestMethod]
		public void MSTestV2_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			ComVisibleAttribute attribute = assembly.GetCustomAttribute<ComVisibleAttribute>();
			Assert.IsNotNull(attribute);
			Assert.IsFalse(attribute.Value);
		}

		[TestMethod]
		public void MSTestV2_AssertThat_MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = assembly.GetCustomAttributes<InternalsVisibleToAttribute>();
			CollectionAssert.AreEqual(new InternalsVisibleToAttribute[]
			{
				new InternalsVisibleToAttribute("F0.Testing.Example.MSTest"),
				new InternalsVisibleToAttribute("F0.Testing.Example.NUnit"),
				new InternalsVisibleToAttribute("F0.Testing.Example.xUnit")
			}, attributes.ToArray());
		}

		[TestMethod]
		public void MSTestV2_AssertThat_AssemblyVersionIsAsExpected()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			Version assemblyVersion = assembly.GetName().Version;
			Assert.AreEqual(new Version(1, 0, 0, 0), assemblyVersion);
		}

		[TestMethod]
		public void Explicitly_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			ComVisibleAttribute attribute = Test.That(assembly).HasAttribute<ComVisibleAttribute>();
			Assert.IsFalse(attribute.Value);
		}

		[TestMethod]
		public void Explicitly_AssertThat_MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = Test.That(assembly).HasAttributes<InternalsVisibleToAttribute>();
			CollectionAssert.AreEqual(new InternalsVisibleToAttribute[]
			{
				new InternalsVisibleToAttribute("F0.Testing.Example.MSTest"),
				new InternalsVisibleToAttribute("F0.Testing.Example.NUnit"),
				new InternalsVisibleToAttribute("F0.Testing.Example.xUnit")
			}, attributes.ToArray());
		}

		[TestMethod]
		public void Explicitly_AssertThat_AssemblyVersionIsAsExpected()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			_ = Test.That(assembly).HasAssemblyVersion(new Version(1, 0, 0, 0));
		}
	}
}
