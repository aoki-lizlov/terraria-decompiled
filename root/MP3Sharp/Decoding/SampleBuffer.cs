using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200001B RID: 27
	public class SampleBuffer : ABuffer
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00012FD4 File Offset: 0x000111D4
		internal SampleBuffer(int sampleFrequency, int numberOfChannels)
		{
			this._Buffer = new short[2304];
			this._Bufferp = new int[2];
			this._Channels = numberOfChannels;
			this._Frequency = sampleFrequency;
			for (int i = 0; i < numberOfChannels; i++)
			{
				this._Bufferp[i] = (int)((short)i);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00013027 File Offset: 0x00011227
		internal virtual int ChannelCount
		{
			get
			{
				return this._Channels;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0001302F File Offset: 0x0001122F
		internal virtual int SampleFrequency
		{
			get
			{
				return this._Frequency;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00013037 File Offset: 0x00011237
		internal virtual short[] Buffer
		{
			get
			{
				return this._Buffer;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0001303F File Offset: 0x0001123F
		internal virtual int BufferLength
		{
			get
			{
				return this._Bufferp[0];
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00013049 File Offset: 0x00011249
		protected override void Append(int channel, short valueRenamed)
		{
			this._Buffer[this._Bufferp[channel]] = valueRenamed;
			this._Bufferp[channel] += this._Channels;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00013074 File Offset: 0x00011274
		internal override void AppendSamples(int channel, float[] samples)
		{
			int num = this._Bufferp[channel];
			int i = 0;
			while (i < 32)
			{
				float num2 = samples[i++];
				num2 = ((num2 > 32767f) ? 32767f : ((num2 < -32767f) ? (-32767f) : num2));
				short num3 = (short)num2;
				this._Buffer[num] = num3;
				num += this._Channels;
			}
			this._Bufferp[channel] = num;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000130D9 File Offset: 0x000112D9
		internal override void WriteBuffer(int val)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000130DB File Offset: 0x000112DB
		internal override void Close()
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000130E0 File Offset: 0x000112E0
		internal override void ClearBuffer()
		{
			for (int i = 0; i < this._Channels; i++)
			{
				this._Bufferp[i] = (int)((short)i);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00013108 File Offset: 0x00011308
		internal override void SetStopFlag()
		{
		}

		// Token: 0x040000B7 RID: 183
		private readonly short[] _Buffer;

		// Token: 0x040000B8 RID: 184
		private readonly int[] _Bufferp;

		// Token: 0x040000B9 RID: 185
		private readonly int _Channels;

		// Token: 0x040000BA RID: 186
		private readonly int _Frequency;
	}
}
