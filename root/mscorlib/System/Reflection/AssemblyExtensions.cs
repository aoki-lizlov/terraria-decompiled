using System;

namespace System.Reflection
{
	// Token: 0x020008A5 RID: 2213
	public static class AssemblyExtensions
	{
		// Token: 0x06004AD5 RID: 19157 RVA: 0x000F0140 File Offset: 0x000EE340
		public static Type[] GetExportedTypes(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetExportedTypes();
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x000F0153 File Offset: 0x000EE353
		public static Module[] GetModules(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetModules();
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x000F0166 File Offset: 0x000EE366
		public static Type[] GetTypes(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetTypes();
		}
	}
}
