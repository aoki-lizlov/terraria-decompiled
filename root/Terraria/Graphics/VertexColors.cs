using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x020001D9 RID: 473
	public struct VertexColors
	{
		// Token: 0x06001FE2 RID: 8162 RVA: 0x0051EB87 File Offset: 0x0051CD87
		public VertexColors(Color color)
		{
			this.TopLeftColor = color;
			this.TopRightColor = color;
			this.BottomRightColor = color;
			this.BottomLeftColor = color;
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0051EBA5 File Offset: 0x0051CDA5
		public VertexColors(Color topLeft, Color topRight, Color bottomRight, Color bottomLeft)
		{
			this.TopLeftColor = topLeft;
			this.TopRightColor = topRight;
			this.BottomLeftColor = bottomLeft;
			this.BottomRightColor = bottomRight;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0051EBC4 File Offset: 0x0051CDC4
		public static implicit operator VertexColors(Color color)
		{
			return new VertexColors(color);
		}

		// Token: 0x04004A58 RID: 19032
		public Color TopLeftColor;

		// Token: 0x04004A59 RID: 19033
		public Color TopRightColor;

		// Token: 0x04004A5A RID: 19034
		public Color BottomLeftColor;

		// Token: 0x04004A5B RID: 19035
		public Color BottomRightColor;
	}
}
