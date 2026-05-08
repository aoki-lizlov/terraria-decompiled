using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044F RID: 1103
	public class MoonLordSky : CustomSky
	{
		// Token: 0x06003213 RID: 12819 RVA: 0x005E5ECD File Offset: 0x005E40CD
		public MoonLordSky(bool forPlayer)
		{
			this._forPlayer = forPlayer;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OnLoad()
		{
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x005E5EE8 File Offset: 0x005E40E8
		public override void Update(GameTime gameTime)
		{
			if (this._forPlayer)
			{
				if (this._isActive)
				{
					this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
					return;
				}
				this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
			}
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x005E5F40 File Offset: 0x005E4140
		private float GetIntensity()
		{
			if (this._forPlayer)
			{
				return this._fadeOpacity;
			}
			float? moonLordSkyIntensity = Main.SceneMetrics.MoonLordSkyIntensity;
			if (moonLordSkyIntensity != null)
			{
				return moonLordSkyIntensity.Value;
			}
			return 0f;
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x005E5F80 File Offset: 0x005E4180
		public override Color OnTileColor(Color inColor)
		{
			float intensity = this.GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x005E5FC8 File Offset: 0x005E41C8
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0f && minDepth < 0f)
			{
				float intensity = this.GetIntensity();
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * intensity);
			}
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x005E6018 File Offset: 0x005E4218
		public override float GetCloudAlpha()
		{
			return 1f - this._fadeOpacity;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x005E6026 File Offset: 0x005E4226
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			if (this._forPlayer)
			{
				this._fadeOpacity = 0.002f;
				return;
			}
			this._fadeOpacity = 1f;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x005E604E File Offset: 0x005E424E
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
			if (!this._forPlayer)
			{
				this._fadeOpacity = 0f;
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x005E606A File Offset: 0x005E426A
		public override void Reset()
		{
			this._isActive = false;
			this._fadeOpacity = 0f;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x005E607E File Offset: 0x005E427E
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040057E9 RID: 22505
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057EA RID: 22506
		private bool _isActive;

		// Token: 0x040057EB RID: 22507
		private bool _forPlayer;

		// Token: 0x040057EC RID: 22508
		private float _fadeOpacity;
	}
}
