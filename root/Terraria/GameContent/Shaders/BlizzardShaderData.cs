using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000296 RID: 662
	public class BlizzardShaderData : ScreenShaderData
	{
		// Token: 0x06002540 RID: 9536 RVA: 0x00554244 File Offset: 0x00552444
		public BlizzardShaderData(string passName)
			: base(passName)
		{
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00554264 File Offset: 0x00552464
		public override void Update(GameTime gameTime)
		{
			float num = Main.windSpeedCurrent;
			if (num >= 0f && num <= 0.1f)
			{
				num = 0.1f;
			}
			else if (num <= 0f && num >= -0.1f)
			{
				num = -0.1f;
			}
			this.windSpeed = num * 0.05f + this.windSpeed * 0.95f;
			Vector2 vector = new Vector2(-this.windSpeed, -1f) * new Vector2(10f, 2f);
			vector.Normalize();
			vector *= new Vector2(0.8f, 0.6f);
			if (FocusHelper.UpdateVisualEffects)
			{
				this._texturePosition += vector * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			this._texturePosition.X = this._texturePosition.X % 10f;
			this._texturePosition.Y = this._texturePosition.Y % 10f;
			base.UseDirection(vector);
			base.UseTargetPosition(this._texturePosition);
			base.Update(gameTime);
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00554373 File Offset: 0x00552573
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x04004F95 RID: 20373
		private Vector2 _texturePosition = Vector2.Zero;

		// Token: 0x04004F96 RID: 20374
		private float windSpeed = 0.1f;
	}
}
