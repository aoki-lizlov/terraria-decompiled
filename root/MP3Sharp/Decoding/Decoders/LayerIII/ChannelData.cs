using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x02000028 RID: 40
	public class ChannelData
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0001B40E File Offset: 0x0001960E
		internal ChannelData()
		{
			this.ScaleFactorBits = new int[4];
			this.Granules = new GranuleInfo[2];
			this.Granules[0] = new GranuleInfo();
			this.Granules[1] = new GranuleInfo();
		}

		// Token: 0x0400016C RID: 364
		internal GranuleInfo[] Granules;

		// Token: 0x0400016D RID: 365
		internal int[] ScaleFactorBits;
	}
}
