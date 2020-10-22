using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using F0.Testing.Example.Services;
using NUnit.Framework;

namespace F0.Testing.Example.NUnit.Services
{
	[TestFixture]
	public class AssemblyServiceTests
	{
		[Test]
		public void NUnit3_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			CLSCompliantAttribute attribute = assembly.GetCustomAttribute<CLSCompliantAttribute>();
			Assert.IsNotNull(attribute);
			Assert.IsTrue(attribute.IsCompliant);

			// ConstraintBasedAssertModel
			Assert.That(assembly,
				Has.Attribute<CLSCompliantAttribute>()
					.With.Property(nameof(CLSCompliantAttribute.IsCompliant)).True);
		}

		[Test]
		public void NUnit3_AssertThat__MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = assembly.GetCustomAttributes<InternalsVisibleToAttribute>();
			CollectionAssert.AreEqual(new[]
			{
				new InternalsVisibleToAttribute("F0.Testing.Example.MSTest"),
				new InternalsVisibleToAttribute("F0.Testing.Example.NUnit"),
				new InternalsVisibleToAttribute("F0.Testing.Example.xUnit")
			}, attributes);

			// ConstraintBasedAssertModel
			Assert.That(assembly.GetCustomAttributes<InternalsVisibleToAttribute>(),
				Is.Ordered.By(nameof(InternalsVisibleToAttribute.AssemblyName)).And.EquivalentTo(new[]
				{
					new InternalsVisibleToAttribute("F0.Testing.Example.MSTest"),
					new InternalsVisibleToAttribute("F0.Testing.Example.NUnit"),
					new InternalsVisibleToAttribute("F0.Testing.Example.xUnit")
				}));
		}

		[Test]
		public void NUnit3_AssertThat_AssemblyVersionIsAsExpected()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			Version assemblyVersion = assembly.GetName().Version;
			Assert.AreEqual(new Version(1, 0, 0, 0), assemblyVersion);

			// ConstraintBasedAssertModel
			Assert.That(assembly.GetName().Version,
				Is.EqualTo(new Version(1, 0, 0, 0)));
		}

		[Test]
		public void Explicitly_AssertThat_SingleCustomAttributeOfSpecifiedTypeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			CLSCompliantAttribute attribute = Test.That(assembly).HasAttribute<CLSCompliantAttribute>();
			Assert.IsTrue(attribute.IsCompliant);
		}

		[Test]
		public void Explicitly_AssertThat_MoreThanOneOfTheRequestedCustomAttributeIsAppliedToTheInspectedAssembly()
		{
			// Arrange
			var service = new AssemblyService();

			// Act
			Assembly assembly = service.GetAssembly();

			// Assert
			IEnumerable<InternalsVisibleToAttribute> attributes = Test.That(assembly).HasAttributes<InternalsVisibleToAttribute>();
			CollectionAssert.AreEqual(new[]
			{
				new InternalsVisibleToAttribute("F0.Testing.Example.MSTest"),
				new InternalsVisibleToAttribute("F0.Testing.Example.NUnit"),
				new InternalsVisibleToAttribute("F0.Testing.Example.xUnit")
			}, attributes);
		}

		[Test]
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
