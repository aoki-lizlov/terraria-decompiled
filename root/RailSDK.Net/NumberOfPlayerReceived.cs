using System;

namespace rail
{
	// Token: 0x02000140 RID: 320
	public class NumberOfPlayerReceived : EventBase
	{
		// Token: 0x060017FA RID: 6138 RVA: 0x0000F049 File Offset: 0x0000D249
		public NumberOfPlayerReceived()
		{
		}

		// Token: 0x040004BE RID: 1214
		public int online_number;

		// Token: 0x040004BF RID: 1215
		public int offline_number;
	}
}
