using System;

namespace System.Threading
{
	// Token: 0x02000277 RID: 631
	public struct NativeOverlapped
	{
		// Token: 0x0400194C RID: 6476
		public IntPtr InternalLow;

		// Token: 0x0400194D RID: 6477
		public IntPtr InternalHigh;

		// Token: 0x0400194E RID: 6478
		public int OffsetLow;

		// Token: 0x0400194F RID: 6479
		public int OffsetHigh;

		// Token: 0x04001950 RID: 6480
		public IntPtr EventHandle;
	}
}
