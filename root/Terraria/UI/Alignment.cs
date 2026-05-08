using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000E6 RID: 230
	public struct Alignment
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x004E5E86 File Offset: 0x004E4086
		public Vector2 OffsetMultiplier
		{
			get
			{
				return new Vector2(this.HorizontalOffsetMultiplier, this.VerticalOffsetMultiplier);
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x004E5E99 File Offset: 0x004E4099
		private Alignment(float horizontal, float vertical)
		{
			this.HorizontalOffsetMultiplier = horizontal;
			this.VerticalOffsetMultiplier = vertical;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x004E5EAC File Offset: 0x004E40AC
		// Note: this type is marked as 'beforefieldinit'.
		static Alignment()
		{
		}

		// Token: 0x04001308 RID: 4872
		public static readonly Alignment TopLeft = new Alignment(0f, 0f);

		// Token: 0x04001309 RID: 4873
		public static readonly Alignment Top = new Alignment(0.5f, 0f);

		// Token: 0x0400130A RID: 4874
		public static readonly Alignment TopRight = new Alignment(1f, 0f);

		// Token: 0x0400130B RID: 4875
		public static readonly Alignment Left = new Alignment(0f, 0.5f);

		// Token: 0x0400130C RID: 4876
		public static readonly Alignment Center = new Alignment(0.5f, 0.5f);

		// Token: 0x0400130D RID: 4877
		public static readonly Alignment Right = new Alignment(1f, 0.5f);

		// Token: 0x0400130E RID: 4878
		public static readonly Alignment BottomLeft = new Alignment(0f, 1f);

		// Token: 0x0400130F RID: 4879
		public static readonly Alignment Bottom = new Alignment(0.5f, 1f);

		// Token: 0x04001310 RID: 4880
		public static readonly Alignment BottomRight = new Alignment(1f, 1f);

		// Token: 0x04001311 RID: 4881
		public readonly float VerticalOffsetMultiplier;

		// Token: 0x04001312 RID: 4882
		public readonly float HorizontalOffsetMultiplier;
	}
}
