using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerI
{
	// Token: 0x02000023 RID: 35
	public class SubbandLayer1IntensityStereo : SubbandLayer1
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0001A333 File Offset: 0x00018533
		internal SubbandLayer1IntensityStereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0001A33C File Offset: 0x0001853C
		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (this.Allocation != 0)
			{
				this.Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
				this.Channel2Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0001A36C File Offset: 0x0001856C
		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (this.Allocation != 0)
			{
				this.Sample = this.Sample * this.Factor + this.Offset;
				if (channels == 0)
				{
					float num = this.Sample * this.Scalefactor;
					float num2 = this.Sample * this.Channel2Scalefactor;
					filter1.AddSample(num, this.Subbandnumber);
					filter2.AddSample(num2, this.Subbandnumber);
				}
				else if (channels == 1)
				{
					float num3 = this.Sample * this.Scalefactor;
					filter1.AddSample(num3, this.Subbandnumber);
				}
				else
				{
					float num4 = this.Sample * this.Channel2Scalefactor;
					filter1.AddSample(num4, this.Subbandnumber);
				}
			}
			return true;
		}

		// Token: 0x0400012F RID: 303
		protected float Channel2Scalefactor;
	}
}
