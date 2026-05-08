using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001AB RID: 427
	public class VoiceChannelRemoveUsersResult : EventBase
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x00011207 File Offset: 0x0000F407
		public VoiceChannelRemoveUsersResult()
		{
		}

		// Token: 0x040005E1 RID: 1505
		public List<RailID> success_ids = new List<RailID>();

		// Token: 0x040005E2 RID: 1506
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005E3 RID: 1507
		public List<RailID> failed_ids = new List<RailID>();
	}
}
