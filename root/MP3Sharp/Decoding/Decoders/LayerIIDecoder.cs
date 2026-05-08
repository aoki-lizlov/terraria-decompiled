using System;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerII;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders
{
	// Token: 0x02000020 RID: 32
	public class LayerIIDecoder : LayerIDecoder
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000157B4 File Offset: 0x000139B4
		protected override void CreateSubbands()
		{
			int mode = this.Mode;
			if (mode == 1)
			{
				int i;
				for (i = 0; i < this.Header.IntensityStereoBound(); i++)
				{
					this.Subbands[i] = new SubbandLayer2Stereo(i);
				}
				while (i < this.NuSubbands)
				{
					this.Subbands[i] = new SubbandLayer2IntensityStereo(i);
					i++;
				}
				return;
			}
			if (mode == 3)
			{
				for (int i = 0; i < this.NuSubbands; i++)
				{
					this.Subbands[i] = new SubbandLayer2(i);
				}
				return;
			}
			for (int i = 0; i < this.NuSubbands; i++)
			{
				this.Subbands[i] = new SubbandLayer2Stereo(i);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00015854 File Offset: 0x00013A54
		protected override void ReadScaleFactorSelection()
		{
			for (int i = 0; i < this.NuSubbands; i++)
			{
				((SubbandLayer2)this.Subbands[i]).ReadScaleFactorSelection(this.Stream, this.CRC);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00015890 File Offset: 0x00013A90
		public LayerIIDecoder()
		{
		}
	}
}
