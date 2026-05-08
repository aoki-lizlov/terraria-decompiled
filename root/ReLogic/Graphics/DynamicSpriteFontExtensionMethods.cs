using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReLogic.Graphics
{
	// Token: 0x02000084 RID: 132
	public static class DynamicSpriteFontExtensionMethods
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, string text, Vector2 position, Color color)
		{
			Vector2 one = Vector2.One;
			spriteFont.InternalDraw(text, spriteBatch, position, color, 0f, Vector2.Zero, ref one, 0, 0f);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BA9C File Offset: 0x00009C9C
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
		{
			Vector2 one = Vector2.One;
			spriteFont.InternalDraw(text.ToString(), spriteBatch, position, color, 0f, Vector2.Zero, ref one, 0, 0f);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, Vector2[] charOffsets = null, Color[] charColors = null)
		{
			Vector2 vector = default(Vector2);
			vector.X = scale;
			vector.Y = scale;
			spriteFont.InternalDraw(text, spriteBatch, position, color, rotation, origin, ref vector, effects, layerDepth, charOffsets, charColors, null);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000BB18 File Offset: 0x00009D18
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			Vector2 vector;
			vector..ctor(scale);
			spriteFont.InternalDraw(text.ToString(), spriteBatch, position, color, rotation, origin, ref vector, effects, layerDepth);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000BB48 File Offset: 0x00009D48
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			spriteFont.InternalDraw(text, spriteBatch, position, color, rotation, origin, ref scale, effects, layerDepth);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public static void DrawString(this SpriteBatch spriteBatch, DynamicSpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			spriteFont.InternalDraw(text.ToString(), spriteBatch, position, color, rotation, origin, ref scale, effects, layerDepth);
		}
	}
}
