using System;

namespace rail
{
	// Token: 0x02000064 RID: 100
	public class BrowserTryNavigateNewPageRequest : EventBase
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x0000F049 File Offset: 0x0000D249
		public BrowserTryNavigateNewPageRequest()
		{
		}

		// Token: 0x0400005B RID: 91
		public string url;

		// Token: 0x0400005C RID: 92
		public string target_type;

		// Token: 0x0400005D RID: 93
		public bool is_redirect_request;
	}
}
