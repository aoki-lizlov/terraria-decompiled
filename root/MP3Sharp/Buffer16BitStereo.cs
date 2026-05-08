using System;
using XPT.Core.Audio.MP3Sharp.Decoding;

namespace XPT.Core.Audio.MP3Sharp
{
	// Token: 0x02000002 RID: 2
	public class Buffer16BitStereo : ABuffer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal Buffer16BitStereo()
		{
			this.ClearBuffer();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000207A File Offset: 0x0000027A
		internal int BytesLeft
		{
			get
			{
				return this._End - this._Offset;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		internal int Read(byte[] bufferOut, int offset, int count)
		{
			if (bufferOut == null)
			{
				throw new ArgumentNullException("bufferOut");
			}
			if (count + offset > bufferOut.Length)
			{
				throw new ArgumentException("The sum of offset and count is larger than the buffer length");
			}
			int bytesLeft = this.BytesLeft;
			int num;
			if (count > bytesLeft)
			{
				num = bytesLeft;
			}
			else
			{
				int num2 = count % 4;
				num = count - num2;
			}
			Array.Copy(this._Buffer, this._Offset, bufferOut, offset, num);
			this._Offset += num;
			return num;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		protected override void Append(int channel, short sampleValue)
		{
			this._Buffer[this._Bufferp[channel]] = (byte)(sampleValue & 255);
			this._Buffer[this._Bufferp[channel] + 1] = (byte)(sampleValue >> 8);
			this._Bufferp[channel] += 4;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002134 File Offset: 0x00000334
		internal override void AppendSamples(int channel, float[] samples)
		{
			if (samples == null)
			{
				throw new ArgumentNullException("samples");
			}
			if (samples.Length < 32)
			{
				throw new ArgumentException("samples must have 32 values");
			}
			if (this._Bufferp == null || channel >= this._Bufferp.Length)
			{
				throw new Exception("Song is closing...");
			}
			int num = this._Bufferp[channel];
			for (int i = 0; i < 32; i++)
			{
				float num2 = samples[i];
				if (num2 > 32767f)
				{
					num2 = 32767f;
				}
				else if (num2 < -32767f)
				{
					num2 = -32767f;
				}
				int num3 = (int)num2;
				this._Buffer[num] = (byte)(num3 & 255);
				this._Buffer[num + 1] = (byte)(num3 >> 8);
				if (this.DoubleMonoToStereo)
				{
					this._Buffer[num + 2] = (byte)(num3 & 255);
					this._Buffer[num + 3] = (byte)(num3 >> 8);
				}
				num += 4;
			}
			this._Bufferp[channel] = num;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002210 File Offset: 0x00000410
		internal sealed override void ClearBuffer()
		{
			this._Offset = 0;
			this._End = 0;
			for (int i = 0; i < 2; i++)
			{
				this._Bufferp[i] = i * 2;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002242 File Offset: 0x00000442
		internal override void SetStopFlag()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002244 File Offset: 0x00000444
		internal override void WriteBuffer(int val)
		{
			this._Offset = 0;
			this._End = this._Bufferp[0];
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000225B File Offset: 0x0000045B
		internal override void Close()
		{
		}

		// Token: 0x04000001 RID: 1
		internal bool DoubleMonoToStereo;

		// Token: 0x04000002 RID: 2
		private const int OUTPUT_CHANNELS = 2;

		// Token: 0x04000003 RID: 3
		private readonly byte[] _Buffer = new byte[4608];

		// Token: 0x04000004 RID: 4
		private readonly int[] _Bufferp = new int[2];

		// Token: 0x04000005 RID: 5
		private int _End;

		// Token: 0x04000006 RID: 6
		private int _Offset;
	}
}
