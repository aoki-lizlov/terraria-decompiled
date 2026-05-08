using System;

namespace rail
{
	// Token: 0x02000164 RID: 356
	public class RailInviteOptions
	{
		// Token: 0x06001845 RID: 6213 RVA: 0x00002119 File Offset: 0x00000319
		public RailInviteOptions()
		{
		}

		// Token: 0x0400051A RID: 1306
		public string additional_message;

		// Token: 0x0400051B RID: 1307
		public uint expire_time;

		// Token: 0x0400051C RID: 1308
		public EnumRailUsersInviteType invite_type;

		// Token: 0x0400051D RID: 1309
		public bool need_respond_in_game;
	}
}
