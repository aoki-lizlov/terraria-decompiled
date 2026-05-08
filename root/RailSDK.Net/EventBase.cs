using System;

namespace rail
{
	// Token: 0x0200008A RID: 138
	public class EventBase
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x00010950 File Offset: 0x0000EB50
		public EventBase()
		{
		}

		// Token: 0x040000C8 RID: 200
		public RailResult result;

		// Token: 0x040000C9 RID: 201
		public RailGameID game_id = new RailGameID();

		// Token: 0x040000CA RID: 202
		public string user_data;

		// Token: 0x040000CB RID: 203
		public RailID rail_id = new RailID();
	}
}
