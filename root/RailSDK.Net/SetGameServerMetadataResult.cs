using System;

namespace rail
{
	// Token: 0x020000C8 RID: 200
	public class SetGameServerMetadataResult : EventBase
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00010BDD File Offset: 0x0000EDDD
		public SetGameServerMetadataResult()
		{
		}

		// Token: 0x04000235 RID: 565
		public RailID game_server_id = new RailID();
	}
}
