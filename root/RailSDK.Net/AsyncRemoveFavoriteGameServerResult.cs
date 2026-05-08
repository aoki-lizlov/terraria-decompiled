using System;

namespace rail
{
	// Token: 0x020000BA RID: 186
	public class AsyncRemoveFavoriteGameServerResult : EventBase
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public AsyncRemoveFavoriteGameServerResult()
		{
		}

		// Token: 0x040001FA RID: 506
		public RailID server_id = new RailID();
	}
}
