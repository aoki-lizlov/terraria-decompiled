using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x0200002C RID: 44
	public class ScaleFactorData
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0001B4D0 File Offset: 0x000196D0
		internal ScaleFactorData()
		{
			this.L = new int[23];
			this.S = new int[3][];
			for (int i = 0; i < 3; i++)
			{
				this.S[i] = new int[13];
			}
		}

		// Token: 0x04000181 RID: 385
		internal int[] L;

		// Token: 0x04000182 RID: 386
		internal int[][] S;
	}
}
