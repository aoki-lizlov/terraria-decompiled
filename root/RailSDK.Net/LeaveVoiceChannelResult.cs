using System;

namespace rail
{
	// Token: 0x020001A3 RID: 419
	public class LeaveVoiceChannelResult : EventBase
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x00011187 File Offset: 0x0000F387
		public LeaveVoiceChannelResult()
		{
		}

		// Token: 0x040005CF RID: 1487
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040005D0 RID: 1488
		public EnumRailVoiceLeaveChannelReason reason;
	}
}
