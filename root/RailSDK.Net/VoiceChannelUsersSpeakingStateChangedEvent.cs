using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001AD RID: 429
	public class VoiceChannelUsersSpeakingStateChangedEvent : EventBase
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x00011259 File Offset: 0x0000F459
		public VoiceChannelUsersSpeakingStateChangedEvent()
		{
		}

		// Token: 0x040005E7 RID: 1511
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005E8 RID: 1512
		public List<RailVoiceChannelUserSpeakingState> users_speaking_state = new List<RailVoiceChannelUserSpeakingState>();
	}
}
