using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200000B RID: 11
	public abstract class ABuffer
	{
		// Token: 0x0600005B RID: 91
		protected abstract void Append(int channel, short sampleValue);

		// Token: 0x0600005C RID: 92 RVA: 0x00003784 File Offset: 0x00001984
		internal virtual void AppendSamples(int channel, float[] samples)
		{
			for (int i = 0; i < 32; i++)
			{
				this.Append(channel, ABuffer.Clip(samples[i]));
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037AD File Offset: 0x000019AD
		private static short Clip(float sample)
		{
			if (sample > 32767f)
			{
				return short.MaxValue;
			}
			if (sample >= -32768f)
			{
				return (short)sample;
			}
			return short.MinValue;
		}

		// Token: 0x0600005E RID: 94
		internal abstract void WriteBuffer(int val);

		// Token: 0x0600005F RID: 95
		internal abstract void Close();

		// Token: 0x06000060 RID: 96
		internal abstract void ClearBuffer();

		// Token: 0x06000061 RID: 97
		internal abstract void SetStopFlag();

		// Token: 0x06000062 RID: 98 RVA: 0x000037CD File Offset: 0x000019CD
		protected ABuffer()
		{
		}

		// Token: 0x0400002A RID: 42
		internal const int OBUFFERSIZE = 2304;

		// Token: 0x0400002B RID: 43
		internal const int MAXCHANNELS = 2;
	}
}
