using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000214 RID: 532
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class MonoCQItem
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x000025BE File Offset: 0x000007BE
		public MonoCQItem()
		{
		}

		// Token: 0x0400160C RID: 5644
		private object[] array;

		// Token: 0x0400160D RID: 5645
		private byte[] array_state;

		// Token: 0x0400160E RID: 5646
		private int head;

		// Token: 0x0400160F RID: 5647
		private int tail;
	}
}
