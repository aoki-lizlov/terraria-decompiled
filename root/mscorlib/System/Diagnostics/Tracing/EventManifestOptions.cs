using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A4E RID: 2638
	[Flags]
	public enum EventManifestOptions
	{
		// Token: 0x04003A57 RID: 14935
		AllCultures = 2,
		// Token: 0x04003A58 RID: 14936
		AllowEventSourceOverride = 8,
		// Token: 0x04003A59 RID: 14937
		None = 0,
		// Token: 0x04003A5A RID: 14938
		OnlyIfNeededForRegistration = 4,
		// Token: 0x04003A5B RID: 14939
		Strict = 1
	}
}
