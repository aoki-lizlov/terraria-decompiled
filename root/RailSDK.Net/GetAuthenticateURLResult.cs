using System;

namespace rail
{
	// Token: 0x02000104 RID: 260
	public class GetAuthenticateURLResult : EventBase
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x0000F049 File Offset: 0x0000D249
		public GetAuthenticateURLResult()
		{
		}

		// Token: 0x040002D6 RID: 726
		public uint ticket_expire_time;

		// Token: 0x040002D7 RID: 727
		public string authenticate_url;

		// Token: 0x040002D8 RID: 728
		public string source_url;
	}
}
