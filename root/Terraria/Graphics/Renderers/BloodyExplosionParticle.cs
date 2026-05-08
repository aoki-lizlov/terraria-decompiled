using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000211 RID: 529
	public class BloodyExplosionParticle : ABasicParticle
	{
		// Token: 0x06002191 RID: 8593 RVA: 0x00530004 File Offset: 0x0052E204
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.InnerOpacity = 0f;
			this.FadeInNormalizedTime = 0.1f;
			this.FadeOutNormalizedTime = 0.9f;
			this.TimeToLive = 20f;
			this.InitialScale = 1f;
			this.ColorTint = Color.White;
			this.LightColorTint = Color.Transparent;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0053007C File Offset: 0x0052E27C
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			float num = this._timeSinceSpawn / this.TimeToLive;
			this.Scale = Vector2.One * this.InitialScale * Utils.Remap(num, 0f, 0.3f, 0.5f, 1f, true);
			float num2 = 0.4f;
			this.Opacity = MathHelper.Clamp(Utils.Remap(num, 0f, this.FadeInNormalizedTime, 0f, 1f, true) * Utils.Remap(num, this.FadeOutNormalizedTime, 1f, 1f, 0f, true), 0f, 1f) * num2;
			this.InnerOpacity = MathHelper.Clamp(Utils.Remap(num, 0f, this.FadeInNormalizedTime * 0.75f, 0f, 1f, true) * Utils.Remap(num, 0.3f, 0.45f, 1f, 0f, true), 0f, 1f) * num2;
			if (this._timeSinceSpawn == 3f)
			{
				Rectangle rectangle = Utils.CenteredRectangle(this.LocalPosition, new Vector2(16f, 16f));
				for (int i = 0; i < 50; i++)
				{
					Vector2 vector = Main.rand.NextVector2CircularEdge(4f, 4f);
					if (i % 2 == 0)
					{
						vector *= 0.5f;
					}
					Dust dust = Main.dust[Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 5, 0f, 0f, 100, default(Color), 1.25f + Main.rand.NextFloat() * 0.5f)];
					dust.velocity = vector;
					dust.noGravity = i % 3 == 0;
				}
			}
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

		// Token: 0x06002193 RID: 8595 RVA: 0x005302C4 File Offset: 0x0052E4C4
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			float num = this._timeSinceSpawn / this.TimeToLive;
			Vector2 vector = settings.AnchorPosition + this.LocalPosition;
			Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()).MultiplyRGBA(this.ColorTint), this.ColorTint, 0.65f);
			Texture2D value = TextureAssets.Extra[174].Value;
			Vector2 vector2 = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			Vector2 vector3 = this.Scale * (1.1f + 0.15f * num);
			Color color2 = color * this.Opacity;
			Texture2D value2 = TextureAssets.Extra[267].Value;
			Vector2 vector4 = new Vector2((float)(value2.Width / 2), (float)(value2.Height / 2));
			Vector2 vector5 = this.Scale * (1f + 0.05f * num);
			Color color3 = color * this.InnerOpacity;
			spritebatch.Draw(value, vector, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color2, this.Rotation, vector2, vector3, SpriteEffects.None, 0f);
			spritebatch.Draw(value2, vector, new Rectangle?(value2.Frame(1, 1, 0, 0, 0, 0)), color3, this.Rotation, vector4, vector5, SpriteEffects.None, 0f);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0053041C File Offset: 0x0052E61C
		public BloodyExplosionParticle()
		{
		}

		// Token: 0x04004C00 RID: 19456
		public float FadeInNormalizedTime = 0.25f;

		// Token: 0x04004C01 RID: 19457
		public float FadeOutNormalizedTime = 0.75f;

		// Token: 0x04004C02 RID: 19458
		public float TimeToLive = 20f;

		// Token: 0x04004C03 RID: 19459
		public float Opacity;

		// Token: 0x04004C04 RID: 19460
		public float InnerOpacity;

		// Token: 0x04004C05 RID: 19461
		public float InitialScale = 1f;

		// Token: 0x04004C06 RID: 19462
		public Color ColorTint = Color.White;

		// Token: 0x04004C07 RID: 19463
		public Color LightColorTint = Color.Transparent;

		// Token: 0x04004C08 RID: 19464
		private float _timeSinceSpawn;
	}
}
