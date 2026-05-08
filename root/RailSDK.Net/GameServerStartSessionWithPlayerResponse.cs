using System;

namespace rail
{
	// Token: 0x020000C4 RID: 196
	public class GameServerStartSessionWithPlayerResponse : EventBase
	{
		// Token: 0x0600170A RID: 5898 RVA: 0x00010B7B File Offset: 0x0000ED7B
		public GameServerStartSessionWithPlayerResponse()
		{
		}

		// Token: 0x0400022C RID: 556
		public RailID remote_rail_id = new RailID();
	}
}
