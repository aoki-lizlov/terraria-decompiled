using System;
using System.IO;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.IO
{
	// Token: 0x02000009 RID: 9
	public class WaveFile : RiffFile
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003374 File Offset: 0x00001574
		internal WaveFile()
		{
			this._PcmData = new RiffFile.RiffChunkHeader(this);
			this._WaveFormat = new WaveFile.WaveFormatChunk(this);
			this._PcmData.CkId = RiffFile.FourCC("data");
			this._PcmData.CkSize = 0;
			this._NumSamples = 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033C8 File Offset: 0x000015C8
		internal virtual int OpenForWrite(string filename, Stream stream, int samplingRate, short bitsPerSample, short numChannels)
		{
			if ((bitsPerSample != 8 && bitsPerSample != 16) || numChannels < 1 || numChannels > 2)
			{
				return 4;
			}
			this._WaveFormat.Data.Config(samplingRate, bitsPerSample, numChannels);
			int num = 0;
			if (stream != null)
			{
				this.Open(stream, 1);
			}
			else
			{
				this.Open(filename, 1);
			}
			if (num == 0)
			{
				sbyte[] array = new sbyte[]
				{
					(sbyte)SupportClass.Identity(87L),
					(sbyte)SupportClass.Identity(65L),
					(sbyte)SupportClass.Identity(86L),
					(sbyte)SupportClass.Identity(69L)
				};
				num = this.Write(array, 4);
				if (num == 0)
				{
					num = this.Write(this._WaveFormat.Header, 8);
					num = this.Write(this._WaveFormat.Data.FormatTag, 2);
					num = this.Write(this._WaveFormat.Data.NumChannels, 2);
					num = this.Write(this._WaveFormat.Data.NumSamplesPerSec, 4);
					num = this.Write(this._WaveFormat.Data.NumAvgBytesPerSec, 4);
					num = this.Write(this._WaveFormat.Data.NumBlockAlign, 2);
					num = this.Write(this._WaveFormat.Data.NumBitsPerSample, 2);
					if (num == 0)
					{
						this._PcmDataOffset = this.CurrentFilePosition();
						num = this.Write(this._PcmData, 8);
					}
				}
			}
			return num;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003528 File Offset: 0x00001728
		internal virtual int WriteData(short[] data, int numData)
		{
			int num = numData * 2;
			this._PcmData.CkSize += num;
			return this.Write(data, num);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003554 File Offset: 0x00001754
		internal override int Close()
		{
			int num = 0;
			if (this.Fmode == 1)
			{
				num = this.Backpatch(this._PcmDataOffset, this._PcmData, 8);
			}
			if (!this._JustWriteLengthBytes && num == 0)
			{
				num = base.Close();
			}
			return num;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003593 File Offset: 0x00001793
		internal int Close(bool justWriteLengthBytes)
		{
			this._JustWriteLengthBytes = justWriteLengthBytes;
			int num = this.Close();
			this._JustWriteLengthBytes = false;
			return num;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000035A9 File Offset: 0x000017A9
		internal virtual int SamplingRate()
		{
			return this._WaveFormat.Data.NumSamplesPerSec;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000035BB File Offset: 0x000017BB
		internal virtual short BitsPerSample()
		{
			return this._WaveFormat.Data.NumBitsPerSample;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035CD File Offset: 0x000017CD
		internal virtual short NumChannels()
		{
			return this._WaveFormat.Data.NumChannels;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000035DF File Offset: 0x000017DF
		internal virtual int NumSamples()
		{
			return this._NumSamples;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000035E7 File Offset: 0x000017E7
		internal virtual int OpenForWrite(string filename, WaveFile otherWave)
		{
			return this.OpenForWrite(filename, null, otherWave.SamplingRate(), otherWave.BitsPerSample(), otherWave.NumChannels());
		}

		// Token: 0x04000020 RID: 32
		internal const int MAX_WAVE_CHANNELS = 2;

		// Token: 0x04000021 RID: 33
		private readonly int _NumSamples;

		// Token: 0x04000022 RID: 34
		private readonly RiffFile.RiffChunkHeader _PcmData;

		// Token: 0x04000023 RID: 35
		private readonly WaveFile.WaveFormatChunk _WaveFormat;

		// Token: 0x04000024 RID: 36
		private bool _JustWriteLengthBytes;

		// Token: 0x04000025 RID: 37
		private long _PcmDataOffset;

		// Token: 0x02000030 RID: 48
		internal sealed class WaveFormatChunkData
		{
			// Token: 0x0600016B RID: 363 RVA: 0x0001B58C File Offset: 0x0001978C
			internal WaveFormatChunkData(WaveFile enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
				this.FormatTag = 1;
				this.Config(44100, 16, 1);
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600016C RID: 364 RVA: 0x0001B5B0 File Offset: 0x000197B0
			internal WaveFile EnclosingInstance
			{
				get
				{
					return this._EnclosingInstance;
				}
			}

			// Token: 0x0600016D RID: 365 RVA: 0x0001B5B8 File Offset: 0x000197B8
			private void InitBlock(WaveFile enclosingInstance)
			{
				this._EnclosingInstance = enclosingInstance;
			}

			// Token: 0x0600016E RID: 366 RVA: 0x0001B5C4 File Offset: 0x000197C4
			internal void Config(int newSamplingRate, short newBitsPerSample, short newNumChannels)
			{
				this.NumSamplesPerSec = newSamplingRate;
				this.NumChannels = newNumChannels;
				this.NumBitsPerSample = newBitsPerSample;
				this.NumAvgBytesPerSec = (int)this.NumChannels * this.NumSamplesPerSec * (int)this.NumBitsPerSample / 8;
				this.NumBlockAlign = this.NumChannels * this.NumBitsPerSample / 8;
			}

			// Token: 0x040001DA RID: 474
			private WaveFile _EnclosingInstance;

			// Token: 0x040001DB RID: 475
			internal int NumAvgBytesPerSec;

			// Token: 0x040001DC RID: 476
			internal short NumBitsPerSample;

			// Token: 0x040001DD RID: 477
			internal short NumBlockAlign;

			// Token: 0x040001DE RID: 478
			internal short NumChannels;

			// Token: 0x040001DF RID: 479
			internal int NumSamplesPerSec;

			// Token: 0x040001E0 RID: 480
			internal short FormatTag;
		}

		// Token: 0x02000031 RID: 49
		public class WaveFormatChunk
		{
			// Token: 0x0600016F RID: 367 RVA: 0x0001B618 File Offset: 0x00019818
			internal WaveFormatChunk(WaveFile enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
				this.Header = new RiffFile.RiffChunkHeader(enclosingInstance);
				this.Data = new WaveFile.WaveFormatChunkData(enclosingInstance);
				this.Header.CkId = RiffFile.FourCC("fmt ");
				this.Header.CkSize = 16;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000170 RID: 368 RVA: 0x0001B66C File Offset: 0x0001986C
			internal WaveFile EnclosingInstance
			{
				get
				{
					return this._EnclosingInstance;
				}
			}

			// Token: 0x06000171 RID: 369 RVA: 0x0001B674 File Offset: 0x00019874
			private void InitBlock(WaveFile enclosingInstance)
			{
				this._EnclosingInstance = enclosingInstance;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0001B680 File Offset: 0x00019880
			internal virtual int VerifyValidity()
			{
				if (this.Header.CkId != RiffFile.FourCC("fmt ") || (this.Data.NumChannels != 1 && this.Data.NumChannels != 2) || this.Data.NumAvgBytesPerSec != (int)this.Data.NumChannels * this.Data.NumSamplesPerSec * (int)this.Data.NumBitsPerSample / 8 || this.Data.NumBlockAlign != this.Data.NumChannels * this.Data.NumBitsPerSample / 8)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x040001E1 RID: 481
			internal WaveFile.WaveFormatChunkData Data;

			// Token: 0x040001E2 RID: 482
			private WaveFile _EnclosingInstance;

			// Token: 0x040001E3 RID: 483
			internal RiffFile.RiffChunkHeader Header;
		}

		// Token: 0x02000032 RID: 50
		public class WaveFileSample
		{
			// Token: 0x06000173 RID: 371 RVA: 0x0001B720 File Offset: 0x00019920
			internal WaveFileSample(WaveFile enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
				this.Chan = new short[2];
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000174 RID: 372 RVA: 0x0001B73B File Offset: 0x0001993B
			internal WaveFile EnclosingInstance
			{
				get
				{
					return this._EnclosingInstance;
				}
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0001B743 File Offset: 0x00019943
			private void InitBlock(WaveFile enclosingInstance)
			{
				this._EnclosingInstance = enclosingInstance;
			}

			// Token: 0x040001E4 RID: 484
			internal short[] Chan;

			// Token: 0x040001E5 RID: 485
			private WaveFile _EnclosingInstance;
		}
	}
}
