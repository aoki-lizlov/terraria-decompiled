using System;

namespace rail
{
	// Token: 0x020001A6 RID: 422
	public class RailVoiceChannelUserSpeakingState
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x0001119A File Offset: 0x0000F39A
		public RailVoiceChannelUserSpeakingState()
		{
		}

		// Token: 0x040005D6 RID: 1494
		public EnumRailVoiceChannelUserSpeakingLimit speaking_limit;

		// Token: 0x040005D7 RID: 1495
		public RailID user_id = new RailID();
	}
}
