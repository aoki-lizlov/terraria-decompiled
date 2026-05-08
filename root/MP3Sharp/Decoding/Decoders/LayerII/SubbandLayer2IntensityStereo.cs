using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerII
{
	// Token: 0x02000026 RID: 38
	public class SubbandLayer2IntensityStereo : SubbandLayer2
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		internal SubbandLayer2IntensityStereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		internal override void ReadScaleFactorSelection(Bitstream stream, Crc16 crc)
		{
			if (this.Allocation != 0)
			{
				this.Scfsi = stream.GetBitsFromBuffer(2);
				this.Channel2Scfsi = stream.GetBitsFromBuffer(2);
				if (crc != null)
				{
					crc.AddBits(this.Scfsi, 2);
					crc.AddBits(this.Channel2Scfsi, 2);
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0001AE00 File Offset: 0x00019000
		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (this.Allocation != 0)
			{
				base.ReadScaleFactor(stream, header);
				switch (this.Channel2Scfsi)
				{
				case 0:
					this.Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					return;
				case 1:
					this.Channel2Scalefactor1 = (this.Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					return;
				case 2:
					this.Channel2Scalefactor1 = (this.Channel2Scalefactor2 = (this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]));
					return;
				case 3:
					this.Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor2 = (this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0001AF00 File Offset: 0x00019100
		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (this.Allocation != 0)
			{
				float num = this.Samples[this.Samplenumber];
				if (this.Groupingtable[0] == null)
				{
					num = (num + this.D[0]) * this.CFactor[0];
				}
				if (channels == 0)
				{
					float num2 = num;
					if (this.Groupnumber <= 4)
					{
						num *= this.Scalefactor1;
						num2 *= this.Channel2Scalefactor1;
					}
					else if (this.Groupnumber <= 8)
					{
						num *= this.Scalefactor2;
						num2 *= this.Channel2Scalefactor2;
					}
					else
					{
						num *= this.Scalefactor3;
						num2 *= this.Channel2Scalefactor3;
					}
					filter1.AddSample(num, this.Subbandnumber);
					filter2.AddSample(num2, this.Subbandnumber);
				}
				else if (channels == 1)
				{
					if (this.Groupnumber <= 4)
					{
						num *= this.Scalefactor1;
					}
					else if (this.Groupnumber <= 8)
					{
						num *= this.Scalefactor2;
					}
					else
					{
						num *= this.Scalefactor3;
					}
					filter1.AddSample(num, this.Subbandnumber);
				}
				else
				{
					if (this.Groupnumber <= 4)
					{
						num *= this.Channel2Scalefactor1;
					}
					else if (this.Groupnumber <= 8)
					{
						num *= this.Channel2Scalefactor2;
					}
					else
					{
						num *= this.Channel2Scalefactor3;
					}
					filter1.AddSample(num, this.Subbandnumber);
				}
			}
			int num3 = this.Samplenumber + 1;
			this.Samplenumber = num3;
			return num3 == 3;
		}

		// Token: 0x0400015E RID: 350
		protected float Channel2Scalefactor1;

		// Token: 0x0400015F RID: 351
		protected float Channel2Scalefactor2;

		// Token: 0x04000160 RID: 352
		protected float Channel2Scalefactor3;

		// Token: 0x04000161 RID: 353
		protected int Channel2Scfsi;
	}
}
