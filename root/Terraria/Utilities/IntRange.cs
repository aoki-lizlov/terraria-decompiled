using System;
using Newtonsoft.Json;

namespace Terraria.Utilities
{
	// Token: 0x020000D2 RID: 210
	public struct IntRange
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x004E192C File Offset: 0x004DFB2C
		public IntRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x004E193C File Offset: 0x004DFB3C
		public static IntRange operator *(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum * scale), (int)((float)range.Maximum * scale));
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x004E1957 File Offset: 0x004DFB57
		public static IntRange operator *(float scale, IntRange range)
		{
			return range * scale;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x004E1960 File Offset: 0x004DFB60
		public static IntRange operator /(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum / scale), (int)((float)range.Maximum / scale));
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x004E197B File Offset: 0x004DFB7B
		public static IntRange operator /(float scale, IntRange range)
		{
			return range / scale;
		}

		// Token: 0x040012C8 RID: 4808
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x040012C9 RID: 4809
		[JsonProperty("Max")]
		public readonly int Maximum;
	}
}
