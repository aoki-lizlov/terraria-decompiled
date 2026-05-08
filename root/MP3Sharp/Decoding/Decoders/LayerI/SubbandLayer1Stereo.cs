using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerI
{
	// Token: 0x02000024 RID: 36
	public class SubbandLayer1Stereo : SubbandLayer1
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0001A416 File Offset: 0x00018616
		internal SubbandLayer1Stereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0001A420 File Offset: 0x00018620
		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			this.Allocation = stream.GetBitsFromBuffer(4);
			if (this.Allocation > 14)
			{
				return;
			}
			this.Channel2Allocation = stream.GetBitsFromBuffer(4);
			if (crc != null)
			{
				crc.AddBits(this.Allocation, 4);
				crc.AddBits(this.Channel2Allocation, 4);
			}
			if (this.Allocation != 0)
			{
				this.Samplelength = this.Allocation + 1;
				this.Factor = SubbandLayer1.TableFactor[this.Allocation];
				this.Offset = SubbandLayer1.TableOffset[this.Allocation];
			}
			if (this.Channel2Allocation != 0)
			{
				this.Channel2Samplelength = this.Channel2Allocation + 1;
				this.Channel2Factor = SubbandLayer1.TableFactor[this.Channel2Allocation];
				this.Channel2Offset = SubbandLayer1.TableOffset[this.Channel2Allocation];
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0001A4E3 File Offset: 0x000186E3
		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (this.Allocation != 0)
			{
				this.Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
			if (this.Channel2Allocation != 0)
			{
				this.Channel2Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0001A51B File Offset: 0x0001871B
		internal override bool ReadSampleData(Bitstream stream)
		{
			bool flag = base.ReadSampleData(stream);
			if (this.Channel2Allocation != 0)
			{
				this.Channel2Sample = (float)stream.GetBitsFromBuffer(this.Channel2Samplelength);
			}
			return flag;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0001A540 File Offset: 0x00018740
		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			base.PutNextSample(channels, filter1, filter2);
			if (this.Channel2Allocation != 0 && channels != 1)
			{
				float num = (this.Channel2Sample * this.Channel2Factor + this.Channel2Offset) * this.Channel2Scalefactor;
				if (channels == 0)
				{
					filter2.AddSample(num, this.Subbandnumber);
				}
				else
				{
					filter1.AddSample(num, this.Subbandnumber);
				}
			}
			return true;
		}

		// Token: 0x04000130 RID: 304
		protected int Channel2Allocation;

		// Token: 0x04000131 RID: 305
		protected float Channel2Factor;

		// Token: 0x04000132 RID: 306
		protected float Channel2Offset;

		// Token: 0x04000133 RID: 307
		protected float Channel2Sample;

		// Token: 0x04000134 RID: 308
		protected int Channel2Samplelength;

		// Token: 0x04000135 RID: 309
		protected float Channel2Scalefactor;
	}
}
