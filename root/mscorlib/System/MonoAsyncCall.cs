using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000213 RID: 531
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoAsyncCall
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x000025BE File Offset: 0x000007BE
		public MonoAsyncCall()
		{
		}

		// Token: 0x04001606 RID: 5638
		private object msg;

		// Token: 0x04001607 RID: 5639
		private IntPtr cb_method;

		// Token: 0x04001608 RID: 5640
		private object cb_target;

		// Token: 0x04001609 RID: 5641
		private object state;

		// Token: 0x0400160A RID: 5642
		private object res;

		// Token: 0x0400160B RID: 5643
		private object out_args;
	}
}
