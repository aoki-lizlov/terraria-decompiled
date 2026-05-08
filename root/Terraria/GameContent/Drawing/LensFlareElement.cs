using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200043B RID: 1083
	public struct LensFlareElement
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x005BDE78 File Offset: 0x005BC078
		public void Draw(SpriteBatch spriteBatch, Vector2 sunPosition, Vector2 screenCenterPosition, float intensity)
		{
			if (intensity == 0f)
			{
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			int availableAdvancedShadowsCount = localPlayer.availableAdvancedShadowsCount;
			Vector2 vector = localPlayer.GetAdvancedShadow(0).Position - localPlayer.GetAdvancedShadow(Math.Min(4, availableAdvancedShadowsCount - 1)).Position;
			float num = Vector2.Dot(vector.SafeNormalize(Vector2.UnitX), (sunPosition - screenCenterPosition).SafeNormalize(-Vector2.UnitY)) * vector.Length();
			for (int i = 0; i < this.RepeatTimes; i++)
			{
				float num2 = this.ScaleStart + this.ScaleOverIndex * (float)i;
				Color color = this.Color * (1f + this.IntensityOverIndex * (float)i) * intensity;
				float num3 = this.DistanceStart + this.DistanceAlongIndex * (float)i;
				num3 += num * -0.0002f;
				num3 %= 1f;
				Vector2 vector2 = Vector2.Lerp(sunPosition, screenCenterPosition, num3 * 2f);
				float num4 = (screenCenterPosition - sunPosition).ToRotation() + this.Rotation;
				if (this.Rotation == 0f)
				{
					num4 += Main.screenPosition.Y * 0.001f;
				}
				spriteBatch.Draw(this.Texture.Value, vector2, null, color, num4, this.Texture.Size() / 2f, num2, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x0400570E RID: 22286
		public Asset<Texture2D> Texture;

		// Token: 0x0400570F RID: 22287
		public int RepeatTimes;

		// Token: 0x04005710 RID: 22288
		public float ScaleStart;

		// Token: 0x04005711 RID: 22289
		public float ScaleOverIndex;

		// Token: 0x04005712 RID: 22290
		public float DistanceStart;

		// Token: 0x04005713 RID: 22291
		public float DistanceAlongIndex;

		// Token: 0x04005714 RID: 22292
		public Color Color;

		// Token: 0x04005715 RID: 22293
		public float IntensityOverIndex;

		// Token: 0x04005716 RID: 22294
		public float Rotation;
	}
}
