using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A53 RID: 2643
	[Flags]
	public enum EventSourceSettings
	{
		// Token: 0x04003A66 RID: 14950
		Default = 0,
		// Token: 0x04003A67 RID: 14951
		ThrowOnEventWriteErrors = 1,
		// Token: 0x04003A68 RID: 14952
		EtwManifestEventFormat = 4,
		// Token: 0x04003A69 RID: 14953
		EtwSelfDescribingEventFormat = 8
	}
}
