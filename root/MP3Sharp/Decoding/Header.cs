using System;
using System.Text;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000016 RID: 22
	public class Header
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004B14 File Offset: 0x00002D14
		internal Header()
		{
			this.InitBlock();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004B29 File Offset: 0x00002D29
		internal virtual int SyncHeader
		{
			get
			{
				return this._Headerstring;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004B31 File Offset: 0x00002D31
		private void InitBlock()
		{
			this._Syncmode = 0;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004B3C File Offset: 0x00002D3C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("Layer ");
			stringBuilder.Append(this.LayerAsString());
			stringBuilder.Append(" frame ");
			stringBuilder.Append(this.ModeAsString());
			stringBuilder.Append(' ');
			stringBuilder.Append(this.VersionAsString());
			if (!this.IsProtection())
			{
				stringBuilder.Append(" no");
			}
			stringBuilder.Append(" checksums");
			stringBuilder.Append(' ');
			stringBuilder.Append(this.SampleFrequencyAsString());
			stringBuilder.Append(',');
			stringBuilder.Append(' ');
			stringBuilder.Append(this.BitrateAsString());
			return stringBuilder.ToString();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004BF8 File Offset: 0x00002DF8
		internal void read_header(Bitstream stream, Crc16[] crcp)
		{
			bool flag = false;
			for (;;)
			{
				int num = stream.SyncHeader(this._Syncmode);
				this._Headerstring = num;
				if (this._Syncmode == 0)
				{
					this._Version = SupportClass.URShift(num, 19) & 1;
					if ((SupportClass.URShift(num, 20) & 1) == 0)
					{
						if (this._Version != 0)
						{
							break;
						}
						this._Version = 2;
					}
					if ((this._SampleFrequency = SupportClass.URShift(num, 10) & 3) == 3)
					{
						goto Block_4;
					}
				}
				this._Layer = (4 - SupportClass.URShift(num, 17)) & 3;
				this._ProtectionBit = SupportClass.URShift(num, 16) & 1;
				this._BitrateIndex = SupportClass.URShift(num, 12) & 15;
				this._PaddingBit = SupportClass.URShift(num, 9) & 1;
				this._Mode = SupportClass.URShift(num, 6) & 3;
				this._ModeExtension = SupportClass.URShift(num, 4) & 3;
				if (this._Mode == 1)
				{
					this._IntensityStereoBound = (this._ModeExtension << 2) + 4;
				}
				else
				{
					this._IntensityStereoBound = 0;
				}
				this._Copyright |= (SupportClass.URShift(num, 3) & 1) == 1;
				this._Original |= (SupportClass.URShift(num, 2) & 1) == 1;
				if (this._Layer == 1)
				{
					this._NumberOfSubbands = 32;
				}
				else
				{
					int num2 = this._BitrateIndex;
					if (this._Mode != 3)
					{
						if (num2 == 4)
						{
							num2 = 1;
						}
						else
						{
							num2 -= 4;
						}
					}
					if (num2 == 1 || num2 == 2)
					{
						if (this._SampleFrequency == 2)
						{
							this._NumberOfSubbands = 12;
						}
						else
						{
							this._NumberOfSubbands = 8;
						}
					}
					else if (this._SampleFrequency == 1 || (num2 >= 3 && num2 <= 5))
					{
						this._NumberOfSubbands = 27;
					}
					else
					{
						this._NumberOfSubbands = 30;
					}
				}
				if (this._IntensityStereoBound > this._NumberOfSubbands)
				{
					this._IntensityStereoBound = this._NumberOfSubbands;
				}
				this.CalculateFrameSize();
				stream.Read_frame_data(this.Framesize);
				if (stream.IsSyncCurrentPosition((int)this._Syncmode))
				{
					if (this._Syncmode == 0)
					{
						this._Syncmode = 1;
						stream.SetSyncWord(num & -521024);
					}
					flag = true;
				}
				else
				{
					stream.UnreadFrame();
				}
				if (flag)
				{
					goto Block_16;
				}
			}
			throw stream.NewBitstreamException(256);
			Block_4:
			throw stream.NewBitstreamException(256);
			Block_16:
			stream.ParseFrame();
			if (this._ProtectionBit == 0)
			{
				this.Checksum = (short)stream.GetBitsFromBuffer(16);
				if (this._CRC == null)
				{
					this._CRC = new Crc16();
				}
				int num;
				this._CRC.AddBits(num, 16);
				crcp[0] = this._CRC;
			}
			else
			{
				crcp[0] = null;
			}
			int sampleFrequency = this._SampleFrequency;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004E64 File Offset: 0x00003064
		internal int Version()
		{
			return this._Version;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004E6C File Offset: 0x0000306C
		internal int Layer()
		{
			return this._Layer;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E74 File Offset: 0x00003074
		internal int bitrate_index()
		{
			return this._BitrateIndex;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004E7C File Offset: 0x0000307C
		internal int sample_frequency()
		{
			return this._SampleFrequency;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004E84 File Offset: 0x00003084
		internal int Frequency()
		{
			return Header.Frequencies[this._Version][this._SampleFrequency];
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004E99 File Offset: 0x00003099
		internal int Mode()
		{
			return this._Mode;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004EA1 File Offset: 0x000030A1
		internal bool IsProtection()
		{
			return this._ProtectionBit == 0;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004EAE File Offset: 0x000030AE
		internal bool IsCopyright()
		{
			return this._Copyright;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004EB6 File Offset: 0x000030B6
		internal bool IsOriginal()
		{
			return this._Original;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004EBE File Offset: 0x000030BE
		internal bool IsChecksumOK()
		{
			return this.Checksum == this._CRC.Checksum();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004ED3 File Offset: 0x000030D3
		internal bool IsPadding()
		{
			return this._PaddingBit != 0;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004EE0 File Offset: 0x000030E0
		internal int Slots()
		{
			return this.NSlots;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004EE8 File Offset: 0x000030E8
		internal int mode_extension()
		{
			return this._ModeExtension;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004EF0 File Offset: 0x000030F0
		internal int CalculateFrameSize()
		{
			if (this._Layer == 1)
			{
				this.Framesize = 12 * Header.Bitrates[this._Version][0][this._BitrateIndex] / Header.Frequencies[this._Version][this._SampleFrequency];
				if (this._PaddingBit != 0)
				{
					this.Framesize++;
				}
				this.Framesize <<= 2;
				this.NSlots = 0;
			}
			else
			{
				this.Framesize = 144 * Header.Bitrates[this._Version][this._Layer - 1][this._BitrateIndex] / Header.Frequencies[this._Version][this._SampleFrequency];
				if (this._Version == 0 || this._Version == 2)
				{
					this.Framesize >>= 1;
				}
				if (this._PaddingBit != 0)
				{
					this.Framesize++;
				}
				if (this._Layer == 3)
				{
					if (this._Version == 1)
					{
						this.NSlots = this.Framesize - ((this._Mode == 3) ? 17 : 32) - ((this._ProtectionBit != 0) ? 0 : 2) - 4;
					}
					else
					{
						this.NSlots = this.Framesize - ((this._Mode == 3) ? 9 : 17) - ((this._ProtectionBit != 0) ? 0 : 2) - 4;
					}
				}
				else
				{
					this.NSlots = 0;
				}
			}
			this.Framesize -= 4;
			return this.Framesize;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005060 File Offset: 0x00003260
		internal int MaxNumberOfFrame(int streamsize)
		{
			if (this.Framesize + 4 - this._PaddingBit == 0)
			{
				return 0;
			}
			return streamsize / (this.Framesize + 4 - this._PaddingBit);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005086 File Offset: 0x00003286
		internal int min_number_of_frames(int streamsize)
		{
			if (this.Framesize + 5 - this._PaddingBit == 0)
			{
				return 0;
			}
			return streamsize / (this.Framesize + 5 - this._PaddingBit);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000050AC File Offset: 0x000032AC
		internal float MsPerFrame()
		{
			return (new float[][]
			{
				new float[] { 8.707483f, 8f, 12f },
				new float[] { 26.12245f, 24f, 36f },
				new float[] { 26.12245f, 24f, 36f }
			})[this._Layer - 1][this._SampleFrequency];
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000510B File Offset: 0x0000330B
		internal float TotalMS(int streamsize)
		{
			return (float)this.MaxNumberOfFrame(streamsize) * this.MsPerFrame();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000511C File Offset: 0x0000331C
		internal string LayerAsString()
		{
			switch (this._Layer)
			{
			case 1:
				return "I";
			case 2:
				return "II";
			case 3:
				return "III";
			default:
				return null;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005159 File Offset: 0x00003359
		internal string BitrateAsString()
		{
			return Header.BitrateStr[this._Version][this._Layer - 1][this._BitrateIndex];
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005178 File Offset: 0x00003378
		internal string SampleFrequencyAsString()
		{
			switch (this._SampleFrequency)
			{
			case 0:
				if (this._Version == 1)
				{
					return "44.1 kHz";
				}
				if (this._Version == 0)
				{
					return "22.05 kHz";
				}
				return "11.025 kHz";
			case 1:
				if (this._Version == 1)
				{
					return "48 kHz";
				}
				if (this._Version == 0)
				{
					return "24 kHz";
				}
				return "12 kHz";
			case 2:
				if (this._Version == 1)
				{
					return "32 kHz";
				}
				if (this._Version == 0)
				{
					return "16 kHz";
				}
				return "8 kHz";
			default:
				return null;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000520C File Offset: 0x0000340C
		internal string ModeAsString()
		{
			switch (this._Mode)
			{
			case 0:
				return "Stereo";
			case 1:
				return "Joint stereo";
			case 2:
				return "Dual channel";
			case 3:
				return "Single channel";
			default:
				return null;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005254 File Offset: 0x00003454
		internal string VersionAsString()
		{
			switch (this._Version)
			{
			case 0:
				return "MPEG-2 LSF";
			case 1:
				return "MPEG-1";
			case 2:
				return "MPEG-2.5 LSF";
			default:
				return null;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000528F File Offset: 0x0000348F
		internal int NumberSubbands()
		{
			return this._NumberOfSubbands;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005297 File Offset: 0x00003497
		internal int IntensityStereoBound()
		{
			return this._IntensityStereoBound;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000052A0 File Offset: 0x000034A0
		// Note: this type is marked as 'beforefieldinit'.
		static Header()
		{
		}

		// Token: 0x04000062 RID: 98
		internal const int MPEG2_LSF = 0;

		// Token: 0x04000063 RID: 99
		internal const int MPEG25_LSF = 2;

		// Token: 0x04000064 RID: 100
		internal const int MPEG1 = 1;

		// Token: 0x04000065 RID: 101
		internal const int STEREO = 0;

		// Token: 0x04000066 RID: 102
		internal const int JOINT_STEREO = 1;

		// Token: 0x04000067 RID: 103
		internal const int DUAL_CHANNEL = 2;

		// Token: 0x04000068 RID: 104
		internal const int SINGLE_CHANNEL = 3;

		// Token: 0x04000069 RID: 105
		internal const int FOURTYFOUR_POINT_ONE = 0;

		// Token: 0x0400006A RID: 106
		internal const int FOURTYEIGHT = 1;

		// Token: 0x0400006B RID: 107
		internal const int THIRTYTWO = 2;

		// Token: 0x0400006C RID: 108
		internal static readonly int[][] Frequencies = new int[][]
		{
			new int[] { 22050, 24000, 16000, 1 },
			new int[] { 44100, 48000, 32000, 1 },
			new int[] { 11025, 12000, 8000, 1 }
		};

		// Token: 0x0400006D RID: 109
		internal static readonly int[][][] Bitrates = new int[][][]
		{
			new int[][]
			{
				new int[]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 144000,
					160000, 176000, 192000, 224000, 256000, 0
				},
				new int[]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				},
				new int[]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				}
			},
			new int[][]
			{
				new int[]
				{
					0, 32000, 64000, 96000, 128000, 160000, 192000, 224000, 256000, 288000,
					320000, 352000, 384000, 416000, 448000, 0
				},
				new int[]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 160000,
					192000, 224000, 256000, 320000, 384000, 0
				},
				new int[]
				{
					0, 32000, 40000, 48000, 56000, 64000, 80000, 96000, 112000, 128000,
					160000, 192000, 224000, 256000, 320000, 0
				}
			},
			new int[][]
			{
				new int[]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 144000,
					160000, 176000, 192000, 224000, 256000, 0
				},
				new int[]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				},
				new int[]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				}
			}
		};

		// Token: 0x0400006E RID: 110
		internal static readonly string[][][] BitrateStr = new string[][][]
		{
			new string[][]
			{
				new string[]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s",
					"160 kbit/s", "176 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				}
			},
			new string[][]
			{
				new string[]
				{
					"free format", "32 kbit/s", "64 kbit/s", "96 kbit/s", "128 kbit/s", "160 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "288 kbit/s",
					"320 kbit/s", "352 kbit/s", "384 kbit/s", "416 kbit/s", "448 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "160 kbit/s",
					"192 kbit/s", "224 kbit/s", "256 kbit/s", "320 kbit/s", "384 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s",
					"160 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "320 kbit/s", "forbidden"
				}
			},
			new string[][]
			{
				new string[]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s",
					"160 kbit/s", "176 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				},
				new string[]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				}
			}
		};

		// Token: 0x0400006F RID: 111
		internal short Checksum;

		// Token: 0x04000070 RID: 112
		internal int NSlots;

		// Token: 0x04000071 RID: 113
		private Crc16 _CRC;

		// Token: 0x04000072 RID: 114
		internal int Framesize;

		// Token: 0x04000073 RID: 115
		private bool _Copyright;

		// Token: 0x04000074 RID: 116
		private bool _Original;

		// Token: 0x04000075 RID: 117
		private int _Headerstring = -1;

		// Token: 0x04000076 RID: 118
		private int _Layer;

		// Token: 0x04000077 RID: 119
		private int _ProtectionBit;

		// Token: 0x04000078 RID: 120
		private int _BitrateIndex;

		// Token: 0x04000079 RID: 121
		private int _PaddingBit;

		// Token: 0x0400007A RID: 122
		private int _ModeExtension;

		// Token: 0x0400007B RID: 123
		private int _Mode;

		// Token: 0x0400007C RID: 124
		private int _NumberOfSubbands;

		// Token: 0x0400007D RID: 125
		private int _IntensityStereoBound;

		// Token: 0x0400007E RID: 126
		private int _SampleFrequency;

		// Token: 0x0400007F RID: 127
		private sbyte _Syncmode;

		// Token: 0x04000080 RID: 128
		private int _Version;
	}
}
