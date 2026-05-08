using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x020000DC RID: 220
	public struct ExtensionFilter
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x004E2E6D File Offset: 0x004E106D
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x040012DF RID: 4831
		public string Name;

		// Token: 0x040012E0 RID: 4832
		public string[] Extensions;
	}
}
