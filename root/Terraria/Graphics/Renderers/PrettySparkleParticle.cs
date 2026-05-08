using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000214 RID: 532
	public class PrettySparkleParticle : ABasicParticle
	{
		// Token: 0x0600219D RID: 8605 RVA: 0x00530AA0 File Offset: 0x0052ECA0
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.ColorTint = Color.Transparent;
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.FadeInNormalizedTime = 0.05f;
			this.FadeOutNormalizedTime = 0.9f;
			this.TimeToLive = 60f;
			this.AdditiveAmount = 1f;
			this.FadeInEnd = 20f;
			this.FadeOutStart = 30f;
			this.FadeOutEnd = 45f;
			this.DrawVerticalAxis = (this.DrawHorizontalAxis = true);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00530B34 File Offset: 0x0052ED34
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			this.Opacity = Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true);
			if (this._timeSinceSpawn >= this.TimeToLive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00530BB4 File Offset: 0x0052EDB4
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = Color.White * this.Opacity * 0.9f;
			color.A /= 2;
			Texture2D value = TextureAssets.Extra[98].Value;
			Color color2 = this.ColorTint * this.Opacity * 0.5f;
			color2.A = (byte)((float)color2.A * (1f - this.AdditiveAmount));
			Vector2 vector = value.Size() / 2f;
			Color color3 = color * 0.5f;
			float num = this._timeSinceSpawn / this.TimeToLive * 60f;
			float num2 = Utils.GetLerpValue(0f, this.FadeInEnd, num, true) * Utils.GetLerpValue(this.FadeOutEnd, this.FadeOutStart, num, true);
			Vector2 vector2 = new Vector2(0.3f, 2f) * num2 * this.Scale;
			Vector2 vector3 = new Vector2(0.3f, 1f) * num2 * this.Scale;
			color2 *= num2;
			color3 *= num2;
			Vector2 vector4 = settings.AnchorPosition + this.LocalPosition;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value, vector4, null, color2, 1.5707964f + this.Rotation, vector, vector2, spriteEffects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value, vector4, null, color2, 0f + this.Rotation, vector, vector3, spriteEffects, 0f);
			}
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value, vector4, null, color3, 1.5707964f + this.Rotation, vector, vector2 * 0.6f, spriteEffects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value, vector4, null, color3, 0f + this.Rotation, vector, vector3 * 0.6f, spriteEffects, 0f);
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00530DE0 File Offset: 0x0052EFE0
		public PrettySparkleParticle()
		{
		}

		// Token: 0x04004C21 RID: 19489
		public float FadeInNormalizedTime = 0.05f;

		// Token: 0x04004C22 RID: 19490
		public float FadeOutNormalizedTime = 0.9f;

		// Token: 0x04004C23 RID: 19491
		public float TimeToLive = 60f;

		// Token: 0x04004C24 RID: 19492
		public Color ColorTint;

		// Token: 0x04004C25 RID: 19493
		public float Opacity;

		// Token: 0x04004C26 RID: 19494
		public float AdditiveAmount = 1f;

		// Token: 0x04004C27 RID: 19495
		public float FadeInEnd = 20f;

		// Token: 0x04004C28 RID: 19496
		public float FadeOutStart = 30f;

		// Token: 0x04004C29 RID: 19497
		public float FadeOutEnd = 45f;

		// Token: 0x04004C2A RID: 19498
		public bool DrawHorizontalAxis = true;

		// Token: 0x04004C2B RID: 19499
		public bool DrawVerticalAxis = true;

		// Token: 0x04004C2C RID: 19500
		private float _timeSinceSpawn;
	}
}
