using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000333 RID: 819
	[Flags]
	public enum TaskContinuationOptions
	{
		// Token: 0x04001BE1 RID: 7137
		None = 0,
		// Token: 0x04001BE2 RID: 7138
		PreferFairness = 1,
		// Token: 0x04001BE3 RID: 7139
		LongRunning = 2,
		// Token: 0x04001BE4 RID: 7140
		AttachedToParent = 4,
		// Token: 0x04001BE5 RID: 7141
		DenyChildAttach = 8,
		// Token: 0x04001BE6 RID: 7142
		HideScheduler = 16,
		// Token: 0x04001BE7 RID: 7143
		LazyCancellation = 32,
		// Token: 0x04001BE8 RID: 7144
		RunContinuationsAsynchronously = 64,
		// Token: 0x04001BE9 RID: 7145
		NotOnRanToCompletion = 65536,
		// Token: 0x04001BEA RID: 7146
		NotOnFaulted = 131072,
		// Token: 0x04001BEB RID: 7147
		NotOnCanceled = 262144,
		// Token: 0x04001BEC RID: 7148
		OnlyOnRanToCompletion = 393216,
		// Token: 0x04001BED RID: 7149
		OnlyOnFaulted = 327680,
		// Token: 0x04001BEE RID: 7150
		OnlyOnCanceled = 196608,
		// Token: 0x04001BEF RID: 7151
		ExecuteSynchronously = 524288
	}
}
