using System;

namespace rail
{
	// Token: 0x0200019B RID: 411
	public class CreateVoiceChannelResult : EventBase
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x00011156 File Offset: 0x0000F356
		public CreateVoiceChannelResult()
		{
		}

		// Token: 0x040005AE RID: 1454
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();
	}
}
