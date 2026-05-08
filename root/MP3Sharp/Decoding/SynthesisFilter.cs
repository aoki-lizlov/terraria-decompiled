using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200001C RID: 28
	public class SynthesisFilter
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0001310C File Offset: 0x0001130C
		internal SynthesisFilter(int channelnumber, float factor, float[] eq0)
		{
			this.InitBlock();
			if (SynthesisFilter._d == null)
			{
				SynthesisFilter._d = SynthesisFilter.DData;
				SynthesisFilter._d16 = SynthesisFilter.SplitArray(SynthesisFilter._d, 16);
			}
			this._V1 = new float[512];
			this._V2 = new float[512];
			this._Samples = new float[32];
			this._Channel = channelnumber;
			this._Scalefactor = factor;
			this.Eq = this._Eq;
			this.Reset();
		}

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00013194 File Offset: 0x00011394
		internal float[] Eq
		{
			set
			{
				this._Eq = value;
				if (this._Eq == null)
				{
					this._Eq = new float[32];
					for (int i = 0; i < 32; i++)
					{
						this._Eq[i] = 1f;
					}
				}
				if (this._Eq.Length < 32)
				{
					throw new ArgumentException("eq0");
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000131EE File Offset: 0x000113EE
		private void InitBlock()
		{
			this._TmpOut = new float[32];
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00013200 File Offset: 0x00011400
		internal void Reset()
		{
			for (int i = 0; i < 512; i++)
			{
				this._V1[i] = (this._V2[i] = 0f);
			}
			for (int j = 0; j < 32; j++)
			{
				this._Samples[j] = 0f;
			}
			this._ActualV = this._V1;
			this._ActualWritePos = 15;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00013263 File Offset: 0x00011463
		internal void AddSample(float sample, int subbandnumber)
		{
			this._Samples[subbandnumber] = this._Eq[subbandnumber] * sample;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00013278 File Offset: 0x00011478
		internal void AddSamples(float[] s)
		{
			for (int i = 31; i >= 0; i--)
			{
				this._Samples[i] = s[i] * this._Eq[i];
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000132A8 File Offset: 0x000114A8
		private void ComputeNewValues()
		{
			float[] samples = this._Samples;
			float num = samples[0];
			float num2 = samples[1];
			float num3 = samples[2];
			float num4 = samples[3];
			float num5 = samples[4];
			float num6 = samples[5];
			float num7 = samples[6];
			float num8 = samples[7];
			float num9 = samples[8];
			float num10 = samples[9];
			float num11 = samples[10];
			float num12 = samples[11];
			float num13 = samples[12];
			float num14 = samples[13];
			float num15 = samples[14];
			float num16 = samples[15];
			float num17 = samples[16];
			float num18 = samples[17];
			float num19 = samples[18];
			float num20 = samples[19];
			float num21 = samples[20];
			float num22 = samples[21];
			float num23 = samples[22];
			float num24 = samples[23];
			float num25 = samples[24];
			float num26 = samples[25];
			float num27 = samples[26];
			float num28 = samples[27];
			float num29 = samples[28];
			float num30 = samples[29];
			float num31 = samples[30];
			float num32 = samples[31];
			float num33 = num + num32;
			float num34 = num2 + num31;
			float num35 = num3 + num30;
			float num36 = num4 + num29;
			float num37 = num5 + num28;
			float num38 = num6 + num27;
			float num39 = num7 + num26;
			float num40 = num8 + num25;
			float num41 = num9 + num24;
			float num42 = num10 + num23;
			float num43 = num11 + num22;
			float num44 = num12 + num21;
			float num45 = num13 + num20;
			float num46 = num14 + num19;
			float num47 = num15 + num18;
			float num48 = num16 + num17;
			float num49 = num33 + num48;
			float num50 = num34 + num47;
			float num51 = num35 + num46;
			float num52 = num36 + num45;
			float num53 = num37 + num44;
			float num54 = num38 + num43;
			float num55 = num39 + num42;
			float num56 = num40 + num41;
			float num57 = (num33 - num48) * SynthesisFilter.Cos132;
			float num58 = (num34 - num47) * SynthesisFilter.Cos332;
			float num59 = (num35 - num46) * SynthesisFilter.Cos532;
			float num60 = (num36 - num45) * SynthesisFilter.Cos732;
			float num61 = (num37 - num44) * SynthesisFilter.Cos932;
			float num62 = (num38 - num43) * SynthesisFilter.Cos1132;
			float num63 = (num39 - num42) * SynthesisFilter.Cos1332;
			float num64 = (num40 - num41) * SynthesisFilter.Cos1532;
			num33 = num49 + num56;
			num34 = num50 + num55;
			num35 = num51 + num54;
			num36 = num52 + num53;
			num37 = (num49 - num56) * SynthesisFilter.Cos116;
			num38 = (num50 - num55) * SynthesisFilter.Cos316;
			num39 = (num51 - num54) * SynthesisFilter.Cos516;
			num40 = (num52 - num53) * SynthesisFilter.Cos716;
			num41 = num57 + num64;
			num42 = num58 + num63;
			num43 = num59 + num62;
			num44 = num60 + num61;
			num45 = (num57 - num64) * SynthesisFilter.Cos116;
			num46 = (num58 - num63) * SynthesisFilter.Cos316;
			num47 = (num59 - num62) * SynthesisFilter.Cos516;
			num48 = (num60 - num61) * SynthesisFilter.Cos716;
			float num65 = num33 + num36;
			num50 = num34 + num35;
			num51 = (num33 - num36) * SynthesisFilter.Cos18;
			num52 = (num34 - num35) * SynthesisFilter.Cos38;
			num53 = num37 + num40;
			num54 = num38 + num39;
			num55 = (num37 - num40) * SynthesisFilter.Cos18;
			num56 = (num38 - num39) * SynthesisFilter.Cos38;
			num57 = num41 + num44;
			num58 = num42 + num43;
			num59 = (num41 - num44) * SynthesisFilter.Cos18;
			num60 = (num42 - num43) * SynthesisFilter.Cos38;
			num61 = num45 + num48;
			num62 = num46 + num47;
			num63 = (num45 - num48) * SynthesisFilter.Cos18;
			num64 = (num46 - num47) * SynthesisFilter.Cos38;
			num33 = num65 + num50;
			num34 = (num65 - num50) * SynthesisFilter.Cos14;
			num35 = num51 + num52;
			num36 = (num51 - num52) * SynthesisFilter.Cos14;
			num37 = num53 + num54;
			num38 = (num53 - num54) * SynthesisFilter.Cos14;
			num39 = num55 + num56;
			num40 = (num55 - num56) * SynthesisFilter.Cos14;
			num41 = num57 + num58;
			num42 = (num57 - num58) * SynthesisFilter.Cos14;
			num43 = num59 + num60;
			num44 = (num59 - num60) * SynthesisFilter.Cos14;
			num45 = num61 + num62;
			num46 = (num61 - num62) * SynthesisFilter.Cos14;
			num47 = num63 + num64;
			num48 = (num63 - num64) * SynthesisFilter.Cos14;
			float num68;
			float num67;
			float num66 = -(num67 = (num68 = num40) + num38) - num39;
			float num69 = -num39 - num40 - num37;
			float num72;
			float num71;
			float num70 = (num71 = (num72 = num48) + num44) + num46;
			float num74;
			float num73 = -(num74 = num48 + num46 + num42) - num47;
			float num75 = -num47 - num48 - num43 - num44;
			float num76 = num75 - num46;
			float num77 = -num47 - num48 - num45 - num41;
			float num78 = num75 - num45;
			float num79 = -num33;
			float num80 = num34;
			float num82;
			float num81 = -(num82 = num36) - num35;
			num33 = (num - num32) * SynthesisFilter.Cos164;
			num34 = (num2 - num31) * SynthesisFilter.Cos364;
			num35 = (num3 - num30) * SynthesisFilter.Cos564;
			num36 = (num4 - num29) * SynthesisFilter.Cos764;
			num37 = (num5 - num28) * SynthesisFilter.Cos964;
			num38 = (num6 - num27) * SynthesisFilter.Cos1164;
			num39 = (num7 - num26) * SynthesisFilter.Cos1364;
			num40 = (num8 - num25) * SynthesisFilter.Cos1564;
			num41 = (num9 - num24) * SynthesisFilter.Cos1764;
			num42 = (num10 - num23) * SynthesisFilter.Cos1964;
			num43 = (num11 - num22) * SynthesisFilter.Cos2164;
			num44 = (num12 - num21) * SynthesisFilter.Cos2364;
			num45 = (num13 - num20) * SynthesisFilter.Cos2564;
			num46 = (num14 - num19) * SynthesisFilter.Cos2764;
			num47 = (num15 - num18) * SynthesisFilter.Cos2964;
			num48 = (num16 - num17) * SynthesisFilter.Cos3164;
			float num83 = num33 + num48;
			num50 = num34 + num47;
			num51 = num35 + num46;
			num52 = num36 + num45;
			num53 = num37 + num44;
			num54 = num38 + num43;
			num55 = num39 + num42;
			num56 = num40 + num41;
			num57 = (num33 - num48) * SynthesisFilter.Cos132;
			num58 = (num34 - num47) * SynthesisFilter.Cos332;
			num59 = (num35 - num46) * SynthesisFilter.Cos532;
			num60 = (num36 - num45) * SynthesisFilter.Cos732;
			num61 = (num37 - num44) * SynthesisFilter.Cos932;
			num62 = (num38 - num43) * SynthesisFilter.Cos1132;
			num63 = (num39 - num42) * SynthesisFilter.Cos1332;
			num64 = (num40 - num41) * SynthesisFilter.Cos1532;
			num33 = num83 + num56;
			num34 = num50 + num55;
			num35 = num51 + num54;
			num36 = num52 + num53;
			num37 = (num83 - num56) * SynthesisFilter.Cos116;
			num38 = (num50 - num55) * SynthesisFilter.Cos316;
			num39 = (num51 - num54) * SynthesisFilter.Cos516;
			num40 = (num52 - num53) * SynthesisFilter.Cos716;
			num41 = num57 + num64;
			num42 = num58 + num63;
			num43 = num59 + num62;
			num44 = num60 + num61;
			num45 = (num57 - num64) * SynthesisFilter.Cos116;
			num46 = (num58 - num63) * SynthesisFilter.Cos316;
			num47 = (num59 - num62) * SynthesisFilter.Cos516;
			num48 = (num60 - num61) * SynthesisFilter.Cos716;
			float num84 = num33 + num36;
			num50 = num34 + num35;
			num51 = (num33 - num36) * SynthesisFilter.Cos18;
			num52 = (num34 - num35) * SynthesisFilter.Cos38;
			num53 = num37 + num40;
			num54 = num38 + num39;
			num55 = (num37 - num40) * SynthesisFilter.Cos18;
			num56 = (num38 - num39) * SynthesisFilter.Cos38;
			num57 = num41 + num44;
			num58 = num42 + num43;
			num59 = (num41 - num44) * SynthesisFilter.Cos18;
			num60 = (num42 - num43) * SynthesisFilter.Cos38;
			num61 = num45 + num48;
			num62 = num46 + num47;
			num63 = (num45 - num48) * SynthesisFilter.Cos18;
			num64 = (num46 - num47) * SynthesisFilter.Cos38;
			num33 = num84 + num50;
			num34 = (num84 - num50) * SynthesisFilter.Cos14;
			num35 = num51 + num52;
			num36 = (num51 - num52) * SynthesisFilter.Cos14;
			num37 = num53 + num54;
			num38 = (num53 - num54) * SynthesisFilter.Cos14;
			num39 = num55 + num56;
			num40 = (num55 - num56) * SynthesisFilter.Cos14;
			num41 = num57 + num58;
			num42 = (num57 - num58) * SynthesisFilter.Cos14;
			num43 = num59 + num60;
			num44 = (num59 - num60) * SynthesisFilter.Cos14;
			num45 = num61 + num62;
			num46 = (num61 - num62) * SynthesisFilter.Cos14;
			num47 = num63 + num64;
			num48 = (num63 - num64) * SynthesisFilter.Cos14;
			float num88;
			float num87;
			float num86;
			float num85 = (num86 = (num87 = (num88 = num48) + num40) + num44) + num38 + num46;
			float num90;
			float num89 = (num90 = num48 + num44 + num36) + num46;
			float num91 = num46 + num48 + num42;
			float num93;
			float num92 = -(num93 = num91 + num34) - num47;
			float num95;
			float num94 = -(num95 = num91 + num38 + num40) - num39 - num47;
			float num96 = -num43 - num44 - num47 - num48;
			float num97 = num96 - num46 - num35 - num36;
			float num98 = num96 - num46 - num38 - num39 - num40;
			float num99 = num96 - num45 - num35 - num36;
			float num101;
			float num100 = num96 - num45 - (num101 = num37 + num39 + num40);
			float num102 = -num41 - num45 - num47 - num48;
			float num103 = num102 - num33;
			float num104 = num102 - num101;
			float[] actualV = this._ActualV;
			int actualWritePos = this._ActualWritePos;
			actualV[actualWritePos] = num80;
			actualV[16 + actualWritePos] = num93;
			actualV[32 + actualWritePos] = num74;
			actualV[48 + actualWritePos] = num95;
			actualV[64 + actualWritePos] = num67;
			actualV[80 + actualWritePos] = num85;
			actualV[96 + actualWritePos] = num70;
			actualV[112 + actualWritePos] = num89;
			actualV[128 + actualWritePos] = num82;
			actualV[144 + actualWritePos] = num90;
			actualV[160 + actualWritePos] = num71;
			actualV[176 + actualWritePos] = num86;
			actualV[192 + actualWritePos] = num68;
			actualV[208 + actualWritePos] = num87;
			actualV[224 + actualWritePos] = num72;
			actualV[240 + actualWritePos] = num88;
			actualV[256 + actualWritePos] = 0f;
			actualV[272 + actualWritePos] = -num88;
			actualV[288 + actualWritePos] = -num72;
			actualV[304 + actualWritePos] = -num87;
			actualV[320 + actualWritePos] = -num68;
			actualV[336 + actualWritePos] = -num86;
			actualV[352 + actualWritePos] = -num71;
			actualV[368 + actualWritePos] = -num90;
			actualV[384 + actualWritePos] = -num82;
			actualV[400 + actualWritePos] = -num89;
			actualV[416 + actualWritePos] = -num70;
			actualV[432 + actualWritePos] = -num85;
			actualV[448 + actualWritePos] = -num67;
			actualV[464 + actualWritePos] = -num95;
			actualV[480 + actualWritePos] = -num74;
			actualV[496 + actualWritePos] = -num93;
			float[] array = ((this._ActualV == this._V1) ? this._V2 : this._V1);
			array[actualWritePos] = -num80;
			array[16 + actualWritePos] = num92;
			array[32 + actualWritePos] = num73;
			array[48 + actualWritePos] = num94;
			array[64 + actualWritePos] = num66;
			array[80 + actualWritePos] = num98;
			array[96 + actualWritePos] = num76;
			array[112 + actualWritePos] = num97;
			array[128 + actualWritePos] = num81;
			array[144 + actualWritePos] = num99;
			array[160 + actualWritePos] = num78;
			array[176 + actualWritePos] = num100;
			array[192 + actualWritePos] = num69;
			array[208 + actualWritePos] = num104;
			array[224 + actualWritePos] = num77;
			array[240 + actualWritePos] = num103;
			array[256 + actualWritePos] = num79;
			array[272 + actualWritePos] = num103;
			array[288 + actualWritePos] = num77;
			array[304 + actualWritePos] = num104;
			array[320 + actualWritePos] = num69;
			array[336 + actualWritePos] = num100;
			array[352 + actualWritePos] = num78;
			array[368 + actualWritePos] = num99;
			array[384 + actualWritePos] = num81;
			array[400 + actualWritePos] = num97;
			array[416 + actualWritePos] = num76;
			array[432 + actualWritePos] = num98;
			array[448 + actualWritePos] = num66;
			array[464 + actualWritePos] = num94;
			array[480 + actualWritePos] = num73;
			array[496 + actualWritePos] = num92;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00013DB8 File Offset: 0x00011FB8
		private void compute_pc_samples0(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[num] * array[0] + actualV[15 + num] * array[1] + actualV[14 + num] * array[2] + actualV[13 + num] * array[3] + actualV[12 + num] * array[4] + actualV[11 + num] * array[5] + actualV[10 + num] * array[6] + actualV[9 + num] * array[7] + actualV[8 + num] * array[8] + actualV[7 + num] * array[9] + actualV[6 + num] * array[10] + actualV[5 + num] * array[11] + actualV[4 + num] * array[12] + actualV[3 + num] * array[13] + actualV[2 + num] * array[14] + actualV[1 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00013EC0 File Offset: 0x000120C0
		private void compute_pc_samples1(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[1 + num] * array[0] + actualV[num] * array[1] + actualV[15 + num] * array[2] + actualV[14 + num] * array[3] + actualV[13 + num] * array[4] + actualV[12 + num] * array[5] + actualV[11 + num] * array[6] + actualV[10 + num] * array[7] + actualV[9 + num] * array[8] + actualV[8 + num] * array[9] + actualV[7 + num] * array[10] + actualV[6 + num] * array[11] + actualV[5 + num] * array[12] + actualV[4 + num] * array[13] + actualV[3 + num] * array[14] + actualV[2 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00013FC8 File Offset: 0x000121C8
		private void compute_pc_samples2(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[2 + num] * array[0] + actualV[1 + num] * array[1] + actualV[num] * array[2] + actualV[15 + num] * array[3] + actualV[14 + num] * array[4] + actualV[13 + num] * array[5] + actualV[12 + num] * array[6] + actualV[11 + num] * array[7] + actualV[10 + num] * array[8] + actualV[9 + num] * array[9] + actualV[8 + num] * array[10] + actualV[7 + num] * array[11] + actualV[6 + num] * array[12] + actualV[5 + num] * array[13] + actualV[4 + num] * array[14] + actualV[3 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000140D0 File Offset: 0x000122D0
		private void compute_pc_samples3(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[3 + num] * array[0] + actualV[2 + num] * array[1] + actualV[1 + num] * array[2] + actualV[num] * array[3] + actualV[15 + num] * array[4] + actualV[14 + num] * array[5] + actualV[13 + num] * array[6] + actualV[12 + num] * array[7] + actualV[11 + num] * array[8] + actualV[10 + num] * array[9] + actualV[9 + num] * array[10] + actualV[8 + num] * array[11] + actualV[7 + num] * array[12] + actualV[6 + num] * array[13] + actualV[5 + num] * array[14] + actualV[4 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000141D8 File Offset: 0x000123D8
		private void compute_pc_samples4(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[4 + num] * array[0] + actualV[3 + num] * array[1] + actualV[2 + num] * array[2] + actualV[1 + num] * array[3] + actualV[num] * array[4] + actualV[15 + num] * array[5] + actualV[14 + num] * array[6] + actualV[13 + num] * array[7] + actualV[12 + num] * array[8] + actualV[11 + num] * array[9] + actualV[10 + num] * array[10] + actualV[9 + num] * array[11] + actualV[8 + num] * array[12] + actualV[7 + num] * array[13] + actualV[6 + num] * array[14] + actualV[5 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000142E0 File Offset: 0x000124E0
		private void compute_pc_samples5(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[5 + num] * array[0] + actualV[4 + num] * array[1] + actualV[3 + num] * array[2] + actualV[2 + num] * array[3] + actualV[1 + num] * array[4] + actualV[num] * array[5] + actualV[15 + num] * array[6] + actualV[14 + num] * array[7] + actualV[13 + num] * array[8] + actualV[12 + num] * array[9] + actualV[11 + num] * array[10] + actualV[10 + num] * array[11] + actualV[9 + num] * array[12] + actualV[8 + num] * array[13] + actualV[7 + num] * array[14] + actualV[6 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000143E8 File Offset: 0x000125E8
		private void compute_pc_samples6(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[6 + num] * array[0] + actualV[5 + num] * array[1] + actualV[4 + num] * array[2] + actualV[3 + num] * array[3] + actualV[2 + num] * array[4] + actualV[1 + num] * array[5] + actualV[num] * array[6] + actualV[15 + num] * array[7] + actualV[14 + num] * array[8] + actualV[13 + num] * array[9] + actualV[12 + num] * array[10] + actualV[11 + num] * array[11] + actualV[10 + num] * array[12] + actualV[9 + num] * array[13] + actualV[8 + num] * array[14] + actualV[7 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000144F0 File Offset: 0x000126F0
		private void compute_pc_samples7(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[7 + num] * array[0] + actualV[6 + num] * array[1] + actualV[5 + num] * array[2] + actualV[4 + num] * array[3] + actualV[3 + num] * array[4] + actualV[2 + num] * array[5] + actualV[1 + num] * array[6] + actualV[num] * array[7] + actualV[15 + num] * array[8] + actualV[14 + num] * array[9] + actualV[13 + num] * array[10] + actualV[12 + num] * array[11] + actualV[11 + num] * array[12] + actualV[10 + num] * array[13] + actualV[9 + num] * array[14] + actualV[8 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000145F8 File Offset: 0x000127F8
		private void compute_pc_samples8(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[8 + num] * array[0] + actualV[7 + num] * array[1] + actualV[6 + num] * array[2] + actualV[5 + num] * array[3] + actualV[4 + num] * array[4] + actualV[3 + num] * array[5] + actualV[2 + num] * array[6] + actualV[1 + num] * array[7] + actualV[num] * array[8] + actualV[15 + num] * array[9] + actualV[14 + num] * array[10] + actualV[13 + num] * array[11] + actualV[12 + num] * array[12] + actualV[11 + num] * array[13] + actualV[10 + num] * array[14] + actualV[9 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00014700 File Offset: 0x00012900
		private void compute_pc_samples9(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[9 + num] * array[0] + actualV[8 + num] * array[1] + actualV[7 + num] * array[2] + actualV[6 + num] * array[3] + actualV[5 + num] * array[4] + actualV[4 + num] * array[5] + actualV[3 + num] * array[6] + actualV[2 + num] * array[7] + actualV[1 + num] * array[8] + actualV[num] * array[9] + actualV[15 + num] * array[10] + actualV[14 + num] * array[11] + actualV[13 + num] * array[12] + actualV[12 + num] * array[13] + actualV[11 + num] * array[14] + actualV[10 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00014808 File Offset: 0x00012A08
		private void compute_pc_samples10(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[10 + num] * array[0] + actualV[9 + num] * array[1] + actualV[8 + num] * array[2] + actualV[7 + num] * array[3] + actualV[6 + num] * array[4] + actualV[5 + num] * array[5] + actualV[4 + num] * array[6] + actualV[3 + num] * array[7] + actualV[2 + num] * array[8] + actualV[1 + num] * array[9] + actualV[num] * array[10] + actualV[15 + num] * array[11] + actualV[14 + num] * array[12] + actualV[13 + num] * array[13] + actualV[12 + num] * array[14] + actualV[11 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00014910 File Offset: 0x00012B10
		private void compute_pc_samples11(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[11 + num] * array[0] + actualV[10 + num] * array[1] + actualV[9 + num] * array[2] + actualV[8 + num] * array[3] + actualV[7 + num] * array[4] + actualV[6 + num] * array[5] + actualV[5 + num] * array[6] + actualV[4 + num] * array[7] + actualV[3 + num] * array[8] + actualV[2 + num] * array[9] + actualV[1 + num] * array[10] + actualV[num] * array[11] + actualV[15 + num] * array[12] + actualV[14 + num] * array[13] + actualV[13 + num] * array[14] + actualV[12 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00014A18 File Offset: 0x00012C18
		private void compute_pc_samples12(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[12 + num] * array[0] + actualV[11 + num] * array[1] + actualV[10 + num] * array[2] + actualV[9 + num] * array[3] + actualV[8 + num] * array[4] + actualV[7 + num] * array[5] + actualV[6 + num] * array[6] + actualV[5 + num] * array[7] + actualV[4 + num] * array[8] + actualV[3 + num] * array[9] + actualV[2 + num] * array[10] + actualV[1 + num] * array[11] + actualV[num] * array[12] + actualV[15 + num] * array[13] + actualV[14 + num] * array[14] + actualV[13 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00014B20 File Offset: 0x00012D20
		private void compute_pc_samples13(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[13 + num] * array[0] + actualV[12 + num] * array[1] + actualV[11 + num] * array[2] + actualV[10 + num] * array[3] + actualV[9 + num] * array[4] + actualV[8 + num] * array[5] + actualV[7 + num] * array[6] + actualV[6 + num] * array[7] + actualV[5 + num] * array[8] + actualV[4 + num] * array[9] + actualV[3 + num] * array[10] + actualV[2 + num] * array[11] + actualV[1 + num] * array[12] + actualV[num] * array[13] + actualV[15 + num] * array[14] + actualV[14 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00014C28 File Offset: 0x00012E28
		private void compute_pc_samples14(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[14 + num] * array[0] + actualV[13 + num] * array[1] + actualV[12 + num] * array[2] + actualV[11 + num] * array[3] + actualV[10 + num] * array[4] + actualV[9 + num] * array[5] + actualV[8 + num] * array[6] + actualV[7 + num] * array[7] + actualV[6 + num] * array[8] + actualV[5 + num] * array[9] + actualV[4 + num] * array[10] + actualV[3 + num] * array[11] + actualV[2 + num] * array[12] + actualV[1 + num] * array[13] + actualV[num] * array[14] + actualV[15 + num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00014D30 File Offset: 0x00012F30
		private void Compute_pc_samples15(ABuffer buffer)
		{
			float[] actualV = this._ActualV;
			float[] tmpOut = this._TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = SynthesisFilter._d16[i];
				float num2 = (actualV[15 + num] * array[0] + actualV[14 + num] * array[1] + actualV[13 + num] * array[2] + actualV[12 + num] * array[3] + actualV[11 + num] * array[4] + actualV[10 + num] * array[5] + actualV[9 + num] * array[6] + actualV[8 + num] * array[7] + actualV[7 + num] * array[8] + actualV[6 + num] * array[9] + actualV[5 + num] * array[10] + actualV[4 + num] * array[11] + actualV[3 + num] * array[12] + actualV[2 + num] * array[13] + actualV[1 + num] * array[14] + actualV[num] * array[15]) * this._Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00014E38 File Offset: 0x00013038
		private void compute_pc_samples(ABuffer buffer)
		{
			switch (this._ActualWritePos)
			{
			case 0:
				this.compute_pc_samples0(buffer);
				break;
			case 1:
				this.compute_pc_samples1(buffer);
				break;
			case 2:
				this.compute_pc_samples2(buffer);
				break;
			case 3:
				this.compute_pc_samples3(buffer);
				break;
			case 4:
				this.compute_pc_samples4(buffer);
				break;
			case 5:
				this.compute_pc_samples5(buffer);
				break;
			case 6:
				this.compute_pc_samples6(buffer);
				break;
			case 7:
				this.compute_pc_samples7(buffer);
				break;
			case 8:
				this.compute_pc_samples8(buffer);
				break;
			case 9:
				this.compute_pc_samples9(buffer);
				break;
			case 10:
				this.compute_pc_samples10(buffer);
				break;
			case 11:
				this.compute_pc_samples11(buffer);
				break;
			case 12:
				this.compute_pc_samples12(buffer);
				break;
			case 13:
				this.compute_pc_samples13(buffer);
				break;
			case 14:
				this.compute_pc_samples14(buffer);
				break;
			case 15:
				this.Compute_pc_samples15(buffer);
				break;
			}
			if (buffer != null)
			{
				buffer.AppendSamples(this._Channel, this._TmpOut);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00014F40 File Offset: 0x00013140
		internal void calculate_pc_samples(ABuffer buffer)
		{
			this.ComputeNewValues();
			this.compute_pc_samples(buffer);
			this._ActualWritePos = (this._ActualWritePos + 1) & 15;
			this._ActualV = ((this._ActualV == this._V1) ? this._V2 : this._V1);
			for (int i = 0; i < 32; i++)
			{
				this._Samples[i] = 0f;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00014FA8 File Offset: 0x000131A8
		private static float[][] SplitArray(float[] array, int blockSize)
		{
			int num = array.Length / blockSize;
			float[][] array2 = new float[num][];
			for (int i = 0; i < num; i++)
			{
				array2[i] = SynthesisFilter.SubArray(array, i * blockSize, blockSize);
			}
			return array2;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00014FDC File Offset: 0x000131DC
		private static float[] SubArray(float[] array, int offs, int len)
		{
			if (offs + len > array.Length)
			{
				len = array.Length - offs;
			}
			if (len < 0)
			{
				len = 0;
			}
			float[] array2 = new float[len];
			for (int i = 0; i < len; i++)
			{
				array2[i] = array[offs + i];
			}
			return array2;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0001501C File Offset: 0x0001321C
		// Note: this type is marked as 'beforefieldinit'.
		static SynthesisFilter()
		{
		}

		// Token: 0x040000BB RID: 187
		private const double MY_PI = 3.141592653589793;

		// Token: 0x040000BC RID: 188
		private static readonly float Cos164 = (float)(1.0 / (2.0 * Math.Cos(0.04908738521234052)));

		// Token: 0x040000BD RID: 189
		private static readonly float Cos364 = (float)(1.0 / (2.0 * Math.Cos(0.14726215563702155)));

		// Token: 0x040000BE RID: 190
		private static readonly float Cos564 = (float)(1.0 / (2.0 * Math.Cos(0.2454369260617026)));

		// Token: 0x040000BF RID: 191
		private static readonly float Cos764 = (float)(1.0 / (2.0 * Math.Cos(0.3436116964863836)));

		// Token: 0x040000C0 RID: 192
		private static readonly float Cos964 = (float)(1.0 / (2.0 * Math.Cos(0.44178646691106466)));

		// Token: 0x040000C1 RID: 193
		private static readonly float Cos1164 = (float)(1.0 / (2.0 * Math.Cos(0.5399612373357456)));

		// Token: 0x040000C2 RID: 194
		private static readonly float Cos1364 = (float)(1.0 / (2.0 * Math.Cos(0.6381360077604268)));

		// Token: 0x040000C3 RID: 195
		private static readonly float Cos1564 = (float)(1.0 / (2.0 * Math.Cos(0.7363107781851077)));

		// Token: 0x040000C4 RID: 196
		private static readonly float Cos1764 = (float)(1.0 / (2.0 * Math.Cos(0.8344855486097889)));

		// Token: 0x040000C5 RID: 197
		private static readonly float Cos1964 = (float)(1.0 / (2.0 * Math.Cos(0.9326603190344698)));

		// Token: 0x040000C6 RID: 198
		private static readonly float Cos2164 = (float)(1.0 / (2.0 * Math.Cos(1.030835089459151)));

		// Token: 0x040000C7 RID: 199
		private static readonly float Cos2364 = (float)(1.0 / (2.0 * Math.Cos(1.1290098598838318)));

		// Token: 0x040000C8 RID: 200
		private static readonly float Cos2564 = (float)(1.0 / (2.0 * Math.Cos(1.227184630308513)));

		// Token: 0x040000C9 RID: 201
		private static readonly float Cos2764 = (float)(1.0 / (2.0 * Math.Cos(1.325359400733194)));

		// Token: 0x040000CA RID: 202
		private static readonly float Cos2964 = (float)(1.0 / (2.0 * Math.Cos(1.423534171157875)));

		// Token: 0x040000CB RID: 203
		private static readonly float Cos3164 = (float)(1.0 / (2.0 * Math.Cos(1.521708941582556)));

		// Token: 0x040000CC RID: 204
		private static readonly float Cos132 = (float)(1.0 / (2.0 * Math.Cos(0.09817477042468103)));

		// Token: 0x040000CD RID: 205
		private static readonly float Cos332 = (float)(1.0 / (2.0 * Math.Cos(0.2945243112740431)));

		// Token: 0x040000CE RID: 206
		private static readonly float Cos532 = (float)(1.0 / (2.0 * Math.Cos(0.4908738521234052)));

		// Token: 0x040000CF RID: 207
		private static readonly float Cos732 = (float)(1.0 / (2.0 * Math.Cos(0.6872233929727672)));

		// Token: 0x040000D0 RID: 208
		private static readonly float Cos932 = (float)(1.0 / (2.0 * Math.Cos(0.8835729338221293)));

		// Token: 0x040000D1 RID: 209
		private static readonly float Cos1132 = (float)(1.0 / (2.0 * Math.Cos(1.0799224746714913)));

		// Token: 0x040000D2 RID: 210
		private static readonly float Cos1332 = (float)(1.0 / (2.0 * Math.Cos(1.2762720155208536)));

		// Token: 0x040000D3 RID: 211
		private static readonly float Cos1532 = (float)(1.0 / (2.0 * Math.Cos(1.4726215563702154)));

		// Token: 0x040000D4 RID: 212
		private static readonly float Cos116 = (float)(1.0 / (2.0 * Math.Cos(0.19634954084936207)));

		// Token: 0x040000D5 RID: 213
		private static readonly float Cos316 = (float)(1.0 / (2.0 * Math.Cos(0.5890486225480862)));

		// Token: 0x040000D6 RID: 214
		private static readonly float Cos516 = (float)(1.0 / (2.0 * Math.Cos(0.9817477042468103)));

		// Token: 0x040000D7 RID: 215
		private static readonly float Cos716 = (float)(1.0 / (2.0 * Math.Cos(1.3744467859455345)));

		// Token: 0x040000D8 RID: 216
		private static readonly float Cos18 = (float)(1.0 / (2.0 * Math.Cos(0.39269908169872414)));

		// Token: 0x040000D9 RID: 217
		private static readonly float Cos38 = (float)(1.0 / (2.0 * Math.Cos(1.1780972450961724)));

		// Token: 0x040000DA RID: 218
		private static readonly float Cos14 = (float)(1.0 / (2.0 * Math.Cos(0.7853981633974483)));

		// Token: 0x040000DB RID: 219
		private static float[] _d;

		// Token: 0x040000DC RID: 220
		private static float[][] _d16;

		// Token: 0x040000DD RID: 221
		private static readonly float[] DData = new float[]
		{
			0f, -0.000442505f, 0.003250122f, -0.007003784f, 0.031082153f, -0.07862854f, 0.10031128f, -0.57203674f, 1.144989f, 0.57203674f,
			0.10031128f, 0.07862854f, 0.031082153f, 0.007003784f, 0.003250122f, 0.000442505f, -1.5259E-05f, -0.000473022f, 0.003326416f, -0.007919312f,
			0.030517578f, -0.08418274f, 0.090927124f, -0.6002197f, 1.1442871f, 0.54382324f, 0.1088562f, 0.07305908f, 0.03147888f, 0.006118774f,
			0.003173828f, 0.000396729f, -1.5259E-05f, -0.000534058f, 0.003387451f, -0.008865356f, 0.029785156f, -0.08970642f, 0.08068848f, -0.6282959f,
			1.1422119f, 0.51560974f, 0.11657715f, 0.06752014f, 0.03173828f, 0.0052948f, 0.003082275f, 0.000366211f, -1.5259E-05f, -0.000579834f,
			0.003433228f, -0.009841919f, 0.028884888f, -0.09516907f, 0.06959534f, -0.6562195f, 1.1387634f, 0.48747253f, 0.12347412f, 0.06199646f,
			0.031845093f, 0.004486084f, 0.002990723f, 0.000320435f, -1.5259E-05f, -0.00062561f, 0.003463745f, -0.010848999f, 0.027801514f, -0.10054016f,
			0.057617188f, -0.6839142f, 1.1339264f, 0.45947266f, 0.12957764f, 0.056533813f, 0.031814575f, 0.003723145f, 0.00289917f, 0.000289917f,
			-1.5259E-05f, -0.000686646f, 0.003479004f, -0.011886597f, 0.026535034f, -0.1058197f, 0.044784546f, -0.71131897f, 1.1277466f, 0.43165588f,
			0.1348877f, 0.051132202f, 0.031661987f, 0.003005981f, 0.002792358f, 0.000259399f, -1.5259E-05f, -0.000747681f, 0.003479004f, -0.012939453f,
			0.02508545f, -0.110946655f, 0.031082153f, -0.7383728f, 1.120224f, 0.40408325f, 0.13945007f, 0.045837402f, 0.03138733f, 0.002334595f,
			0.002685547f, 0.000244141f, -3.0518E-05f, -0.000808716f, 0.003463745f, -0.014022827f, 0.023422241f, -0.11592102f, 0.01651001f, -0.7650299f,
			1.1113739f, 0.37680054f, 0.14326477f, 0.040634155f, 0.03100586f, 0.001693726f, 0.002578735f, 0.000213623f, -3.0518E-05f, -0.00088501f,
			0.003417969f, -0.01512146f, 0.021575928f, -0.12069702f, 0.001068115f, -0.791214f, 1.1012115f, 0.34986877f, 0.1463623f, 0.03555298f,
			0.030532837f, 0.001098633f, 0.002456665f, 0.000198364f, -3.0518E-05f, -0.000961304f, 0.003372192f, -0.016235352f, 0.01953125f, -0.1252594f,
			-0.015228271f, -0.816864f, 1.0897827f, 0.32331848f, 0.1487732f, 0.03060913f, 0.029937744f, 0.000549316f, 0.002349854f, 0.000167847f,
			-3.0518E-05f, -0.001037598f, 0.00328064f, -0.017349243f, 0.01725769f, -0.12956238f, -0.03237915f, -0.84194946f, 1.0771179f, 0.2972107f,
			0.15049744f, 0.025817871f, 0.029281616f, 3.0518E-05f, 0.002243042f, 0.000152588f, -4.5776E-05f, -0.001113892f, 0.003173828f, -0.018463135f,
			0.014801025f, -0.1335907f, -0.050354004f, -0.8663635f, 1.0632172f, 0.2715912f, 0.15159607f, 0.0211792f, 0.028533936f, -0.000442505f,
			0.002120972f, 0.000137329f, -4.5776E-05f, -0.001205444f, 0.003051758f, -0.019577026f, 0.012115479f, -0.13729858f, -0.06916809f, -0.89009094f,
			1.0481567f, 0.24650574f, 0.15206909f, 0.016708374f, 0.02772522f, -0.000869751f, 0.00201416f, 0.00012207f, -6.1035E-05f, -0.001296997f,
			0.002883911f, -0.020690918f, 0.009231567f, -0.14067078f, -0.088775635f, -0.9130554f, 1.0319366f, 0.22198486f, 0.15196228f, 0.012420654f,
			0.02684021f, -0.001266479f, 0.001907349f, 0.000106812f, -6.1035E-05f, -0.00138855f, 0.002700806f, -0.02178955f, 0.006134033f, -0.14367676f,
			-0.10916138f, -0.9351959f, 1.0146179f, 0.19805908f, 0.15130615f, 0.00831604f, 0.025909424f, -0.001617432f, 0.001785278f, 0.000106812f,
			-7.6294E-05f, -0.001480103f, 0.002487183f, -0.022857666f, 0.002822876f, -0.1462555f, -0.13031006f, -0.95648193f, 0.99624634f, 0.17478943f,
			0.15011597f, 0.004394531f, 0.024932861f, -0.001937866f, 0.001693726f, 9.1553E-05f, -7.6294E-05f, -0.001586914f, 0.002227783f, -0.023910522f,
			-0.000686646f, -0.14842224f, -0.15220642f, -0.9768524f, 0.9768524f, 0.15220642f, 0.14842224f, 0.000686646f, 0.023910522f, -0.002227783f,
			0.001586914f, 7.6294E-05f, -9.1553E-05f, -0.001693726f, 0.001937866f, -0.024932861f, -0.004394531f, -0.15011597f, -0.17478943f, -0.99624634f,
			0.95648193f, 0.13031006f, 0.1462555f, -0.002822876f, 0.022857666f, -0.002487183f, 0.001480103f, 7.6294E-05f, -0.000106812f, -0.001785278f,
			0.001617432f, -0.025909424f, -0.00831604f, -0.15130615f, -0.19805908f, -1.0146179f, 0.9351959f, 0.10916138f, 0.14367676f, -0.006134033f,
			0.02178955f, -0.002700806f, 0.00138855f, 6.1035E-05f, -0.000106812f, -0.001907349f, 0.001266479f, -0.02684021f, -0.012420654f, -0.15196228f,
			-0.22198486f, -1.0319366f, 0.9130554f, 0.088775635f, 0.14067078f, -0.009231567f, 0.020690918f, -0.002883911f, 0.001296997f, 6.1035E-05f,
			-0.00012207f, -0.00201416f, 0.000869751f, -0.02772522f, -0.016708374f, -0.15206909f, -0.24650574f, -1.0481567f, 0.89009094f, 0.06916809f,
			0.13729858f, -0.012115479f, 0.019577026f, -0.003051758f, 0.001205444f, 4.5776E-05f, -0.000137329f, -0.002120972f, 0.000442505f, -0.028533936f,
			-0.0211792f, -0.15159607f, -0.2715912f, -1.0632172f, 0.8663635f, 0.050354004f, 0.1335907f, -0.014801025f, 0.018463135f, -0.003173828f,
			0.001113892f, 4.5776E-05f, -0.000152588f, -0.002243042f, -3.0518E-05f, -0.029281616f, -0.025817871f, -0.15049744f, -0.2972107f, -1.0771179f,
			0.84194946f, 0.03237915f, 0.12956238f, -0.01725769f, 0.017349243f, -0.00328064f, 0.001037598f, 3.0518E-05f, -0.000167847f, -0.002349854f,
			-0.000549316f, -0.029937744f, -0.03060913f, -0.1487732f, -0.32331848f, -1.0897827f, 0.816864f, 0.015228271f, 0.1252594f, -0.01953125f,
			0.016235352f, -0.003372192f, 0.000961304f, 3.0518E-05f, -0.000198364f, -0.002456665f, -0.001098633f, -0.030532837f, -0.03555298f, -0.1463623f,
			-0.34986877f, -1.1012115f, 0.791214f, -0.001068115f, 0.12069702f, -0.021575928f, 0.01512146f, -0.003417969f, 0.00088501f, 3.0518E-05f,
			-0.000213623f, -0.002578735f, -0.001693726f, -0.03100586f, -0.040634155f, -0.14326477f, -0.37680054f, -1.1113739f, 0.7650299f, -0.01651001f,
			0.11592102f, -0.023422241f, 0.014022827f, -0.003463745f, 0.000808716f, 3.0518E-05f, -0.000244141f, -0.002685547f, -0.002334595f, -0.03138733f,
			-0.045837402f, -0.13945007f, -0.40408325f, -1.120224f, 0.7383728f, -0.031082153f, 0.110946655f, -0.02508545f, 0.012939453f, -0.003479004f,
			0.000747681f, 1.5259E-05f, -0.000259399f, -0.002792358f, -0.003005981f, -0.031661987f, -0.051132202f, -0.1348877f, -0.43165588f, -1.1277466f,
			0.71131897f, -0.044784546f, 0.1058197f, -0.026535034f, 0.011886597f, -0.003479004f, 0.000686646f, 1.5259E-05f, -0.000289917f, -0.00289917f,
			-0.003723145f, -0.031814575f, -0.056533813f, -0.12957764f, -0.45947266f, -1.1339264f, 0.6839142f, -0.057617188f, 0.10054016f, -0.027801514f,
			0.010848999f, -0.003463745f, 0.00062561f, 1.5259E-05f, -0.000320435f, -0.002990723f, -0.004486084f, -0.031845093f, -0.06199646f, -0.12347412f,
			-0.48747253f, -1.1387634f, 0.6562195f, -0.06959534f, 0.09516907f, -0.028884888f, 0.009841919f, -0.003433228f, 0.000579834f, 1.5259E-05f,
			-0.000366211f, -0.003082275f, -0.0052948f, -0.03173828f, -0.06752014f, -0.11657715f, -0.51560974f, -1.1422119f, 0.6282959f, -0.08068848f,
			0.08970642f, -0.029785156f, 0.008865356f, -0.003387451f, 0.000534058f, 1.5259E-05f, -0.000396729f, -0.003173828f, -0.006118774f, -0.03147888f,
			-0.07305908f, -0.1088562f, -0.54382324f, -1.1442871f, 0.6002197f, -0.090927124f, 0.08418274f, -0.030517578f, 0.007919312f, -0.003326416f,
			0.000473022f, 1.5259E-05f
		};

		// Token: 0x040000DE RID: 222
		private readonly int _Channel;

		// Token: 0x040000DF RID: 223
		private readonly float[] _Samples;

		// Token: 0x040000E0 RID: 224
		private readonly float _Scalefactor;

		// Token: 0x040000E1 RID: 225
		private readonly float[] _V1;

		// Token: 0x040000E2 RID: 226
		private readonly float[] _V2;

		// Token: 0x040000E3 RID: 227
		private float[] _TmpOut;

		// Token: 0x040000E4 RID: 228
		private float[] _ActualV;

		// Token: 0x040000E5 RID: 229
		private int _ActualWritePos;

		// Token: 0x040000E6 RID: 230
		private float[] _Eq;
	}
}
