using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044E RID: 1102
	public class SandstormSky : CustomSky
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OnLoad()
		{
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x005E5D54 File Offset: 0x005E3F54
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

		// Token: 0x0600320D RID: 12813 RVA: 0x005E5DE4 File Offset: 0x005E3FE4
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (minDepth < 1f || maxDepth == 3.4028235E+38f)
			{
				float num = Math.Min(1f, Sandstorm.Severity * 1.5f);
				Color color = new Color(new Vector4(0.85f, 0.66f, 0.33f, 1f) * 0.8f * Main.ColorOfTheSkies.ToVector4()) * this._opacity * num;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
			}
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x005E5E85 File Offset: 0x005E4085
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x005E5E95 File Offset: 0x005E4095
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x005E5E9E File Offset: 0x005E409E
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x005E5EB2 File Offset: 0x005E40B2
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x005E5EBA File Offset: 0x005E40BA
		public SandstormSky()
		{
		}

		// Token: 0x040057E5 RID: 22501
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057E6 RID: 22502
		private bool _isActive;

		// Token: 0x040057E7 RID: 22503
		private bool _isLeaving;

		// Token: 0x040057E8 RID: 22504
		private float _opacity;
	}
}
