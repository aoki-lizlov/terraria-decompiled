using System;

namespace Terraria.Utilities
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class UnifiedRandom
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x004E2236 File Offset: 0x004E0436
		public UnifiedRandom()
			: this(Environment.TickCount)
		{
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x004E2243 File Offset: 0x004E0443
		public UnifiedRandom(int Seed)
		{
			this.SetSeed(Seed);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x004E2260 File Offset: 0x004E0460
		public void SetSeed(int Seed)
		{
			for (int i = 0; i < this.SeedArray.Length; i++)
			{
				this.SeedArray[i] = 0;
			}
			int num = ((Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed));
			int num2 = 161803398 - num;
			this.SeedArray[55] = num2;
			int num3 = 1;
			for (int j = 1; j < 55; j++)
			{
				int num4 = 21 * j % 55;
				this.SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = this.SeedArray[num4];
			}
			for (int k = 1; k < 5; k++)
			{
				for (int l = 1; l < 56; l++)
				{
					this.SeedArray[l] -= this.SeedArray[1 + (l + 30) % 55];
					if (this.SeedArray[l] < 0)
					{
						this.SeedArray[l] += int.MaxValue;
					}
				}
			}
			this.inext = 0U;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x004E2360 File Offset: 0x004E0560
		protected double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x004E2374 File Offset: 0x004E0574
		private int InternalSample()
		{
			uint num = this.inext + 1U;
			if (num > 55U)
			{
				num = 1U;
			}
			uint num2 = num + 21U;
			if (num2 > 55U)
			{
				num2 -= 55U;
			}
			int[] seedArray = this.SeedArray;
			int num3 = seedArray[(int)num] - seedArray[(int)num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			num3 += (num3 >> 31) & int.MaxValue;
			seedArray[(int)num] = num3;
			this.inext = num;
			return num3;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x004E23D4 File Offset: 0x004E05D4
		public int Peek()
		{
			uint num = this.inext + 1U;
			if (num > 55U)
			{
				num = 1U;
			}
			uint num2 = num + 21U;
			if (num2 > 55U)
			{
				num2 -= 55U;
			}
			return this.SeedArray[(int)num] - this.SeedArray[(int)num2];
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x004E2411 File Offset: 0x004E0611
		public int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x004E241C File Offset: 0x004E061C
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x004E245C File Offset: 0x004E065C
		public int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x004E24A7 File Offset: 0x004E06A7
		public int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", "maxValue must be positive.");
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x004E24C7 File Offset: 0x004E06C7
		public double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x004E24D0 File Offset: 0x004E06D0
		public void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.InternalSample() % 256);
			}
		}

		// Token: 0x040012D3 RID: 4819
		private const int MBIG = 2147483647;

		// Token: 0x040012D4 RID: 4820
		private const int MSEED = 161803398;

		// Token: 0x040012D5 RID: 4821
		private const int MZ = 0;

		// Token: 0x040012D6 RID: 4822
		private uint inext;

		// Token: 0x040012D7 RID: 4823
		private int[] SeedArray = new int[56];
	}
}
