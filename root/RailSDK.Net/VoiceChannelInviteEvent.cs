using System;

namespace rail
{
	// Token: 0x020001A8 RID: 424
	public class VoiceChannelInviteEvent : EventBase
	{
		// Token: 0x060018EB RID: 6379 RVA: 0x000111D6 File Offset: 0x0000F3D6
		public VoiceChannelInviteEvent()
		{
		}

		// Token: 0x040005DB RID: 1499
		public string channel_name;

		// Token: 0x040005DC RID: 1500
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005DD RID: 1501
		public string inviter_name;
	}
}
