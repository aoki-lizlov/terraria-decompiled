using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x02000254 RID: 596
	public class OutlinedDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x0053D397 File Offset: 0x0053B597
		public void SetTexture(Texture2D texture)
		{
			if (this._theTexture == texture)
			{
				return;
			}
			this._theTexture = texture;
			this._wasPrepared = false;
			this.width = texture.Width + 8;
			this.height = texture.Height + 8;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0053D3D0 File Offset: 0x0053B5D0
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._theTexture, new Vector2(4f, 4f), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0053D41B File Offset: 0x0053B61B
		public OutlinedDrawRenderTargetContent()
		{
		}

		// Token: 0x04004D60 RID: 19808
		private Texture2D _theTexture;
	}
}
