using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020E RID: 526
	public class FadingParticle : ABasicParticle
	{
		// Token: 0x06002180 RID: 8576 RVA: 0x0052FA40 File Offset: 0x0052DC40
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeInNormalizedTime = 0f;
			this.FadeOutNormalizedTime = 1f;
			this.ColorTint = Color.White;
			this.timeTolive = 0f;
			this.timeSinceSpawn = 0f;
			this.followPlayerIndex = -1;
			this.Delay = 0;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0052FA98 File Offset: 0x0052DC98
		public void SetTypeInfo(float timeToLive, bool fullbright = true)
		{
			this.timeTolive = timeToLive;
			this.fullbright = fullbright;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0052FAA8 File Offset: 0x0052DCA8
		public override void Update(ref ParticleRendererSettings settings)
		{
			if (this.Delay > 0)
			{
				this.Delay--;
				return;
			}
			base.Update(ref settings);
			this.timeSinceSpawn += 1f;
			if (this.timeSinceSpawn >= this.timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0052FAFC File Offset: 0x0052DCFC
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = settings.AnchorPosition + this.LocalPosition;
			if (this.followPlayerIndex != -1)
			{
				vector += Main.player[this.followPlayerIndex].MountedCenter;
			}
			Color color = (this.fullbright ? this.ColorTint : this.ColorTint.MultiplyRGB(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()))) * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this.timeSinceSpawn / this.timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this.timeSinceSpawn / this.timeTolive, true);
			spritebatch.Draw(this._texture.Value, vector, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0052FBE4 File Offset: 0x0052DDE4
		public FadingParticle()
		{
		}

		// Token: 0x04004BEB RID: 19435
		public float FadeInNormalizedTime;

		// Token: 0x04004BEC RID: 19436
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04004BED RID: 19437
		public Color ColorTint = Color.White;

		// Token: 0x04004BEE RID: 19438
		public int Delay;

		// Token: 0x04004BEF RID: 19439
		protected float timeTolive;

		// Token: 0x04004BF0 RID: 19440
		protected float timeSinceSpawn;

		// Token: 0x04004BF1 RID: 19441
		protected bool fullbright = true;

		// Token: 0x04004BF2 RID: 19442
		public int followPlayerIndex = -1;
	}
}
