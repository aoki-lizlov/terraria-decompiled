using System;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerI;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders
{
	// Token: 0x0200001F RID: 31
	public class LayerIDecoder : IFrameDecoder
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0001553C File Offset: 0x0001373C
		internal LayerIDecoder()
		{
			this.CRC = new Crc16();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00015550 File Offset: 0x00013750
		public virtual void DecodeFrame()
		{
			this.NuSubbands = this.Header.NumberSubbands();
			this.Subbands = new ASubband[32];
			this.Mode = this.Header.Mode();
			this.CreateSubbands();
			this.ReadAllocation();
			this.ReadScaleFactorSelection();
			if (this.CRC != null || this.Header.IsChecksumOK())
			{
				this.ReadScaleFactors();
				this.ReadSampleData();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000155BF File Offset: 0x000137BF
		internal virtual void Create(Bitstream stream0, Header header0, SynthesisFilter filtera, SynthesisFilter filterb, ABuffer buffer0, int whichCh0)
		{
			this.Stream = stream0;
			this.Header = header0;
			this.Filter1 = filtera;
			this.Filter2 = filterb;
			this.Buffer = buffer0;
			this.WhichChannels = whichCh0;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000155F0 File Offset: 0x000137F0
		protected virtual void CreateSubbands()
		{
			if (this.Mode == 3)
			{
				for (int i = 0; i < this.NuSubbands; i++)
				{
					this.Subbands[i] = new SubbandLayer1(i);
				}
				return;
			}
			if (this.Mode == 1)
			{
				int i;
				for (i = 0; i < this.Header.IntensityStereoBound(); i++)
				{
					this.Subbands[i] = new SubbandLayer1Stereo(i);
				}
				while (i < this.NuSubbands)
				{
					this.Subbands[i] = new SubbandLayer1IntensityStereo(i);
					i++;
				}
				return;
			}
			for (int i = 0; i < this.NuSubbands; i++)
			{
				this.Subbands[i] = new SubbandLayer1Stereo(i);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00015690 File Offset: 0x00013890
		protected virtual void ReadAllocation()
		{
			for (int i = 0; i < this.NuSubbands; i++)
			{
				this.Subbands[i].ReadAllocation(this.Stream, this.Header, this.CRC);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000156CD File Offset: 0x000138CD
		protected virtual void ReadScaleFactorSelection()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000156D0 File Offset: 0x000138D0
		protected virtual void ReadScaleFactors()
		{
			for (int i = 0; i < this.NuSubbands; i++)
			{
				this.Subbands[i].ReadScaleFactor(this.Stream, this.Header);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00015708 File Offset: 0x00013908
		protected virtual void ReadSampleData()
		{
			bool flag = false;
			bool flag2 = false;
			int num = this.Header.Mode();
			do
			{
				for (int i = 0; i < this.NuSubbands; i++)
				{
					flag = this.Subbands[i].ReadSampleData(this.Stream);
				}
				do
				{
					for (int i = 0; i < this.NuSubbands; i++)
					{
						flag2 = this.Subbands[i].PutNextSample(this.WhichChannels, this.Filter1, this.Filter2);
					}
					this.Filter1.calculate_pc_samples(this.Buffer);
					if (this.WhichChannels == 0 && num != 3)
					{
						this.Filter2.calculate_pc_samples(this.Buffer);
					}
				}
				while (!flag2);
			}
			while (!flag);
		}

		// Token: 0x040000E8 RID: 232
		protected ABuffer Buffer;

		// Token: 0x040000E9 RID: 233
		protected readonly Crc16 CRC;

		// Token: 0x040000EA RID: 234
		protected SynthesisFilter Filter1;

		// Token: 0x040000EB RID: 235
		protected SynthesisFilter Filter2;

		// Token: 0x040000EC RID: 236
		protected Header Header;

		// Token: 0x040000ED RID: 237
		protected int Mode;

		// Token: 0x040000EE RID: 238
		protected int NuSubbands;

		// Token: 0x040000EF RID: 239
		protected Bitstream Stream;

		// Token: 0x040000F0 RID: 240
		protected ASubband[] Subbands;

		// Token: 0x040000F1 RID: 241
		protected int WhichChannels;
	}
}
