using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x0200002B RID: 43
	public class SBI
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0001B496 File Offset: 0x00019696
		internal SBI()
		{
			this.L = new int[23];
			this.S = new int[14];
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0001B4B8 File Offset: 0x000196B8
		internal SBI(int[] thel, int[] thes)
		{
			this.L = thel;
			this.S = thes;
		}

		// Token: 0x0400017F RID: 383
		internal int[] L;

		// Token: 0x04000180 RID: 384
		internal int[] S;
	}
}
