using System;

namespace rail
{
	// Token: 0x0200016B RID: 363
	public class RailUsersInviteJoinGameResult : EventBase
	{
		// Token: 0x0600184C RID: 6220 RVA: 0x00010F7C File Offset: 0x0000F17C
		public RailUsersInviteJoinGameResult()
		{
		}

		// Token: 0x04000527 RID: 1319
		public EnumRailUsersInviteResponseType response_value;

		// Token: 0x04000528 RID: 1320
		public RailID invitee_id = new RailID();

		// Token: 0x04000529 RID: 1321
		public EnumRailUsersInviteType invite_type;
	}
}
