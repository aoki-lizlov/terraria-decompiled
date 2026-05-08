using System;

namespace System
{
	// Token: 0x020001D3 RID: 467
	[Serializable]
	internal enum InternalGCCollectionMode
	{
		// Token: 0x04001423 RID: 5155
		NonBlocking = 1,
		// Token: 0x04001424 RID: 5156
		Blocking,
		// Token: 0x04001425 RID: 5157
		Optimized = 4,
		// Token: 0x04001426 RID: 5158
		Compacting = 8
	}
}
