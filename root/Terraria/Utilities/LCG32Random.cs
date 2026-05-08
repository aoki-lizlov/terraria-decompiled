using System;

namespace Terraria.Utilities
{
	// Token: 0x020000CC RID: 204
	public struct LCG32Random
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x004E1165 File Offset: 0x004DF365
		public LCG32Random(uint seed)
		{
			this.state = seed;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x004E116E File Offset: 0x004DF36E
		public void Advance()
		{
			this.state = this.state * 2438952949U + 1U;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x004E1184 File Offset: 0x004DF384
		public uint Next(uint maxValue)
		{
			this.Advance();
			return (uint)((ulong)this.state * (ulong)maxValue >> 32);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x004E119A File Offset: 0x004DF39A
		public int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", "maxValue must be positive.");
			}
			return (int)this.Next((uint)maxValue);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x004E11B7 File Offset: 0x004DF3B7
		public int Next(int minValue, int maxValue)
		{
			return minValue + (int)this.Next((uint)(maxValue - minValue));
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x004E11C4 File Offset: 0x004DF3C4
		public double NextDouble()
		{
			this.Advance();
			return this.state / 4294967296.0;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x004E11DE File Offset: 0x004DF3DE
		public float NextFloat()
		{
			return (float)this.NextDouble();
		}

		// Token: 0x040012BE RID: 4798
		public uint state;
	}
}
