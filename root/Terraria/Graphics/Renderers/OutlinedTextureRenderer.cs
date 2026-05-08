using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000219 RID: 537
	public class OutlinedTextureRenderer : INeedRenderTargetContent
	{
		// Token: 0x060021BE RID: 8638 RVA: 0x00532756 File Offset: 0x00530956
		public OutlinedTextureRenderer(Asset<Texture2D>[] matchingArray)
		{
			this._matchingArray = matchingArray;
			this.Reset();
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0053276B File Offset: 0x0053096B
		public void Reset()
		{
			this._contents = new OutlinedDrawRenderTargetContent[this._matchingArray.Length];
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00532780 File Offset: 0x00530980
		public void DrawWithOutlines(int textureIndex, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects)
		{
			if (this._contents[textureIndex] == null)
			{
				this._contents[textureIndex] = new OutlinedDrawRenderTargetContent();
				this._contents[textureIndex].SetTexture(this._matchingArray[textureIndex].Value);
			}
			OutlinedDrawRenderTargetContent outlinedDrawRenderTargetContent = this._contents[textureIndex];
			if (outlinedDrawRenderTargetContent.IsReady)
			{
				RenderTarget2D target = outlinedDrawRenderTargetContent.GetTarget();
				Main.spriteBatch.Draw(target, position, null, color, rotation, target.Size() / 2f, scale, effects, 0f);
				return;
			}
			outlinedDrawRenderTargetContent.Request();
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool IsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00532810 File Offset: 0x00530A10
		public bool RequestAndTryGet(int textureIndex, out RenderTarget2D renderTarget)
		{
			renderTarget = null;
			if (this._contents[textureIndex] == null)
			{
				this._contents[textureIndex] = new OutlinedDrawRenderTargetContent();
				this._contents[textureIndex].SetTexture(this._matchingArray[textureIndex].Value);
			}
			OutlinedDrawRenderTargetContent outlinedDrawRenderTargetContent = this._contents[textureIndex];
			if (!outlinedDrawRenderTargetContent.IsReady)
			{
				outlinedDrawRenderTargetContent.Request();
				return false;
			}
			renderTarget = outlinedDrawRenderTargetContent.GetTarget();
			return true;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00532874 File Offset: 0x00530A74
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this._contents.Length; i++)
			{
				if (this._contents[i] != null && !this._contents[i].IsReady)
				{
					this._contents[i].PrepareRenderTarget(device, spriteBatch);
				}
			}
		}

		// Token: 0x04004C36 RID: 19510
		private OutlinedDrawRenderTargetContent[] _contents;

		// Token: 0x04004C37 RID: 19511
		private Asset<Texture2D>[] _matchingArray;
	}
}
