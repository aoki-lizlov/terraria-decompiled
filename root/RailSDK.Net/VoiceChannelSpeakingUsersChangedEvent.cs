using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001AC RID: 428
	public class VoiceChannelSpeakingUsersChangedEvent : EventBase
	{
		// Token: 0x060018EF RID: 6383 RVA: 0x00011230 File Offset: 0x0000F430
		public VoiceChannelSpeakingUsersChangedEvent()
		{
		}

		// Token: 0x040005E4 RID: 1508
		public List<RailID> speaking_users = new List<RailID>();

		// Token: 0x040005E5 RID: 1509
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005E6 RID: 1510
		public List<RailID> not_speaking_users = new List<RailID>();
	}
}
