using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001A9 RID: 425
	public class VoiceChannelMemeberChangedEvent : EventBase
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x000111E9 File Offset: 0x0000F3E9
		public VoiceChannelMemeberChangedEvent()
		{
		}

		// Token: 0x040005DE RID: 1502
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005DF RID: 1503
		public List<RailID> member_ids = new List<RailID>();
	}
}
