using System;
using System.Runtime.CompilerServices;

namespace Terraria.Utilities
{
	// Token: 0x020000CF RID: 207
	public struct FastRandom
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x004E1386 File Offset: 0x004DF586
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x004E138E File Offset: 0x004DF58E
		public ulong Seed
		{
			[CompilerGenerated]
			get
			{
				return this.<Seed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Seed>k__BackingField = value;
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x004E1397 File Offset: 0x004DF597
		public FastRandom(ulong seed)
		{
			this = default(FastRandom);
			this.Seed = seed;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x004E13A7 File Offset: 0x004DF5A7
		public FastRandom(int seed)
		{
			this = default(FastRandom);
			this.Seed = (ulong)((long)seed);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x004E13B8 File Offset: 0x004DF5B8
		public FastRandom WithModifier(ulong modifier)
		{
			return new FastRandom(FastRandom.NextSeed(modifier) ^ this.Seed);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x004E13CC File Offset: 0x004DF5CC
		public FastRandom WithModifier(int x, int y)
		{
			return this.WithModifier((ulong)((long)x + (long)((ulong)(-1640531527)) + ((long)y << 6) + (long)((ulong)((long)y) >> 2)));
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x004E13E8 File Offset: 0x004DF5E8
		public static FastRandom CreateWithRandomSeed()
		{
			return new FastRandom((ulong)((long)Guid.NewGuid().GetHashCode()));
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x004E140E File Offset: 0x004DF60E
		public void NextSeed()
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x004E1421 File Offset: 0x004DF621
		private int NextBits(int bits)
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
			return (int)(this.Seed >> 48 - bits);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x004E1443 File Offset: 0x004DF643
		public float NextFloat()
		{
			return (float)this.NextBits(24) * 5.9604645E-08f;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x004E1454 File Offset: 0x004DF654
		public double NextDouble()
		{
			return (double)((float)this.NextBits(32) * 4.656613E-10f);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x004E1468 File Offset: 0x004DF668
		public int Next(int max)
		{
			if ((max & -max) == max)
			{
				return (int)((long)max * (long)this.NextBits(31) >> 31);
			}
			int num;
			int num2;
			do
			{
				num = this.NextBits(31);
				num2 = num % max;
			}
			while (num - num2 + (max - 1) < 0);
			return num2;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x004E14A5 File Offset: 0x004DF6A5
		public int Next(int min, int max)
		{
			return this.Next(max - min) + min;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0040E949 File Offset: 0x0040CB49
		private static ulong NextSeed(ulong seed)
		{
			return (seed * 25214903917UL + 11UL) & 281474976710655UL;
		}

		// Token: 0x040012C4 RID: 4804
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x040012C5 RID: 4805
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x040012C6 RID: 4806
		private const ulong RANDOM_MASK = 281474976710655UL;

		// Token: 0x040012C7 RID: 4807
		[CompilerGenerated]
		private ulong <Seed>k__BackingField;
	}
}
