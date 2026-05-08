using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200043D RID: 1085
	public interface INatureRenderer
	{
		// Token: 0x060030DA RID: 12506
		void DrawNature(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, SideFlags seams = SideFlags.None);

		// Token: 0x060030DB RID: 12507
		void DrawGlowmask(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

		// Token: 0x060030DC RID: 12508
		void DrawAfterAllObjects(SpriteBatchBeginner beginner);
	}
}
