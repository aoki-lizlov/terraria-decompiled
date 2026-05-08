using System;

namespace rail
{
	// Token: 0x020000C2 RID: 194
	public class GameServerPlayerInfo
	{
		// Token: 0x06001708 RID: 5896 RVA: 0x00010B68 File Offset: 0x0000ED68
		public GameServerPlayerInfo()
		{
		}

		// Token: 0x04000229 RID: 553
		public string member_nickname;

		// Token: 0x0400022A RID: 554
		public long member_score;

		// Token: 0x0400022B RID: 555
		public RailID member_id = new RailID();
	}
}
