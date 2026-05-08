using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A41 RID: 2625
	public enum EventOpcode
	{
		// Token: 0x04003A1F RID: 14879
		Info,
		// Token: 0x04003A20 RID: 14880
		Start,
		// Token: 0x04003A21 RID: 14881
		Stop,
		// Token: 0x04003A22 RID: 14882
		DataCollectionStart,
		// Token: 0x04003A23 RID: 14883
		DataCollectionStop,
		// Token: 0x04003A24 RID: 14884
		Extension,
		// Token: 0x04003A25 RID: 14885
		Reply,
		// Token: 0x04003A26 RID: 14886
		Resume,
		// Token: 0x04003A27 RID: 14887
		Suspend,
		// Token: 0x04003A28 RID: 14888
		Send,
		// Token: 0x04003A29 RID: 14889
		Receive = 240
	}
}
