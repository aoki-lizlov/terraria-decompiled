using System;

namespace rail
{
	// Token: 0x02000096 RID: 150
	public class ShowNotifyWindow : EventBase
	{
		// Token: 0x06001686 RID: 5766 RVA: 0x0000F049 File Offset: 0x0000D249
		public ShowNotifyWindow()
		{
		}

		// Token: 0x040001BA RID: 442
		public EnumRailNotifyWindowType window_type;

		// Token: 0x040001BB RID: 443
		public string json_content;
	}
}
