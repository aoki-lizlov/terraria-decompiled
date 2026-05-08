using System;

namespace rail
{
	// Token: 0x020001A2 RID: 418
	public class JoinVoiceChannelResult : EventBase
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x00011169 File Offset: 0x0000F369
		public JoinVoiceChannelResult()
		{
		}

		// Token: 0x040005CD RID: 1485
		public RailVoiceChannelID already_joined_channel_id = new RailVoiceChannelID();

		// Token: 0x040005CE RID: 1486
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();
	}
}
