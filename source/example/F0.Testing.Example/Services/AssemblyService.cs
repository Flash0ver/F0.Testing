using System.Reflection;

namespace F0.Testing.Example.Services
{
	internal sealed class AssemblyService
	{
		public AssemblyService()
		{
		}

		public Assembly GetAssembly()
		{
			return GetType().Assembly;
		}
	}
}
