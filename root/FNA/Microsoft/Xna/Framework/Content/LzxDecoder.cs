using System;
using System.IO;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000147 RID: 327
	internal class LzxDecoder
	{
		// Token: 0x060017C0 RID: 6080 RVA: 0x0003AFB4 File Offset: 0x000391B4
		public LzxDecoder(int window)
		{
			uint num = 1U << window;
			if (window < 15 || window > 21)
			{
				throw new UnsupportedWindowSizeRange();
			}
			this.m_state = default(LzxDecoder.LzxState);
			this.m_state.actual_size = 0U;
			this.m_state.window = new byte[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.m_state.window[num2] = 220;
				num2++;
			}
			this.m_state.actual_size = num;
			this.m_state.window_size = num;
			this.m_state.window_posn = 0U;
			if (LzxDecoder.extra_bits == null)
			{
				LzxDecoder.extra_bits = new byte[52];
				int i = 0;
				int num3 = 0;
				while (i <= 50)
				{
					LzxDecoder.extra_bits[i] = (LzxDecoder.extra_bits[i + 1] = (byte)num3);
					if (i != 0 && num3 < 17)
					{
						num3++;
					}
					i += 2;
				}
			}
			if (LzxDecoder.position_base == null)
			{
				LzxDecoder.position_base = new uint[51];
				int j = 0;
				int num4 = 0;
				while (j <= 50)
				{
					LzxDecoder.position_base[j] = (uint)num4;
					num4 += 1 << (int)LzxDecoder.extra_bits[j];
					j++;
				}
			}
			int num5;
			if (window == 20)
			{
				num5 = 42;
			}
			else if (window == 21)
			{
				num5 = 50;
			}
			else
			{
				num5 = window << 1;
			}
			this.m_state.R0 = (this.m_state.R1 = (this.m_state.R2 = 1U));
			this.m_state.main_elements = (ushort)(256 + (num5 << 3));
			this.m_state.header_read = 0;
			this.m_state.frames_read = 0U;
			this.m_state.block_remaining = 0U;
			this.m_state.block_type = LzxConstants.BLOCKTYPE.INVALID;
			this.m_state.intel_curpos = 0;
			this.m_state.intel_started = 0;
			this.m_state.PRETREE_table = new ushort[104];
			this.m_state.PRETREE_len = new byte[84];
			this.m_state.MAINTREE_table = new ushort[5408];
			this.m_state.MAINTREE_len = new byte[720];
			this.m_state.LENGTH_table = new ushort[4596];
			this.m_state.LENGTH_len = new byte[314];
			this.m_state.ALIGNED_table = new ushort[144];
			this.m_state.ALIGNED_len = new byte[72];
			for (int k = 0; k < 656; k++)
			{
				this.m_state.MAINTREE_len[k] = 0;
			}
			for (int l = 0; l < 250; l++)
			{
				this.m_state.LENGTH_len[l] = 0;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0003B260 File Offset: 0x00039460
		public int Decompress(Stream inData, int inLen, Stream outData, int outLen)
		{
			LzxDecoder.BitBuffer bitBuffer = new LzxDecoder.BitBuffer(inData);
			long position = inData.Position;
			long num = inData.Position + (long)inLen;
			byte[] window = this.m_state.window;
			uint num2 = this.m_state.window_posn;
			uint window_size = this.m_state.window_size;
			uint num3 = this.m_state.R0;
			uint num4 = this.m_state.R1;
			uint num5 = this.m_state.R2;
			int i = outLen;
			bitBuffer.InitBitStream();
			if (this.m_state.header_read == 0)
			{
				if (bitBuffer.ReadBits(1) != 0U)
				{
					uint num6 = bitBuffer.ReadBits(16);
					uint num7 = bitBuffer.ReadBits(16);
					this.m_state.intel_filesize = (int)((num6 << 16) | num7);
				}
				this.m_state.header_read = 1;
			}
			while (i > 0)
			{
				if (this.m_state.block_remaining == 0U)
				{
					if (this.m_state.block_type == LzxConstants.BLOCKTYPE.UNCOMPRESSED)
					{
						if ((this.m_state.block_length & 1U) == 1U)
						{
							inData.ReadByte();
						}
						bitBuffer.InitBitStream();
					}
					this.m_state.block_type = (LzxConstants.BLOCKTYPE)bitBuffer.ReadBits(3);
					uint num6 = bitBuffer.ReadBits(16);
					uint num7 = bitBuffer.ReadBits(8);
					this.m_state.block_remaining = (this.m_state.block_length = (num6 << 8) | num7);
					switch (this.m_state.block_type)
					{
					case LzxConstants.BLOCKTYPE.VERBATIM:
						break;
					case LzxConstants.BLOCKTYPE.ALIGNED:
						for (num6 = 0U; num6 < 8U; num6 += 1U)
						{
							num7 = bitBuffer.ReadBits(3);
							this.m_state.ALIGNED_len[(int)num6] = (byte)num7;
						}
						this.MakeDecodeTable(8U, 7U, this.m_state.ALIGNED_len, this.m_state.ALIGNED_table);
						break;
					case LzxConstants.BLOCKTYPE.UNCOMPRESSED:
					{
						this.m_state.intel_started = 1;
						bitBuffer.EnsureBits(16);
						if (bitBuffer.GetBitsLeft() > 16)
						{
							inData.Seek(-2L, SeekOrigin.Current);
						}
						uint num8 = (uint)((byte)inData.ReadByte());
						byte b = (byte)inData.ReadByte();
						byte b2 = (byte)inData.ReadByte();
						byte b3 = (byte)inData.ReadByte();
						num3 = num8 | (uint)((uint)b << 8) | (uint)((uint)b2 << 16) | (uint)((uint)b3 << 24);
						uint num9 = (uint)((byte)inData.ReadByte());
						b = (byte)inData.ReadByte();
						b2 = (byte)inData.ReadByte();
						b3 = (byte)inData.ReadByte();
						num4 = num9 | (uint)((uint)b << 8) | (uint)((uint)b2 << 16) | (uint)((uint)b3 << 24);
						uint num10 = (uint)((byte)inData.ReadByte());
						b = (byte)inData.ReadByte();
						b2 = (byte)inData.ReadByte();
						b3 = (byte)inData.ReadByte();
						num5 = num10 | (uint)((uint)b << 8) | (uint)((uint)b2 << 16) | (uint)((uint)b3 << 24);
						goto IL_033F;
					}
					default:
						return -1;
					}
					this.ReadLengths(this.m_state.MAINTREE_len, 0U, 256U, bitBuffer);
					this.ReadLengths(this.m_state.MAINTREE_len, 256U, (uint)this.m_state.main_elements, bitBuffer);
					this.MakeDecodeTable(656U, 12U, this.m_state.MAINTREE_len, this.m_state.MAINTREE_table);
					if (this.m_state.MAINTREE_len[232] != 0)
					{
						this.m_state.intel_started = 1;
					}
					this.ReadLengths(this.m_state.LENGTH_len, 0U, 249U, bitBuffer);
					this.MakeDecodeTable(250U, 12U, this.m_state.LENGTH_len, this.m_state.LENGTH_table);
				}
				IL_033F:
				if (inData.Position > position + (long)inLen && (inData.Position > position + (long)inLen + 2L || bitBuffer.GetBitsLeft() < 16))
				{
					return -1;
				}
				int j;
				while ((j = (int)this.m_state.block_remaining) > 0 && i > 0)
				{
					if (j > i)
					{
						j = i;
					}
					i -= j;
					this.m_state.block_remaining = this.m_state.block_remaining - (uint)j;
					num2 &= window_size - 1U;
					if ((ulong)num2 + (ulong)((long)j) > (ulong)window_size)
					{
						return -1;
					}
					switch (this.m_state.block_type)
					{
					case LzxConstants.BLOCKTYPE.VERBATIM:
						while (j > 0)
						{
							int num11 = (int)this.ReadHuffSym(this.m_state.MAINTREE_table, this.m_state.MAINTREE_len, 656U, 12U, bitBuffer);
							if (num11 < 256)
							{
								window[(int)num2++] = (byte)num11;
								j--;
							}
							else
							{
								num11 -= 256;
								int num12 = num11 & 7;
								if (num12 == 7)
								{
									int num13 = (int)this.ReadHuffSym(this.m_state.LENGTH_table, this.m_state.LENGTH_len, 250U, 12U, bitBuffer);
									num12 += num13;
								}
								num12 += 2;
								int num14 = num11 >> 3;
								if (num14 > 2)
								{
									if (num14 != 3)
									{
										int num15 = (int)LzxDecoder.extra_bits[num14];
										int num16 = (int)bitBuffer.ReadBits((byte)num15);
										num14 = (int)(LzxDecoder.position_base[num14] - 2U + (uint)num16);
									}
									else
									{
										num14 = 1;
									}
									num5 = num4;
									num4 = num3;
									num3 = (uint)num14;
								}
								else if (num14 == 0)
								{
									num14 = (int)num3;
								}
								else if (num14 == 1)
								{
									num14 = (int)num4;
									num4 = num3;
									num3 = (uint)num14;
								}
								else
								{
									num14 = (int)num5;
									num5 = num3;
									num3 = (uint)num14;
								}
								int num17 = (int)num2;
								j -= num12;
								int num18;
								if ((ulong)num2 >= (ulong)((long)num14))
								{
									num18 = num17 - num14;
								}
								else
								{
									num18 = num17 + (int)(window_size - (uint)num14);
									int num19 = num14 - (int)num2;
									if (num19 < num12)
									{
										num12 -= num19;
										num2 += (uint)num19;
										while (num19-- > 0)
										{
											window[num17++] = window[num18++];
										}
										num18 = 0;
									}
								}
								num2 += (uint)num12;
								while (num12-- > 0)
								{
									window[num17++] = window[num18++];
								}
							}
						}
						break;
					case LzxConstants.BLOCKTYPE.ALIGNED:
						while (j > 0)
						{
							int num11 = (int)this.ReadHuffSym(this.m_state.MAINTREE_table, this.m_state.MAINTREE_len, 656U, 12U, bitBuffer);
							if (num11 < 256)
							{
								window[(int)num2++] = (byte)num11;
								j--;
							}
							else
							{
								num11 -= 256;
								int num12 = num11 & 7;
								if (num12 == 7)
								{
									int num13 = (int)this.ReadHuffSym(this.m_state.LENGTH_table, this.m_state.LENGTH_len, 250U, 12U, bitBuffer);
									num12 += num13;
								}
								num12 += 2;
								int num14 = num11 >> 3;
								if (num14 > 2)
								{
									int num15 = (int)LzxDecoder.extra_bits[num14];
									num14 = (int)(LzxDecoder.position_base[num14] - 2U);
									if (num15 > 3)
									{
										num15 -= 3;
										int num16 = (int)bitBuffer.ReadBits((byte)num15);
										num14 += num16 << 3;
										int num20 = (int)this.ReadHuffSym(this.m_state.ALIGNED_table, this.m_state.ALIGNED_len, 8U, 7U, bitBuffer);
										num14 += num20;
									}
									else if (num15 == 3)
									{
										int num20 = (int)this.ReadHuffSym(this.m_state.ALIGNED_table, this.m_state.ALIGNED_len, 8U, 7U, bitBuffer);
										num14 += num20;
									}
									else if (num15 > 0)
									{
										int num16 = (int)bitBuffer.ReadBits((byte)num15);
										num14 += num16;
									}
									else
									{
										num14 = 1;
									}
									num5 = num4;
									num4 = num3;
									num3 = (uint)num14;
								}
								else if (num14 == 0)
								{
									num14 = (int)num3;
								}
								else if (num14 == 1)
								{
									num14 = (int)num4;
									num4 = num3;
									num3 = (uint)num14;
								}
								else
								{
									num14 = (int)num5;
									num5 = num3;
									num3 = (uint)num14;
								}
								int num17 = (int)num2;
								j -= num12;
								int num18;
								if ((ulong)num2 >= (ulong)((long)num14))
								{
									num18 = num17 - num14;
								}
								else
								{
									num18 = num17 + (int)(window_size - (uint)num14);
									int num19 = num14 - (int)num2;
									if (num19 < num12)
									{
										num12 -= num19;
										num2 += (uint)num19;
										while (num19-- > 0)
										{
											window[num17++] = window[num18++];
										}
										num18 = 0;
									}
								}
								num2 += (uint)num12;
								while (num12-- > 0)
								{
									window[num17++] = window[num18++];
								}
							}
						}
						break;
					case LzxConstants.BLOCKTYPE.UNCOMPRESSED:
					{
						if (inData.Position + (long)j > num)
						{
							return -1;
						}
						byte[] array = new byte[j];
						inData.Read(array, 0, j);
						array.CopyTo(window, (int)num2);
						num2 += (uint)j;
						break;
					}
					default:
						return -1;
					}
				}
			}
			if (i != 0)
			{
				return -1;
			}
			int num21 = (int)num2;
			if (num21 == 0)
			{
				num21 = (int)window_size;
			}
			num21 -= outLen;
			outData.Write(window, num21, outLen);
			this.m_state.window_posn = num2;
			this.m_state.R0 = num3;
			this.m_state.R1 = num4;
			this.m_state.R2 = num5;
			uint frames_read = this.m_state.frames_read;
			this.m_state.frames_read = frames_read + 1U;
			if (frames_read < 32768U && this.m_state.intel_filesize != 0)
			{
				if (outLen <= 6 || this.m_state.intel_started == 0)
				{
					this.m_state.intel_curpos = this.m_state.intel_curpos + outLen;
				}
				else
				{
					int num22 = outLen - 10;
					uint num23 = (uint)this.m_state.intel_curpos;
					this.m_state.intel_curpos = (int)(num23 + (uint)outLen);
					while (outData.Position < (long)num22)
					{
						if (outData.ReadByte() != 232)
						{
							num23 += 1U;
						}
					}
				}
				return -1;
			}
			return 0;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0003BB24 File Offset: 0x00039D24
		private int MakeDecodeTable(uint nsyms, uint nbits, byte[] length, ushort[] table)
		{
			byte b = 1;
			uint num = 0U;
			uint num2 = 1U << (int)nbits;
			uint num3 = num2 >> 1;
			uint num4 = num3;
			ushort num5;
			while ((uint)b <= nbits)
			{
				num5 = 0;
				while ((uint)num5 < nsyms)
				{
					if (length[(int)num5] == b)
					{
						uint num6 = num;
						if ((num += num3) > num2)
						{
							return 1;
						}
						uint num7 = num3;
						while (num7-- > 0U)
						{
							table[(int)num6++] = num5;
						}
					}
					num5 += 1;
				}
				num3 >>= 1;
				b += 1;
			}
			if (num != num2)
			{
				num5 = (ushort)num;
				while ((uint)num5 < num2)
				{
					table[(int)num5] = 0;
					num5 += 1;
				}
				num <<= 16;
				num2 <<= 16;
				num3 = 32768U;
				while (b <= 16)
				{
					num5 = 0;
					while ((uint)num5 < nsyms)
					{
						if (length[(int)num5] == b)
						{
							uint num6 = num >> 16;
							for (uint num7 = 0U; num7 < (uint)b - nbits; num7 += 1U)
							{
								if (table[(int)num6] == 0)
								{
									table[(int)((int)num4 << 1)] = 0;
									table[(int)((num4 << 1) + 1U)] = 0;
									table[(int)num6] = (ushort)num4++;
								}
								num6 = (uint)((uint)table[(int)num6] << 1);
								if (((num >> (int)(15U - num7)) & 1U) == 1U)
								{
									num6 += 1U;
								}
							}
							table[(int)num6] = num5;
							if ((num += num3) > num2)
							{
								return 1;
							}
						}
						num5 += 1;
					}
					num3 >>= 1;
					b += 1;
				}
			}
			if (num == num2)
			{
				return 0;
			}
			num5 = 0;
			while ((uint)num5 < nsyms)
			{
				if (length[(int)num5] != 0)
				{
					return 1;
				}
				num5 += 1;
			}
			return 0;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0003BC74 File Offset: 0x00039E74
		private void ReadLengths(byte[] lens, uint first, uint last, LzxDecoder.BitBuffer bitbuf)
		{
			uint num;
			for (num = 0U; num < 20U; num += 1U)
			{
				uint num2 = bitbuf.ReadBits(4);
				this.m_state.PRETREE_len[(int)num] = (byte)num2;
			}
			this.MakeDecodeTable(20U, 6U, this.m_state.PRETREE_len, this.m_state.PRETREE_table);
			num = first;
			while (num < last)
			{
				int num3 = (int)this.ReadHuffSym(this.m_state.PRETREE_table, this.m_state.PRETREE_len, 20U, 6U, bitbuf);
				if (num3 == 17)
				{
					uint num2 = bitbuf.ReadBits(4);
					num2 += 4U;
					while (num2-- != 0U)
					{
						lens[(int)num++] = 0;
					}
				}
				else if (num3 == 18)
				{
					uint num2 = bitbuf.ReadBits(5);
					num2 += 20U;
					while (num2-- != 0U)
					{
						lens[(int)num++] = 0;
					}
				}
				else if (num3 == 19)
				{
					uint num2 = bitbuf.ReadBits(1);
					num2 += 4U;
					num3 = (int)this.ReadHuffSym(this.m_state.PRETREE_table, this.m_state.PRETREE_len, 20U, 6U, bitbuf);
					num3 = (int)lens[(int)num] - num3;
					if (num3 < 0)
					{
						num3 += 17;
					}
					while (num2-- != 0U)
					{
						lens[(int)num++] = (byte)num3;
					}
				}
				else
				{
					num3 = (int)lens[(int)num] - num3;
					if (num3 < 0)
					{
						num3 += 17;
					}
					lens[(int)num++] = (byte)num3;
				}
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		private uint ReadHuffSym(ushort[] table, byte[] lengths, uint nsyms, uint nbits, LzxDecoder.BitBuffer bitbuf)
		{
			bitbuf.EnsureBits(16);
			uint num;
			uint num2;
			if ((num = (uint)table[(int)bitbuf.PeekBits((byte)nbits)]) >= nsyms)
			{
				num2 = 1U << (int)(32U - nbits);
				for (;;)
				{
					num2 >>= 1;
					num <<= 1;
					num |= (((bitbuf.GetBuffer() & num2) > 0U) ? 1U : 0U);
					if (num2 == 0U)
					{
						break;
					}
					if ((num = (uint)table[(int)num]) < nsyms)
					{
						goto IL_0049;
					}
				}
				return 0U;
			}
			IL_0049:
			num2 = (uint)lengths[(int)num];
			bitbuf.RemoveBits((byte)num2);
			return num;
		}

		// Token: 0x04000AD1 RID: 2769
		public static uint[] position_base;

		// Token: 0x04000AD2 RID: 2770
		public static byte[] extra_bits;

		// Token: 0x04000AD3 RID: 2771
		private LzxDecoder.LzxState m_state;

		// Token: 0x020003E1 RID: 993
		private class BitBuffer
		{
			// Token: 0x06001B0A RID: 6922 RVA: 0x0003FAF6 File Offset: 0x0003DCF6
			public BitBuffer(Stream stream)
			{
				this.byteStream = stream;
				this.InitBitStream();
			}

			// Token: 0x06001B0B RID: 6923 RVA: 0x0003FB0B File Offset: 0x0003DD0B
			public void InitBitStream()
			{
				this.buffer = 0U;
				this.bitsleft = 0;
			}

			// Token: 0x06001B0C RID: 6924 RVA: 0x0003FB1C File Offset: 0x0003DD1C
			public void EnsureBits(byte bits)
			{
				while (this.bitsleft < bits)
				{
					int num = (int)((byte)this.byteStream.ReadByte());
					int num2 = (int)((byte)this.byteStream.ReadByte());
					this.buffer |= (uint)((uint)((num2 << 8) | num) << (int)(16 - this.bitsleft));
					this.bitsleft += 16;
				}
			}

			// Token: 0x06001B0D RID: 6925 RVA: 0x0003FB7D File Offset: 0x0003DD7D
			public uint PeekBits(byte bits)
			{
				return this.buffer >> (int)(32 - bits);
			}

			// Token: 0x06001B0E RID: 6926 RVA: 0x0003FB8D File Offset: 0x0003DD8D
			public void RemoveBits(byte bits)
			{
				this.buffer <<= (int)bits;
				this.bitsleft -= bits;
			}

			// Token: 0x06001B0F RID: 6927 RVA: 0x0003FBB0 File Offset: 0x0003DDB0
			public uint ReadBits(byte bits)
			{
				uint num = 0U;
				if (bits > 0)
				{
					this.EnsureBits(bits);
					num = this.PeekBits(bits);
					this.RemoveBits(bits);
				}
				return num;
			}

			// Token: 0x06001B10 RID: 6928 RVA: 0x0003FBDA File Offset: 0x0003DDDA
			public uint GetBuffer()
			{
				return this.buffer;
			}

			// Token: 0x06001B11 RID: 6929 RVA: 0x0003FBE2 File Offset: 0x0003DDE2
			public byte GetBitsLeft()
			{
				return this.bitsleft;
			}

			// Token: 0x04001DF2 RID: 7666
			private uint buffer;

			// Token: 0x04001DF3 RID: 7667
			private byte bitsleft;

			// Token: 0x04001DF4 RID: 7668
			private Stream byteStream;
		}

		// Token: 0x020003E2 RID: 994
		private struct LzxState
		{
			// Token: 0x04001DF5 RID: 7669
			public uint R0;

			// Token: 0x04001DF6 RID: 7670
			public uint R1;

			// Token: 0x04001DF7 RID: 7671
			public uint R2;

			// Token: 0x04001DF8 RID: 7672
			public ushort main_elements;

			// Token: 0x04001DF9 RID: 7673
			public int header_read;

			// Token: 0x04001DFA RID: 7674
			public LzxConstants.BLOCKTYPE block_type;

			// Token: 0x04001DFB RID: 7675
			public uint block_length;

			// Token: 0x04001DFC RID: 7676
			public uint block_remaining;

			// Token: 0x04001DFD RID: 7677
			public uint frames_read;

			// Token: 0x04001DFE RID: 7678
			public int intel_filesize;

			// Token: 0x04001DFF RID: 7679
			public int intel_curpos;

			// Token: 0x04001E00 RID: 7680
			public int intel_started;

			// Token: 0x04001E01 RID: 7681
			public ushort[] PRETREE_table;

			// Token: 0x04001E02 RID: 7682
			public byte[] PRETREE_len;

			// Token: 0x04001E03 RID: 7683
			public ushort[] MAINTREE_table;

			// Token: 0x04001E04 RID: 7684
			public byte[] MAINTREE_len;

			// Token: 0x04001E05 RID: 7685
			public ushort[] LENGTH_table;

			// Token: 0x04001E06 RID: 7686
			public byte[] LENGTH_len;

			// Token: 0x04001E07 RID: 7687
			public ushort[] ALIGNED_table;

			// Token: 0x04001E08 RID: 7688
			public byte[] ALIGNED_len;

			// Token: 0x04001E09 RID: 7689
			public uint actual_size;

			// Token: 0x04001E0A RID: 7690
			public byte[] window;

			// Token: 0x04001E0B RID: 7691
			public uint window_size;

			// Token: 0x04001E0C RID: 7692
			public uint window_posn;
		}
	}
}
