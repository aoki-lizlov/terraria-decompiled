using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x0200002A RID: 42
	public class Layer3SideInfo
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0001B468 File Offset: 0x00019668
		internal Layer3SideInfo()
		{
			this.Channels = new ChannelData[2];
			this.Channels[0] = new ChannelData();
			this.Channels[1] = new ChannelData();
		}

		// Token: 0x0400017C RID: 380
		internal ChannelData[] Channels;

		// Token: 0x0400017D RID: 381
		internal int MainDataBegin;

		// Token: 0x0400017E RID: 382
		internal int PrivateBits;
	}
}
