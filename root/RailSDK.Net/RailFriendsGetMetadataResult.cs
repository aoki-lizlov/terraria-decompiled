using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000A2 RID: 162
	public class RailFriendsGetMetadataResult : EventBase
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x000109F6 File Offset: 0x0000EBF6
		public RailFriendsGetMetadataResult()
		{
		}

		// Token: 0x040001D7 RID: 471
		public RailID friend_id = new RailID();

		// Token: 0x040001D8 RID: 472
		public List<RailKeyValueResult> friend_kvs = new List<RailKeyValueResult>();
	}
}
