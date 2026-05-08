using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerI
{
	// Token: 0x02000022 RID: 34
	public class SubbandLayer1 : ASubband
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0001A1DA File Offset: 0x000183DA
		internal SubbandLayer1(int subbandnumber)
		{
			this.Subbandnumber = subbandnumber;
			this.Samplenumber = 0;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0001A1F0 File Offset: 0x000183F0
		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			int num = (this.Allocation = stream.GetBitsFromBuffer(4));
			if (crc != null)
			{
				crc.AddBits(this.Allocation, 4);
			}
			if (this.Allocation != 0)
			{
				this.Samplelength = this.Allocation + 1;
				this.Factor = SubbandLayer1.TableFactor[this.Allocation];
				this.Offset = SubbandLayer1.TableOffset[this.Allocation];
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0001A25B File Offset: 0x0001845B
		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (this.Allocation != 0)
			{
				this.Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0001A278 File Offset: 0x00018478
		internal override bool ReadSampleData(Bitstream stream)
		{
			if (this.Allocation != 0)
			{
				this.Sample = (float)stream.GetBitsFromBuffer(this.Samplelength);
			}
			int num = this.Samplenumber + 1;
			this.Samplenumber = num;
			if (num == 12)
			{
				this.Samplenumber = 0;
				return true;
			}
			return false;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0001A2C0 File Offset: 0x000184C0
		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (this.Allocation != 0 && channels != 2)
			{
				float num = (this.Sample * this.Factor + this.Offset) * this.Scalefactor;
				filter1.AddSample(num, this.Subbandnumber);
			}
			return true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0001A303 File Offset: 0x00018503
		// Note: this type is marked as 'beforefieldinit'.
		static SubbandLayer1()
		{
		}

		// Token: 0x04000125 RID: 293
		internal static readonly float[] TableFactor = new float[]
		{
			0f, 0.6666667f, 0.2857143f, 0.13333334f, 0.06451613f, 0.031746034f, 0.015748031f, 0.007843138f, 0.0039138943f, 0.0019550342f,
			0.0009770396f, 0.0004884005f, 0.00024417043f, 0.00012207776f, 6.103702E-05f
		};

		// Token: 0x04000126 RID: 294
		internal static readonly float[] TableOffset = new float[]
		{
			0f, -0.6666667f, -0.8571429f, -0.9333334f, -0.9677419f, -0.98412704f, -0.992126f, -0.9960785f, -0.99804306f, -0.9990225f,
			-0.9995115f, -0.99975586f, -0.9998779f, -0.99993896f, -0.9999695f
		};

		// Token: 0x04000127 RID: 295
		protected int Allocation;

		// Token: 0x04000128 RID: 296
		protected float Factor;

		// Token: 0x04000129 RID: 297
		protected float Offset;

		// Token: 0x0400012A RID: 298
		protected float Sample;

		// Token: 0x0400012B RID: 299
		protected int Samplelength;

		// Token: 0x0400012C RID: 300
		protected int Samplenumber;

		// Token: 0x0400012D RID: 301
		protected float Scalefactor;

		// Token: 0x0400012E RID: 302
		protected readonly int Subbandnumber;
	}
}
