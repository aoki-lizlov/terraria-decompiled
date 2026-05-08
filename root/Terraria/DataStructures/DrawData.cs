using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000594 RID: 1428
	public struct DrawData
	{
		// Token: 0x0600385B RID: 14427 RVA: 0x00632038 File Offset: 0x00630238
		public DrawData(Texture2D texture, Vector2 position, Color color)
		{
			this.texture = texture;
			this.position = position;
			this.color = color;
			this.destinationRectangle = default(Rectangle);
			this.sourceRect = DrawData.nullRectangle;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x006320B0 File Offset: 0x006302B0
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color)
		{
			this.texture = texture;
			this.position = position;
			this.color = color;
			this.destinationRectangle = default(Rectangle);
			this.sourceRect = sourceRect;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x00632124 File Offset: 0x00630324
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.position = position;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.scale = new Vector2(scale, scale);
			this.effect = effect;
			this.destinationRectangle = default(Rectangle);
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x00632198 File Offset: 0x00630398
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.position = position;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.scale = scale;
			this.effect = effect;
			this.destinationRectangle = default(Rectangle);
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x00632204 File Offset: 0x00630404
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.color = color;
			this.position = Vector2.Zero;
			this.sourceRect = DrawData.nullRectangle;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x0063227C File Offset: 0x0063047C
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.color = color;
			this.position = Vector2.Zero;
			this.sourceRect = sourceRect;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x006322F0 File Offset: 0x006304F0
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.effect = effect;
			this.position = Vector2.Zero;
			this.scale = Vector2.One;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x00632360 File Offset: 0x00630560
		public void Draw(SpriteBatch sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect, 0f);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect, 0f);
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x006323E8 File Offset: 0x006305E8
		public void Draw(SpriteDrawBuffer sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect);
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static DrawData()
		{
		}

		// Token: 0x04005C80 RID: 23680
		public Texture2D texture;

		// Token: 0x04005C81 RID: 23681
		public Vector2 position;

		// Token: 0x04005C82 RID: 23682
		public Rectangle destinationRectangle;

		// Token: 0x04005C83 RID: 23683
		public Rectangle? sourceRect;

		// Token: 0x04005C84 RID: 23684
		public Color color;

		// Token: 0x04005C85 RID: 23685
		public float rotation;

		// Token: 0x04005C86 RID: 23686
		public Vector2 origin;

		// Token: 0x04005C87 RID: 23687
		public Vector2 scale;

		// Token: 0x04005C88 RID: 23688
		public SpriteEffects effect;

		// Token: 0x04005C89 RID: 23689
		public int shader;

		// Token: 0x04005C8A RID: 23690
		public bool ignorePlayerRotation;

		// Token: 0x04005C8B RID: 23691
		public readonly bool useDestinationRectangle;

		// Token: 0x04005C8C RID: 23692
		public static Rectangle? nullRectangle;
	}
}
