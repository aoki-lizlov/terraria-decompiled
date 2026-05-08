using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime
{
	// Token: 0x02000523 RID: 1315
	public static class GCSettings
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("Always returns false")]
		public static bool IsServerGC
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x00003FB7 File Offset: 0x000021B7
		// (set) Token: 0x0600354F RID: 13647 RVA: 0x00004088 File Offset: 0x00002288
		[MonoTODO("Always returns GCLatencyMode.Interactive and ignores set")]
		public static GCLatencyMode LatencyMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCLatencyMode.Interactive;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000C19FD File Offset: 0x000BFBFD
		// (set) Token: 0x06003551 RID: 13649 RVA: 0x000C1A04 File Offset: 0x000BFC04
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[CompilerGenerated]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCSettings.<LargeObjectHeapCompactionMode>k__BackingField;
			}
			[CompilerGenerated]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				GCSettings.<LargeObjectHeapCompactionMode>k__BackingField = value;
			}
		}

		// Token: 0x04002493 RID: 9363
		[CompilerGenerated]
		private static GCLargeObjectHeapCompactionMode <LargeObjectHeapCompactionMode>k__BackingField;
	}
}
