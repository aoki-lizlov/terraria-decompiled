using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200043E RID: 1086
	public class OriginalNatureRenderer : INatureRenderer
	{
		// Token: 0x060030DD RID: 12509 RVA: 0x005BDFF4 File Offset: 0x005BC1F4
		public void DrawNature(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, SideFlags seams = SideFlags.None)
		{
			Main.spriteBatch.Draw(texture, position, new Rectangle?(sourceRectangle), color, rotation, origin, scale, effects, layerDepth);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x005BE020 File Offset: 0x005BC220
		public void DrawGlowmask(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			Main.spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x00009E46 File Offset: 0x00008046
		public void DrawAfterAllObjects(SpriteBatchBeginner beginner)
		{
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0000357B File Offset: 0x0000177B
		public OriginalNatureRenderer()
		{
		}
	}
}
