using System;

namespace rail
{
	// Token: 0x02000095 RID: 149
	public class ShowFloatingWindowResult : EventBase
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x0000F049 File Offset: 0x0000D249
		public ShowFloatingWindowResult()
		{
		}

		// Token: 0x040001B8 RID: 440
		public EnumRailWindowType window_type;

		// Token: 0x040001B9 RID: 441
		public bool is_show;
	}
}
