using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BF RID: 959
	public struct ResourceDrawSettings
	{
		// Token: 0x06002D1E RID: 11550 RVA: 0x005A2468 File Offset: 0x005A0668
		public void Draw(SpriteBatch spriteBatch, ref bool isHovered)
		{
			int elementCount = this.ElementCount;
			Vector2 vector = this.TopLeftAnchor;
			Point point = Main.MouseScreen.ToPoint();
			for (int i = 0; i < elementCount; i++)
			{
				int num = i + this.ElementIndexOffset;
				Asset<Texture2D> asset;
				Vector2 vector2;
				float num2;
				Rectangle? rectangle;
				this.GetTextureMethod(num, this.ElementIndexOffset, this.ElementIndexOffset + elementCount - 1, out asset, out vector2, out num2, out rectangle);
				Rectangle rectangle2 = asset.Frame(1, 1, 0, 0, 0, 0);
				if (rectangle != null)
				{
					rectangle2 = rectangle.Value;
				}
				Vector2 vector3 = vector + vector2;
				Vector2 vector4 = this.OffsetSpriteAnchor + rectangle2.Size() * this.OffsetSpriteAnchorByTexturePercentile;
				Rectangle rectangle3 = rectangle2;
				rectangle3.X += (int)(vector3.X - vector4.X);
				rectangle3.Y += (int)(vector3.Y - vector4.Y);
				if (rectangle3.Contains(point))
				{
					isHovered = true;
				}
				spriteBatch.Draw(asset.Value, vector3, new Rectangle?(rectangle2), Color.White, 0f, vector4, num2, SpriteEffects.None, 0f);
				vector += this.OffsetPerDraw + rectangle2.Size() * this.OffsetPerDrawByTexturePercentile;
			}
		}

		// Token: 0x04005492 RID: 21650
		public Vector2 TopLeftAnchor;

		// Token: 0x04005493 RID: 21651
		public int ElementCount;

		// Token: 0x04005494 RID: 21652
		public int ElementIndexOffset;

		// Token: 0x04005495 RID: 21653
		public ResourceDrawSettings.TextureGetter GetTextureMethod;

		// Token: 0x04005496 RID: 21654
		public Vector2 OffsetPerDraw;

		// Token: 0x04005497 RID: 21655
		public Vector2 OffsetPerDrawByTexturePercentile;

		// Token: 0x04005498 RID: 21656
		public Vector2 OffsetSpriteAnchor;

		// Token: 0x04005499 RID: 21657
		public Vector2 OffsetSpriteAnchorByTexturePercentile;

		// Token: 0x02000922 RID: 2338
		// (Invoke) Token: 0x060047E9 RID: 18409
		public delegate void TextureGetter(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> texture, out Vector2 drawOffset, out float drawScale, out Rectangle? sourceRect);
	}
}
