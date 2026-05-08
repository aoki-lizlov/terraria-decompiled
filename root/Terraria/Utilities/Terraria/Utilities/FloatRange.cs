using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Terraria.Utilities.Terraria.Utilities
{
	// Token: 0x020000D9 RID: 217
	public struct FloatRange
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x004E2D9F File Offset: 0x004E0F9F
		public FloatRange(float minimum, float maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x004E2DAF File Offset: 0x004E0FAF
		public bool Contains(float f)
		{
			return this.Minimum <= f && f <= this.Maximum;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x004E2DC8 File Offset: 0x004E0FC8
		public float Lerp(float amount)
		{
			return MathHelper.Lerp(this.Minimum, this.Maximum, amount);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x004E2DDC File Offset: 0x004E0FDC
		public static FloatRange operator *(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum * scale, range.Maximum * scale);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x004E2DF3 File Offset: 0x004E0FF3
		public static FloatRange operator *(float scale, FloatRange range)
		{
			return range * scale;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x004E2DFC File Offset: 0x004E0FFC
		public static FloatRange operator /(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum / scale, range.Maximum / scale);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x004E2E13 File Offset: 0x004E1013
		public static FloatRange operator /(float scale, FloatRange range)
		{
			return range / scale;
		}

		// Token: 0x040012DD RID: 4829
		[JsonProperty("Min")]
		public readonly float Minimum;

		// Token: 0x040012DE RID: 4830
		[JsonProperty("Max")]
		public readonly float Maximum;
	}
}
