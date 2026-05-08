using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200058F RID: 1423
	public class ColorSlidersSet
	{
		// Token: 0x06003843 RID: 14403 RVA: 0x00631798 File Offset: 0x0062F998
		public void SetHSL(Color color)
		{
			Vector3 vector = Main.rgbToHsl(color);
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x006317D0 File Offset: 0x0062F9D0
		public void SetHSL(Vector3 vector)
		{
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x006317F8 File Offset: 0x0062F9F8
		public Color GetColor()
		{
			Color color = Main.hslToRgb(this.Hue, this.Saturation, this.Luminance, byte.MaxValue);
			color.A = (byte)(this.Alpha * 255f);
			return color;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x00631837 File Offset: 0x0062FA37
		public Vector3 GetHSLVector()
		{
			return new Vector3(this.Hue, this.Saturation, this.Luminance);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x00631850 File Offset: 0x0062FA50
		public void ApplyToMainLegacyBars()
		{
			Main.hBar = this.Hue;
			Main.sBar = this.Saturation;
			Main.lBar = this.Luminance;
			Main.aBar = this.Alpha;
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x0063187E File Offset: 0x0062FA7E
		public ColorSlidersSet()
		{
		}

		// Token: 0x04005C6B RID: 23659
		public float Hue;

		// Token: 0x04005C6C RID: 23660
		public float Saturation;

		// Token: 0x04005C6D RID: 23661
		public float Luminance;

		// Token: 0x04005C6E RID: 23662
		public float Alpha = 1f;
	}
}
