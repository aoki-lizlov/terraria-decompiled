using System;

namespace rail
{
	// Token: 0x020000B8 RID: 184
	public class AsyncAddFavoriteGameServerResult : EventBase
	{
		// Token: 0x060016FF RID: 5887 RVA: 0x00010ACA File Offset: 0x0000ECCA
		public AsyncAddFavoriteGameServerResult()
		{
		}

		// Token: 0x040001F8 RID: 504
		public RailID server_id = new RailID();
	}
}
