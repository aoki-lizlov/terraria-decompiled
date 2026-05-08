using System;
using System.IO;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200000D RID: 13
	public sealed class Bitstream
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000039F8 File Offset: 0x00001BF8
		internal Bitstream(PushbackStream stream)
		{
			this.InitBlock();
			if (stream == null)
			{
				throw new NullReferenceException("in stream is null");
			}
			this._SourceStream = stream;
			this.CloseFrame();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A48 File Offset: 0x00001C48
		private void InitBlock()
		{
			this._CRC = new Crc16[1];
			this._SyncBuffer = new sbyte[4];
			this._FrameBytes = new sbyte[1732];
			this._FrameBuffer = new int[433];
			this._Header = new Header();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A98 File Offset: 0x00001C98
		internal void Close()
		{
			try
			{
				this._SourceStream.Close();
			}
			catch (IOException ex)
			{
				throw this.NewBitstreamException(258, ex);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AD0 File Offset: 0x00001CD0
		internal Header ReadFrame()
		{
			Header header = null;
			try
			{
				header = this.ReadNextFrame();
			}
			catch (BitstreamException ex)
			{
				if (ex.ErrorCode != 260)
				{
					throw this.NewBitstreamException(ex.ErrorCode, ex);
				}
			}
			return header;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003B18 File Offset: 0x00001D18
		private Header ReadNextFrame()
		{
			if (this._FrameSize == -1)
			{
				this._Header.read_header(this, this._CRC);
			}
			return this._Header;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B3C File Offset: 0x00001D3C
		internal void UnreadFrame()
		{
			if (this._WordPointer == -1 && this._BitIndex == -1 && this._FrameSize > 0)
			{
				try
				{
					this._SourceStream.UnRead(this._FrameSize);
				}
				catch
				{
					throw this.NewBitstreamException(258);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B94 File Offset: 0x00001D94
		internal void CloseFrame()
		{
			this._FrameSize = -1;
			this._WordPointer = -1;
			this._BitIndex = -1;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003BAC File Offset: 0x00001DAC
		internal bool IsSyncCurrentPosition(int syncmode)
		{
			int num = this.ReadBytes(this._SyncBuffer, 0, 4);
			int num2 = (((int)this._SyncBuffer[0] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)this._SyncBuffer[1] << 16) & 16711680) | (((int)this._SyncBuffer[2] << 8) & 65280) | ((int)this._SyncBuffer[3] & 255);
			try
			{
				this._SourceStream.UnRead(num);
			}
			catch (Exception ex)
			{
				throw new MP3SharpException("Could not restore file after reading frame header.", ex);
			}
			bool flag = false;
			if (num != 0)
			{
				if (num == 4)
				{
					flag = this.IsSyncMark(num2, syncmode, this._SyncWord);
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C60 File Offset: 0x00001E60
		internal int ReadBits(int n)
		{
			return this.GetBitsFromBuffer(n);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C69 File Offset: 0x00001E69
		internal int ReadCheckedBits(int n)
		{
			return this.GetBitsFromBuffer(n);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C72 File Offset: 0x00001E72
		internal BitstreamException NewBitstreamException(int errorcode)
		{
			return new BitstreamException(errorcode, null);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003C7B File Offset: 0x00001E7B
		internal BitstreamException NewBitstreamException(int errorcode, Exception throwable)
		{
			return new BitstreamException(errorcode, throwable);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003C84 File Offset: 0x00001E84
		internal int SyncHeader(sbyte syncmode)
		{
			if (this.ReadBytes(this._SyncBuffer, 0, 3) != 3)
			{
				throw this.NewBitstreamException(260, null);
			}
			int num = (((int)this._SyncBuffer[0] << 16) & 16711680) | (((int)this._SyncBuffer[1] << 8) & 65280) | ((int)this._SyncBuffer[2] & 255);
			for (;;)
			{
				num <<= 8;
				if (this.ReadBytes(this._SyncBuffer, 3, 1) != 1)
				{
					break;
				}
				num |= (int)this._SyncBuffer[3] & 255;
				bool flag = this.IsSyncMark(num, (int)syncmode, this._SyncWord);
				if (flag)
				{
					return num;
				}
			}
			throw this.NewBitstreamException(260, null);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003D28 File Offset: 0x00001F28
		internal bool IsSyncMark(int headerstring, int syncmode, int word)
		{
			bool flag;
			if (syncmode == 0)
			{
				flag = ((long)headerstring & (long)((ulong)(-2097152))) == (long)((ulong)(-2097152));
			}
			else
			{
				flag = ((long)headerstring & (long)((ulong)(-2097152))) == (long)((ulong)(-2097152)) && (headerstring & 192) == 192 == this._SingleChMode;
			}
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 10) & 3) != 3;
			}
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 17) & 3) != 0;
			}
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 19) & 3) != 1;
				if (!flag)
				{
					Console.WriteLine("INVALID VERSION DETECTED");
				}
			}
			return flag;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003DBE File Offset: 0x00001FBE
		internal void Read_frame_data(int bytesize)
		{
			this.ReadFully(this._FrameBytes, 0, bytesize);
			this._FrameSize = bytesize;
			this._WordPointer = -1;
			this._BitIndex = -1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003DE4 File Offset: 0x00001FE4
		internal void ParseFrame()
		{
			int num = 0;
			sbyte[] frameBytes = this._FrameBytes;
			int frameSize = this._FrameSize;
			for (int i = 0; i < frameSize; i += 4)
			{
				sbyte b = frameBytes[i];
				sbyte b2 = 0;
				sbyte b3 = 0;
				sbyte b4 = 0;
				if (i + 1 < frameSize)
				{
					b2 = frameBytes[i + 1];
				}
				if (i + 2 < frameSize)
				{
					b3 = frameBytes[i + 2];
				}
				if (i + 3 < frameSize)
				{
					b4 = frameBytes[i + 3];
				}
				this._FrameBuffer[num++] = (((int)b << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)b2 << 16) & 16711680) | (((int)b3 << 8) & 65280) | ((int)b4 & 255);
			}
			this._WordPointer = 0;
			this._BitIndex = 0;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003E90 File Offset: 0x00002090
		internal int GetBitsFromBuffer(int countBits)
		{
			int num = this._BitIndex + countBits;
			if (this._WordPointer < 0)
			{
				this._WordPointer = 0;
			}
			if (num <= 32)
			{
				int num2 = SupportClass.URShift(this._FrameBuffer[this._WordPointer], 32 - num) & this._Bitmask[countBits];
				if ((this._BitIndex += countBits) == 32)
				{
					this._BitIndex = 0;
					this._WordPointer++;
				}
				return num2;
			}
			int num3 = this._FrameBuffer[this._WordPointer] & 65535;
			this._WordPointer++;
			int num4 = this._FrameBuffer[this._WordPointer] & (int)SupportClass.Identity((long)((ulong)(-65536)));
			int num5 = SupportClass.URShift(((num3 << 16) & (int)SupportClass.Identity((long)((ulong)(-65536)))) | (SupportClass.URShift(num4, 16) & 65535), 48 - num) & this._Bitmask[countBits];
			this._BitIndex = num - 32;
			return num5;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003F7C File Offset: 0x0000217C
		internal void SetSyncWord(int syncword0)
		{
			this._SyncWord = syncword0 & -193;
			this._SingleChMode = (syncword0 & 192) == 192;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003FA0 File Offset: 0x000021A0
		private void ReadFully(sbyte[] b, int offs, int len)
		{
			try
			{
				while (len > 0)
				{
					int num = this._SourceStream.Read(b, offs, len);
					if (num != -1)
					{
						if (num != 0)
						{
							offs += num;
							len -= num;
							continue;
						}
					}
					while (len-- > 0)
					{
						b[offs++] = 0;
					}
					break;
				}
			}
			catch (IOException ex)
			{
				throw this.NewBitstreamException(258, ex);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000400C File Offset: 0x0000220C
		private int ReadBytes(sbyte[] b, int offs, int len)
		{
			int num = 0;
			try
			{
				while (len > 0)
				{
					int num2 = this._SourceStream.Read(b, offs, len);
					if (num2 == -1 || num2 == 0)
					{
						break;
					}
					num += num2;
					offs += num2;
					len -= num2;
				}
			}
			catch (IOException ex)
			{
				throw this.NewBitstreamException(258, ex);
			}
			return num;
		}

		// Token: 0x04000032 RID: 50
		private const int BUFFER_INT_SIZE = 433;

		// Token: 0x04000033 RID: 51
		internal const sbyte INITIAL_SYNC = 0;

		// Token: 0x04000034 RID: 52
		internal const sbyte STRICT_SYNC = 1;

		// Token: 0x04000035 RID: 53
		private readonly int[] _Bitmask = new int[]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535, 131071
		};

		// Token: 0x04000036 RID: 54
		private readonly PushbackStream _SourceStream;

		// Token: 0x04000037 RID: 55
		private int _BitIndex;

		// Token: 0x04000038 RID: 56
		private Crc16[] _CRC;

		// Token: 0x04000039 RID: 57
		private sbyte[] _FrameBytes;

		// Token: 0x0400003A RID: 58
		private int[] _FrameBuffer;

		// Token: 0x0400003B RID: 59
		private int _FrameSize;

		// Token: 0x0400003C RID: 60
		private Header _Header;

		// Token: 0x0400003D RID: 61
		private bool _SingleChMode;

		// Token: 0x0400003E RID: 62
		private sbyte[] _SyncBuffer;

		// Token: 0x0400003F RID: 63
		private int _SyncWord;

		// Token: 0x04000040 RID: 64
		private int _WordPointer;
	}
}
