using System;

namespace System
{
	// Token: 0x02000139 RID: 313
	public class Random
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00033667 File Offset: 0x00031867
		public Random()
			: this(Random.GenerateSeed())
		{
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00033674 File Offset: 0x00031874
		public Random(int Seed)
		{
			int num = 0;
			int num2 = ((Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed));
			int num3 = 161803398 - num2;
			this._seedArray[55] = num3;
			int num4 = 1;
			for (int i = 1; i < 55; i++)
			{
				if ((num += 21) >= 55)
				{
					num -= 55;
				}
				this._seedArray[num] = num4;
				num4 = num3 - num4;
				if (num4 < 0)
				{
					num4 += int.MaxValue;
				}
				num3 = this._seedArray[num];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					int num5 = k + 30;
					if (num5 >= 55)
					{
						num5 -= 55;
					}
					this._seedArray[k] -= this._seedArray[1 + num5];
					if (this._seedArray[k] < 0)
					{
						this._seedArray[k] += int.MaxValue;
					}
				}
			}
			this._inext = 0;
			this._inextp = 21;
			Seed = 1;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00033787 File Offset: 0x00031987
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003379C File Offset: 0x0003199C
		private int InternalSample()
		{
			int num = this._inext;
			int num2 = this._inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this._seedArray[num] - this._seedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this._seedArray[num] = num3;
			this._inext = num;
			this._inextp = num2;
			return num3;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00033810 File Offset: 0x00031A10
		private static int GenerateSeed()
		{
			Random random = Random.t_threadRandom;
			if (random == null)
			{
				Random random2 = Random.s_globalRandom;
				int num;
				lock (random2)
				{
					num = Random.s_globalRandom.Next();
				}
				random = new Random(num);
				Random.t_threadRandom = random;
			}
			return random.Next();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00033870 File Offset: 0x00031A70
		private unsafe static int GenerateGlobalSeed()
		{
			int num;
			Interop.GetRandomBytes((byte*)(&num), 4);
			return num;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00033887 File Offset: 0x00031A87
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00033890 File Offset: 0x00031A90
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000338D0 File Offset: 0x00031AD0
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", SR.Format("'{0}' cannot be greater than {1}.", "minValue", "maxValue"));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003392A File Offset: 0x00031B2A
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", SR.Format("'{0}' must be greater than zero.", "maxValue"));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00033954 File Offset: 0x00031B54
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003395C File Offset: 0x00031B5C
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)this.InternalSample();
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00033990 File Offset: 0x00031B90
		public unsafe virtual void NextBytes(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = (byte)this.Next();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000339BF File Offset: 0x00031BBF
		// Note: this type is marked as 'beforefieldinit'.
		static Random()
		{
		}

		// Token: 0x04001148 RID: 4424
		private const int MBIG = 2147483647;

		// Token: 0x04001149 RID: 4425
		private const int MSEED = 161803398;

		// Token: 0x0400114A RID: 4426
		private const int MZ = 0;

		// Token: 0x0400114B RID: 4427
		private int _inext;

		// Token: 0x0400114C RID: 4428
		private int _inextp;

		// Token: 0x0400114D RID: 4429
		private int[] _seedArray = new int[56];

		// Token: 0x0400114E RID: 4430
		[ThreadStatic]
		private static Random t_threadRandom;

		// Token: 0x0400114F RID: 4431
		private static readonly Random s_globalRandom = new Random(Random.GenerateGlobalSeed());
	}
}
