using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000212 RID: 530
	public class ShockIconParticle : ABasicParticle
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x00530474 File Offset: 0x0052E674
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.FadeInNormalizedTime = 0.1f;
			this.FadeOutNormalizedTime = 0.9f;
			this.TimeToLive = 20f;
			this.InitialScale = 1f;
			this.ColorTint = Color.White;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x005304D4 File Offset: 0x0052E6D4
		public override void Update(ref ParticleRendererSettings settings)
		{
			if (this._timeSinceSpawn == 0f)
			{
				this.initialPosition = this.LocalPosition;
			}
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			float num = this._timeSinceSpawn / this.TimeToLive;
			this.Scale = Vector2.One * this.InitialScale * Utils.MultiLerp(num, new float[] { 0.2f, 0.9f, 1.3f, 0.9f });
			this.Opacity = MathHelper.Clamp(Utils.Remap(num, 0f, this.FadeInNormalizedTime, 0f, 1f, true) * Utils.Remap(num, this.FadeOutNormalizedTime, 1f, 1f, 0f, true), 0f, 1f) * 0.5f;
			if (this.ParentProjectileID != -1 && this.ParentProjectileID >= 0 && this.ParentProjectileID < 1000)
			{
				Projectile projectile = Main.projectile[this.ParentProjectileID];
				this.LocalPosition = projectile.Top + num * this.OffsetFromParent;
			}
			else
			{
				this.LocalPosition = this.initialPosition + num * this.OffsetFromParent;
			}
			if (this._timeSinceSpawn >= this.TimeToLive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00530624 File Offset: 0x0052E824
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = settings.AnchorPosition + this.LocalPosition;
			Texture2D value = TextureAssets.Extra[268].Value;
			Vector2 vector2 = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			Vector2 scale = this.Scale;
			Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()).MultiplyRGBA(this.ColorTint), this.ColorTint, 0.75f) * this.Opacity;
			spritebatch.Draw(value, vector, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color, this.Rotation, vector2, scale, SpriteEffects.None, 0f);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x005306D8 File Offset: 0x0052E8D8
		public ShockIconParticle()
		{
		}

		// Token: 0x04004C09 RID: 19465
		public float FadeInNormalizedTime = 0.25f;

		// Token: 0x04004C0A RID: 19466
		public float FadeOutNormalizedTime = 0.75f;

		// Token: 0x04004C0B RID: 19467
		public float TimeToLive = 20f;

		// Token: 0x04004C0C RID: 19468
		public float Opacity;

		// Token: 0x04004C0D RID: 19469
		public float InitialScale = 1f;

		// Token: 0x04004C0E RID: 19470
		public Color ColorTint = Color.White;

		// Token: 0x04004C0F RID: 19471
		public int ParentProjectileID = -1;

		// Token: 0x04004C10 RID: 19472
		public Vector2 OffsetFromParent;

		// Token: 0x04004C11 RID: 19473
		private Vector2 initialPosition;

		// Token: 0x04004C12 RID: 19474
		private float _timeSinceSpawn;
	}
}
