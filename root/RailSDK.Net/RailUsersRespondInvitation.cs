using System;

namespace rail
{
	// Token: 0x0200016E RID: 366
	public class RailUsersRespondInvitation : EventBase
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x00010FA2 File Offset: 0x0000F1A2
		public RailUsersRespondInvitation()
		{
		}

		// Token: 0x0400052C RID: 1324
		public RailInviteOptions original_invite_option = new RailInviteOptions();

		// Token: 0x0400052D RID: 1325
		public EnumRailUsersInviteResponseType response;

		// Token: 0x0400052E RID: 1326
		public RailID inviter_id = new RailID();
	}
}
