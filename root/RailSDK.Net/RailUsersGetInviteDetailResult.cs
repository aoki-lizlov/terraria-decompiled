using System;

namespace rail
{
	// Token: 0x02000168 RID: 360
	public class RailUsersGetInviteDetailResult : EventBase
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x00010F38 File Offset: 0x0000F138
		public RailUsersGetInviteDetailResult()
		{
		}

		// Token: 0x04000521 RID: 1313
		public string command_line;

		// Token: 0x04000522 RID: 1314
		public EnumRailUsersInviteType invite_type;

		// Token: 0x04000523 RID: 1315
		public RailID inviter_id = new RailID();
	}
}
