using System;
using System.IO;
using XPT.Core.Audio.MP3Sharp.Decoding;

namespace XPT.Core.Audio.MP3Sharp.IO
{
	// Token: 0x0200000A RID: 10
	public class WaveFileBuffer : ABuffer
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003604 File Offset: 0x00001804
		internal WaveFileBuffer(int numberOfChannels, int freq, string fileName)
		{
			if (fileName == null)
			{
				throw new NullReferenceException("FileName");
			}
			this._Buffer = new short[2304];
			this._Bufferp = new short[2];
			this._Channels = numberOfChannels;
			for (int i = 0; i < numberOfChannels; i++)
			{
				this._Bufferp[i] = (short)i;
			}
			this._OutWave = new WaveFile();
			this._OutWave.OpenForWrite(fileName, null, freq, 16, (short)this._Channels);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003684 File Offset: 0x00001884
		internal WaveFileBuffer(int numberOfChannels, int freq, Stream stream)
		{
			this._Buffer = new short[2304];
			this._Bufferp = new short[2];
			this._Channels = numberOfChannels;
			for (int i = 0; i < numberOfChannels; i++)
			{
				this._Bufferp[i] = (short)i;
			}
			this._OutWave = new WaveFile();
			this._OutWave.OpenForWrite(null, stream, freq, 16, (short)this._Channels);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000036F3 File Offset: 0x000018F3
		protected override void Append(int channel, short valueRenamed)
		{
			this._Buffer[(int)this._Bufferp[channel]] = valueRenamed;
			this._Bufferp[channel] = (short)((int)this._Bufferp[channel] + this._Channels);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003720 File Offset: 0x00001920
		internal override void WriteBuffer(int val)
		{
			this._OutWave.WriteData(this._Buffer, (int)this._Bufferp[0]);
			for (int i = 0; i < this._Channels; i++)
			{
				this._Bufferp[i] = (short)i;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003762 File Offset: 0x00001962
		internal void Close(bool justWriteLengthBytes)
		{
			this._OutWave.Close(justWriteLengthBytes);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003771 File Offset: 0x00001971
		internal override void Close()
		{
			this._OutWave.Close();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000377F File Offset: 0x0000197F
		internal override void ClearBuffer()
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003781 File Offset: 0x00001981
		internal override void SetStopFlag()
		{
		}

		// Token: 0x04000026 RID: 38
		private readonly short[] _Buffer;

		// Token: 0x04000027 RID: 39
		private readonly short[] _Bufferp;

		// Token: 0x04000028 RID: 40
		private readonly int _Channels;

		// Token: 0x04000029 RID: 41
		private readonly WaveFile _OutWave;
	}
}
