using System;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000017 RID: 23
	internal sealed class Huffman
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00005918 File Offset: 0x00003B18
		static Huffman()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00012650 File Offset: 0x00010850
		private Huffman(string s, int xlen, int ylen, int linbits, int linmax, int @ref, int[] table, int[] hlen, int[][] val, int treelen)
		{
			this._Tablename0 = s.get_Chars(0);
			this._Tablename1 = s.get_Chars(1);
			this._Tablename2 = s.get_Chars(2);
			this._Xlen = xlen;
			this._Ylen = ylen;
			this._Linbits = linbits;
			this._Linmax = linmax;
			this._RefRenamed = @ref;
			this._Table = table;
			this._Hlen = hlen;
			this._Val = val;
			this._Treelen = treelen;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000126D0 File Offset: 0x000108D0
		internal static int Decode(Huffman h, int[] x, int[] y, int[] v, int[] w, BitReserve br)
		{
			int minValue = int.MinValue;
			int num = 0;
			int num2 = 1;
			int num3 = minValue;
			if (h._Val == null)
			{
				return 2;
			}
			if (h._Treelen == 0)
			{
				x[0] = (y[0] = 0);
				return 0;
			}
			while (h._Val[num][0] != 0)
			{
				if (br.ReadOneBit() != 0)
				{
					while (h._Val[num][1] >= 250)
					{
						num += h._Val[num][1];
					}
					num += h._Val[num][1];
				}
				else
				{
					while (h._Val[num][0] >= 250)
					{
						num += h._Val[num][0];
					}
					num += h._Val[num][0];
				}
				num3 = SupportClass.URShift(num3, 1);
				if (num3 == 0 && num >= 0)
				{
					IL_00D3:
					if (h._Tablename0 == '3' && (h._Tablename1 == '2' || h._Tablename1 == '3'))
					{
						v[0] = (y[0] >> 3) & 1;
						w[0] = (y[0] >> 2) & 1;
						x[0] = (y[0] >> 1) & 1;
						y[0] = y[0] & 1;
						if (v[0] != 0 && br.ReadOneBit() != 0)
						{
							v[0] = -v[0];
						}
						if (w[0] != 0 && br.ReadOneBit() != 0)
						{
							w[0] = -w[0];
						}
						if (x[0] != 0 && br.ReadOneBit() != 0)
						{
							x[0] = -x[0];
						}
						if (y[0] != 0 && br.ReadOneBit() != 0)
						{
							y[0] = -y[0];
						}
					}
					else
					{
						if (h._Linbits != 0 && h._Xlen - 1 == x[0])
						{
							x[0] += br.ReadBits(h._Linbits);
						}
						if (x[0] != 0 && br.ReadOneBit() != 0)
						{
							x[0] = -x[0];
						}
						if (h._Linbits != 0 && h._Ylen - 1 == y[0])
						{
							y[0] += br.ReadBits(h._Linbits);
						}
						if (y[0] != 0 && br.ReadOneBit() != 0)
						{
							y[0] = -y[0];
						}
					}
					return num2;
				}
			}
			x[0] = SupportClass.URShift(h._Val[num][1], 4);
			y[0] = h._Val[num][1] & 15;
			num2 = 0;
			goto IL_00D3;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000128E4 File Offset: 0x00010AE4
		internal static void Initialize()
		{
			if (Huffman.HuffmanTable != null)
			{
				return;
			}
			Huffman.HuffmanTable = new Huffman[34];
			Huffman.HuffmanTable[0] = new Huffman("0  ", 0, 0, 0, 0, -1, null, null, Huffman.ValTab0, 0);
			Huffman.HuffmanTable[1] = new Huffman("1  ", 2, 2, 0, 0, -1, null, null, Huffman.ValTab1, 7);
			Huffman.HuffmanTable[2] = new Huffman("2  ", 3, 3, 0, 0, -1, null, null, Huffman.ValTab2, 17);
			Huffman.HuffmanTable[3] = new Huffman("3  ", 3, 3, 0, 0, -1, null, null, Huffman.ValTab3, 17);
			Huffman.HuffmanTable[4] = new Huffman("4  ", 0, 0, 0, 0, -1, null, null, Huffman.ValTab4, 0);
			Huffman.HuffmanTable[5] = new Huffman("5  ", 4, 4, 0, 0, -1, null, null, Huffman.ValTab5, 31);
			Huffman.HuffmanTable[6] = new Huffman("6  ", 4, 4, 0, 0, -1, null, null, Huffman.ValTab6, 31);
			Huffman.HuffmanTable[7] = new Huffman("7  ", 6, 6, 0, 0, -1, null, null, Huffman.ValTab7, 71);
			Huffman.HuffmanTable[8] = new Huffman("8  ", 6, 6, 0, 0, -1, null, null, Huffman.ValTab8, 71);
			Huffman.HuffmanTable[9] = new Huffman("9  ", 6, 6, 0, 0, -1, null, null, Huffman.ValTab9, 71);
			Huffman.HuffmanTable[10] = new Huffman("10 ", 8, 8, 0, 0, -1, null, null, Huffman.ValTab10, 127);
			Huffman.HuffmanTable[11] = new Huffman("11 ", 8, 8, 0, 0, -1, null, null, Huffman.ValTab11, 127);
			Huffman.HuffmanTable[12] = new Huffman("12 ", 8, 8, 0, 0, -1, null, null, Huffman.ValTab12, 127);
			Huffman.HuffmanTable[13] = new Huffman("13 ", 16, 16, 0, 0, -1, null, null, Huffman.ValTab13, 511);
			Huffman.HuffmanTable[14] = new Huffman("14 ", 0, 0, 0, 0, -1, null, null, Huffman.ValTab14, 0);
			Huffman.HuffmanTable[15] = new Huffman("15 ", 16, 16, 0, 0, -1, null, null, Huffman.ValTab15, 511);
			Huffman.HuffmanTable[16] = new Huffman("16 ", 16, 16, 1, 1, -1, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[17] = new Huffman("17 ", 16, 16, 2, 3, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[18] = new Huffman("18 ", 16, 16, 3, 7, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[19] = new Huffman("19 ", 16, 16, 4, 15, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[20] = new Huffman("20 ", 16, 16, 6, 63, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[21] = new Huffman("21 ", 16, 16, 8, 255, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[22] = new Huffman("22 ", 16, 16, 10, 1023, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[23] = new Huffman("23 ", 16, 16, 13, 8191, 16, null, null, Huffman.ValTab16, 511);
			Huffman.HuffmanTable[24] = new Huffman("24 ", 16, 16, 4, 15, -1, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[25] = new Huffman("25 ", 16, 16, 5, 31, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[26] = new Huffman("26 ", 16, 16, 6, 63, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[27] = new Huffman("27 ", 16, 16, 7, 127, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[28] = new Huffman("28 ", 16, 16, 8, 255, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[29] = new Huffman("29 ", 16, 16, 9, 511, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[30] = new Huffman("30 ", 16, 16, 11, 2047, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[31] = new Huffman("31 ", 16, 16, 13, 8191, 24, null, null, Huffman.ValTab24, 512);
			Huffman.HuffmanTable[32] = new Huffman("32 ", 1, 16, 0, 0, -1, null, null, Huffman.ValTab32, 31);
			Huffman.HuffmanTable[33] = new Huffman("33 ", 1, 16, 0, 0, -1, null, null, Huffman.ValTab33, 31);
		}

		// Token: 0x04000081 RID: 129
		private const int MXOFF = 250;

		// Token: 0x04000082 RID: 130
		private const int HTN = 34;

		// Token: 0x04000083 RID: 131
		private static readonly int[][] ValTab0 = new int[][] { new int[2] };

		// Token: 0x04000084 RID: 132
		private static readonly int[][] ValTab1 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				17
			}
		};

		// Token: 0x04000085 RID: 133
		private static readonly int[][] ValTab2 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				34
			}
		};

		// Token: 0x04000086 RID: 134
		private static readonly int[][] ValTab3 = new int[][]
		{
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				34
			}
		};

		// Token: 0x04000087 RID: 135
		private static readonly int[][] ValTab4 = new int[][] { new int[2] };

		// Token: 0x04000088 RID: 136
		private static readonly int[][] ValTab5 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[]
			{
				default(int),
				48
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				35
			},
			new int[]
			{
				default(int),
				51
			}
		};

		// Token: 0x04000089 RID: 137
		private static readonly int[][] ValTab6 = new int[][]
		{
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				17
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				34
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				50
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				35
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				51
			}
		};

		// Token: 0x0400008A RID: 138
		private static readonly int[][] ValTab7 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 18, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[]
			{
				default(int),
				48
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				50
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				35
			},
			new int[]
			{
				default(int),
				4
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				65
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				66
			},
			new int[]
			{
				default(int),
				36
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				21
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				53
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				83
			},
			new int[]
			{
				default(int),
				84
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				85
			}
		};

		// Token: 0x0400008B RID: 139
		private static readonly int[][] ValTab8 = new int[][]
		{
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 14, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				3
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				4
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				65
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[]
			{
				default(int),
				66
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				36
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				21
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				82
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				37
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				53
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				83
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				85
			}
		};

		// Token: 0x0400008C RID: 140
		private static readonly int[][] ValTab9 = new int[][]
		{
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[]
			{
				default(int),
				16
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				17
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				34
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				49
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				19
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				65
			},
			new int[]
			{
				default(int),
				20
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				51
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				66
			},
			new int[]
			{
				default(int),
				36
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				80
			},
			new int[]
			{
				default(int),
				67
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				21
			},
			new int[]
			{
				default(int),
				82
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				37
			},
			new int[]
			{
				default(int),
				68
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				83
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				53
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				85
			}
		};

		// Token: 0x0400008D RID: 141
		private static readonly int[][] ValTab10 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 10, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 28, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[]
			{
				default(int),
				48
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				50
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				35
			},
			new int[]
			{
				default(int),
				64
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				65
			},
			new int[]
			{
				default(int),
				20
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				51
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				66
			},
			new int[]
			{
				default(int),
				36
			},
			new int[] { 28, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				80
			},
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				96
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				97
			},
			new int[]
			{
				default(int),
				22
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				21
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				38
			},
			new int[]
			{
				default(int),
				54
			},
			new int[]
			{
				default(int),
				113
			},
			new int[] { 20, 1 },
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				23
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				83
			},
			new int[]
			{
				default(int),
				6
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				53
			},
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				98
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				112
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				7
			},
			new int[]
			{
				default(int),
				100
			},
			new int[] { 14, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				114
			},
			new int[]
			{
				default(int),
				39
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				99
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				85
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				115
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				55
			},
			new int[]
			{
				default(int),
				101
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				86
			},
			new int[]
			{
				default(int),
				116
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				71
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				102
			},
			new int[]
			{
				default(int),
				117
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				87
			},
			new int[]
			{
				default(int),
				118
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				119
			}
		};

		// Token: 0x0400008E RID: 142
		private static readonly int[][] ValTab11 = new int[][]
		{
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 24, 1 },
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				3
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				4
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				65
			},
			new int[]
			{
				default(int),
				20
			},
			new int[] { 30, 1 },
			new int[] { 16, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				66
			},
			new int[]
			{
				default(int),
				36
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				81
			},
			new int[]
			{
				default(int),
				97
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				22
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				6
			},
			new int[]
			{
				default(int),
				38
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				98
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				21
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				82
			},
			new int[] { 16, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				37
			},
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				96
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				99
			},
			new int[]
			{
				default(int),
				54
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				112
			},
			new int[]
			{
				default(int),
				23
			},
			new int[]
			{
				default(int),
				113
			},
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				7
			},
			new int[]
			{
				default(int),
				100
			},
			new int[]
			{
				default(int),
				114
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				39
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				83
			},
			new int[]
			{
				default(int),
				53
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				69
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				115
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				55
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				101
			},
			new int[]
			{
				default(int),
				86
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				85
			},
			new int[]
			{
				default(int),
				87
			},
			new int[]
			{
				default(int),
				116
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				71
			},
			new int[]
			{
				default(int),
				102
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				118
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				119
			}
		};

		// Token: 0x0400008F RID: 143
		private static readonly int[][] ValTab12 = new int[][]
		{
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 2, 1 },
			new int[2],
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 16, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[]
			{
				default(int),
				49
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				19
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				64
			},
			new int[] { 26, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				65
			},
			new int[]
			{
				default(int),
				51
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[]
			{
				default(int),
				66
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				36
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				81
			},
			new int[]
			{
				default(int),
				21
			},
			new int[] { 28, 1 },
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				83
			},
			new int[]
			{
				default(int),
				53
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				96
			},
			new int[]
			{
				default(int),
				22
			},
			new int[]
			{
				default(int),
				97
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				98
			},
			new int[]
			{
				default(int),
				38
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				6
			},
			new int[]
			{
				default(int),
				68
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				69
			},
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				99
			},
			new int[]
			{
				default(int),
				54
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				112
			},
			new int[]
			{
				default(int),
				7
			},
			new int[]
			{
				default(int),
				113
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				23
			},
			new int[]
			{
				default(int),
				100
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				114
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				39
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				85
			},
			new int[]
			{
				default(int),
				115
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				55
			},
			new int[]
			{
				default(int),
				86
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				101
			},
			new int[]
			{
				default(int),
				116
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				71
			},
			new int[]
			{
				default(int),
				102
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				87
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				118
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				119
			}
		};

		// Token: 0x04000090 RID: 144
		private static readonly int[][] ValTab13 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				17
			},
			new int[] { 28, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[]
			{
				default(int),
				48
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				49
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				19
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				65
			},
			new int[] { 70, 1 },
			new int[] { 28, 1 },
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				66
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				36
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				81
			},
			new int[]
			{
				default(int),
				21
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				82
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				37
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				83
			},
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				96
			},
			new int[]
			{
				default(int),
				6
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				97
			},
			new int[]
			{
				default(int),
				22
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				128
			},
			new int[]
			{
				default(int),
				8
			},
			new int[]
			{
				default(int),
				129
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				53
			},
			new int[]
			{
				default(int),
				98
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				38
			},
			new int[]
			{
				default(int),
				84
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				99
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				54
			},
			new int[]
			{
				default(int),
				112
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				7
			},
			new int[]
			{
				default(int),
				85
			},
			new int[]
			{
				default(int),
				113
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				23
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				39
			},
			new int[]
			{
				default(int),
				55
			},
			new int[] { 72, 1 },
			new int[] { 24, 1 },
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				24
			},
			new int[]
			{
				default(int),
				130
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				40
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				100
			},
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				114
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				132
			},
			new int[]
			{
				default(int),
				72
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				144
			},
			new int[]
			{
				default(int),
				9
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				145
			},
			new int[]
			{
				default(int),
				25
			},
			new int[] { 24, 1 },
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				115
			},
			new int[]
			{
				default(int),
				101
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				86
			},
			new int[]
			{
				default(int),
				116
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				71
			},
			new int[]
			{
				default(int),
				102
			},
			new int[]
			{
				default(int),
				131
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				56
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				87
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				146
			},
			new int[]
			{
				default(int),
				41
			},
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				133
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				88
			},
			new int[]
			{
				default(int),
				57
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				147
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				73
			},
			new int[]
			{
				default(int),
				134
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				160
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				104
			},
			new int[]
			{
				default(int),
				10
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				161
			},
			new int[]
			{
				default(int),
				26
			},
			new int[] { 68, 1 },
			new int[] { 24, 1 },
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				162
			},
			new int[]
			{
				default(int),
				42
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				149
			},
			new int[]
			{
				default(int),
				89
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				163
			},
			new int[]
			{
				default(int),
				58
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				74
			},
			new int[]
			{
				default(int),
				150
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				176
			},
			new int[]
			{
				default(int),
				11
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				177
			},
			new int[]
			{
				default(int),
				27
			},
			new int[] { 20, 1 },
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				178
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				118
			},
			new int[]
			{
				default(int),
				119
			},
			new int[]
			{
				default(int),
				148
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				135
			},
			new int[]
			{
				default(int),
				120
			},
			new int[]
			{
				default(int),
				164
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				105
			},
			new int[]
			{
				default(int),
				165
			},
			new int[]
			{
				default(int),
				43
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				90
			},
			new int[]
			{
				default(int),
				136
			},
			new int[]
			{
				default(int),
				179
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				59
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				121
			},
			new int[]
			{
				default(int),
				166
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				106
			},
			new int[]
			{
				default(int),
				180
			},
			new int[]
			{
				default(int),
				192
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				152
			},
			new int[]
			{
				default(int),
				193
			},
			new int[] { 60, 1 },
			new int[] { 22, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				28
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				137
			},
			new int[]
			{
				default(int),
				181
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				91
			},
			new int[]
			{
				default(int),
				194
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				44
			},
			new int[]
			{
				default(int),
				60
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				182
			},
			new int[]
			{
				default(int),
				107
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				196
			},
			new int[]
			{
				default(int),
				76
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				168
			},
			new int[]
			{
				default(int),
				138
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				208
			},
			new int[]
			{
				default(int),
				13
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				209
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				75
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				151
			},
			new int[]
			{
				default(int),
				167
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				195
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				122
			},
			new int[]
			{
				default(int),
				153
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				197
			},
			new int[]
			{
				default(int),
				92
			},
			new int[]
			{
				default(int),
				183
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				29
			},
			new int[]
			{
				default(int),
				210
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				45
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				123
			},
			new int[]
			{
				default(int),
				211
			},
			new int[] { 52, 1 },
			new int[] { 28, 1 },
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				61
			},
			new int[]
			{
				default(int),
				198
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				108
			},
			new int[]
			{
				default(int),
				169
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				154
			},
			new int[]
			{
				default(int),
				212
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				184
			},
			new int[]
			{
				default(int),
				139
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				77
			},
			new int[]
			{
				default(int),
				199
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				124
			},
			new int[]
			{
				default(int),
				213
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				93
			},
			new int[]
			{
				default(int),
				224
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				225
			},
			new int[]
			{
				default(int),
				30
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				14
			},
			new int[]
			{
				default(int),
				46
			},
			new int[]
			{
				default(int),
				226
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				227
			},
			new int[]
			{
				default(int),
				109
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				140
			},
			new int[]
			{
				default(int),
				228
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				229
			},
			new int[]
			{
				default(int),
				186
			},
			new int[]
			{
				default(int),
				240
			},
			new int[] { 38, 1 },
			new int[] { 16, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				241
			},
			new int[]
			{
				default(int),
				31
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				170
			},
			new int[]
			{
				default(int),
				155
			},
			new int[]
			{
				default(int),
				185
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				62
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				214
			},
			new int[]
			{
				default(int),
				200
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				78
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				215
			},
			new int[]
			{
				default(int),
				125
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				171
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				94
			},
			new int[]
			{
				default(int),
				201
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				15
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				156
			},
			new int[]
			{
				default(int),
				110
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				242
			},
			new int[]
			{
				default(int),
				47
			},
			new int[] { 32, 1 },
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				216
			},
			new int[]
			{
				default(int),
				141
			},
			new int[]
			{
				default(int),
				63
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				243
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				230
			},
			new int[]
			{
				default(int),
				202
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				244
			},
			new int[]
			{
				default(int),
				79
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				187
			},
			new int[]
			{
				default(int),
				172
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				231
			},
			new int[]
			{
				default(int),
				245
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				217
			},
			new int[]
			{
				default(int),
				157
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				95
			},
			new int[]
			{
				default(int),
				232
			},
			new int[] { 30, 1 },
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				111
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				246
			},
			new int[]
			{
				default(int),
				203
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				188
			},
			new int[]
			{
				default(int),
				173
			},
			new int[]
			{
				default(int),
				218
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				247
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				126
			},
			new int[]
			{
				default(int),
				127
			},
			new int[]
			{
				default(int),
				142
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				158
			},
			new int[]
			{
				default(int),
				174
			},
			new int[]
			{
				default(int),
				204
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				248
			},
			new int[]
			{
				default(int),
				143
			},
			new int[] { 18, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				219
			},
			new int[]
			{
				default(int),
				189
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				234
			},
			new int[]
			{
				default(int),
				249
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				159
			},
			new int[]
			{
				default(int),
				235
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				190
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				205
			},
			new int[]
			{
				default(int),
				250
			},
			new int[] { 14, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				221
			},
			new int[]
			{
				default(int),
				236
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				233
			},
			new int[]
			{
				default(int),
				175
			},
			new int[]
			{
				default(int),
				220
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				206
			},
			new int[]
			{
				default(int),
				251
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				191
			},
			new int[]
			{
				default(int),
				222
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				207
			},
			new int[]
			{
				default(int),
				238
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				223
			},
			new int[]
			{
				default(int),
				239
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				255
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				237
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				253
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				252
			},
			new int[]
			{
				default(int),
				254
			}
		};

		// Token: 0x04000091 RID: 145
		private static readonly int[][] ValTab14 = new int[][] { new int[2] };

		// Token: 0x04000092 RID: 146
		private static readonly int[][] ValTab15 = new int[][]
		{
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				17
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 50, 1 },
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				49
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				19
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				64
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				20
			},
			new int[]
			{
				default(int),
				65
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				66
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				36
			},
			new int[]
			{
				default(int),
				67
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				52
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				80
			},
			new int[]
			{
				default(int),
				5
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				81
			},
			new int[]
			{
				default(int),
				21
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				83
			},
			new int[]
			{
				default(int),
				97
			},
			new int[] { 90, 1 },
			new int[] { 36, 1 },
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				53
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				96
			},
			new int[]
			{
				default(int),
				6
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				22
			},
			new int[]
			{
				default(int),
				98
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				38
			},
			new int[]
			{
				default(int),
				84
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				99
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				54
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				112
			},
			new int[]
			{
				default(int),
				7
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				113
			},
			new int[]
			{
				default(int),
				85
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				23
			},
			new int[]
			{
				default(int),
				100
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				114
			},
			new int[]
			{
				default(int),
				39
			},
			new int[] { 24, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				115
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				55
			},
			new int[]
			{
				default(int),
				101
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				86
			},
			new int[]
			{
				default(int),
				128
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				8
			},
			new int[]
			{
				default(int),
				116
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				129
			},
			new int[]
			{
				default(int),
				24
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				130
			},
			new int[]
			{
				default(int),
				40
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				71
			},
			new int[]
			{
				default(int),
				102
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				131
			},
			new int[]
			{
				default(int),
				56
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				87
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				132
			},
			new int[]
			{
				default(int),
				72
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				144
			},
			new int[]
			{
				default(int),
				25
			},
			new int[]
			{
				default(int),
				145
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				146
			},
			new int[]
			{
				default(int),
				118
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				41
			},
			new int[] { 92, 1 },
			new int[] { 36, 1 },
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				133
			},
			new int[]
			{
				default(int),
				88
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				9
			},
			new int[]
			{
				default(int),
				119
			},
			new int[]
			{
				default(int),
				147
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				57
			},
			new int[]
			{
				default(int),
				148
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				73
			},
			new int[]
			{
				default(int),
				134
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				104
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				160
			},
			new int[]
			{
				default(int),
				10
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				161
			},
			new int[]
			{
				default(int),
				26
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				162
			},
			new int[]
			{
				default(int),
				42
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				149
			},
			new int[]
			{
				default(int),
				89
			},
			new int[] { 26, 1 },
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				163
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				58
			},
			new int[]
			{
				default(int),
				135
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				120
			},
			new int[]
			{
				default(int),
				164
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				74
			},
			new int[]
			{
				default(int),
				150
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				105
			},
			new int[]
			{
				default(int),
				176
			},
			new int[]
			{
				default(int),
				177
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				27
			},
			new int[]
			{
				default(int),
				165
			},
			new int[]
			{
				default(int),
				178
			},
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				90
			},
			new int[]
			{
				default(int),
				43
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				136
			},
			new int[]
			{
				default(int),
				151
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				179
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				121
			},
			new int[]
			{
				default(int),
				59
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				106
			},
			new int[]
			{
				default(int),
				180
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				75
			},
			new int[]
			{
				default(int),
				193
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				152
			},
			new int[]
			{
				default(int),
				137
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				28
			},
			new int[]
			{
				default(int),
				181
			},
			new int[] { 80, 1 },
			new int[] { 34, 1 },
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				91
			},
			new int[]
			{
				default(int),
				44
			},
			new int[]
			{
				default(int),
				194
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				11
			},
			new int[]
			{
				default(int),
				192
			},
			new int[]
			{
				default(int),
				166
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				167
			},
			new int[]
			{
				default(int),
				122
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				195
			},
			new int[]
			{
				default(int),
				60
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				153
			},
			new int[]
			{
				default(int),
				182
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				107
			},
			new int[]
			{
				default(int),
				196
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				76
			},
			new int[]
			{
				default(int),
				168
			},
			new int[] { 20, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				138
			},
			new int[]
			{
				default(int),
				197
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				208
			},
			new int[]
			{
				default(int),
				92
			},
			new int[]
			{
				default(int),
				209
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				183
			},
			new int[]
			{
				default(int),
				123
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				29
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				13
			},
			new int[]
			{
				default(int),
				45
			},
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				210
			},
			new int[]
			{
				default(int),
				211
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				61
			},
			new int[]
			{
				default(int),
				198
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				108
			},
			new int[]
			{
				default(int),
				169
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				154
			},
			new int[]
			{
				default(int),
				184
			},
			new int[]
			{
				default(int),
				212
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				139
			},
			new int[]
			{
				default(int),
				77
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				199
			},
			new int[]
			{
				default(int),
				124
			},
			new int[] { 68, 1 },
			new int[] { 34, 1 },
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				213
			},
			new int[]
			{
				default(int),
				93
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				224
			},
			new int[]
			{
				default(int),
				14
			},
			new int[]
			{
				default(int),
				225
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				30
			},
			new int[]
			{
				default(int),
				226
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				170
			},
			new int[]
			{
				default(int),
				46
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				185
			},
			new int[]
			{
				default(int),
				155
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				227
			},
			new int[]
			{
				default(int),
				214
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				109
			},
			new int[]
			{
				default(int),
				62
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				200
			},
			new int[]
			{
				default(int),
				140
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				228
			},
			new int[]
			{
				default(int),
				78
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				215
			},
			new int[]
			{
				default(int),
				125
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				229
			},
			new int[]
			{
				default(int),
				186
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				171
			},
			new int[]
			{
				default(int),
				94
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				201
			},
			new int[]
			{
				default(int),
				156
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				241
			},
			new int[]
			{
				default(int),
				31
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				240
			},
			new int[]
			{
				default(int),
				110
			},
			new int[]
			{
				default(int),
				242
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				47
			},
			new int[]
			{
				default(int),
				230
			},
			new int[] { 38, 1 },
			new int[] { 18, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				216
			},
			new int[]
			{
				default(int),
				243
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				63
			},
			new int[]
			{
				default(int),
				244
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				79
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				141
			},
			new int[]
			{
				default(int),
				217
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				187
			},
			new int[]
			{
				default(int),
				202
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				172
			},
			new int[]
			{
				default(int),
				231
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				126
			},
			new int[]
			{
				default(int),
				245
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				157
			},
			new int[]
			{
				default(int),
				95
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				232
			},
			new int[]
			{
				default(int),
				142
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				246
			},
			new int[]
			{
				default(int),
				203
			},
			new int[] { 34, 1 },
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				15
			},
			new int[]
			{
				default(int),
				174
			},
			new int[]
			{
				default(int),
				111
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				188
			},
			new int[]
			{
				default(int),
				218
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				173
			},
			new int[]
			{
				default(int),
				247
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				127
			},
			new int[]
			{
				default(int),
				233
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				158
			},
			new int[]
			{
				default(int),
				204
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				248
			},
			new int[]
			{
				default(int),
				143
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				219
			},
			new int[]
			{
				default(int),
				189
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				234
			},
			new int[]
			{
				default(int),
				249
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				159
			},
			new int[]
			{
				default(int),
				220
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				205
			},
			new int[]
			{
				default(int),
				235
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				190
			},
			new int[]
			{
				default(int),
				250
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				175
			},
			new int[]
			{
				default(int),
				221
			},
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				236
			},
			new int[]
			{
				default(int),
				206
			},
			new int[]
			{
				default(int),
				251
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				191
			},
			new int[]
			{
				default(int),
				237
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				222
			},
			new int[]
			{
				default(int),
				252
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				207
			},
			new int[]
			{
				default(int),
				253
			},
			new int[]
			{
				default(int),
				238
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				223
			},
			new int[]
			{
				default(int),
				254
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				239
			},
			new int[]
			{
				default(int),
				255
			}
		};

		// Token: 0x04000093 RID: 147
		private static readonly int[][] ValTab16 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				16
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				17
			},
			new int[] { 42, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				33
			},
			new int[]
			{
				default(int),
				18
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				3
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				65
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				51
			},
			new int[]
			{
				default(int),
				66
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				36
			},
			new int[]
			{
				default(int),
				80
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[] { 138, 1 },
			new int[] { 40, 1 },
			new int[] { 16, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				21
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				53
			},
			new int[]
			{
				default(int),
				83
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				96
			},
			new int[]
			{
				default(int),
				6
			},
			new int[]
			{
				default(int),
				97
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				22
			},
			new int[]
			{
				default(int),
				98
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				38
			},
			new int[]
			{
				default(int),
				84
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				69
			},
			new int[]
			{
				default(int),
				99
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				54
			},
			new int[]
			{
				default(int),
				112
			},
			new int[]
			{
				default(int),
				113
			},
			new int[] { 40, 1 },
			new int[] { 18, 1 },
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				23
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				7
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				85
			},
			new int[]
			{
				default(int),
				100
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				114
			},
			new int[]
			{
				default(int),
				39
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				70
			},
			new int[]
			{
				default(int),
				101
			},
			new int[]
			{
				default(int),
				115
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				55
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				86
			},
			new int[]
			{
				default(int),
				8
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				128
			},
			new int[]
			{
				default(int),
				129
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				24
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				116
			},
			new int[]
			{
				default(int),
				71
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				130
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				40
			},
			new int[]
			{
				default(int),
				102
			},
			new int[] { 24, 1 },
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				131
			},
			new int[]
			{
				default(int),
				56
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				132
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				72
			},
			new int[]
			{
				default(int),
				144
			},
			new int[]
			{
				default(int),
				145
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				25
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				9
			},
			new int[]
			{
				default(int),
				118
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				146
			},
			new int[]
			{
				default(int),
				41
			},
			new int[] { 14, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				133
			},
			new int[]
			{
				default(int),
				88
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				147
			},
			new int[]
			{
				default(int),
				57
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				160
			},
			new int[]
			{
				default(int),
				10
			},
			new int[]
			{
				default(int),
				26
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				162
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				87
			},
			new int[]
			{
				default(int),
				73
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				148
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				119
			},
			new int[]
			{
				default(int),
				134
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				161
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				104
			},
			new int[]
			{
				default(int),
				149
			},
			new int[] { 220, 1 },
			new int[] { 126, 1 },
			new int[] { 50, 1 },
			new int[] { 26, 1 },
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				42
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				89
			},
			new int[]
			{
				default(int),
				58
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				163
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				135
			},
			new int[]
			{
				default(int),
				120
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				164
			},
			new int[]
			{
				default(int),
				74
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				150
			},
			new int[]
			{
				default(int),
				105
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				176
			},
			new int[]
			{
				default(int),
				11
			},
			new int[]
			{
				default(int),
				177
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				27
			},
			new int[]
			{
				default(int),
				178
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				43
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				165
			},
			new int[]
			{
				default(int),
				90
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				179
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				166
			},
			new int[]
			{
				default(int),
				106
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				180
			},
			new int[]
			{
				default(int),
				75
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				193
			},
			new int[] { 30, 1 },
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				181
			},
			new int[]
			{
				default(int),
				194
			},
			new int[]
			{
				default(int),
				44
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				167
			},
			new int[]
			{
				default(int),
				195
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				107
			},
			new int[]
			{
				default(int),
				196
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				29
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				136
			},
			new int[]
			{
				default(int),
				151
			},
			new int[]
			{
				default(int),
				59
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				209
			},
			new int[]
			{
				default(int),
				210
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				45
			},
			new int[]
			{
				default(int),
				211
			},
			new int[] { 18, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				30
			},
			new int[]
			{
				default(int),
				46
			},
			new int[]
			{
				default(int),
				226
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				121
			},
			new int[]
			{
				default(int),
				152
			},
			new int[]
			{
				default(int),
				192
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				28
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				137
			},
			new int[]
			{
				default(int),
				91
			},
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				60
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				122
			},
			new int[]
			{
				default(int),
				182
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				76
			},
			new int[]
			{
				default(int),
				153
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				168
			},
			new int[]
			{
				default(int),
				138
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				13
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				197
			},
			new int[]
			{
				default(int),
				92
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				61
			},
			new int[]
			{
				default(int),
				198
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				108
			},
			new int[]
			{
				default(int),
				154
			},
			new int[] { 88, 1 },
			new int[] { 86, 1 },
			new int[] { 36, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				139
			},
			new int[]
			{
				default(int),
				77
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				199
			},
			new int[]
			{
				default(int),
				124
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				213
			},
			new int[]
			{
				default(int),
				93
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				224
			},
			new int[]
			{
				default(int),
				14
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				227
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				208
			},
			new int[]
			{
				default(int),
				183
			},
			new int[]
			{
				default(int),
				123
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				169
			},
			new int[]
			{
				default(int),
				184
			},
			new int[]
			{
				default(int),
				212
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				225
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				170
			},
			new int[]
			{
				default(int),
				185
			},
			new int[] { 24, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				155
			},
			new int[]
			{
				default(int),
				214
			},
			new int[]
			{
				default(int),
				109
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				62
			},
			new int[]
			{
				default(int),
				200
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				140
			},
			new int[]
			{
				default(int),
				228
			},
			new int[]
			{
				default(int),
				78
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				215
			},
			new int[]
			{
				default(int),
				229
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				186
			},
			new int[]
			{
				default(int),
				171
			},
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				156
			},
			new int[]
			{
				default(int),
				230
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				110
			},
			new int[]
			{
				default(int),
				216
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				141
			},
			new int[]
			{
				default(int),
				187
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				231
			},
			new int[]
			{
				default(int),
				157
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				232
			},
			new int[]
			{
				default(int),
				142
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				203
			},
			new int[]
			{
				default(int),
				188
			},
			new int[]
			{
				default(int),
				158
			},
			new int[]
			{
				default(int),
				241
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				31
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				15
			},
			new int[]
			{
				default(int),
				47
			},
			new int[] { 66, 1 },
			new int[] { 56, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				242
			},
			new int[] { 52, 1 },
			new int[] { 50, 1 },
			new int[] { 20, 1 },
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				189
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				94
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				125
			},
			new int[]
			{
				default(int),
				201
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				202
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				172
			},
			new int[]
			{
				default(int),
				126
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				218
			},
			new int[]
			{
				default(int),
				173
			},
			new int[]
			{
				default(int),
				204
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				174
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				219
			},
			new int[]
			{
				default(int),
				220
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				205
			},
			new int[]
			{
				default(int),
				190
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				235
			},
			new int[]
			{
				default(int),
				237
			},
			new int[]
			{
				default(int),
				238
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				217
			},
			new int[]
			{
				default(int),
				234
			},
			new int[]
			{
				default(int),
				233
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				222
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				221
			},
			new int[]
			{
				default(int),
				236
			},
			new int[]
			{
				default(int),
				206
			},
			new int[]
			{
				default(int),
				63
			},
			new int[]
			{
				default(int),
				240
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				243
			},
			new int[]
			{
				default(int),
				244
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				79
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				245
			},
			new int[]
			{
				default(int),
				95
			},
			new int[] { 10, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				255
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				246
			},
			new int[]
			{
				default(int),
				111
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				247
			},
			new int[]
			{
				default(int),
				127
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				143
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				248
			},
			new int[]
			{
				default(int),
				249
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				159
			},
			new int[]
			{
				default(int),
				250
			},
			new int[]
			{
				default(int),
				175
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				251
			},
			new int[]
			{
				default(int),
				191
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				252
			},
			new int[]
			{
				default(int),
				207
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				253
			},
			new int[]
			{
				default(int),
				223
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				254
			},
			new int[]
			{
				default(int),
				239
			}
		};

		// Token: 0x04000094 RID: 148
		private static readonly int[][] ValTab24 = new int[][]
		{
			new int[] { 60, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[]
			{
				default(int),
				16
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				17
			},
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				32
			},
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				33
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				18
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				34
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				48
			},
			new int[]
			{
				default(int),
				3
			},
			new int[] { 14, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				49
			},
			new int[]
			{
				default(int),
				19
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				50
			},
			new int[]
			{
				default(int),
				35
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				64
			},
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				65
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				20
			},
			new int[]
			{
				default(int),
				51
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				66
			},
			new int[]
			{
				default(int),
				36
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				67
			},
			new int[]
			{
				default(int),
				52
			},
			new int[]
			{
				default(int),
				81
			},
			new int[] { 6, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				80
			},
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				21
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				82
			},
			new int[]
			{
				default(int),
				37
			},
			new int[] { 250, 1 },
			new int[] { 98, 1 },
			new int[] { 34, 1 },
			new int[] { 18, 1 },
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				68
			},
			new int[]
			{
				default(int),
				83
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				53
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				96
			},
			new int[]
			{
				default(int),
				6
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				97
			},
			new int[]
			{
				default(int),
				22
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				98
			},
			new int[]
			{
				default(int),
				38
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				84
			},
			new int[]
			{
				default(int),
				69
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				99
			},
			new int[]
			{
				default(int),
				54
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				113
			},
			new int[]
			{
				default(int),
				85
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				100
			},
			new int[]
			{
				default(int),
				70
			},
			new int[] { 32, 1 },
			new int[] { 14, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				114
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				39
			},
			new int[]
			{
				default(int),
				55
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				115
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				112
			},
			new int[]
			{
				default(int),
				7
			},
			new int[]
			{
				default(int),
				23
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				101
			},
			new int[]
			{
				default(int),
				86
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				128
			},
			new int[]
			{
				default(int),
				8
			},
			new int[]
			{
				default(int),
				129
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				116
			},
			new int[]
			{
				default(int),
				71
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				24
			},
			new int[]
			{
				default(int),
				130
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				40
			},
			new int[]
			{
				default(int),
				102
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				131
			},
			new int[]
			{
				default(int),
				56
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				117
			},
			new int[]
			{
				default(int),
				87
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				132
			},
			new int[]
			{
				default(int),
				72
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				145
			},
			new int[]
			{
				default(int),
				25
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				146
			},
			new int[]
			{
				default(int),
				118
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				103
			},
			new int[]
			{
				default(int),
				41
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				133
			},
			new int[]
			{
				default(int),
				88
			},
			new int[] { 92, 1 },
			new int[] { 34, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				147
			},
			new int[]
			{
				default(int),
				57
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				148
			},
			new int[]
			{
				default(int),
				73
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				119
			},
			new int[]
			{
				default(int),
				134
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				104
			},
			new int[]
			{
				default(int),
				161
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				162
			},
			new int[]
			{
				default(int),
				42
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				149
			},
			new int[]
			{
				default(int),
				89
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				163
			},
			new int[]
			{
				default(int),
				58
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				135
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				120
			},
			new int[]
			{
				default(int),
				74
			},
			new int[] { 22, 1 },
			new int[] { 12, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				164
			},
			new int[]
			{
				default(int),
				150
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				105
			},
			new int[]
			{
				default(int),
				177
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				27
			},
			new int[]
			{
				default(int),
				165
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				178
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				90
			},
			new int[]
			{
				default(int),
				43
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				136
			},
			new int[]
			{
				default(int),
				179
			},
			new int[] { 16, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				144
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				9
			},
			new int[]
			{
				default(int),
				160
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				151
			},
			new int[]
			{
				default(int),
				121
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				166
			},
			new int[]
			{
				default(int),
				106
			},
			new int[]
			{
				default(int),
				180
			},
			new int[] { 12, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				26
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				10
			},
			new int[]
			{
				default(int),
				176
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				59
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				11
			},
			new int[]
			{
				default(int),
				192
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				75
			},
			new int[]
			{
				default(int),
				193
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				152
			},
			new int[]
			{
				default(int),
				137
			},
			new int[] { 67, 1 },
			new int[] { 34, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				28
			},
			new int[]
			{
				default(int),
				181
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				91
			},
			new int[]
			{
				default(int),
				194
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				44
			},
			new int[]
			{
				default(int),
				167
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				122
			},
			new int[]
			{
				default(int),
				195
			},
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				60
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				208
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				182
			},
			new int[]
			{
				default(int),
				107
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				196
			},
			new int[]
			{
				default(int),
				76
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				153
			},
			new int[]
			{
				default(int),
				168
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				138
			},
			new int[]
			{
				default(int),
				197
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				92
			},
			new int[]
			{
				default(int),
				209
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				183
			},
			new int[]
			{
				default(int),
				123
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				29
			},
			new int[]
			{
				default(int),
				210
			},
			new int[] { 9, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				45
			},
			new int[]
			{
				default(int),
				211
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				61
			},
			new int[]
			{
				default(int),
				198
			},
			new int[] { 85, 250 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				108
			},
			new int[]
			{
				default(int),
				169
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				154
			},
			new int[]
			{
				default(int),
				212
			},
			new int[] { 32, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				184
			},
			new int[]
			{
				default(int),
				139
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				77
			},
			new int[]
			{
				default(int),
				199
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				124
			},
			new int[]
			{
				default(int),
				213
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				93
			},
			new int[]
			{
				default(int),
				225
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				30
			},
			new int[]
			{
				default(int),
				226
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				170
			},
			new int[]
			{
				default(int),
				185
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				155
			},
			new int[]
			{
				default(int),
				227
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				214
			},
			new int[]
			{
				default(int),
				109
			},
			new int[] { 20, 1 },
			new int[] { 10, 1 },
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				62
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				46
			},
			new int[]
			{
				default(int),
				78
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				200
			},
			new int[]
			{
				default(int),
				140
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				228
			},
			new int[]
			{
				default(int),
				215
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				125
			},
			new int[]
			{
				default(int),
				171
			},
			new int[]
			{
				default(int),
				229
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				186
			},
			new int[]
			{
				default(int),
				94
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				201
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				156
			},
			new int[]
			{
				default(int),
				110
			},
			new int[] { 8, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				230
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				13
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				224
			},
			new int[]
			{
				default(int),
				14
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				216
			},
			new int[]
			{
				default(int),
				141
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				187
			},
			new int[]
			{
				default(int),
				202
			},
			new int[] { 74, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				255
			},
			new int[] { 64, 1 },
			new int[] { 58, 1 },
			new int[] { 32, 1 },
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				172
			},
			new int[]
			{
				default(int),
				231
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				126
			},
			new int[]
			{
				default(int),
				217
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				157
			},
			new int[]
			{
				default(int),
				232
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				142
			},
			new int[]
			{
				default(int),
				203
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				188
			},
			new int[]
			{
				default(int),
				218
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				173
			},
			new int[]
			{
				default(int),
				233
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				158
			},
			new int[]
			{
				default(int),
				204
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				219
			},
			new int[]
			{
				default(int),
				189
			},
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				234
			},
			new int[]
			{
				default(int),
				174
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				220
			},
			new int[]
			{
				default(int),
				205
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				235
			},
			new int[]
			{
				default(int),
				190
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				221
			},
			new int[]
			{
				default(int),
				236
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				206
			},
			new int[]
			{
				default(int),
				237
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				222
			},
			new int[]
			{
				default(int),
				238
			},
			new int[]
			{
				default(int),
				15
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				240
			},
			new int[]
			{
				default(int),
				31
			},
			new int[]
			{
				default(int),
				241
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				242
			},
			new int[]
			{
				default(int),
				47
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				243
			},
			new int[]
			{
				default(int),
				63
			},
			new int[] { 18, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				244
			},
			new int[]
			{
				default(int),
				79
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				245
			},
			new int[]
			{
				default(int),
				95
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				246
			},
			new int[]
			{
				default(int),
				111
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				247
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				127
			},
			new int[]
			{
				default(int),
				143
			},
			new int[] { 10, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				248
			},
			new int[]
			{
				default(int),
				249
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				159
			},
			new int[]
			{
				default(int),
				175
			},
			new int[]
			{
				default(int),
				250
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				251
			},
			new int[]
			{
				default(int),
				191
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				252
			},
			new int[]
			{
				default(int),
				207
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				253
			},
			new int[]
			{
				default(int),
				223
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				254
			},
			new int[]
			{
				default(int),
				239
			}
		};

		// Token: 0x04000095 RID: 149
		private static readonly int[][] ValTab32 = new int[][]
		{
			new int[] { 2, 1 },
			new int[2],
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				8
			},
			new int[]
			{
				default(int),
				4
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				1
			},
			new int[]
			{
				default(int),
				2
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				10
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				3
			},
			new int[]
			{
				default(int),
				6
			},
			new int[] { 6, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				9
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				5
			},
			new int[]
			{
				default(int),
				7
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				14
			},
			new int[]
			{
				default(int),
				13
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				15
			},
			new int[]
			{
				default(int),
				11
			}
		};

		// Token: 0x04000096 RID: 150
		private static readonly int[][] ValTab33 = new int[][]
		{
			new int[] { 16, 1 },
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[2],
			new int[]
			{
				default(int),
				1
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				2
			},
			new int[]
			{
				default(int),
				3
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				4
			},
			new int[]
			{
				default(int),
				5
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				6
			},
			new int[]
			{
				default(int),
				7
			},
			new int[] { 8, 1 },
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				8
			},
			new int[]
			{
				default(int),
				9
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				10
			},
			new int[]
			{
				default(int),
				11
			},
			new int[] { 4, 1 },
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				12
			},
			new int[]
			{
				default(int),
				13
			},
			new int[] { 2, 1 },
			new int[]
			{
				default(int),
				14
			},
			new int[]
			{
				default(int),
				15
			}
		};

		// Token: 0x04000097 RID: 151
		internal static Huffman[] HuffmanTable;

		// Token: 0x04000098 RID: 152
		private readonly int _Linbits;

		// Token: 0x04000099 RID: 153
		private readonly char _Tablename0;

		// Token: 0x0400009A RID: 154
		private readonly char _Tablename1;

		// Token: 0x0400009B RID: 155
		private readonly int _Treelen;

		// Token: 0x0400009C RID: 156
		private readonly int[][] _Val;

		// Token: 0x0400009D RID: 157
		private readonly int _Xlen;

		// Token: 0x0400009E RID: 158
		private readonly int _Ylen;

		// Token: 0x0400009F RID: 159
		private readonly int[] _Hlen;

		// Token: 0x040000A0 RID: 160
		private readonly int _Linmax;

		// Token: 0x040000A1 RID: 161
		private readonly int _RefRenamed;

		// Token: 0x040000A2 RID: 162
		private readonly int[] _Table;

		// Token: 0x040000A3 RID: 163
		private readonly char _Tablename2;
	}
}
