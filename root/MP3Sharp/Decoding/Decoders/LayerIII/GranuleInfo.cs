using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x02000029 RID: 41
	public class GranuleInfo
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0001B448 File Offset: 0x00019648
		internal GranuleInfo()
		{
			this.TableSelect = new int[3];
			this.SubblockGain = new int[3];
		}

		// Token: 0x0400016E RID: 366
		internal int BigValues;

		// Token: 0x0400016F RID: 367
		internal int BlockType;

		// Token: 0x04000170 RID: 368
		internal int Count1TableSelect;

		// Token: 0x04000171 RID: 369
		internal int GlobalGain;

		// Token: 0x04000172 RID: 370
		internal int MixedBlockFlag;

		// Token: 0x04000173 RID: 371
		internal int Part23Length;

		// Token: 0x04000174 RID: 372
		internal int Preflag;

		// Token: 0x04000175 RID: 373
		internal int Region0Count;

		// Token: 0x04000176 RID: 374
		internal int Region1Count;

		// Token: 0x04000177 RID: 375
		internal int ScaleFacCompress;

		// Token: 0x04000178 RID: 376
		internal int ScaleFacScale;

		// Token: 0x04000179 RID: 377
		internal int[] SubblockGain;

		// Token: 0x0400017A RID: 378
		internal int[] TableSelect;

		// Token: 0x0400017B RID: 379
		internal int WindowSwitchingFlag;
	}
}
