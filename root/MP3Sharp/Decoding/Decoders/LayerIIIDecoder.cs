using System;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders
{
	// Token: 0x02000021 RID: 33
	internal sealed class LayerIIIDecoder : IFrameDecoder
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00015898 File Offset: 0x00013A98
		static LayerIIIDecoder()
		{
			int[][][] array = new int[6][][];
			array[0] = new int[][]
			{
				new int[] { 6, 5, 5, 5 },
				new int[] { 9, 9, 9, 9 },
				new int[] { 6, 9, 9, 9 }
			};
			array[1] = new int[][]
			{
				new int[] { 6, 5, 7, 3 },
				new int[] { 9, 9, 12, 6 },
				new int[] { 6, 9, 12, 6 }
			};
			int num = 2;
			int[][] array2 = new int[3][];
			int num2 = 0;
			int[] array3 = new int[4];
			array3[0] = 11;
			array3[1] = 10;
			array2[num2] = array3;
			int num3 = 1;
			int[] array4 = new int[4];
			array4[0] = 18;
			array4[1] = 18;
			array2[num3] = array4;
			int num4 = 2;
			int[] array5 = new int[4];
			array5[0] = 15;
			array5[1] = 18;
			array2[num4] = array5;
			array[num] = array2;
			array[3] = new int[][]
			{
				new int[] { 7, 7, 7, 0 },
				new int[] { 12, 12, 12, 0 },
				new int[] { 6, 15, 12, 0 }
			};
			array[4] = new int[][]
			{
				new int[] { 6, 6, 6, 3 },
				new int[] { 12, 9, 9, 6 },
				new int[] { 6, 12, 9, 6 }
			};
			array[5] = new int[][]
			{
				new int[] { 8, 8, 5, 0 },
				new int[] { 15, 12, 9, 0 },
				new int[] { 6, 18, 9, 0 }
			};
			LayerIIIDecoder.NrOfSfbBlock = array;
			LayerIIIDecoder.PowerTable = LayerIIIDecoder.CreatePowerTable();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00015B90 File Offset: 0x00013D90
		internal LayerIIIDecoder(Bitstream stream, Header header, SynthesisFilter filtera, SynthesisFilter filterb, ABuffer buffer, int whichCh)
		{
			Huffman.Initialize();
			this.InitBlock();
			this._Is1D = new int[580];
			this._Ro = new float[2][][];
			for (int i = 0; i < 2; i++)
			{
				this._Ro[i] = new float[32][];
				for (int j = 0; j < 32; j++)
				{
					this._Ro[i][j] = new float[18];
				}
			}
			this._Lr = new float[2][][];
			for (int k = 0; k < 2; k++)
			{
				this._Lr[k] = new float[32][];
				for (int l = 0; l < 32; l++)
				{
					this._Lr[k][l] = new float[18];
				}
			}
			this._Out1D = new float[576];
			this._Prevblck = new float[2][];
			for (int m = 0; m < 2; m++)
			{
				this._Prevblck[m] = new float[576];
			}
			this._K = new float[2][];
			for (int n = 0; n < 2; n++)
			{
				this._K[n] = new float[576];
			}
			this._Nonzero = new int[2];
			this._Scalefac = new ScaleFactorData[]
			{
				new ScaleFactorData(),
				new ScaleFactorData()
			};
			this._SfBandIndex = new SBI[9];
			int[] array = new int[]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] array2 = new int[]
			{
				0, 4, 8, 12, 18, 24, 32, 42, 56, 74,
				100, 132, 174, 192
			};
			int[] array3 = new int[]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 114, 136, 162, 194, 232, 278, 330, 394,
				464, 540, 576
			};
			int[] array4 = new int[]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 136, 180, 192
			};
			int[] array5 = new int[]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] array6 = new int[]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] array7 = new int[]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 44,
				52, 62, 74, 90, 110, 134, 162, 196, 238, 288,
				342, 418, 576
			};
			int[] array8 = new int[]
			{
				0, 4, 8, 12, 16, 22, 30, 40, 52, 66,
				84, 106, 136, 192
			};
			int[] array9 = new int[]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 42,
				50, 60, 72, 88, 106, 128, 156, 190, 230, 276,
				330, 384, 576
			};
			int[] array10 = new int[]
			{
				0, 4, 8, 12, 16, 22, 28, 38, 50, 64,
				80, 100, 126, 192
			};
			int[] array11 = new int[]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 44,
				54, 66, 82, 102, 126, 156, 194, 240, 296, 364,
				448, 550, 576
			};
			int[] array12 = new int[]
			{
				0, 4, 8, 12, 16, 22, 30, 42, 58, 78,
				104, 138, 180, 192
			};
			int[] array13 = new int[]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] array14 = new int[]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] array15 = new int[]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] array16 = new int[]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] array17 = new int[]
			{
				0, 12, 24, 36, 48, 60, 72, 88, 108, 132,
				160, 192, 232, 280, 336, 400, 476, 566, 568, 570,
				572, 574, 576
			};
			int[] array18 = new int[]
			{
				0, 8, 16, 24, 36, 52, 72, 96, 124, 160,
				162, 164, 166, 192
			};
			this._SfBandIndex[0] = new SBI(array, array2);
			this._SfBandIndex[1] = new SBI(array3, array4);
			this._SfBandIndex[2] = new SBI(array5, array6);
			this._SfBandIndex[3] = new SBI(array7, array8);
			this._SfBandIndex[4] = new SBI(array9, array10);
			this._SfBandIndex[5] = new SBI(array11, array12);
			this._SfBandIndex[6] = new SBI(array13, array14);
			this._SfBandIndex[7] = new SBI(array15, array16);
			this._SfBandIndex[8] = new SBI(array17, array18);
			if (LayerIIIDecoder._reorderTable == null)
			{
				LayerIIIDecoder._reorderTable = new int[9][];
				for (int num = 0; num < 9; num++)
				{
					LayerIIIDecoder._reorderTable[num] = LayerIIIDecoder.Reorder(this._SfBandIndex[num].S);
				}
			}
			int[] array19 = new int[] { 0, 6, 11, 16, 21 };
			int[] array20 = new int[]
			{
				default(int),
				6,
				12
			};
			this.Sftable = new ScaleFactorTable(this, array19, array20);
			this.ScalefacBuffer = new int[54];
			this._Stream = stream;
			this._Header = header;
			this._Filter1 = filtera;
			this._Filter2 = filterb;
			this._Buffer = buffer;
			this._WhichChannels = whichCh;
			this._FrameStart = 0;
			this._Channels = ((this._Header.Mode() == 3) ? 1 : 2);
			this._MaxGr = ((this._Header.Version() == 1) ? 2 : 1);
			this._Sfreq = this._Header.sample_frequency() + ((this._Header.Version() == 1) ? 3 : ((this._Header.Version() == 2) ? 6 : 0));
			if (this._Channels == 2)
			{
				switch (this._WhichChannels)
				{
				case 1:
				case 3:
					this._FirstChannel = (this._LastChannel = 0);
					break;
				case 2:
					this._FirstChannel = (this._LastChannel = 1);
					break;
				default:
					this._FirstChannel = 0;
					this._LastChannel = 1;
					break;
				}
			}
			else
			{
				this._FirstChannel = (this._LastChannel = 0);
			}
			for (int num2 = 0; num2 < 2; num2++)
			{
				for (int num3 = 0; num3 < 576; num3++)
				{
					this._Prevblck[num2][num3] = 0f;
				}
			}
			this._Nonzero[0] = (this._Nonzero[1] = 576);
			this._BitReserve = new BitReserve();
			this._SideInfo = new Layer3SideInfo();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0001611E File Offset: 0x0001431E
		public void DecodeFrame()
		{
			this.Decode();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00016128 File Offset: 0x00014328
		private void InitBlock()
		{
			this.Rawout = new float[36];
			this.TsOutCopy = new float[18];
			this.IsRatio = new float[576];
			this.IsPos = new int[576];
			this._NewSlen = new int[4];
			this._Samples2 = new float[32];
			this._Samples1 = new float[32];
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00016198 File Offset: 0x00014398
		internal void SeekNotify()
		{
			this._FrameStart = 0;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 576; j++)
				{
					this._Prevblck[i][j] = 0f;
				}
			}
			this._BitReserve = new BitReserve();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000161E4 File Offset: 0x000143E4
		internal void Decode()
		{
			int num = this._Header.Slots();
			this.ReadSideInfo();
			for (int i = 0; i < num; i++)
			{
				this._BitReserve.PutBuffer(this._Stream.GetBitsFromBuffer(8));
			}
			int num2 = SupportClass.URShift(this._BitReserve.HssTell(), 3);
			int num3;
			if ((num3 = this._BitReserve.HssTell() & 7) != 0)
			{
				this._BitReserve.ReadBits(8 - num3);
				num2++;
			}
			int j = this._FrameStart - num2 - this._SideInfo.MainDataBegin;
			this._FrameStart += num;
			if (j < 0)
			{
				return;
			}
			if (num2 > 4096)
			{
				this._FrameStart -= 4096;
				this._BitReserve.RewindStreamBytes(4096);
			}
			while (j > 0)
			{
				this._BitReserve.ReadBits(8);
				j--;
			}
			for (int k = 0; k < this._MaxGr; k++)
			{
				for (int l = 0; l < this._Channels; l++)
				{
					this._Part2Start = this._BitReserve.HssTell();
					if (this._Header.Version() == 1)
					{
						this.ReadScaleFactors(l, k);
					}
					else
					{
						this.GLSFScaleFactors(l, k);
					}
					this.HuffmanDecode(l, k);
					this.DequantizeSample(this._Ro[l], l, k);
				}
				this.Stereo(k);
				if (this._WhichChannels == 3 && this._Channels > 1)
				{
					this.DoDownMix();
				}
				for (int l = this._FirstChannel; l <= this._LastChannel; l++)
				{
					this.Reorder(this._Lr[l], l, k);
					this.Antialias(l, k);
					this.Hybrid(l, k);
					for (int m = 18; m < 576; m += 36)
					{
						for (int n = 1; n < 18; n += 2)
						{
							this._Out1D[m + n] = -this._Out1D[m + n];
						}
					}
					if (l == 0 || this._WhichChannels == 2)
					{
						for (int n = 0; n < 18; n++)
						{
							int num4 = 0;
							for (int m = 0; m < 576; m += 18)
							{
								this._Samples1[num4] = this._Out1D[m + n];
								num4++;
							}
							this._Filter1.AddSamples(this._Samples1);
							this._Filter1.calculate_pc_samples(this._Buffer);
						}
					}
					else
					{
						for (int n = 0; n < 18; n++)
						{
							int num4 = 0;
							for (int m = 0; m < 576; m += 18)
							{
								this._Samples2[num4] = this._Out1D[m + n];
								num4++;
							}
							this._Filter2.AddSamples(this._Samples2);
							this._Filter2.calculate_pc_samples(this._Buffer);
						}
					}
				}
			}
			this._Buffer.WriteBuffer(1);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000164C8 File Offset: 0x000146C8
		private bool ReadSideInfo()
		{
			if (this._Header.Version() == 1)
			{
				this._SideInfo.MainDataBegin = this._Stream.GetBitsFromBuffer(9);
				if (this._Channels == 1)
				{
					this._SideInfo.PrivateBits = this._Stream.GetBitsFromBuffer(5);
				}
				else
				{
					this._SideInfo.PrivateBits = this._Stream.GetBitsFromBuffer(3);
				}
				for (int i = 0; i < this._Channels; i++)
				{
					this._SideInfo.Channels[i].ScaleFactorBits[0] = this._Stream.GetBitsFromBuffer(1);
					this._SideInfo.Channels[i].ScaleFactorBits[1] = this._Stream.GetBitsFromBuffer(1);
					this._SideInfo.Channels[i].ScaleFactorBits[2] = this._Stream.GetBitsFromBuffer(1);
					this._SideInfo.Channels[i].ScaleFactorBits[3] = this._Stream.GetBitsFromBuffer(1);
				}
				for (int j = 0; j < 2; j++)
				{
					for (int i = 0; i < this._Channels; i++)
					{
						this._SideInfo.Channels[i].Granules[j].Part23Length = this._Stream.GetBitsFromBuffer(12);
						this._SideInfo.Channels[i].Granules[j].BigValues = this._Stream.GetBitsFromBuffer(9);
						this._SideInfo.Channels[i].Granules[j].GlobalGain = this._Stream.GetBitsFromBuffer(8);
						this._SideInfo.Channels[i].Granules[j].ScaleFacCompress = this._Stream.GetBitsFromBuffer(4);
						this._SideInfo.Channels[i].Granules[j].WindowSwitchingFlag = this._Stream.GetBitsFromBuffer(1);
						if (this._SideInfo.Channels[i].Granules[j].WindowSwitchingFlag != 0)
						{
							this._SideInfo.Channels[i].Granules[j].BlockType = this._Stream.GetBitsFromBuffer(2);
							this._SideInfo.Channels[i].Granules[j].MixedBlockFlag = this._Stream.GetBitsFromBuffer(1);
							this._SideInfo.Channels[i].Granules[j].TableSelect[0] = this._Stream.GetBitsFromBuffer(5);
							this._SideInfo.Channels[i].Granules[j].TableSelect[1] = this._Stream.GetBitsFromBuffer(5);
							this._SideInfo.Channels[i].Granules[j].SubblockGain[0] = this._Stream.GetBitsFromBuffer(3);
							this._SideInfo.Channels[i].Granules[j].SubblockGain[1] = this._Stream.GetBitsFromBuffer(3);
							this._SideInfo.Channels[i].Granules[j].SubblockGain[2] = this._Stream.GetBitsFromBuffer(3);
							if (this._SideInfo.Channels[i].Granules[j].BlockType == 0)
							{
								return false;
							}
							if (this._SideInfo.Channels[i].Granules[j].BlockType == 2 && this._SideInfo.Channels[i].Granules[j].MixedBlockFlag == 0)
							{
								this._SideInfo.Channels[i].Granules[j].Region0Count = 8;
							}
							else
							{
								this._SideInfo.Channels[i].Granules[j].Region0Count = 7;
							}
							this._SideInfo.Channels[i].Granules[j].Region1Count = 20 - this._SideInfo.Channels[i].Granules[j].Region0Count;
						}
						else
						{
							this._SideInfo.Channels[i].Granules[j].TableSelect[0] = this._Stream.GetBitsFromBuffer(5);
							this._SideInfo.Channels[i].Granules[j].TableSelect[1] = this._Stream.GetBitsFromBuffer(5);
							this._SideInfo.Channels[i].Granules[j].TableSelect[2] = this._Stream.GetBitsFromBuffer(5);
							this._SideInfo.Channels[i].Granules[j].Region0Count = this._Stream.GetBitsFromBuffer(4);
							this._SideInfo.Channels[i].Granules[j].Region1Count = this._Stream.GetBitsFromBuffer(3);
							this._SideInfo.Channels[i].Granules[j].BlockType = 0;
						}
						this._SideInfo.Channels[i].Granules[j].Preflag = this._Stream.GetBitsFromBuffer(1);
						this._SideInfo.Channels[i].Granules[j].ScaleFacScale = this._Stream.GetBitsFromBuffer(1);
						this._SideInfo.Channels[i].Granules[j].Count1TableSelect = this._Stream.GetBitsFromBuffer(1);
					}
				}
			}
			else
			{
				this._SideInfo.MainDataBegin = this._Stream.GetBitsFromBuffer(8);
				if (this._Channels == 1)
				{
					this._SideInfo.PrivateBits = this._Stream.GetBitsFromBuffer(1);
				}
				else
				{
					this._SideInfo.PrivateBits = this._Stream.GetBitsFromBuffer(2);
				}
				for (int i = 0; i < this._Channels; i++)
				{
					this._SideInfo.Channels[i].Granules[0].Part23Length = this._Stream.GetBitsFromBuffer(12);
					this._SideInfo.Channels[i].Granules[0].BigValues = this._Stream.GetBitsFromBuffer(9);
					this._SideInfo.Channels[i].Granules[0].GlobalGain = this._Stream.GetBitsFromBuffer(8);
					this._SideInfo.Channels[i].Granules[0].ScaleFacCompress = this._Stream.GetBitsFromBuffer(9);
					this._SideInfo.Channels[i].Granules[0].WindowSwitchingFlag = this._Stream.GetBitsFromBuffer(1);
					if (this._SideInfo.Channels[i].Granules[0].WindowSwitchingFlag != 0)
					{
						this._SideInfo.Channels[i].Granules[0].BlockType = this._Stream.GetBitsFromBuffer(2);
						this._SideInfo.Channels[i].Granules[0].MixedBlockFlag = this._Stream.GetBitsFromBuffer(1);
						this._SideInfo.Channels[i].Granules[0].TableSelect[0] = this._Stream.GetBitsFromBuffer(5);
						this._SideInfo.Channels[i].Granules[0].TableSelect[1] = this._Stream.GetBitsFromBuffer(5);
						this._SideInfo.Channels[i].Granules[0].SubblockGain[0] = this._Stream.GetBitsFromBuffer(3);
						this._SideInfo.Channels[i].Granules[0].SubblockGain[1] = this._Stream.GetBitsFromBuffer(3);
						this._SideInfo.Channels[i].Granules[0].SubblockGain[2] = this._Stream.GetBitsFromBuffer(3);
						if (this._SideInfo.Channels[i].Granules[0].BlockType == 0)
						{
							return false;
						}
						if (this._SideInfo.Channels[i].Granules[0].BlockType == 2 && this._SideInfo.Channels[i].Granules[0].MixedBlockFlag == 0)
						{
							this._SideInfo.Channels[i].Granules[0].Region0Count = 8;
						}
						else
						{
							this._SideInfo.Channels[i].Granules[0].Region0Count = 7;
							this._SideInfo.Channels[i].Granules[0].Region1Count = 20 - this._SideInfo.Channels[i].Granules[0].Region0Count;
						}
					}
					else
					{
						this._SideInfo.Channels[i].Granules[0].TableSelect[0] = this._Stream.GetBitsFromBuffer(5);
						this._SideInfo.Channels[i].Granules[0].TableSelect[1] = this._Stream.GetBitsFromBuffer(5);
						this._SideInfo.Channels[i].Granules[0].TableSelect[2] = this._Stream.GetBitsFromBuffer(5);
						this._SideInfo.Channels[i].Granules[0].Region0Count = this._Stream.GetBitsFromBuffer(4);
						this._SideInfo.Channels[i].Granules[0].Region1Count = this._Stream.GetBitsFromBuffer(3);
						this._SideInfo.Channels[i].Granules[0].BlockType = 0;
					}
					this._SideInfo.Channels[i].Granules[0].ScaleFacScale = this._Stream.GetBitsFromBuffer(1);
					this._SideInfo.Channels[i].Granules[0].Count1TableSelect = this._Stream.GetBitsFromBuffer(1);
				}
			}
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00016E28 File Offset: 0x00015028
		private void ReadScaleFactors(int ch, int gr)
		{
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			int scaleFacCompress = granuleInfo.ScaleFacCompress;
			int num = LayerIIIDecoder.Slen[0][scaleFacCompress];
			int num2 = LayerIIIDecoder.Slen[1][scaleFacCompress];
			if (granuleInfo.WindowSwitchingFlag == 0 || granuleInfo.BlockType != 2)
			{
				if (this._SideInfo.Channels[ch].ScaleFactorBits[0] == 0 || gr == 0)
				{
					this._Scalefac[ch].L[0] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[1] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[2] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[3] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[4] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[5] = this._BitReserve.ReadBits(num);
				}
				if (this._SideInfo.Channels[ch].ScaleFactorBits[1] == 0 || gr == 0)
				{
					this._Scalefac[ch].L[6] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[7] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[8] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[9] = this._BitReserve.ReadBits(num);
					this._Scalefac[ch].L[10] = this._BitReserve.ReadBits(num);
				}
				if (this._SideInfo.Channels[ch].ScaleFactorBits[2] == 0 || gr == 0)
				{
					this._Scalefac[ch].L[11] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[12] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[13] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[14] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[15] = this._BitReserve.ReadBits(num2);
				}
				if (this._SideInfo.Channels[ch].ScaleFactorBits[3] == 0 || gr == 0)
				{
					this._Scalefac[ch].L[16] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[17] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[18] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[19] = this._BitReserve.ReadBits(num2);
					this._Scalefac[ch].L[20] = this._BitReserve.ReadBits(num2);
				}
				this._Scalefac[ch].L[21] = 0;
				this._Scalefac[ch].L[22] = 0;
				return;
			}
			if (granuleInfo.MixedBlockFlag != 0)
			{
				int i;
				for (i = 0; i < 8; i++)
				{
					this._Scalefac[ch].L[i] = this._BitReserve.ReadBits(LayerIIIDecoder.Slen[0][granuleInfo.ScaleFacCompress]);
				}
				for (i = 3; i < 6; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						this._Scalefac[ch].S[j][i] = this._BitReserve.ReadBits(LayerIIIDecoder.Slen[0][granuleInfo.ScaleFacCompress]);
					}
				}
				for (i = 6; i < 12; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						this._Scalefac[ch].S[j][i] = this._BitReserve.ReadBits(LayerIIIDecoder.Slen[1][granuleInfo.ScaleFacCompress]);
					}
				}
				i = 12;
				for (int j = 0; j < 3; j++)
				{
					this._Scalefac[ch].S[j][i] = 0;
				}
				return;
			}
			this._Scalefac[ch].S[0][0] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][0] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][0] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][1] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][1] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][1] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][2] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][2] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][2] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][3] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][3] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][3] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][4] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][4] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][4] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][5] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[1][5] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[2][5] = this._BitReserve.ReadBits(num);
			this._Scalefac[ch].S[0][6] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][6] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][6] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][7] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][7] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][7] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][8] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][8] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][8] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][9] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][9] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][9] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][10] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][10] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][10] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][11] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[1][11] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[2][11] = this._BitReserve.ReadBits(num2);
			this._Scalefac[ch].S[0][12] = 0;
			this._Scalefac[ch].S[1][12] = 0;
			this._Scalefac[ch].S[2][12] = 0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000176CC File Offset: 0x000158CC
		private void GetLSFScaleData(int ch, int gr)
		{
			int num = this._Header.mode_extension();
			int num2 = 0;
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			int scaleFacCompress = granuleInfo.ScaleFacCompress;
			int num3;
			if (granuleInfo.BlockType == 2)
			{
				if (granuleInfo.MixedBlockFlag == 0)
				{
					num3 = 1;
				}
				else if (granuleInfo.MixedBlockFlag == 1)
				{
					num3 = 2;
				}
				else
				{
					num3 = 0;
				}
			}
			else
			{
				num3 = 0;
			}
			if ((num != 1 && num != 3) || ch != 1)
			{
				if (scaleFacCompress < 400)
				{
					this._NewSlen[0] = SupportClass.URShift(scaleFacCompress, 4) / 5;
					this._NewSlen[1] = SupportClass.URShift(scaleFacCompress, 4) % 5;
					this._NewSlen[2] = SupportClass.URShift(scaleFacCompress & 15, 2);
					this._NewSlen[3] = scaleFacCompress & 3;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 0;
				}
				else if (scaleFacCompress < 500)
				{
					this._NewSlen[0] = SupportClass.URShift(scaleFacCompress - 400, 2) / 5;
					this._NewSlen[1] = SupportClass.URShift(scaleFacCompress - 400, 2) % 5;
					this._NewSlen[2] = (scaleFacCompress - 400) & 3;
					this._NewSlen[3] = 0;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 1;
				}
				else if (scaleFacCompress < 512)
				{
					this._NewSlen[0] = (scaleFacCompress - 500) / 3;
					this._NewSlen[1] = (scaleFacCompress - 500) % 3;
					this._NewSlen[2] = 0;
					this._NewSlen[3] = 0;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 1;
					num2 = 2;
				}
			}
			if ((num == 1 || num == 3) && ch == 1)
			{
				int num4 = SupportClass.URShift(scaleFacCompress, 1);
				if (num4 < 180)
				{
					this._NewSlen[0] = num4 / 36;
					this._NewSlen[1] = num4 % 36 / 6;
					this._NewSlen[2] = num4 % 36 % 6;
					this._NewSlen[3] = 0;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 3;
				}
				else if (num4 < 244)
				{
					this._NewSlen[0] = SupportClass.URShift((num4 - 180) & 63, 4);
					this._NewSlen[1] = SupportClass.URShift((num4 - 180) & 15, 2);
					this._NewSlen[2] = (num4 - 180) & 3;
					this._NewSlen[3] = 0;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 4;
				}
				else if (num4 < 255)
				{
					this._NewSlen[0] = (num4 - 244) / 3;
					this._NewSlen[1] = (num4 - 244) % 3;
					this._NewSlen[2] = 0;
					this._NewSlen[3] = 0;
					this._SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 5;
				}
			}
			for (int i = 0; i < 45; i++)
			{
				this.ScalefacBuffer[i] = 0;
			}
			int num5 = 0;
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < LayerIIIDecoder.NrOfSfbBlock[num2][num3][j]; k++)
				{
					this.ScalefacBuffer[num5] = ((this._NewSlen[j] == 0) ? 0 : this._BitReserve.ReadBits(this._NewSlen[j]));
					num5++;
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00017A2C File Offset: 0x00015C2C
		private void GLSFScaleFactors(int ch, int gr)
		{
			int num = 0;
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			this.GetLSFScaleData(ch, gr);
			if (granuleInfo.WindowSwitchingFlag == 0 || granuleInfo.BlockType != 2)
			{
				for (int i = 0; i < 21; i++)
				{
					this._Scalefac[ch].L[i] = this.ScalefacBuffer[num];
					num++;
				}
				this._Scalefac[ch].L[21] = 0;
				this._Scalefac[ch].L[22] = 0;
				return;
			}
			if (granuleInfo.MixedBlockFlag != 0)
			{
				for (int i = 0; i < 8; i++)
				{
					this._Scalefac[ch].L[i] = this.ScalefacBuffer[num];
					num++;
				}
				for (int i = 3; i < 12; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						this._Scalefac[ch].S[j][i] = this.ScalefacBuffer[num];
						num++;
					}
				}
				for (int j = 0; j < 3; j++)
				{
					this._Scalefac[ch].S[j][12] = 0;
				}
				return;
			}
			for (int i = 0; i < 12; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					this._Scalefac[ch].S[j][i] = this.ScalefacBuffer[num];
					num++;
				}
			}
			for (int j = 0; j < 3; j++)
			{
				this._Scalefac[ch].S[j][12] = 0;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00017B94 File Offset: 0x00015D94
		private void HuffmanDecode(int ch, int gr)
		{
			this.X[0] = 0;
			this.Y[0] = 0;
			this.V[0] = 0;
			this.W[0] = 0;
			int num = this._Part2Start + this._SideInfo.Channels[ch].Granules[gr].Part23Length;
			int num2;
			int num3;
			if (this._SideInfo.Channels[ch].Granules[gr].WindowSwitchingFlag != 0 && this._SideInfo.Channels[ch].Granules[gr].BlockType == 2)
			{
				num2 = ((this._Sfreq == 8) ? 72 : 36);
				num3 = 576;
			}
			else
			{
				int num4 = this._SideInfo.Channels[ch].Granules[gr].Region0Count + 1;
				int num5 = num4 + this._SideInfo.Channels[ch].Granules[gr].Region1Count + 1;
				if (num5 > this._SfBandIndex[this._Sfreq].L.Length - 1)
				{
					num5 = this._SfBandIndex[this._Sfreq].L.Length - 1;
				}
				num2 = this._SfBandIndex[this._Sfreq].L[num4];
				num3 = this._SfBandIndex[this._Sfreq].L[num5];
			}
			int i = 0;
			Huffman huffman;
			for (int j = 0; j < this._SideInfo.Channels[ch].Granules[gr].BigValues << 1; j += 2)
			{
				if (j < num2)
				{
					huffman = Huffman.HuffmanTable[this._SideInfo.Channels[ch].Granules[gr].TableSelect[0]];
				}
				else if (j < num3)
				{
					huffman = Huffman.HuffmanTable[this._SideInfo.Channels[ch].Granules[gr].TableSelect[1]];
				}
				else
				{
					huffman = Huffman.HuffmanTable[this._SideInfo.Channels[ch].Granules[gr].TableSelect[2]];
				}
				Huffman.Decode(huffman, this.X, this.Y, this.V, this.W, this._BitReserve);
				this._Is1D[i++] = this.X[0];
				this._Is1D[i++] = this.Y[0];
				this._CheckSumHuff = this._CheckSumHuff + this.X[0] + this.Y[0];
			}
			huffman = Huffman.HuffmanTable[this._SideInfo.Channels[ch].Granules[gr].Count1TableSelect + 32];
			int num6 = this._BitReserve.HssTell();
			while (num6 < num && i < 576)
			{
				Huffman.Decode(huffman, this.X, this.Y, this.V, this.W, this._BitReserve);
				this._Is1D[i++] = this.V[0];
				this._Is1D[i++] = this.W[0];
				this._Is1D[i++] = this.X[0];
				this._Is1D[i++] = this.Y[0];
				this._CheckSumHuff = this._CheckSumHuff + this.V[0] + this.W[0] + this.X[0] + this.Y[0];
				num6 = this._BitReserve.HssTell();
			}
			if (num6 > num)
			{
				this._BitReserve.RewindStreamBits(num6 - num);
				i -= 4;
			}
			num6 = this._BitReserve.HssTell();
			if (num6 < num)
			{
				this._BitReserve.ReadBits(num - num6);
			}
			if (i < 576)
			{
				this._Nonzero[ch] = i;
			}
			else
			{
				this._Nonzero[ch] = 576;
			}
			if (i < 0)
			{
				i = 0;
			}
			while (i < 576)
			{
				this._Is1D[i] = 0;
				i++;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00017F68 File Offset: 0x00016168
		private void GetKStereoValues(int isPos, int ioType, int i)
		{
			if (isPos == 0)
			{
				this._K[0][i] = 1f;
				this._K[1][i] = 1f;
				return;
			}
			if ((isPos & 1) != 0)
			{
				this._K[0][i] = LayerIIIDecoder.Io[ioType][SupportClass.URShift(isPos + 1, 1)];
				this._K[1][i] = 1f;
				return;
			}
			this._K[0][i] = 1f;
			this._K[1][i] = LayerIIIDecoder.Io[ioType][SupportClass.URShift(isPos, 1)];
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00017FF0 File Offset: 0x000161F0
		private void DequantizeSample(float[][] xr, int ch, int gr)
		{
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5;
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
			{
				if (granuleInfo.MixedBlockFlag != 0)
				{
					num5 = this._SfBandIndex[this._Sfreq].L[1];
				}
				else
				{
					num3 = this._SfBandIndex[this._Sfreq].S[1];
					num5 = (num3 << 2) - num3;
					num2 = 0;
				}
			}
			else
			{
				num5 = this._SfBandIndex[this._Sfreq].L[1];
			}
			float num6 = (float)Math.Pow(2.0, 0.25 * ((double)granuleInfo.GlobalGain - 210.0));
			for (int i = 0; i < this._Nonzero[ch]; i++)
			{
				int num7 = i % 18;
				int num8 = (i - num7) / 18;
				if (this._Is1D[i] == 0)
				{
					xr[num8][num7] = 0f;
				}
				else
				{
					int num9 = this._Is1D[i];
					if (num9 < LayerIIIDecoder.PowerTable.Length)
					{
						if (this._Is1D[i] > 0)
						{
							xr[num8][num7] = num6 * LayerIIIDecoder.PowerTable[num9];
						}
						else if (-num9 < LayerIIIDecoder.PowerTable.Length)
						{
							xr[num8][num7] = -num6 * LayerIIIDecoder.PowerTable[-num9];
						}
						else
						{
							xr[num8][num7] = -num6 * (float)Math.Pow((double)(-(double)num9), 1.3333333333333333);
						}
					}
					else if (this._Is1D[i] > 0)
					{
						xr[num8][num7] = num6 * (float)Math.Pow((double)num9, 1.3333333333333333);
					}
					else
					{
						xr[num8][num7] = -num6 * (float)Math.Pow((double)(-(double)num9), 1.3333333333333333);
					}
				}
			}
			for (int i = 0; i < this._Nonzero[ch]; i++)
			{
				int num10 = i % 18;
				int num11 = (i - num10) / 18;
				if (num4 == num5)
				{
					if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
					{
						if (granuleInfo.MixedBlockFlag != 0)
						{
							if (num4 == this._SfBandIndex[this._Sfreq].L[8])
							{
								num5 = this._SfBandIndex[this._Sfreq].S[4];
								num5 = (num5 << 2) - num5;
								num = 3;
								num3 = this._SfBandIndex[this._Sfreq].S[4] - this._SfBandIndex[this._Sfreq].S[3];
								num2 = this._SfBandIndex[this._Sfreq].S[3];
								num2 = (num2 << 2) - num2;
							}
							else if (num4 < this._SfBandIndex[this._Sfreq].L[8])
							{
								num5 = this._SfBandIndex[this._Sfreq].L[++num + 1];
							}
							else
							{
								num5 = this._SfBandIndex[this._Sfreq].S[++num + 1];
								num5 = (num5 << 2) - num5;
								num2 = this._SfBandIndex[this._Sfreq].S[num];
								num3 = this._SfBandIndex[this._Sfreq].S[num + 1] - num2;
								num2 = (num2 << 2) - num2;
							}
						}
						else
						{
							num5 = this._SfBandIndex[this._Sfreq].S[++num + 1];
							num5 = (num5 << 2) - num5;
							num2 = this._SfBandIndex[this._Sfreq].S[num];
							num3 = this._SfBandIndex[this._Sfreq].S[num + 1] - num2;
							num2 = (num2 << 2) - num2;
						}
					}
					else
					{
						num5 = this._SfBandIndex[this._Sfreq].L[++num + 1];
					}
				}
				if (granuleInfo.WindowSwitchingFlag != 0 && ((granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag == 0) || (granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag != 0 && i >= 36)))
				{
					int num12 = (num4 - num2) / num3;
					int num13 = this._Scalefac[ch].S[num12][num] << granuleInfo.ScaleFacScale;
					num13 += granuleInfo.SubblockGain[num12] << 2;
					xr[num11][num10] *= LayerIIIDecoder.TwoToNegativeHalfPow[num13];
				}
				else
				{
					int num14 = this._Scalefac[ch].L[num];
					if (granuleInfo.Preflag != 0)
					{
						num14 += LayerIIIDecoder.Pretab[num];
					}
					num14 <<= granuleInfo.ScaleFacScale;
					xr[num11][num10] *= LayerIIIDecoder.TwoToNegativeHalfPow[num14];
				}
				num4++;
			}
			for (int i = this._Nonzero[ch]; i < 576; i++)
			{
				int num15 = i % 18;
				int num16 = (i - num15) / 18;
				if (num15 < 0)
				{
					num15 = 0;
				}
				if (num16 < 0)
				{
					num16 = 0;
				}
				xr[num16][num15] = 0f;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000184BC File Offset: 0x000166BC
		private void Reorder(float[][] xr, int ch, int gr)
		{
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			if (granuleInfo.WindowSwitchingFlag == 0 || granuleInfo.BlockType != 2)
			{
				for (int i = 0; i < 576; i++)
				{
					int num = i % 18;
					int num2 = (i - num) / 18;
					this._Out1D[i] = xr[num2][num];
				}
				return;
			}
			for (int i = 0; i < 576; i++)
			{
				this._Out1D[i] = 0f;
			}
			if (granuleInfo.MixedBlockFlag != 0)
			{
				for (int i = 0; i < 36; i++)
				{
					int num3 = i % 18;
					int num4 = (i - num3) / 18;
					this._Out1D[i] = xr[num4][num3];
				}
				int j = 3;
				int num5 = this._SfBandIndex[this._Sfreq].S[3];
				int num6 = this._SfBandIndex[this._Sfreq].S[4] - num5;
				while (j < 13)
				{
					int num7 = (num5 << 2) - num5;
					int k = 0;
					int num8 = 0;
					while (k < num6)
					{
						int num9 = num7 + k;
						int num10 = num7 + num8;
						int num11 = num9 % 18;
						int num12 = (num9 - num11) / 18;
						this._Out1D[num10] = xr[num12][num11];
						num9 += num6;
						num10++;
						num11 = num9 % 18;
						num12 = (num9 - num11) / 18;
						this._Out1D[num10] = xr[num12][num11];
						num9 += num6;
						num10++;
						num11 = num9 % 18;
						num12 = (num9 - num11) / 18;
						this._Out1D[num10] = xr[num12][num11];
						k++;
						num8 += 3;
					}
					j++;
					num5 = this._SfBandIndex[this._Sfreq].S[j];
					num6 = this._SfBandIndex[this._Sfreq].S[j + 1] - num5;
				}
				return;
			}
			for (int i = 0; i < 576; i++)
			{
				int num13 = LayerIIIDecoder._reorderTable[this._Sfreq][i];
				int num14 = num13 % 18;
				int num15 = (num13 - num14) / 18;
				this._Out1D[i] = xr[num15][num14];
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000186E0 File Offset: 0x000168E0
		private void Stereo(int gr)
		{
			if (this._Channels == 1)
			{
				for (int i = 0; i < 32; i++)
				{
					for (int j = 0; j < 18; j += 3)
					{
						this._Lr[0][i][j] = this._Ro[0][i][j];
						this._Lr[0][i][j + 1] = this._Ro[0][i][j + 1];
						this._Lr[0][i][j + 2] = this._Ro[0][i][j + 2];
					}
				}
				return;
			}
			GranuleInfo granuleInfo = this._SideInfo.Channels[0].Granules[gr];
			int num = this._Header.mode_extension();
			bool flag = this._Header.Mode() == 1 && (num & 2) != 0;
			bool flag2 = this._Header.Mode() == 1 && (num & 1) != 0;
			bool flag3 = this._Header.Version() == 0 || this._Header.Version() == 2;
			int num2 = granuleInfo.ScaleFacCompress & 1;
			int k;
			for (k = 0; k < 576; k++)
			{
				this.IsPos[k] = 7;
				this.IsRatio[k] = 0f;
			}
			if (flag2)
			{
				if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
				{
					if (granuleInfo.MixedBlockFlag != 0)
					{
						int num3 = 0;
						for (int l = 0; l < 3; l++)
						{
							int num4 = 2;
							int m;
							for (m = 12; m >= 3; m--)
							{
								k = this._SfBandIndex[this._Sfreq].S[m];
								int n = this._SfBandIndex[this._Sfreq].S[m + 1] - k;
								k = (k << 2) - k + (l + 1) * n - 1;
								while (n > 0)
								{
									if (this._Ro[1][k / 18][k % 18] != 0f)
									{
										num4 = m;
										m = -10;
										n = -10;
									}
									n--;
									k--;
								}
							}
							m = num4 + 1;
							if (m > num3)
							{
								num3 = m;
							}
							int i;
							int num5;
							while (m < 12)
							{
								num5 = this._SfBandIndex[this._Sfreq].S[m];
								i = this._SfBandIndex[this._Sfreq].S[m + 1] - num5;
								k = (num5 << 2) - num5 + l * i;
								while (i > 0)
								{
									this.IsPos[k] = this._Scalefac[1].S[l][m];
									if (this.IsPos[k] != 7)
									{
										if (flag3)
										{
											this.GetKStereoValues(this.IsPos[k], num2, k);
										}
										else
										{
											this.IsRatio[k] = LayerIIIDecoder.Tan12[this.IsPos[k]];
										}
									}
									k++;
									i--;
								}
								m++;
							}
							m = this._SfBandIndex[this._Sfreq].S[10];
							i = this._SfBandIndex[this._Sfreq].S[11] - m;
							m = (m << 2) - m + l * i;
							num5 = this._SfBandIndex[this._Sfreq].S[11];
							i = this._SfBandIndex[this._Sfreq].S[12] - num5;
							k = (num5 << 2) - num5 + l * i;
							while (i > 0)
							{
								this.IsPos[k] = this.IsPos[m];
								if (flag3)
								{
									this._K[0][k] = this._K[0][m];
									this._K[1][k] = this._K[1][m];
								}
								else
								{
									this.IsRatio[k] = this.IsRatio[m];
								}
								k++;
								i--;
							}
						}
						if (num3 <= 3)
						{
							k = 2;
							int j = 17;
							int i = -1;
							while (k >= 0)
							{
								if (this._Ro[1][k][j] != 0f)
								{
									i = (k << 4) + (k << 1) + j;
									k = -1;
								}
								else
								{
									j--;
									if (j < 0)
									{
										k--;
										j = 17;
									}
								}
							}
							k = 0;
							while (this._SfBandIndex[this._Sfreq].L[k] <= i)
							{
								k++;
							}
							int m = k;
							k = this._SfBandIndex[this._Sfreq].L[k];
							while (m < 8)
							{
								for (i = this._SfBandIndex[this._Sfreq].L[m + 1] - this._SfBandIndex[this._Sfreq].L[m]; i > 0; i--)
								{
									this.IsPos[k] = this._Scalefac[1].L[m];
									if (this.IsPos[k] != 7)
									{
										if (flag3)
										{
											this.GetKStereoValues(this.IsPos[k], num2, k);
										}
										else
										{
											this.IsRatio[k] = LayerIIIDecoder.Tan12[this.IsPos[k]];
										}
									}
									k++;
								}
								m++;
							}
						}
					}
					else
					{
						for (int num6 = 0; num6 < 3; num6++)
						{
							int num7 = -1;
							int m;
							int num5;
							for (m = 12; m >= 0; m--)
							{
								num5 = this._SfBandIndex[this._Sfreq].S[m];
								int n = this._SfBandIndex[this._Sfreq].S[m + 1] - num5;
								k = (num5 << 2) - num5 + (num6 + 1) * n - 1;
								while (n > 0)
								{
									if (this._Ro[1][k / 18][k % 18] != 0f)
									{
										num7 = m;
										m = -10;
										n = -10;
									}
									n--;
									k--;
								}
							}
							int i;
							for (m = num7 + 1; m < 12; m++)
							{
								num5 = this._SfBandIndex[this._Sfreq].S[m];
								i = this._SfBandIndex[this._Sfreq].S[m + 1] - num5;
								k = (num5 << 2) - num5 + num6 * i;
								while (i > 0)
								{
									this.IsPos[k] = this._Scalefac[1].S[num6][m];
									if (this.IsPos[k] != 7)
									{
										if (flag3)
										{
											this.GetKStereoValues(this.IsPos[k], num2, k);
										}
										else
										{
											this.IsRatio[k] = LayerIIIDecoder.Tan12[this.IsPos[k]];
										}
									}
									k++;
									i--;
								}
							}
							num5 = this._SfBandIndex[this._Sfreq].S[10];
							int num8 = this._SfBandIndex[this._Sfreq].S[11];
							i = num8 - num5;
							m = (num5 << 2) - num5 + num6 * i;
							i = this._SfBandIndex[this._Sfreq].S[12] - num8;
							k = (num8 << 2) - num8 + num6 * i;
							while (i > 0)
							{
								this.IsPos[k] = this.IsPos[m];
								if (flag3)
								{
									this._K[0][k] = this._K[0][m];
									this._K[1][k] = this._K[1][m];
								}
								else
								{
									this.IsRatio[k] = this.IsRatio[m];
								}
								k++;
								i--;
							}
						}
					}
				}
				else
				{
					k = 31;
					int j = 17;
					int i = 0;
					while (k >= 0)
					{
						if (this._Ro[1][k][j] != 0f)
						{
							i = (k << 4) + (k << 1) + j;
							k = -1;
						}
						else
						{
							j--;
							if (j < 0)
							{
								k--;
								j = 17;
							}
						}
					}
					k = 0;
					while (this._SfBandIndex[this._Sfreq].L[k] <= i)
					{
						k++;
					}
					int m = k;
					k = this._SfBandIndex[this._Sfreq].L[k];
					while (m < 21)
					{
						for (i = this._SfBandIndex[this._Sfreq].L[m + 1] - this._SfBandIndex[this._Sfreq].L[m]; i > 0; i--)
						{
							this.IsPos[k] = this._Scalefac[1].L[m];
							if (this.IsPos[k] != 7)
							{
								if (flag3)
								{
									this.GetKStereoValues(this.IsPos[k], num2, k);
								}
								else
								{
									this.IsRatio[k] = LayerIIIDecoder.Tan12[this.IsPos[k]];
								}
							}
							k++;
						}
						m++;
					}
					m = this._SfBandIndex[this._Sfreq].L[20];
					i = 576 - this._SfBandIndex[this._Sfreq].L[21];
					while (i > 0 && k < 576)
					{
						this.IsPos[k] = this.IsPos[m];
						if (flag3)
						{
							this._K[0][k] = this._K[0][m];
							this._K[1][k] = this._K[1][m];
						}
						else
						{
							this.IsRatio[k] = this.IsRatio[m];
						}
						k++;
						i--;
					}
				}
			}
			k = 0;
			for (int i = 0; i < 32; i++)
			{
				for (int j = 0; j < 18; j++)
				{
					if (this.IsPos[k] == 7)
					{
						if (flag)
						{
							this._Lr[0][i][j] = (this._Ro[0][i][j] + this._Ro[1][i][j]) * 0.70710677f;
							this._Lr[1][i][j] = (this._Ro[0][i][j] - this._Ro[1][i][j]) * 0.70710677f;
						}
						else
						{
							this._Lr[0][i][j] = this._Ro[0][i][j];
							this._Lr[1][i][j] = this._Ro[1][i][j];
						}
					}
					else if (flag2)
					{
						if (flag3)
						{
							this._Lr[0][i][j] = this._Ro[0][i][j] * this._K[0][k];
							this._Lr[1][i][j] = this._Ro[0][i][j] * this._K[1][k];
						}
						else
						{
							this._Lr[1][i][j] = this._Ro[0][i][j] / (1f + this.IsRatio[k]);
							this._Lr[0][i][j] = this._Lr[1][i][j] * this.IsRatio[k];
						}
					}
					k++;
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00019158 File Offset: 0x00017358
		private void Antialias(int ch, int gr)
		{
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag == 0)
			{
				return;
			}
			int num;
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.MixedBlockFlag != 0 && granuleInfo.BlockType == 2)
			{
				num = 18;
			}
			else
			{
				num = 558;
			}
			for (int i = 0; i < num; i += 18)
			{
				for (int j = 0; j < 8; j++)
				{
					int num2 = i + 17 - j;
					int num3 = i + 18 + j;
					float num4 = this._Out1D[num2];
					float num5 = this._Out1D[num3];
					this._Out1D[num2] = num4 * LayerIIIDecoder.Cs[j] - num5 * LayerIIIDecoder.Ca[j];
					this._Out1D[num3] = num5 * LayerIIIDecoder.Cs[j] + num4 * LayerIIIDecoder.Ca[j];
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00019234 File Offset: 0x00017434
		private void Hybrid(int ch, int gr)
		{
			GranuleInfo granuleInfo = this._SideInfo.Channels[ch].Granules[gr];
			for (int i = 0; i < 576; i += 18)
			{
				int num = ((granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.MixedBlockFlag != 0 && i < 36) ? 0 : granuleInfo.BlockType);
				float[] out1D = this._Out1D;
				for (int j = 0; j < 18; j++)
				{
					this.TsOutCopy[j] = out1D[j + i];
				}
				this.InverseMdct(this.TsOutCopy, this.Rawout, num);
				for (int k = 0; k < 18; k++)
				{
					out1D[k + i] = this.TsOutCopy[k];
				}
				float[][] prevblck = this._Prevblck;
				out1D[i] = this.Rawout[0] + prevblck[ch][i];
				prevblck[ch][i] = this.Rawout[18];
				out1D[1 + i] = this.Rawout[1] + prevblck[ch][i + 1];
				prevblck[ch][i + 1] = this.Rawout[19];
				out1D[2 + i] = this.Rawout[2] + prevblck[ch][i + 2];
				prevblck[ch][i + 2] = this.Rawout[20];
				out1D[3 + i] = this.Rawout[3] + prevblck[ch][i + 3];
				prevblck[ch][i + 3] = this.Rawout[21];
				out1D[4 + i] = this.Rawout[4] + prevblck[ch][i + 4];
				prevblck[ch][i + 4] = this.Rawout[22];
				out1D[5 + i] = this.Rawout[5] + prevblck[ch][i + 5];
				prevblck[ch][i + 5] = this.Rawout[23];
				out1D[6 + i] = this.Rawout[6] + prevblck[ch][i + 6];
				prevblck[ch][i + 6] = this.Rawout[24];
				out1D[7 + i] = this.Rawout[7] + prevblck[ch][i + 7];
				prevblck[ch][i + 7] = this.Rawout[25];
				out1D[8 + i] = this.Rawout[8] + prevblck[ch][i + 8];
				prevblck[ch][i + 8] = this.Rawout[26];
				out1D[9 + i] = this.Rawout[9] + prevblck[ch][i + 9];
				prevblck[ch][i + 9] = this.Rawout[27];
				out1D[10 + i] = this.Rawout[10] + prevblck[ch][i + 10];
				prevblck[ch][i + 10] = this.Rawout[28];
				out1D[11 + i] = this.Rawout[11] + prevblck[ch][i + 11];
				prevblck[ch][i + 11] = this.Rawout[29];
				out1D[12 + i] = this.Rawout[12] + prevblck[ch][i + 12];
				prevblck[ch][i + 12] = this.Rawout[30];
				out1D[13 + i] = this.Rawout[13] + prevblck[ch][i + 13];
				prevblck[ch][i + 13] = this.Rawout[31];
				out1D[14 + i] = this.Rawout[14] + prevblck[ch][i + 14];
				prevblck[ch][i + 14] = this.Rawout[32];
				out1D[15 + i] = this.Rawout[15] + prevblck[ch][i + 15];
				prevblck[ch][i + 15] = this.Rawout[33];
				out1D[16 + i] = this.Rawout[16] + prevblck[ch][i + 16];
				prevblck[ch][i + 16] = this.Rawout[34];
				out1D[17 + i] = this.Rawout[17] + prevblck[ch][i + 17];
				prevblck[ch][i + 17] = this.Rawout[35];
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000195CC File Offset: 0x000177CC
		private void DoDownMix()
		{
			for (int i = 0; i < 18; i++)
			{
				for (int j = 0; j < 18; j += 3)
				{
					this._Lr[0][i][j] = (this._Lr[0][i][j] + this._Lr[1][i][j]) * 0.5f;
					this._Lr[0][i][j + 1] = (this._Lr[0][i][j + 1] + this._Lr[1][i][j + 1]) * 0.5f;
					this._Lr[0][i][j + 2] = (this._Lr[0][i][j + 2] + this._Lr[1][i][j + 2]) * 0.5f;
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001968C File Offset: 0x0001788C
		internal void InverseMdct(float[] inValues, float[] outValues, int blockType)
		{
			float num5;
			float num6;
			float num7;
			float num9;
			float num10;
			float num11;
			float num15;
			float num16;
			float num17;
			float num18;
			float num19;
			float num20;
			if (blockType == 2)
			{
				outValues[0] = 0f;
				outValues[1] = 0f;
				outValues[2] = 0f;
				outValues[3] = 0f;
				outValues[4] = 0f;
				outValues[5] = 0f;
				outValues[6] = 0f;
				outValues[7] = 0f;
				outValues[8] = 0f;
				outValues[9] = 0f;
				outValues[10] = 0f;
				outValues[11] = 0f;
				outValues[12] = 0f;
				outValues[13] = 0f;
				outValues[14] = 0f;
				outValues[15] = 0f;
				outValues[16] = 0f;
				outValues[17] = 0f;
				outValues[18] = 0f;
				outValues[19] = 0f;
				outValues[20] = 0f;
				outValues[21] = 0f;
				outValues[22] = 0f;
				outValues[23] = 0f;
				outValues[24] = 0f;
				outValues[25] = 0f;
				outValues[26] = 0f;
				outValues[27] = 0f;
				outValues[28] = 0f;
				outValues[29] = 0f;
				outValues[30] = 0f;
				outValues[31] = 0f;
				outValues[32] = 0f;
				outValues[33] = 0f;
				outValues[34] = 0f;
				outValues[35] = 0f;
				int num = 0;
				for (int i = 0; i < 3; i++)
				{
					inValues[15 + i] += inValues[12 + i];
					inValues[12 + i] += inValues[9 + i];
					inValues[9 + i] += inValues[6 + i];
					inValues[6 + i] += inValues[3 + i];
					inValues[3 + i] += inValues[i];
					inValues[15 + i] += inValues[9 + i];
					inValues[9 + i] += inValues[3 + i];
					float num2 = inValues[12 + i] * 0.5f;
					float num3 = inValues[6 + i] * 0.8660254f;
					float num4 = inValues[i] + num2;
					num5 = inValues[i] - inValues[12 + i];
					num6 = num4 + num3;
					num7 = num4 - num3;
					num2 = inValues[15 + i] * 0.5f;
					num3 = inValues[9 + i] * 0.8660254f;
					float num8 = inValues[3 + i] + num2;
					num9 = inValues[3 + i] - inValues[15 + i];
					num10 = num8 + num3;
					num11 = num8 - num3;
					num11 *= 1.9318516f;
					num9 *= 0.70710677f;
					num10 *= 0.5176381f;
					float num12 = num6;
					num6 += num10;
					num10 = num12 - num10;
					float num13 = num5;
					num5 += num9;
					num9 = num13 - num9;
					float num14 = num7;
					num7 += num11;
					num11 = num14 - num11;
					num6 *= 0.5043145f;
					num5 *= 0.5411961f;
					num7 *= 0.6302362f;
					num11 *= 0.8213398f;
					num9 *= 1.306563f;
					num10 *= 3.830649f;
					num15 = -num6 * 0.7933533f;
					num16 = -num6 * 0.6087614f;
					num17 = -num5 * 0.9238795f;
					num18 = -num5 * 0.38268343f;
					num19 = -num7 * 0.9914449f;
					num20 = -num7 * 0.13052619f;
					num6 = num11;
					num5 = num9 * 0.38268343f;
					num7 = num10 * 0.6087614f;
					num11 = -num10 * 0.7933533f;
					num9 = -num9 * 0.9238795f;
					num10 = -num6 * 0.9914449f;
					num6 *= 0.13052619f;
					outValues[num + 6] += num6;
					outValues[num + 7] += num5;
					outValues[num + 8] += num7;
					outValues[num + 9] += num11;
					outValues[num + 10] += num9;
					outValues[num + 11] += num10;
					outValues[num + 12] += num19;
					outValues[num + 13] += num17;
					outValues[num + 14] += num15;
					outValues[num + 15] += num16;
					outValues[num + 16] += num18;
					outValues[num + 17] += num20;
					num += 6;
				}
				return;
			}
			inValues[17] += inValues[16];
			inValues[16] += inValues[15];
			inValues[15] += inValues[14];
			inValues[14] += inValues[13];
			inValues[13] += inValues[12];
			inValues[12] += inValues[11];
			inValues[11] += inValues[10];
			inValues[10] += inValues[9];
			inValues[9] += inValues[8];
			inValues[8] += inValues[7];
			inValues[7] += inValues[6];
			inValues[6] += inValues[5];
			inValues[5] += inValues[4];
			inValues[4] += inValues[3];
			inValues[3] += inValues[2];
			inValues[2] += inValues[1];
			inValues[1] += inValues[0];
			inValues[17] += inValues[15];
			inValues[15] += inValues[13];
			inValues[13] += inValues[11];
			inValues[11] += inValues[9];
			inValues[9] += inValues[7];
			inValues[7] += inValues[5];
			inValues[5] += inValues[3];
			inValues[3] += inValues[1];
			float num21 = inValues[0] + inValues[0];
			float num22 = num21 + inValues[12];
			float num23 = num22 + inValues[4] * 1.8793852f + inValues[8] * 1.5320889f + inValues[16] * 0.34729636f;
			float num24 = num21 + inValues[4] - inValues[8] - inValues[12] - inValues[12] - inValues[16];
			float num25 = num22 - inValues[4] * 0.34729636f - inValues[8] * 1.8793852f + inValues[16] * 1.5320889f;
			float num26 = num22 - inValues[4] * 1.5320889f + inValues[8] * 0.34729636f - inValues[16] * 1.8793852f;
			float num27 = inValues[0] - inValues[4] + inValues[8] - inValues[12] + inValues[16];
			float num28 = inValues[6] * 1.7320508f;
			float num29 = inValues[2] * 1.9696155f + num28 + inValues[10] * 1.2855753f + inValues[14] * 0.6840403f;
			float num30 = (inValues[2] - inValues[10] - inValues[14]) * 1.7320508f;
			float num31 = inValues[2] * 1.2855753f - num28 - inValues[10] * 0.6840403f + inValues[14] * 1.9696155f;
			float num32 = inValues[2] * 0.6840403f - num28 + inValues[10] * 1.9696155f - inValues[14] * 1.2855753f;
			float num33 = inValues[1] + inValues[1];
			float num34 = num33 + inValues[13];
			float num35 = num34 + inValues[5] * 1.8793852f + inValues[9] * 1.5320889f + inValues[17] * 0.34729636f;
			float num36 = num33 + inValues[5] - inValues[9] - inValues[13] - inValues[13] - inValues[17];
			float num37 = num34 - inValues[5] * 0.34729636f - inValues[9] * 1.8793852f + inValues[17] * 1.5320889f;
			float num38 = num34 - inValues[5] * 1.5320889f + inValues[9] * 0.34729636f - inValues[17] * 1.8793852f;
			float num39 = (inValues[1] - inValues[5] + inValues[9] - inValues[13] + inValues[17]) * 0.70710677f;
			float num40 = inValues[7] * 1.7320508f;
			float num41 = inValues[3] * 1.9696155f + num40 + inValues[11] * 1.2855753f + inValues[15] * 0.6840403f;
			float num42 = (inValues[3] - inValues[11] - inValues[15]) * 1.7320508f;
			float num43 = inValues[3] * 1.2855753f - num40 - inValues[11] * 0.6840403f + inValues[15] * 1.9696155f;
			float num44 = inValues[3] * 0.6840403f - num40 + inValues[11] * 1.9696155f - inValues[15] * 1.2855753f;
			float num45 = num23 + num29;
			float num46 = (num35 + num41) * 0.5019099f;
			num6 = num45 + num46;
			float num47 = num45 - num46;
			float num48 = num24 + num30;
			num46 = (num36 + num42) * 0.5176381f;
			num5 = num48 + num46;
			float num49 = num48 - num46;
			float num50 = num25 + num31;
			num46 = (num37 + num43) * 0.55168897f;
			num7 = num50 + num46;
			float num51 = num50 - num46;
			float num52 = num26 + num32;
			num46 = (num38 + num44) * 0.61038727f;
			num11 = num52 + num46;
			float num53 = num52 - num46;
			num9 = num27 + num39;
			float num54 = num27 - num39;
			float num55 = num26 - num32;
			num46 = (num38 - num44) * 0.8717234f;
			num10 = num55 + num46;
			float num56 = num55 - num46;
			float num57 = num25 - num31;
			num46 = (num37 - num43) * 1.1831008f;
			num19 = num57 + num46;
			num20 = num57 - num46;
			float num58 = num24 - num30;
			num46 = (num36 - num42) * 1.9318516f;
			num17 = num58 + num46;
			num18 = num58 - num46;
			float num59 = num23 - num29;
			num46 = (num35 - num41) * 5.7368565f;
			num15 = num59 + num46;
			num16 = num59 - num46;
			float[] array = LayerIIIDecoder.Win[blockType];
			outValues[0] = -num16 * array[0];
			outValues[1] = -num18 * array[1];
			outValues[2] = -num20 * array[2];
			outValues[3] = -num56 * array[3];
			outValues[4] = -num54 * array[4];
			outValues[5] = -num53 * array[5];
			outValues[6] = -num51 * array[6];
			outValues[7] = -num49 * array[7];
			outValues[8] = -num47 * array[8];
			outValues[9] = num47 * array[9];
			outValues[10] = num49 * array[10];
			outValues[11] = num51 * array[11];
			outValues[12] = num53 * array[12];
			outValues[13] = num54 * array[13];
			outValues[14] = num56 * array[14];
			outValues[15] = num20 * array[15];
			outValues[16] = num18 * array[16];
			outValues[17] = num16 * array[17];
			outValues[18] = num15 * array[18];
			outValues[19] = num17 * array[19];
			outValues[20] = num19 * array[20];
			outValues[21] = num10 * array[21];
			outValues[22] = num9 * array[22];
			outValues[23] = num11 * array[23];
			outValues[24] = num7 * array[24];
			outValues[25] = num5 * array[25];
			outValues[26] = num6 * array[26];
			outValues[27] = num6 * array[27];
			outValues[28] = num5 * array[28];
			outValues[29] = num7 * array[29];
			outValues[30] = num11 * array[30];
			outValues[31] = num9 * array[31];
			outValues[32] = num10 * array[32];
			outValues[33] = num19 * array[33];
			outValues[34] = num17 * array[34];
			outValues[35] = num15 * array[35];
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0001A138 File Offset: 0x00018338
		private static float[] CreatePowerTable()
		{
			float[] array = new float[8192];
			double num = 1.3333333333333333;
			for (int i = 0; i < 8192; i++)
			{
				array[i] = (float)Math.Pow((double)i, num);
			}
			return array;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0001A178 File Offset: 0x00018378
		internal static int[] Reorder(int[] scalefacBand)
		{
			int num = 0;
			int[] array = new int[576];
			for (int i = 0; i < 13; i++)
			{
				int num2 = scalefacBand[i];
				int num3 = scalefacBand[i + 1];
				for (int j = 0; j < 3; j++)
				{
					for (int k = num2; k < num3; k++)
					{
						array[3 * k + j] = num++;
					}
				}
			}
			return array;
		}

		// Token: 0x040000F2 RID: 242
		private const int SSLIMIT = 18;

		// Token: 0x040000F3 RID: 243
		private const int SBLIMIT = 32;

		// Token: 0x040000F4 RID: 244
		private static readonly int[][] Slen = new int[][]
		{
			new int[]
			{
				0, 0, 0, 0, 3, 1, 1, 1, 2, 2,
				2, 3, 3, 3, 4, 4
			},
			new int[]
			{
				0, 1, 2, 3, 0, 1, 2, 3, 1, 2,
				3, 1, 2, 3, 2, 3
			}
		};

		// Token: 0x040000F5 RID: 245
		internal static readonly int[] Pretab = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1, 1, 1, 1, 2, 2, 3, 3, 3,
			2, 0
		};

		// Token: 0x040000F6 RID: 246
		internal static readonly float[] TwoToNegativeHalfPow = new float[]
		{
			1f, 0.70710677f, 0.5f, 0.35355338f, 0.25f, 0.17677669f, 0.125f, 0.088388346f, 0.0625f, 0.044194173f,
			0.03125f, 0.022097087f, 0.015625f, 0.011048543f, 0.0078125f, 0.0055242716f, 0.00390625f, 0.0027621358f, 0.001953125f, 0.0013810679f,
			0.0009765625f, 0.00069053395f, 0.00048828125f, 0.00034526698f, 0.00024414062f, 0.00017263349f, 0.00012207031f, 8.6316744E-05f, 6.1035156E-05f, 4.3158372E-05f,
			3.0517578E-05f, 2.1579186E-05f, 1.5258789E-05f, 1.0789593E-05f, 7.6293945E-06f, 5.3947965E-06f, 3.8146973E-06f, 2.6973983E-06f, 1.9073486E-06f, 1.3486991E-06f,
			9.536743E-07f, 6.7434956E-07f, 4.7683716E-07f, 3.3717478E-07f, 2.3841858E-07f, 1.6858739E-07f, 1.1920929E-07f, 8.4293696E-08f, 5.9604645E-08f, 4.2146848E-08f,
			2.9802322E-08f, 2.1073424E-08f, 1.4901161E-08f, 1.0536712E-08f, 7.450581E-09f, 5.268356E-09f, 3.7252903E-09f, 2.634178E-09f, 1.8626451E-09f, 1.317089E-09f,
			9.313226E-10f, 6.585445E-10f, 4.656613E-10f, 3.2927225E-10f
		};

		// Token: 0x040000F7 RID: 247
		internal static readonly float[] PowerTable;

		// Token: 0x040000F8 RID: 248
		internal static readonly float[][] Io = new float[][]
		{
			new float[]
			{
				1f, 0.8408964f, 0.70710677f, 0.59460354f, 0.5f, 0.4204482f, 0.35355338f, 0.29730177f, 0.25f, 0.2102241f,
				0.17677669f, 0.14865088f, 0.125f, 0.10511205f, 0.088388346f, 0.07432544f, 0.0625f, 0.052556027f, 0.044194173f, 0.03716272f,
				0.03125f, 0.026278013f, 0.022097087f, 0.01858136f, 0.015625f, 0.013139007f, 0.011048543f, 0.00929068f, 0.0078125f, 0.0065695033f,
				0.0055242716f, 0.00464534f
			},
			new float[]
			{
				1f, 0.70710677f, 0.5f, 0.35355338f, 0.25f, 0.17677669f, 0.125f, 0.088388346f, 0.0625f, 0.044194173f,
				0.03125f, 0.022097087f, 0.015625f, 0.011048543f, 0.0078125f, 0.0055242716f, 0.00390625f, 0.0027621358f, 0.001953125f, 0.0013810679f,
				0.0009765625f, 0.00069053395f, 0.00048828125f, 0.00034526698f, 0.00024414062f, 0.00017263349f, 0.00012207031f, 8.6316744E-05f, 6.1035156E-05f, 4.3158372E-05f,
				3.0517578E-05f, 2.1579186E-05f
			}
		};

		// Token: 0x040000F9 RID: 249
		internal static readonly float[] Tan12 = new float[]
		{
			0f, 0.2679492f, 0.57735026f, 1f, 1.7320508f, 3.732051f, 1E+11f, -3.732051f, -1.7320508f, -1f,
			-0.57735026f, -0.2679492f, 0f, 0.2679492f, 0.57735026f, 1f
		};

		// Token: 0x040000FA RID: 250
		private static int[][] _reorderTable;

		// Token: 0x040000FB RID: 251
		private static readonly float[] Cs = new float[] { 0.8574929f, 0.881742f, 0.94962865f, 0.9833146f, 0.9955178f, 0.9991606f, 0.9998992f, 0.99999315f };

		// Token: 0x040000FC RID: 252
		private static readonly float[] Ca = new float[] { -0.51449573f, -0.47173196f, -0.31337744f, -0.1819132f, -0.09457419f, -0.040965583f, -0.014198569f, -0.0036999746f };

		// Token: 0x040000FD RID: 253
		internal static readonly float[][] Win = new float[][]
		{
			new float[]
			{
				-0.016141215f, -0.05360318f, -0.100707136f, -0.16280818f, -0.5f, -0.38388735f, -0.6206114f, -1.1659756f, -3.8720753f, -4.225629f,
				-1.519529f, -0.97416484f, -0.73744076f, -1.2071068f, -0.5163616f, -0.45426053f, -0.40715656f, -0.3696946f, -0.3387627f, -0.31242222f,
				-0.28939587f, -0.26880082f, -0.5f, -0.23251417f, -0.21596715f, -0.20004979f, -0.18449493f, -0.16905846f, -0.15350361f, -0.13758625f,
				-0.12103922f, -0.20710678f, -0.084752575f, -0.06415752f, -0.041131172f, -0.014790705f
			},
			new float[]
			{
				-0.016141215f, -0.05360318f, -0.100707136f, -0.16280818f, -0.5f, -0.38388735f, -0.6206114f, -1.1659756f, -3.8720753f, -4.225629f,
				-1.519529f, -0.97416484f, -0.73744076f, -1.2071068f, -0.5163616f, -0.45426053f, -0.40715656f, -0.3696946f, -0.33908543f, -0.3151181f,
				-0.29642227f, -0.28184548f, -0.5411961f, -0.2621323f, -0.25387916f, -0.2329629f, -0.19852729f, -0.15233535f, -0.0964964f, -0.03342383f,
				0f, 0f, 0f, 0f, 0f, 0f
			},
			new float[]
			{
				-0.0483008f, -0.15715657f, -0.28325045f, -0.42953748f, -1.2071068f, -0.8242648f, -1.1451749f, -1.769529f, -4.5470223f, -3.489053f,
				-0.7329629f, -0.15076515f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
				0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
				0f, 0f, 0f, 0f, 0f, 0f
			},
			new float[]
			{
				0f, 0f, 0f, 0f, 0f, 0f, -0.15076514f, -0.7329629f, -3.489053f, -4.5470223f,
				-1.769529f, -1.1451749f, -0.8313774f, -1.306563f, -0.54142016f, -0.46528974f, -0.4106699f, -0.3700468f, -0.3387627f, -0.31242222f,
				-0.28939587f, -0.26880082f, -0.5f, -0.23251417f, -0.21596715f, -0.20004979f, -0.18449493f, -0.16905846f, -0.15350361f, -0.13758625f,
				-0.12103922f, -0.20710678f, -0.084752575f, -0.06415752f, -0.041131172f, -0.014790705f
			}
		};

		// Token: 0x040000FE RID: 254
		internal static readonly int[][][] NrOfSfbBlock;

		// Token: 0x040000FF RID: 255
		private readonly ABuffer _Buffer;

		// Token: 0x04000100 RID: 256
		private readonly int _Channels;

		// Token: 0x04000101 RID: 257
		private readonly SynthesisFilter _Filter1;

		// Token: 0x04000102 RID: 258
		private readonly SynthesisFilter _Filter2;

		// Token: 0x04000103 RID: 259
		private readonly int _FirstChannel;

		// Token: 0x04000104 RID: 260
		private readonly Header _Header;

		// Token: 0x04000105 RID: 261
		private readonly int[] _Is1D;

		// Token: 0x04000106 RID: 262
		private readonly float[][] _K;

		// Token: 0x04000107 RID: 263
		private readonly int _LastChannel;

		// Token: 0x04000108 RID: 264
		private readonly float[][][] _Lr;

		// Token: 0x04000109 RID: 265
		private readonly int _MaxGr;

		// Token: 0x0400010A RID: 266
		private readonly int[] _Nonzero;

		// Token: 0x0400010B RID: 267
		private readonly float[] _Out1D;

		// Token: 0x0400010C RID: 268
		private readonly float[][] _Prevblck;

		// Token: 0x0400010D RID: 269
		private readonly float[][][] _Ro;

		// Token: 0x0400010E RID: 270
		private readonly ScaleFactorData[] _Scalefac;

		// Token: 0x0400010F RID: 271
		private readonly SBI[] _SfBandIndex;

		// Token: 0x04000110 RID: 272
		private readonly int _Sfreq;

		// Token: 0x04000111 RID: 273
		private readonly Layer3SideInfo _SideInfo;

		// Token: 0x04000112 RID: 274
		private readonly Bitstream _Stream;

		// Token: 0x04000113 RID: 275
		private readonly int _WhichChannels;

		// Token: 0x04000114 RID: 276
		private BitReserve _BitReserve;

		// Token: 0x04000115 RID: 277
		private int _CheckSumHuff;

		// Token: 0x04000116 RID: 278
		private int _FrameStart;

		// Token: 0x04000117 RID: 279
		internal int[] IsPos;

		// Token: 0x04000118 RID: 280
		internal float[] IsRatio;

		// Token: 0x04000119 RID: 281
		private int[] _NewSlen;

		// Token: 0x0400011A RID: 282
		private int _Part2Start;

		// Token: 0x0400011B RID: 283
		internal float[] Rawout;

		// Token: 0x0400011C RID: 284
		private float[] _Samples1;

		// Token: 0x0400011D RID: 285
		private float[] _Samples2;

		// Token: 0x0400011E RID: 286
		internal int[] ScalefacBuffer;

		// Token: 0x0400011F RID: 287
		internal ScaleFactorTable Sftable;

		// Token: 0x04000120 RID: 288
		internal float[] TsOutCopy;

		// Token: 0x04000121 RID: 289
		internal int[] V = new int[1];

		// Token: 0x04000122 RID: 290
		internal int[] W = new int[1];

		// Token: 0x04000123 RID: 291
		internal int[] X = new int[1];

		// Token: 0x04000124 RID: 292
		internal int[] Y = new int[1];
	}
}
