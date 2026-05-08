using System;

namespace rail
{
	// Token: 0x02000062 RID: 98
	public class BrowserRenderStateChanged : EventBase
	{
		// Token: 0x0600162C RID: 5676 RVA: 0x0000F049 File Offset: 0x0000D249
		public BrowserRenderStateChanged()
		{
		}

		// Token: 0x04000058 RID: 88
		public bool can_go_back;

		// Token: 0x04000059 RID: 89
		public bool can_go_forward;
	}
}
