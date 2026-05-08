using System;

namespace Terraria.UI
{
	// Token: 0x020000F4 RID: 244
	public struct StyleDimension
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x004E7E60 File Offset: 0x004E6060
		public StyleDimension(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x004E7E60 File Offset: 0x004E6060
		public void Set(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x004E7E70 File Offset: 0x004E6070
		public float GetValue(float containerSize)
		{
			return this.Pixels + this.Precent * containerSize;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x004E7E81 File Offset: 0x004E6081
		public static StyleDimension FromPixels(float pixels)
		{
			return new StyleDimension(pixels, 0f);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x004E7E8E File Offset: 0x004E608E
		public static StyleDimension FromPercent(float percent)
		{
			return new StyleDimension(0f, percent);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x004E7E9B File Offset: 0x004E609B
		public static StyleDimension FromPixelsAndPercent(float pixels, float percent)
		{
			return new StyleDimension(pixels, percent);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x004E7EA4 File Offset: 0x004E60A4
		// Note: this type is marked as 'beforefieldinit'.
		static StyleDimension()
		{
		}

		// Token: 0x04001340 RID: 4928
		public static StyleDimension Fill = new StyleDimension(0f, 1f);

		// Token: 0x04001341 RID: 4929
		public static StyleDimension Empty = new StyleDimension(0f, 0f);

		// Token: 0x04001342 RID: 4930
		public float Pixels;

		// Token: 0x04001343 RID: 4931
		public float Precent;
	}
}
