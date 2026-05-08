using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020D RID: 525
	public class FadingPlayerShaderParticle : FadingParticle
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x0052F8BF File Offset: 0x0052DABF
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this._player = null;
			this._shader = 0;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0052F8D5 File Offset: 0x0052DAD5
		public void SetTypeInfo(float timeToLive, Player player, int shader, bool fullbright = true)
		{
			base.SetTypeInfo(timeToLive, fullbright);
			this._player = player;
			this._shader = shader;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0052F8F0 File Offset: 0x0052DAF0
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			if (this._player == null || this._shader == 0)
			{
				base.Draw(ref settings, spritebatch);
				return;
			}
			Effect pixelShader = Main.pixelShader;
			Color color = (this.fullbright ? this.ColorTint : this.ColorTint.MultiplyRGB(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()))) * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this.timeSinceSpawn / this.timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this.timeSinceSpawn / this.timeTolive, true);
			DrawData drawData = default(DrawData);
			drawData.texture = this._texture.Value;
			drawData.sourceRect = new Rectangle?(this._texture.Frame(1, 1, 0, 0, 0, 0));
			drawData.shader = this._shader;
			PlayerDrawHelper.SetShaderForData(this._player, this._shader, ref drawData);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f);
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0052F0D4 File Offset: 0x0052D2D4
		public FadingPlayerShaderParticle()
		{
		}

		// Token: 0x04004BE9 RID: 19433
		private Player _player;

		// Token: 0x04004BEA RID: 19434
		private int _shader;
	}
}
