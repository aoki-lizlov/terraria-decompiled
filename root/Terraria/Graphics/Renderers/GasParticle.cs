using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000213 RID: 531
	public class GasParticle : ABasicParticle
	{
		// Token: 0x06002199 RID: 8601 RVA: 0x0053072C File Offset: 0x0052E92C
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.ColorTint = Color.Transparent;
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.FadeInNormalizedTime = 0.25f;
			this.FadeOutNormalizedTime = 0.75f;
			this.TimeToLive = 80f;
			this._internalIndentifier = Main.rand.Next(255);
			this.SlowdownScalar = 0.95f;
			this.LightColorTint = Color.Transparent;
			this.InitialScale = 1f;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x005307B8 File Offset: 0x0052E9B8
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			float num = this._timeSinceSpawn / this.TimeToLive;
			this.Scale = Vector2.One * this.InitialScale * Utils.Remap(num, 0f, 0.95f, 1f, 1.3f, true);
			this.Opacity = MathHelper.Clamp(Utils.Remap(num, 0f, this.FadeInNormalizedTime, 0f, 1f, true) * Utils.Remap(num, this.FadeOutNormalizedTime, 1f, 1f, 0f, true), 0f, 1f) * 0.85f;
			this.Rotation = (float)this._internalIndentifier * 0.4002029f + this._timeSinceSpawn * 6.2831855f / 480f * 0.5f;
			this.Velocity *= this.SlowdownScalar;
			if (this.LightColorTint != Color.Transparent)
			{
				Color color = this.LightColorTint * this.Opacity;
				Lighting.AddLight(this.LocalPosition, (float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f);
			}
			if (this._timeSinceSpawn >= this.TimeToLive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00530928 File Offset: 0x0052EB28
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Main.instance.LoadProjectile(1007);
			Texture2D value = TextureAssets.Projectile[1007].Value;
			Vector2 vector = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			Vector2 vector2 = settings.AnchorPosition + this.LocalPosition;
			Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()), this.ColorTint, 0.2f) * this.Opacity;
			Vector2 scale = this.Scale;
			spritebatch.Draw(value, vector2, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color, this.Rotation, vector, scale, SpriteEffects.None, 0f);
			spritebatch.Draw(value, vector2, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color * 0.25f, this.Rotation, vector, scale * (1f + this.Opacity * 1.5f), SpriteEffects.None, 0f);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00530A28 File Offset: 0x0052EC28
		public GasParticle()
		{
		}

		// Token: 0x04004C13 RID: 19475
		public float FadeInNormalizedTime = 0.25f;

		// Token: 0x04004C14 RID: 19476
		public float FadeOutNormalizedTime = 0.75f;

		// Token: 0x04004C15 RID: 19477
		public float TimeToLive = 80f;

		// Token: 0x04004C16 RID: 19478
		public Color ColorTint;

		// Token: 0x04004C17 RID: 19479
		public float Opacity;

		// Token: 0x04004C18 RID: 19480
		public float AdditiveAmount = 1f;

		// Token: 0x04004C19 RID: 19481
		public float FadeInEnd = 20f;

		// Token: 0x04004C1A RID: 19482
		public float FadeOutStart = 30f;

		// Token: 0x04004C1B RID: 19483
		public float FadeOutEnd = 45f;

		// Token: 0x04004C1C RID: 19484
		public float SlowdownScalar = 0.95f;

		// Token: 0x04004C1D RID: 19485
		private float _timeSinceSpawn;

		// Token: 0x04004C1E RID: 19486
		public Color LightColorTint;

		// Token: 0x04004C1F RID: 19487
		private int _internalIndentifier;

		// Token: 0x04004C20 RID: 19488
		public float InitialScale = 1f;
	}
}
