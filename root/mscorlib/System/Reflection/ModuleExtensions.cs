using System;

namespace System.Reflection
{
	// Token: 0x020008A9 RID: 2217
	public static class ModuleExtensions
	{
		// Token: 0x06004AE2 RID: 19170 RVA: 0x000F0283 File Offset: 0x000EE483
		public static bool HasModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return true;
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x000F0291 File Offset: 0x000EE491
		public static Guid GetModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return module.ModuleVersionId;
		}
	}
}
