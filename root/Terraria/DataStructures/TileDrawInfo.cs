using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x020005A4 RID: 1444
	public class TileDrawInfo
	{
		// Token: 0x0600390D RID: 14605 RVA: 0x00651292 File Offset: 0x0064F492
		public TileDrawInfo()
		{
		}

		// Token: 0x04005D5C RID: 23900
		public Tile tileCache;

		// Token: 0x04005D5D RID: 23901
		public ushort typeCache;

		// Token: 0x04005D5E RID: 23902
		public short tileFrameX;

		// Token: 0x04005D5F RID: 23903
		public short tileFrameY;

		// Token: 0x04005D60 RID: 23904
		public Texture2D drawTexture;

		// Token: 0x04005D61 RID: 23905
		public Color tileLight;

		// Token: 0x04005D62 RID: 23906
		public int tileTop;

		// Token: 0x04005D63 RID: 23907
		public int tileWidth;

		// Token: 0x04005D64 RID: 23908
		public int tileHeight;

		// Token: 0x04005D65 RID: 23909
		public int halfBrickHeight;

		// Token: 0x04005D66 RID: 23910
		public int addFrY;

		// Token: 0x04005D67 RID: 23911
		public int addFrX;

		// Token: 0x04005D68 RID: 23912
		public SpriteEffects tileSpriteEffect;

		// Token: 0x04005D69 RID: 23913
		public Texture2D glowTexture;

		// Token: 0x04005D6A RID: 23914
		public Rectangle glowSourceRect;

		// Token: 0x04005D6B RID: 23915
		public Color glowColor;

		// Token: 0x04005D6C RID: 23916
		public Vector3[] colorSlices = new Vector3[9];

		// Token: 0x04005D6D RID: 23917
		public Color finalColor;

		// Token: 0x04005D6E RID: 23918
		public Color colorTint;
	}
}
