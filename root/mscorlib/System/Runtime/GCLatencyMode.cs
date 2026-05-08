using System;

namespace System.Runtime
{
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public enum GCLatencyMode
	{
		// Token: 0x0400248E RID: 9358
		Batch,
		// Token: 0x0400248F RID: 9359
		Interactive,
		// Token: 0x04002490 RID: 9360
		LowLatency,
		// Token: 0x04002491 RID: 9361
		SustainedLowLatency,
		// Token: 0x04002492 RID: 9362
		NoGCRegion
	}
}
