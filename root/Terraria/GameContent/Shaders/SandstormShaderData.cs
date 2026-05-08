using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000295 RID: 661
	public class SandstormShaderData : ScreenShaderData
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00554167 File Offset: 0x00552367
		public SandstormShaderData(string passName)
			: base(passName)
		{
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0055417C File Offset: 0x0055237C
		public override void Update(GameTime gameTime)
		{
			Vector2 vector = new Vector2(-Main.windSpeedCurrent, -1f) * new Vector2(20f, 0.1f);
			vector.Normalize();
			vector *= new Vector2(2f, 0.2f);
			if (FocusHelper.UpdateVisualEffects)
			{
				this._texturePosition += vector * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			this._texturePosition.X = this._texturePosition.X % 10f;
			this._texturePosition.Y = this._texturePosition.Y % 10f;
			base.UseDirection(vector);
			base.Update(gameTime);
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x0055422F File Offset: 0x0055242F
		public override void Apply()
		{
			base.UseTargetPosition(this._texturePosition);
			base.Apply();
		}

		// Token: 0x04004F94 RID: 20372
		private Vector2 _texturePosition = Vector2.Zero;
	}
}
