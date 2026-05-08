using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044C RID: 1100
	public class BlizzardSky : CustomSky
	{
		// Token: 0x060031F9 RID: 12793 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OnLoad()
		{
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x005E546C File Offset: 0x005E366C
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			if (this._isLeaving)
			{
				this._opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (this._opacity < 0f)
				{
					this._isActive = false;
					this._opacity = 0f;
					return;
				}
			}
			else
			{
				this._opacity += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (this._opacity > 1f)
				{
					this._opacity = 1f;
				}
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x005E54FC File Offset: 0x005E36FC
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float num = Math.Min(1f, Main.cloudAlpha * 2f);
				Color color = new Color(new Vector4(1f) * Main.ColorOfTheSkies.ToVector4()) * this._opacity * 0.7f * num;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x005E558B File Offset: 0x005E378B
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x005E559B File Offset: 0x005E379B
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x005E55A4 File Offset: 0x005E37A4
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x005E55B8 File Offset: 0x005E37B8
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x005E55C0 File Offset: 0x005E37C0
		public BlizzardSky()
		{
		}

		// Token: 0x040057DA RID: 22490
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057DB RID: 22491
		private bool _isActive;

		// Token: 0x040057DC RID: 22492
		private bool _isLeaving;

		// Token: 0x040057DD RID: 22493
		private float _opacity;
	}
}
