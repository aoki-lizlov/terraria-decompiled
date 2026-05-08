using System;

namespace rail
{
	// Token: 0x020000A1 RID: 161
	public class RailFriendsGetInviteCommandLine : EventBase
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x000109E3 File Offset: 0x0000EBE3
		public RailFriendsGetInviteCommandLine()
		{
		}

		// Token: 0x040001D5 RID: 469
		public RailID friend_id = new RailID();

		// Token: 0x040001D6 RID: 470
		public string invite_command_line;
	}
}
