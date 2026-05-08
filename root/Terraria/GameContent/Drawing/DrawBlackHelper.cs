using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000446 RID: 1094
	public struct DrawBlackHelper
	{
		// Token: 0x060031C7 RID: 12743 RVA: 0x005E2E8E File Offset: 0x005E108E
		public DrawBlackHelper(uint layer, Vector2 drawOffset)
		{
			this.layer = layer;
			this.drawOffset = drawOffset;
			this.y = 0;
			this.startX = 0;
			this.endX = 0;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x005E2EB3 File Offset: 0x005E10B3
		public void DrawBlack(int x, int y)
		{
			if (y == this.y && x == this.endX)
			{
				this.endX++;
				return;
			}
			this.EndStrip();
			this.y = y;
			this.startX = x;
			this.endX = x + 1;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x005E2EF4 File Offset: 0x005E10F4
		public void EndStrip()
		{
			if (this.startX == this.endX)
			{
				return;
			}
			Vector2 vector = new Vector2((float)(this.startX << 4), (float)(this.y << 4)) - Main.screenPosition + this.drawOffset;
			Main.tileBatch.SetLayer(this.layer, 0);
			Main.tileBatch.Draw(TextureAssets.BlackTile.Value, new Vector4(vector.X, vector.Y, (float)(this.endX - this.startX << 4), 16f), Color.Black);
		}

		// Token: 0x040057B8 RID: 22456
		private readonly uint layer;

		// Token: 0x040057B9 RID: 22457
		private readonly Vector2 drawOffset;

		// Token: 0x040057BA RID: 22458
		private int y;

		// Token: 0x040057BB RID: 22459
		private int startX;

		// Token: 0x040057BC RID: 22460
		private int endX;
	}
}
