using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerII
{
	// Token: 0x02000027 RID: 39
	public class SubbandLayer2Stereo : SubbandLayer2
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0001B050 File Offset: 0x00019250
		internal SubbandLayer2Stereo(int subbandnumber)
			: base(subbandnumber)
		{
			this.Channel2Samples = new float[3];
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0001B0A0 File Offset: 0x000192A0
		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			int allocationLength = this.GetAllocationLength(header);
			this.Allocation = stream.GetBitsFromBuffer(allocationLength);
			this.Channel2Allocation = stream.GetBitsFromBuffer(allocationLength);
			if (crc != null)
			{
				crc.AddBits(this.Allocation, allocationLength);
				crc.AddBits(this.Channel2Allocation, allocationLength);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0001B0EC File Offset: 0x000192EC
		internal override void ReadScaleFactorSelection(Bitstream stream, Crc16 crc)
		{
			if (this.Allocation != 0)
			{
				this.Scfsi = stream.GetBitsFromBuffer(2);
				if (crc != null)
				{
					crc.AddBits(this.Scfsi, 2);
				}
			}
			if (this.Channel2Allocation != 0)
			{
				this.Channel2Scfsi = stream.GetBitsFromBuffer(2);
				if (crc != null)
				{
					crc.AddBits(this.Channel2Scfsi, 2);
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0001B144 File Offset: 0x00019344
		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			base.ReadScaleFactor(stream, header);
			if (this.Channel2Allocation != 0)
			{
				switch (this.Channel2Scfsi)
				{
				case 0:
					this.Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 1:
					this.Channel2Scalefactor1 = (this.Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 2:
					this.Channel2Scalefactor1 = (this.Channel2Scalefactor2 = (this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]));
					break;
				case 3:
					this.Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					this.Channel2Scalefactor2 = (this.Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					break;
				}
				this.PrepareForSampleRead(header, this.Channel2Allocation, 1, this.Channel2Factor, this.Channel2Codelength, this.Channel2C, this.Channel2D);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0001B274 File Offset: 0x00019474
		internal override bool ReadSampleData(Bitstream stream)
		{
			bool flag = base.ReadSampleData(stream);
			if (this.Channel2Allocation != 0)
			{
				if (this.Groupingtable[1] != null)
				{
					int num = stream.GetBitsFromBuffer(this.Channel2Codelength[0]);
					num += num << 1;
					float[] channel2Samples = this.Channel2Samples;
					float[] array = this.Groupingtable[1];
					int num2 = 0;
					int num3 = num;
					channel2Samples[num2] = array[num3];
					num3++;
					num2++;
					channel2Samples[num2] = array[num3];
					num3++;
					num2++;
					channel2Samples[num2] = array[num3];
					return flag;
				}
				this.Channel2Samples[0] = (float)((double)((float)stream.GetBitsFromBuffer(this.Channel2Codelength[0]) * this.Channel2Factor[0]) - 1.0);
				this.Channel2Samples[1] = (float)((double)((float)stream.GetBitsFromBuffer(this.Channel2Codelength[0]) * this.Channel2Factor[0]) - 1.0);
				this.Channel2Samples[2] = (float)((double)((float)stream.GetBitsFromBuffer(this.Channel2Codelength[0]) * this.Channel2Factor[0]) - 1.0);
			}
			return flag;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001B36C File Offset: 0x0001956C
		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			bool flag = base.PutNextSample(channels, filter1, filter2);
			if (this.Channel2Allocation != 0 && channels != 1)
			{
				float num = this.Channel2Samples[this.Samplenumber - 1];
				if (this.Groupingtable[1] == null)
				{
					num = (num + this.Channel2D[0]) * this.Channel2C[0];
				}
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
				if (channels == 0)
				{
					filter2.AddSample(num, this.Subbandnumber);
					return flag;
				}
				filter1.AddSample(num, this.Subbandnumber);
			}
			return flag;
		}

		// Token: 0x04000162 RID: 354
		protected int Channel2Allocation;

		// Token: 0x04000163 RID: 355
		protected readonly float[] Channel2C = new float[1];

		// Token: 0x04000164 RID: 356
		protected readonly int[] Channel2Codelength = new int[1];

		// Token: 0x04000165 RID: 357
		protected readonly float[] Channel2D = new float[1];

		// Token: 0x04000166 RID: 358
		protected readonly float[] Channel2Factor = new float[1];

		// Token: 0x04000167 RID: 359
		protected readonly float[] Channel2Samples;

		// Token: 0x04000168 RID: 360
		protected float Channel2Scalefactor1;

		// Token: 0x04000169 RID: 361
		protected float Channel2Scalefactor2;

		// Token: 0x0400016A RID: 362
		protected float Channel2Scalefactor3;

		// Token: 0x0400016B RID: 363
		protected int Channel2Scfsi;
	}
}
