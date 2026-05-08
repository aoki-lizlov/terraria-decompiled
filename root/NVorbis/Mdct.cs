using System;
using System.Collections.Generic;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x0200000D RID: 13
	internal class Mdct : IMdct
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003FA4 File Offset: 0x000021A4
		public void Reverse(float[] samples, int sampleCount)
		{
			Mdct.MdctImpl mdctImpl;
			if (!this._setupCache.TryGetValue(sampleCount, ref mdctImpl))
			{
				mdctImpl = new Mdct.MdctImpl(sampleCount);
				this._setupCache[sampleCount] = mdctImpl;
			}
			mdctImpl.CalcReverse(samples);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003FDC File Offset: 0x000021DC
		public Mdct()
		{
		}

		// Token: 0x04000037 RID: 55
		private const float M_PI = 3.1415927f;

		// Token: 0x04000038 RID: 56
		private Dictionary<int, Mdct.MdctImpl> _setupCache = new Dictionary<int, Mdct.MdctImpl>();

		// Token: 0x02000043 RID: 67
		private class MdctImpl
		{
			// Token: 0x06000275 RID: 629 RVA: 0x00008874 File Offset: 0x00006A74
			public MdctImpl(int n)
			{
				this._n = n;
				this._n2 = n >> 1;
				this._n4 = this._n2 >> 1;
				this._n8 = this._n4 >> 1;
				this._ld = Utils.ilog(n) - 1;
				this._a = new float[this._n2];
				this._b = new float[this._n2];
				this._c = new float[this._n4];
				int i;
				int num = (i = 0);
				while (i < this._n4)
				{
					this._a[num] = (float)Math.Cos((double)((float)(4 * i) * 3.1415927f / (float)n));
					this._a[num + 1] = (float)(-(float)Math.Sin((double)((float)(4 * i) * 3.1415927f / (float)n)));
					this._b[num] = (float)Math.Cos((double)((float)(num + 1) * 3.1415927f / (float)n / 2f)) * 0.5f;
					this._b[num + 1] = (float)Math.Sin((double)((float)(num + 1) * 3.1415927f / (float)n / 2f)) * 0.5f;
					i++;
					num += 2;
				}
				num = (i = 0);
				while (i < this._n8)
				{
					this._c[num] = (float)Math.Cos((double)((float)(2 * (num + 1)) * 3.1415927f / (float)n));
					this._c[num + 1] = (float)(-(float)Math.Sin((double)((float)(2 * (num + 1)) * 3.1415927f / (float)n)));
					i++;
					num += 2;
				}
				this._bitrev = new ushort[this._n8];
				for (int j = 0; j < this._n8; j++)
				{
					this._bitrev[j] = (ushort)(Utils.BitReverse((uint)j, this._ld - 3) << 2);
				}
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00008A30 File Offset: 0x00006C30
			internal void CalcReverse(float[] buffer)
			{
				float[] array = new float[this._n2];
				int i = this._n2 - 2;
				int num = 0;
				int num2 = 0;
				int n = this._n2;
				while (num2 != n)
				{
					array[i + 1] = buffer[num2] * this._a[num] - buffer[num2 + 2] * this._a[num + 1];
					array[i] = buffer[num2] * this._a[num + 1] + buffer[num2 + 2] * this._a[num];
					i -= 2;
					num += 2;
					num2 += 4;
				}
				num2 = this._n2 - 3;
				while (i >= 0)
				{
					array[i + 1] = -buffer[num2 + 2] * this._a[num] - -buffer[num2] * this._a[num + 1];
					array[i] = -buffer[num2 + 2] * this._a[num + 1] + -buffer[num2] * this._a[num];
					i -= 2;
					num += 2;
					num2 -= 4;
				}
				float[] array2 = array;
				int j = this._n2 - 8;
				int num3 = this._n4;
				int num4 = 0;
				int num5 = this._n4;
				int num6 = 0;
				while (j >= 0)
				{
					float num7 = array2[num3 + 1] - array2[num4 + 1];
					float num8 = array2[num3] - array2[num4];
					buffer[num5 + 1] = array2[num3 + 1] + array2[num4 + 1];
					buffer[num5] = array2[num3] + array2[num4];
					buffer[num6 + 1] = num7 * this._a[j + 4] - num8 * this._a[j + 5];
					buffer[num6] = num8 * this._a[j + 4] + num7 * this._a[j + 5];
					num7 = array2[num3 + 3] - array2[num4 + 3];
					num8 = array2[num3 + 2] - array2[num4 + 2];
					buffer[num5 + 3] = array2[num3 + 3] + array2[num4 + 3];
					buffer[num5 + 2] = array2[num3 + 2] + array2[num4 + 2];
					buffer[num6 + 3] = num7 * this._a[j] - num8 * this._a[j + 1];
					buffer[num6 + 2] = num8 * this._a[j] + num7 * this._a[j + 1];
					j -= 8;
					num5 += 4;
					num6 += 4;
					num3 += 4;
					num4 += 4;
				}
				int num9 = this._n >> 4;
				int num10 = this._n2 - 1;
				int n2 = this._n4;
				this.step3_iter0_loop(num9, buffer, num10 - 0, -this._n8);
				this.step3_iter0_loop(this._n >> 4, buffer, this._n2 - 1 - this._n4, -this._n8);
				int num11 = this._n >> 5;
				int num12 = this._n2 - 1;
				int n3 = this._n8;
				this.step3_inner_r_loop(num11, buffer, num12 - 0, -(this._n >> 4), 16);
				this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8, -(this._n >> 4), 16);
				this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8 * 2, -(this._n >> 4), 16);
				this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8 * 3, -(this._n >> 4), 16);
				int k;
				for (k = 2; k < this._ld - 3 >> 1; k++)
				{
					int num13 = this._n >> k + 2;
					int num14 = num13 >> 1;
					int num15 = 1 << k + 1;
					for (int l = 0; l < num15; l++)
					{
						this.step3_inner_r_loop(this._n >> k + 4, buffer, this._n2 - 1 - num13 * l, -num14, 1 << k + 3);
					}
				}
				while (k < this._ld - 6)
				{
					int num16 = this._n >> k + 2;
					int num17 = 1 << k + 3;
					int num18 = num16 >> 1;
					int num19 = this._n >> k + 6;
					int num20 = 1 << k + 1;
					int num21 = this._n2 - 1;
					int num22 = 0;
					for (int m = num19; m > 0; m--)
					{
						this.step3_inner_s_loop(num20, buffer, num21, -num18, num22, num17, num16);
						num22 += num17 * 4;
						num21 -= 8;
					}
					k++;
				}
				this.step3_inner_s_loop_ld654(this._n >> 5, buffer, this._n2 - 1, this._n);
				int num23 = 0;
				int num24 = this._n4 - 4;
				int num25 = this._n2 - 4;
				while (num24 >= 0)
				{
					int num26 = (int)this._bitrev[num23];
					array2[num25 + 3] = buffer[num26];
					array2[num25 + 2] = buffer[num26 + 1];
					array2[num24 + 3] = buffer[num26 + 2];
					array2[num24 + 2] = buffer[num26 + 3];
					num26 = (int)this._bitrev[num23 + 1];
					array2[num25 + 1] = buffer[num26];
					array2[num25] = buffer[num26 + 1];
					array2[num24 + 1] = buffer[num26 + 2];
					array2[num24] = buffer[num26 + 3];
					num24 -= 4;
					num25 -= 4;
					num23 += 2;
				}
				int num27 = 0;
				int num28 = 0;
				int num29 = this._n2 - 4;
				while (num28 < num29)
				{
					float num30 = array2[num28] - array2[num29 + 2];
					float num31 = array2[num28 + 1] + array2[num29 + 3];
					float num32 = this._c[num27 + 1] * num30 + this._c[num27] * num31;
					float num33 = this._c[num27 + 1] * num31 - this._c[num27] * num30;
					float num34 = array2[num28] + array2[num29 + 2];
					float num35 = array2[num28 + 1] - array2[num29 + 3];
					array2[num28] = num34 + num32;
					array2[num28 + 1] = num35 + num33;
					array2[num29 + 2] = num34 - num32;
					array2[num29 + 3] = num33 - num35;
					num30 = array2[num28 + 2] - array2[num29];
					num31 = array2[num28 + 3] + array2[num29 + 1];
					num32 = this._c[num27 + 3] * num30 + this._c[num27 + 2] * num31;
					num33 = this._c[num27 + 3] * num31 - this._c[num27 + 2] * num30;
					num34 = array2[num28 + 2] + array2[num29];
					num35 = array2[num28 + 3] - array2[num29 + 1];
					array2[num28 + 2] = num34 + num32;
					array2[num28 + 3] = num35 + num33;
					array2[num29] = num34 - num32;
					array2[num29 + 1] = num33 - num35;
					num27 += 4;
					num28 += 4;
					num29 -= 4;
				}
				int num36 = this._n2 - 8;
				int num37 = this._n2 - 8;
				int num38 = 0;
				int num39 = this._n2 - 4;
				int num40 = this._n2;
				int num41 = this._n - 4;
				while (num37 >= 0)
				{
					float num42 = array[num37 + 6] * this._b[num36 + 7] - array[num37 + 7] * this._b[num36 + 6];
					float num43 = -array[num37 + 6] * this._b[num36 + 6] - array[num37 + 7] * this._b[num36 + 7];
					buffer[num38] = num42;
					buffer[num39 + 3] = -num42;
					buffer[num40] = num43;
					buffer[num41 + 3] = num43;
					float num44 = array[num37 + 4] * this._b[num36 + 5] - array[num37 + 5] * this._b[num36 + 4];
					float num45 = -array[num37 + 4] * this._b[num36 + 4] - array[num37 + 5] * this._b[num36 + 5];
					buffer[num38 + 1] = num44;
					buffer[num39 + 2] = -num44;
					buffer[num40 + 1] = num45;
					buffer[num41 + 2] = num45;
					num42 = array[num37 + 2] * this._b[num36 + 3] - array[num37 + 3] * this._b[num36 + 2];
					num43 = -array[num37 + 2] * this._b[num36 + 2] - array[num37 + 3] * this._b[num36 + 3];
					buffer[num38 + 2] = num42;
					buffer[num39 + 1] = -num42;
					buffer[num40 + 2] = num43;
					buffer[num41 + 1] = num43;
					num44 = array[num37] * this._b[num36 + 1] - array[num37 + 1] * this._b[num36];
					num45 = -array[num37] * this._b[num36] - array[num37 + 1] * this._b[num36 + 1];
					buffer[num38 + 3] = num44;
					buffer[num39] = -num44;
					buffer[num40 + 3] = num45;
					buffer[num41] = num45;
					num36 -= 8;
					num37 -= 8;
					num38 += 4;
					num40 += 4;
					num39 -= 4;
					num41 -= 4;
				}
			}

			// Token: 0x06000277 RID: 631 RVA: 0x000092C4 File Offset: 0x000074C4
			private void step3_iter0_loop(int n, float[] e, int i_off, int k_off)
			{
				int num = i_off;
				int num2 = num + k_off;
				int num3 = 0;
				for (int i = n >> 2; i > 0; i--)
				{
					float num4 = e[num] - e[num2];
					float num5 = e[num - 1] - e[num2 - 1];
					e[num] += e[num2];
					e[num - 1] += e[num2 - 1];
					e[num2] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 1] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += 8;
					num4 = e[num - 2] - e[num2 - 2];
					num5 = e[num - 3] - e[num2 - 3];
					e[num - 2] += e[num2 - 2];
					e[num - 3] += e[num2 - 3];
					e[num2 - 2] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 3] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += 8;
					num4 = e[num - 4] - e[num2 - 4];
					num5 = e[num - 5] - e[num2 - 5];
					e[num - 4] += e[num2 - 4];
					e[num - 5] += e[num2 - 5];
					e[num2 - 4] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 5] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += 8;
					num4 = e[num - 6] - e[num2 - 6];
					num5 = e[num - 7] - e[num2 - 7];
					e[num - 6] += e[num2 - 6];
					e[num - 7] += e[num2 - 7];
					e[num2 - 6] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 7] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += 8;
					num -= 8;
					num2 -= 8;
				}
			}

			// Token: 0x06000278 RID: 632 RVA: 0x000094E4 File Offset: 0x000076E4
			private void step3_inner_r_loop(int lim, float[] e, int d0, int k_off, int k1)
			{
				int num = d0;
				int num2 = num + k_off;
				int num3 = 0;
				for (int i = lim >> 2; i > 0; i--)
				{
					float num4 = e[num] - e[num2];
					float num5 = e[num - 1] - e[num2 - 1];
					e[num] += e[num2];
					e[num - 1] += e[num2 - 1];
					e[num2] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 1] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += k1;
					num4 = e[num - 2] - e[num2 - 2];
					num5 = e[num - 3] - e[num2 - 3];
					e[num - 2] += e[num2 - 2];
					e[num - 3] += e[num2 - 3];
					e[num2 - 2] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 3] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += k1;
					num4 = e[num - 4] - e[num2 - 4];
					num5 = e[num - 5] - e[num2 - 5];
					e[num - 4] += e[num2 - 4];
					e[num - 5] += e[num2 - 5];
					e[num2 - 4] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 5] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += k1;
					num4 = e[num - 6] - e[num2 - 6];
					num5 = e[num - 7] - e[num2 - 7];
					e[num - 6] += e[num2 - 6];
					e[num - 7] += e[num2 - 7];
					e[num2 - 6] = num4 * this._a[num3] - num5 * this._a[num3 + 1];
					e[num2 - 7] = num5 * this._a[num3] + num4 * this._a[num3 + 1];
					num3 += k1;
					num -= 8;
					num2 -= 8;
				}
			}

			// Token: 0x06000279 RID: 633 RVA: 0x00009710 File Offset: 0x00007910
			private void step3_inner_s_loop(int n, float[] e, int i_off, int k_off, int a, int a_off, int k0)
			{
				float num = this._a[a];
				float num2 = this._a[a + 1];
				float num3 = this._a[a + a_off];
				float num4 = this._a[a + a_off + 1];
				float num5 = this._a[a + a_off * 2];
				float num6 = this._a[a + a_off * 2 + 1];
				float num7 = this._a[a + a_off * 3];
				float num8 = this._a[a + a_off * 3 + 1];
				int num9 = i_off;
				int num10 = num9 + k_off;
				for (int i = n; i > 0; i--)
				{
					float num11 = e[num9] - e[num10];
					float num12 = e[num9 - 1] - e[num10 - 1];
					e[num9] += e[num10];
					e[num9 - 1] += e[num10 - 1];
					e[num10] = num11 * num - num12 * num2;
					e[num10 - 1] = num12 * num + num11 * num2;
					num11 = e[num9 - 2] - e[num10 - 2];
					num12 = e[num9 - 3] - e[num10 - 3];
					e[num9 - 2] += e[num10 - 2];
					e[num9 - 3] += e[num10 - 3];
					e[num10 - 2] = num11 * num3 - num12 * num4;
					e[num10 - 3] = num12 * num3 + num11 * num4;
					num11 = e[num9 - 4] - e[num10 - 4];
					num12 = e[num9 - 5] - e[num10 - 5];
					e[num9 - 4] += e[num10 - 4];
					e[num9 - 5] += e[num10 - 5];
					e[num10 - 4] = num11 * num5 - num12 * num6;
					e[num10 - 5] = num12 * num5 + num11 * num6;
					num11 = e[num9 - 6] - e[num10 - 6];
					num12 = e[num9 - 7] - e[num10 - 7];
					e[num9 - 6] += e[num10 - 6];
					e[num9 - 7] += e[num10 - 7];
					e[num10 - 6] = num11 * num7 - num12 * num8;
					e[num10 - 7] = num12 * num7 + num11 * num8;
					num9 -= k0;
					num10 -= k0;
				}
			}

			// Token: 0x0600027A RID: 634 RVA: 0x00009950 File Offset: 0x00007B50
			private void step3_inner_s_loop_ld654(int n, float[] e, int i_off, int base_n)
			{
				int num = base_n >> 3;
				float num2 = this._a[num];
				int i = i_off;
				int num3 = i - 16 * n;
				while (i > num3)
				{
					float num4 = e[i] - e[i - 8];
					float num5 = e[i - 1] - e[i - 9];
					e[i] += e[i - 8];
					e[i - 1] += e[i - 9];
					e[i - 8] = num4;
					e[i - 9] = num5;
					num4 = e[i - 2] - e[i - 10];
					num5 = e[i - 3] - e[i - 11];
					e[i - 2] += e[i - 10];
					e[i - 3] += e[i - 11];
					e[i - 10] = (num4 + num5) * num2;
					e[i - 11] = (num5 - num4) * num2;
					num4 = e[i - 12] - e[i - 4];
					num5 = e[i - 5] - e[i - 13];
					e[i - 4] += e[i - 12];
					e[i - 5] += e[i - 13];
					e[i - 12] = num5;
					e[i - 13] = num4;
					num4 = e[i - 14] - e[i - 6];
					num5 = e[i - 7] - e[i - 15];
					e[i - 6] += e[i - 14];
					e[i - 7] += e[i - 15];
					e[i - 14] = (num4 + num5) * num2;
					e[i - 15] = (num4 - num5) * num2;
					this.iter_54(e, i);
					this.iter_54(e, i - 8);
					i -= 16;
				}
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00009AEC File Offset: 0x00007CEC
			private void iter_54(float[] e, int z)
			{
				float num = e[z] - e[z - 4];
				float num2 = e[z] + e[z - 4];
				float num3 = e[z - 2] + e[z - 6];
				float num4 = e[z - 2] - e[z - 6];
				e[z] = num2 + num3;
				e[z - 2] = num2 - num3;
				float num5 = e[z - 3] - e[z - 7];
				e[z - 4] = num + num5;
				e[z - 6] = num - num5;
				float num6 = e[z - 1] - e[z - 5];
				float num7 = e[z - 1] + e[z - 5];
				float num8 = e[z - 3] + e[z - 7];
				e[z - 1] = num7 + num8;
				e[z - 3] = num7 - num8;
				e[z - 5] = num6 - num4;
				e[z - 7] = num6 + num4;
			}

			// Token: 0x040000FC RID: 252
			private readonly int _n;

			// Token: 0x040000FD RID: 253
			private readonly int _n2;

			// Token: 0x040000FE RID: 254
			private readonly int _n4;

			// Token: 0x040000FF RID: 255
			private readonly int _n8;

			// Token: 0x04000100 RID: 256
			private readonly int _ld;

			// Token: 0x04000101 RID: 257
			private readonly float[] _a;

			// Token: 0x04000102 RID: 258
			private readonly float[] _b;

			// Token: 0x04000103 RID: 259
			private readonly float[] _c;

			// Token: 0x04000104 RID: 260
			private readonly ushort[] _bitrev;
		}
	}
}
