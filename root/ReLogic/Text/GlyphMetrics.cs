using System;

namespace ReLogic.Text
{
	// Token: 0x02000010 RID: 16
	public struct GlyphMetrics
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003D60 File Offset: 0x00001F60
		public float KernedWidth
		{
			get
			{
				return this.LeftPadding + this.CharacterWidth + this.RightPadding;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D76 File Offset: 0x00001F76
		public float KernedWidthOnNewLine
		{
			get
			{
				return Math.Max(0f, this.LeftPadding) + this.CharacterWidth + this.RightPadding;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D96 File Offset: 0x00001F96
		private GlyphMetrics(float leftPadding, float characterWidth, float rightPadding)
		{
			this.LeftPadding = leftPadding;
			this.CharacterWidth = characterWidth;
			this.RightPadding = rightPadding;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003DAD File Offset: 0x00001FAD
		public static GlyphMetrics FromKerningData(float leftPadding, float characterWidth, float rightPadding)
		{
			return new GlyphMetrics(leftPadding, characterWidth, rightPadding);
		}

		// Token: 0x04000023 RID: 35
		public readonly float LeftPadding;

		// Token: 0x04000024 RID: 36
		public readonly float CharacterWidth;

		// Token: 0x04000025 RID: 37
		public readonly float RightPadding;
	}
}
