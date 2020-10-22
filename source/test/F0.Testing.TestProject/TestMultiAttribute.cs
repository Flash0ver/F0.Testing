using System;

namespace F0.Testing.TestNamespace
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class TestMultiAttribute : Attribute
	{
		public TestMultiAttribute(string parameter)
		{
			Parameter = parameter;
		}

		public string Parameter { get; }
	}
}
