using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001A7 RID: 423
	public class VoiceChannelAddUsersResult : EventBase
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x000111AD File Offset: 0x0000F3AD
		public VoiceChannelAddUsersResult()
		{
		}

		// Token: 0x040005D8 RID: 1496
		public List<RailID> success_ids = new List<RailID>();

		// Token: 0x040005D9 RID: 1497
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005DA RID: 1498
		public List<RailID> failed_ids = new List<RailID>();
	}
}
