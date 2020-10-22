using System;

namespace F0.Testing.TestNamespace
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class TestAttribute : Attribute
	{
	}
}
