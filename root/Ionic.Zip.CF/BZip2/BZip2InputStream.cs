using System;
using System.IO;
using Ionic.Crc;

namespace Ionic.BZip2
{
	// Token: 0x02000033 RID: 51
	public class BZip2InputStream : Stream
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000FE2B File Offset: 0x0000E02B
		public BZip2InputStream(Stream input)
			: this(input, false)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FE35 File Offset: 0x0000E035
		public BZip2InputStream(Stream input, bool leaveOpen)
		{
			this.input = input;
			this._leaveOpen = leaveOpen;
			this.init();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException(string.Format("offset ({0}) must be > 0", offset));
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException(string.Format("count ({0}) must be > 0", count));
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException(string.Format("offset({0}) count({1}) bLength({2})", offset, count, buffer.Length));
			}
			if (this.input == null)
			{
				throw new IOException("the stream is not open");
			}
			int num = offset + count;
			int num2 = offset;
			int num3;
			while (num2 < num && (num3 = this.ReadByte()) >= 0)
			{
				buffer[num2++] = (byte)num3;
			}
			if (num2 != offset)
			{
				return num2 - offset;
			}
			return -1;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000FF18 File Offset: 0x0000E118
		private void MakeMaps()
		{
			bool[] inUse = this.data.inUse;
			byte[] seqToUnseq = this.data.seqToUnseq;
			int num = 0;
			for (int i = 0; i < 256; i++)
			{
				if (inUse[i])
				{
					seqToUnseq[num++] = (byte)i;
				}
			}
			this.nInUse = num;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000FF64 File Offset: 0x0000E164
		public override int ReadByte()
		{
			int num = this.currentChar;
			this.totalBytesRead += 1L;
			switch (this.currentState)
			{
			case BZip2InputStream.CState.EOF:
				return -1;
			case BZip2InputStream.CState.START_BLOCK:
				throw new IOException("bad state");
			case BZip2InputStream.CState.RAND_PART_A:
				throw new IOException("bad state");
			case BZip2InputStream.CState.RAND_PART_B:
				this.SetupRandPartB();
				break;
			case BZip2InputStream.CState.RAND_PART_C:
				this.SetupRandPartC();
				break;
			case BZip2InputStream.CState.NO_RAND_PART_A:
				throw new IOException("bad state");
			case BZip2InputStream.CState.NO_RAND_PART_B:
				this.SetupNoRandPartB();
				break;
			case BZip2InputStream.CState.NO_RAND_PART_C:
				this.SetupNoRandPartC();
				break;
			default:
				throw new IOException("bad state");
			}
			return num;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010005 File Offset: 0x0000E205
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return this.input.CanRead;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00010025 File Offset: 0x0000E225
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010028 File Offset: 0x0000E228
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return this.input.CanWrite;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010048 File Offset: 0x0000E248
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("BZip2Stream");
			}
			this.input.Flush();
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010068 File Offset: 0x0000E268
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001006F File Offset: 0x0000E26F
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00010077 File Offset: 0x0000E277
		public override long Position
		{
			get
			{
				return this.totalBytesRead;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001007E File Offset: 0x0000E27E
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00010085 File Offset: 0x0000E285
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001008C File Offset: 0x0000E28C
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010094 File Offset: 0x0000E294
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this.input != null)
					{
						this.input.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000100E0 File Offset: 0x0000E2E0
		private void init()
		{
			if (this.input == null)
			{
				throw new IOException("No input Stream");
			}
			if (!this.input.CanRead)
			{
				throw new IOException("Unreadable input Stream");
			}
			this.CheckMagicChar('B', 0);
			this.CheckMagicChar('Z', 1);
			this.CheckMagicChar('h', 2);
			int num = this.input.ReadByte();
			if (num < 49 || num > 57)
			{
				throw new IOException("Stream is not BZip2 formatted: illegal blocksize " + (char)num);
			}
			this.blockSize100k = num - 48;
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010178 File Offset: 0x0000E378
		private void CheckMagicChar(char expected, int position)
		{
			int num = this.input.ReadByte();
			if (num != (int)expected)
			{
				string text = string.Format("Not a valid BZip2 stream. byte {0}, expected '{1}', got '{2}'", position, (int)expected, num);
				throw new IOException(text);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000101BC File Offset: 0x0000E3BC
		private void InitBlock()
		{
			char c = this.bsGetUByte();
			char c2 = this.bsGetUByte();
			char c3 = this.bsGetUByte();
			char c4 = this.bsGetUByte();
			char c5 = this.bsGetUByte();
			char c6 = this.bsGetUByte();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				this.complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				this.currentState = BZip2InputStream.CState.EOF;
				string text = string.Format("bad block header at offset 0x{0:X}", this.input.Position);
				throw new IOException(text);
			}
			this.storedBlockCRC = this.bsGetInt();
			this.blockRandomised = this.GetBits(1) == 1;
			if (this.data == null)
			{
				this.data = new BZip2InputStream.DecompressionState(this.blockSize100k);
			}
			this.getAndMoveToFrontDecode();
			this.crc.Reset();
			this.currentState = BZip2InputStream.CState.START_BLOCK;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private void EndBlock()
		{
			this.computedBlockCRC = (uint)this.crc.Crc32Result;
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				string text = string.Format("BZip2 CRC error (expected {0:X8}, computed {1:X8})", this.storedBlockCRC, this.computedBlockCRC);
				throw new IOException(text);
			}
			this.computedCombinedCRC = (this.computedCombinedCRC << 1) | (this.computedCombinedCRC >> 31);
			this.computedCombinedCRC ^= this.computedBlockCRC;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010338 File Offset: 0x0000E538
		private void complete()
		{
			this.storedCombinedCRC = this.bsGetInt();
			this.currentState = BZip2InputStream.CState.EOF;
			this.data = null;
			if (this.storedCombinedCRC != this.computedCombinedCRC)
			{
				string text = string.Format("BZip2 CRC error (expected {0:X8}, computed {1:X8})", this.storedCombinedCRC, this.computedCombinedCRC);
				throw new IOException(text);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010398 File Offset: 0x0000E598
		public override void Close()
		{
			Stream stream = this.input;
			if (stream != null)
			{
				try
				{
					if (!this._leaveOpen)
					{
						stream.Close();
					}
				}
				finally
				{
					this.data = null;
					this.input = null;
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000103E0 File Offset: 0x0000E5E0
		private int GetBits(int n)
		{
			int num = this.bsLive;
			int num2 = this.bsBuff;
			if (num < n)
			{
				for (;;)
				{
					int num3 = this.input.ReadByte();
					if (num3 < 0)
					{
						break;
					}
					num2 = (num2 << 8) | num3;
					num += 8;
					if (num >= n)
					{
						goto Block_2;
					}
				}
				throw new IOException("unexpected end of stream");
				Block_2:
				this.bsBuff = num2;
			}
			this.bsLive = num - n;
			return (num2 >> num - n) & ((1 << n) - 1);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001044C File Offset: 0x0000E64C
		private bool bsGetBit()
		{
			int bits = this.GetBits(1);
			return bits != 0;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010468 File Offset: 0x0000E668
		private char bsGetUByte()
		{
			return (char)this.GetBits(8);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010472 File Offset: 0x0000E672
		private uint bsGetInt()
		{
			return (uint)((((((this.GetBits(8) << 8) | this.GetBits(8)) << 8) | this.GetBits(8)) << 8) | this.GetBits(8));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001049C File Offset: 0x0000E69C
		private static void hbCreateDecodeTables(int[] limit, int[] bbase, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int i = minLen;
			int num = 0;
			while (i <= maxLen)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						perm[num++] = j;
					}
				}
				i++;
			}
			int num2 = BZip2.MaxCodeLength;
			while (--num2 > 0)
			{
				bbase[num2] = 0;
				limit[num2] = 0;
			}
			for (int k = 0; k < alphaSize; k++)
			{
				bbase[(int)(length[k] + '\u0001')]++;
			}
			int l = 1;
			int num3 = bbase[0];
			while (l < BZip2.MaxCodeLength)
			{
				num3 += bbase[l];
				bbase[l] = num3;
				l++;
			}
			int m = minLen;
			int num4 = 0;
			int num5 = bbase[m];
			while (m <= maxLen)
			{
				int num6 = bbase[m + 1];
				num4 += num6 - num5;
				num5 = num6;
				limit[m] = num4 - 1;
				num4 <<= 1;
				m++;
			}
			for (int n = minLen + 1; n <= maxLen; n++)
			{
				bbase[n] = (limit[n - 1] + 1 << 1) - bbase[n];
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000105A4 File Offset: 0x0000E7A4
		private void recvDecodingTables()
		{
			BZip2InputStream.DecompressionState decompressionState = this.data;
			bool[] inUse = decompressionState.inUse;
			byte[] recvDecodingTables_pos = decompressionState.recvDecodingTables_pos;
			int num = 0;
			for (int i = 0; i < 16; i++)
			{
				if (this.bsGetBit())
				{
					num |= 1 << i;
				}
			}
			int num2 = 256;
			while (--num2 >= 0)
			{
				inUse[num2] = false;
			}
			for (int j = 0; j < 16; j++)
			{
				if ((num & (1 << j)) != 0)
				{
					int num3 = j << 4;
					for (int k = 0; k < 16; k++)
					{
						if (this.bsGetBit())
						{
							inUse[num3 + k] = true;
						}
					}
				}
			}
			this.MakeMaps();
			int num4 = this.nInUse + 2;
			int bits = this.GetBits(3);
			int bits2 = this.GetBits(15);
			for (int l = 0; l < bits2; l++)
			{
				int num5 = 0;
				while (this.bsGetBit())
				{
					num5++;
				}
				decompressionState.selectorMtf[l] = (byte)num5;
			}
			int num6 = bits;
			while (--num6 >= 0)
			{
				recvDecodingTables_pos[num6] = (byte)num6;
			}
			for (int m = 0; m < bits2; m++)
			{
				int n = (int)decompressionState.selectorMtf[m];
				byte b = recvDecodingTables_pos[n];
				while (n > 0)
				{
					recvDecodingTables_pos[n] = recvDecodingTables_pos[n - 1];
					n--;
				}
				recvDecodingTables_pos[0] = b;
				decompressionState.selector[m] = b;
			}
			char[][] temp_charArray2d = decompressionState.temp_charArray2d;
			for (int num7 = 0; num7 < bits; num7++)
			{
				int num8 = this.GetBits(5);
				char[] array = temp_charArray2d[num7];
				for (int num9 = 0; num9 < num4; num9++)
				{
					while (this.bsGetBit())
					{
						num8 += (this.bsGetBit() ? (-1) : 1);
					}
					array[num9] = (char)num8;
				}
			}
			this.createHuffmanDecodingTables(num4, bits);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010760 File Offset: 0x0000E960
		private void createHuffmanDecodingTables(int alphaSize, int nGroups)
		{
			BZip2InputStream.DecompressionState decompressionState = this.data;
			char[][] temp_charArray2d = decompressionState.temp_charArray2d;
			for (int i = 0; i < nGroups; i++)
			{
				int num = 32;
				int num2 = 0;
				char[] array = temp_charArray2d[i];
				int num3 = alphaSize;
				while (--num3 >= 0)
				{
					char c = array[num3];
					if ((int)c > num2)
					{
						num2 = (int)c;
					}
					if ((int)c < num)
					{
						num = (int)c;
					}
				}
				BZip2InputStream.hbCreateDecodeTables(decompressionState.gLimit[i], decompressionState.gBase[i], decompressionState.gPerm[i], temp_charArray2d[i], num, num2, alphaSize);
				decompressionState.gMinlen[i] = num;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000107E8 File Offset: 0x0000E9E8
		private void getAndMoveToFrontDecode()
		{
			BZip2InputStream.DecompressionState decompressionState = this.data;
			this.origPtr = this.GetBits(24);
			if (this.origPtr < 0)
			{
				throw new IOException("BZ_DATA_ERROR");
			}
			if (this.origPtr > 10 + BZip2.BlockSizeMultiple * this.blockSize100k)
			{
				throw new IOException("BZ_DATA_ERROR");
			}
			this.recvDecodingTables();
			byte[] getAndMoveToFrontDecode_yy = decompressionState.getAndMoveToFrontDecode_yy;
			int num = this.blockSize100k * BZip2.BlockSizeMultiple;
			int num2 = 256;
			while (--num2 >= 0)
			{
				getAndMoveToFrontDecode_yy[num2] = (byte)num2;
				decompressionState.unzftab[num2] = 0;
			}
			int num3 = 0;
			int num4 = BZip2.G_SIZE - 1;
			int num5 = this.nInUse + 1;
			int num6 = this.getAndMoveToFrontDecode0(0);
			int num7 = this.bsBuff;
			int i = this.bsLive;
			int num8 = -1;
			int num9 = (int)(decompressionState.selector[num3] & byte.MaxValue);
			int[] array = decompressionState.gBase[num9];
			int[] array2 = decompressionState.gLimit[num9];
			int[] array3 = decompressionState.gPerm[num9];
			int num10 = decompressionState.gMinlen[num9];
			while (num6 != num5)
			{
				if (num6 == (int)BZip2.RUNA || num6 == (int)BZip2.RUNB)
				{
					int num11 = -1;
					int num12 = 1;
					for (;;)
					{
						if (num6 == (int)BZip2.RUNA)
						{
							num11 += num12;
						}
						else
						{
							if (num6 != (int)BZip2.RUNB)
							{
								break;
							}
							num11 += num12 << 1;
						}
						if (num4 == 0)
						{
							num4 = BZip2.G_SIZE - 1;
							num9 = (int)(decompressionState.selector[++num3] & byte.MaxValue);
							array = decompressionState.gBase[num9];
							array2 = decompressionState.gLimit[num9];
							array3 = decompressionState.gPerm[num9];
							num10 = decompressionState.gMinlen[num9];
						}
						else
						{
							num4--;
						}
						int num13 = num10;
						while (i < num13)
						{
							int num14 = this.input.ReadByte();
							if (num14 < 0)
							{
								goto IL_01B9;
							}
							num7 = (num7 << 8) | num14;
							i += 8;
						}
						int j = (num7 >> i - num13) & ((1 << num13) - 1);
						i -= num13;
						while (j > array2[num13])
						{
							num13++;
							while (i < 1)
							{
								int num15 = this.input.ReadByte();
								if (num15 < 0)
								{
									goto IL_0215;
								}
								num7 = (num7 << 8) | num15;
								i += 8;
							}
							i--;
							j = (j << 1) | ((num7 >> i) & 1);
						}
						num6 = array3[j - array[num13]];
						num12 <<= 1;
					}
					byte b = decompressionState.seqToUnseq[(int)getAndMoveToFrontDecode_yy[0]];
					decompressionState.unzftab[(int)(b & byte.MaxValue)] += num11 + 1;
					while (num11-- >= 0)
					{
						decompressionState.ll8[++num8] = b;
					}
					if (num8 >= num)
					{
						throw new IOException("block overrun");
					}
					continue;
					IL_01B9:
					throw new IOException("unexpected end of stream");
					IL_0215:
					throw new IOException("unexpected end of stream");
				}
				if (++num8 >= num)
				{
					throw new IOException("block overrun");
				}
				byte b2 = getAndMoveToFrontDecode_yy[num6 - 1];
				decompressionState.unzftab[(int)(decompressionState.seqToUnseq[(int)b2] & byte.MaxValue)]++;
				decompressionState.ll8[num8] = decompressionState.seqToUnseq[(int)b2];
				if (num6 <= 16)
				{
					int k = num6 - 1;
					while (k > 0)
					{
						getAndMoveToFrontDecode_yy[k] = getAndMoveToFrontDecode_yy[--k];
					}
				}
				else
				{
					Buffer.BlockCopy(getAndMoveToFrontDecode_yy, 0, getAndMoveToFrontDecode_yy, 1, num6 - 1);
				}
				getAndMoveToFrontDecode_yy[0] = b2;
				if (num4 == 0)
				{
					num4 = BZip2.G_SIZE - 1;
					num9 = (int)(decompressionState.selector[++num3] & byte.MaxValue);
					array = decompressionState.gBase[num9];
					array2 = decompressionState.gLimit[num9];
					array3 = decompressionState.gPerm[num9];
					num10 = decompressionState.gMinlen[num9];
				}
				else
				{
					num4--;
				}
				int num16 = num10;
				while (i < num16)
				{
					int num17 = this.input.ReadByte();
					if (num17 < 0)
					{
						throw new IOException("unexpected end of stream");
					}
					num7 = (num7 << 8) | num17;
					i += 8;
				}
				int l = (num7 >> i - num16) & ((1 << num16) - 1);
				i -= num16;
				while (l > array2[num16])
				{
					num16++;
					while (i < 1)
					{
						int num18 = this.input.ReadByte();
						if (num18 < 0)
						{
							throw new IOException("unexpected end of stream");
						}
						num7 = (num7 << 8) | num18;
						i += 8;
					}
					i--;
					l = (l << 1) | ((num7 >> i) & 1);
				}
				num6 = array3[l - array[num16]];
			}
			this.last = num8;
			this.bsLive = i;
			this.bsBuff = num7;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010C74 File Offset: 0x0000EE74
		private int getAndMoveToFrontDecode0(int groupNo)
		{
			BZip2InputStream.DecompressionState decompressionState = this.data;
			int num = (int)(decompressionState.selector[groupNo] & byte.MaxValue);
			int[] array = decompressionState.gLimit[num];
			int num2 = decompressionState.gMinlen[num];
			int i = this.GetBits(num2);
			int j = this.bsLive;
			int num3 = this.bsBuff;
			while (i > array[num2])
			{
				num2++;
				while (j < 1)
				{
					int num4 = this.input.ReadByte();
					if (num4 < 0)
					{
						throw new IOException("unexpected end of stream");
					}
					num3 = (num3 << 8) | num4;
					j += 8;
				}
				j--;
				i = (i << 1) | ((num3 >> j) & 1);
			}
			this.bsLive = j;
			this.bsBuff = num3;
			return decompressionState.gPerm[num][i - decompressionState.gBase[num][num2]];
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010D44 File Offset: 0x0000EF44
		private void SetupBlock()
		{
			if (this.data == null)
			{
				return;
			}
			BZip2InputStream.DecompressionState decompressionState = this.data;
			int[] array = decompressionState.initTT(this.last + 1);
			int i;
			for (i = 0; i <= 255; i++)
			{
				if (decompressionState.unzftab[i] < 0 || decompressionState.unzftab[i] > this.last)
				{
					throw new Exception("BZ_DATA_ERROR");
				}
			}
			decompressionState.cftab[0] = 0;
			for (i = 1; i <= 256; i++)
			{
				decompressionState.cftab[i] = decompressionState.unzftab[i - 1];
			}
			for (i = 1; i <= 256; i++)
			{
				decompressionState.cftab[i] += decompressionState.cftab[i - 1];
			}
			for (i = 0; i <= 256; i++)
			{
				if (decompressionState.cftab[i] < 0 || decompressionState.cftab[i] > this.last + 1)
				{
					string text = string.Format("BZ_DATA_ERROR: cftab[{0}]={1} last={2}", i, decompressionState.cftab[i], this.last);
					throw new Exception(text);
				}
			}
			for (i = 1; i <= 256; i++)
			{
				if (decompressionState.cftab[i - 1] > decompressionState.cftab[i])
				{
					throw new Exception("BZ_DATA_ERROR");
				}
			}
			i = 0;
			int num = this.last;
			while (i <= num)
			{
				array[decompressionState.cftab[(int)(decompressionState.ll8[i] & byte.MaxValue)]++] = i;
				i++;
			}
			if (this.origPtr < 0 || this.origPtr >= array.Length)
			{
				throw new IOException("stream corrupted");
			}
			this.su_tPos = array[this.origPtr];
			this.su_count = 0;
			this.su_i2 = 0;
			this.su_ch2 = 256;
			if (this.blockRandomised)
			{
				this.su_rNToGo = 0;
				this.su_rTPos = 0;
				this.SetupRandPartA();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010F3C File Offset: 0x0000F13C
		private void SetupRandPartA()
		{
			if (this.su_i2 <= this.last)
			{
				this.su_chPrev = this.su_ch2;
				int num = (int)(this.data.ll8[this.su_tPos] & byte.MaxValue);
				this.su_tPos = this.data.tt[this.su_tPos];
				if (this.su_rNToGo == 0)
				{
					this.su_rNToGo = Rand.Rnums(this.su_rTPos) - 1;
					if (++this.su_rTPos == 512)
					{
						this.su_rTPos = 0;
					}
				}
				else
				{
					this.su_rNToGo--;
				}
				num = (this.su_ch2 = num ^ ((this.su_rNToGo == 1) ? 1 : 0));
				this.su_i2++;
				this.currentChar = num;
				this.currentState = BZip2InputStream.CState.RAND_PART_B;
				this.crc.UpdateCRC((byte)num);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011034 File Offset: 0x0000F234
		private void SetupNoRandPartA()
		{
			if (this.su_i2 <= this.last)
			{
				this.su_chPrev = this.su_ch2;
				int num = (int)(this.data.ll8[this.su_tPos] & byte.MaxValue);
				this.su_ch2 = num;
				this.su_tPos = this.data.tt[this.su_tPos];
				this.su_i2++;
				this.currentChar = num;
				this.currentState = BZip2InputStream.CState.NO_RAND_PART_B;
				this.crc.UpdateCRC((byte)num);
				return;
			}
			this.currentState = BZip2InputStream.CState.NO_RAND_PART_A;
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000110D8 File Offset: 0x0000F2D8
		private void SetupRandPartB()
		{
			if (this.su_ch2 != this.su_chPrev)
			{
				this.currentState = BZip2InputStream.CState.RAND_PART_A;
				this.su_count = 1;
				this.SetupRandPartA();
				return;
			}
			if (++this.su_count >= 4)
			{
				this.su_z = (char)(this.data.ll8[this.su_tPos] & byte.MaxValue);
				this.su_tPos = this.data.tt[this.su_tPos];
				if (this.su_rNToGo == 0)
				{
					this.su_rNToGo = Rand.Rnums(this.su_rTPos) - 1;
					if (++this.su_rTPos == 512)
					{
						this.su_rTPos = 0;
					}
				}
				else
				{
					this.su_rNToGo--;
				}
				this.su_j2 = 0;
				this.currentState = BZip2InputStream.CState.RAND_PART_C;
				if (this.su_rNToGo == 1)
				{
					this.su_z ^= '\u0001';
				}
				this.SetupRandPartC();
				return;
			}
			this.currentState = BZip2InputStream.CState.RAND_PART_A;
			this.SetupRandPartA();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000111DC File Offset: 0x0000F3DC
		private void SetupRandPartC()
		{
			if (this.su_j2 < (int)this.su_z)
			{
				this.currentChar = this.su_ch2;
				this.crc.UpdateCRC((byte)this.su_ch2);
				this.su_j2++;
				return;
			}
			this.currentState = BZip2InputStream.CState.RAND_PART_A;
			this.su_i2++;
			this.su_count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011248 File Offset: 0x0000F448
		private void SetupNoRandPartB()
		{
			if (this.su_ch2 != this.su_chPrev)
			{
				this.su_count = 1;
				this.SetupNoRandPartA();
				return;
			}
			if (++this.su_count >= 4)
			{
				this.su_z = (char)(this.data.ll8[this.su_tPos] & byte.MaxValue);
				this.su_tPos = this.data.tt[this.su_tPos];
				this.su_j2 = 0;
				this.SetupNoRandPartC();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000112D0 File Offset: 0x0000F4D0
		private void SetupNoRandPartC()
		{
			if (this.su_j2 < (int)this.su_z)
			{
				int num = this.su_ch2;
				this.currentChar = num;
				this.crc.UpdateCRC((byte)num);
				this.su_j2++;
				this.currentState = BZip2InputStream.CState.NO_RAND_PART_C;
				return;
			}
			this.su_i2++;
			this.su_count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x04000176 RID: 374
		private bool _disposed;

		// Token: 0x04000177 RID: 375
		private bool _leaveOpen;

		// Token: 0x04000178 RID: 376
		private long totalBytesRead;

		// Token: 0x04000179 RID: 377
		private int last;

		// Token: 0x0400017A RID: 378
		private int origPtr;

		// Token: 0x0400017B RID: 379
		private int blockSize100k;

		// Token: 0x0400017C RID: 380
		private bool blockRandomised;

		// Token: 0x0400017D RID: 381
		private int bsBuff;

		// Token: 0x0400017E RID: 382
		private int bsLive;

		// Token: 0x0400017F RID: 383
		private readonly CRC32 crc = new CRC32(true);

		// Token: 0x04000180 RID: 384
		private int nInUse;

		// Token: 0x04000181 RID: 385
		private Stream input;

		// Token: 0x04000182 RID: 386
		private int currentChar = -1;

		// Token: 0x04000183 RID: 387
		private BZip2InputStream.CState currentState = BZip2InputStream.CState.START_BLOCK;

		// Token: 0x04000184 RID: 388
		private uint storedBlockCRC;

		// Token: 0x04000185 RID: 389
		private uint storedCombinedCRC;

		// Token: 0x04000186 RID: 390
		private uint computedBlockCRC;

		// Token: 0x04000187 RID: 391
		private uint computedCombinedCRC;

		// Token: 0x04000188 RID: 392
		private int su_count;

		// Token: 0x04000189 RID: 393
		private int su_ch2;

		// Token: 0x0400018A RID: 394
		private int su_chPrev;

		// Token: 0x0400018B RID: 395
		private int su_i2;

		// Token: 0x0400018C RID: 396
		private int su_j2;

		// Token: 0x0400018D RID: 397
		private int su_rNToGo;

		// Token: 0x0400018E RID: 398
		private int su_rTPos;

		// Token: 0x0400018F RID: 399
		private int su_tPos;

		// Token: 0x04000190 RID: 400
		private char su_z;

		// Token: 0x04000191 RID: 401
		private BZip2InputStream.DecompressionState data;

		// Token: 0x02000034 RID: 52
		private enum CState
		{
			// Token: 0x04000193 RID: 403
			EOF,
			// Token: 0x04000194 RID: 404
			START_BLOCK,
			// Token: 0x04000195 RID: 405
			RAND_PART_A,
			// Token: 0x04000196 RID: 406
			RAND_PART_B,
			// Token: 0x04000197 RID: 407
			RAND_PART_C,
			// Token: 0x04000198 RID: 408
			NO_RAND_PART_A,
			// Token: 0x04000199 RID: 409
			NO_RAND_PART_B,
			// Token: 0x0400019A RID: 410
			NO_RAND_PART_C
		}

		// Token: 0x02000035 RID: 53
		private sealed class DecompressionState
		{
			// Token: 0x060002E5 RID: 741 RVA: 0x00011338 File Offset: 0x0000F538
			public DecompressionState(int blockSize100k)
			{
				this.unzftab = new int[256];
				this.gLimit = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.gBase = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.gPerm = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.gMinlen = new int[BZip2.NGroups];
				this.cftab = new int[257];
				this.getAndMoveToFrontDecode_yy = new byte[256];
				this.temp_charArray2d = BZip2.InitRectangularArray<char>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.recvDecodingTables_pos = new byte[BZip2.NGroups];
				this.ll8 = new byte[blockSize100k * BZip2.BlockSizeMultiple];
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x00011444 File Offset: 0x0000F644
			public int[] initTT(int length)
			{
				int[] array = this.tt;
				if (array == null || array.Length < length)
				{
					array = (this.tt = new int[length]);
				}
				return array;
			}

			// Token: 0x0400019B RID: 411
			public readonly bool[] inUse = new bool[256];

			// Token: 0x0400019C RID: 412
			public readonly byte[] seqToUnseq = new byte[256];

			// Token: 0x0400019D RID: 413
			public readonly byte[] selector = new byte[BZip2.MaxSelectors];

			// Token: 0x0400019E RID: 414
			public readonly byte[] selectorMtf = new byte[BZip2.MaxSelectors];

			// Token: 0x0400019F RID: 415
			public readonly int[] unzftab;

			// Token: 0x040001A0 RID: 416
			public readonly int[][] gLimit;

			// Token: 0x040001A1 RID: 417
			public readonly int[][] gBase;

			// Token: 0x040001A2 RID: 418
			public readonly int[][] gPerm;

			// Token: 0x040001A3 RID: 419
			public readonly int[] gMinlen;

			// Token: 0x040001A4 RID: 420
			public readonly int[] cftab;

			// Token: 0x040001A5 RID: 421
			public readonly byte[] getAndMoveToFrontDecode_yy;

			// Token: 0x040001A6 RID: 422
			public readonly char[][] temp_charArray2d;

			// Token: 0x040001A7 RID: 423
			public readonly byte[] recvDecodingTables_pos;

			// Token: 0x040001A8 RID: 424
			public int[] tt;

			// Token: 0x040001A9 RID: 425
			public byte[] ll8;
		}
	}
}
