using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x02000256 RID: 598
	public class PlayerQueenSlimeMountTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x06002346 RID: 9030 RVA: 0x0053D4D0 File Offset: 0x0053B6D0
		protected override void HandleUseRequest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[204];
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData drawData = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			GameShaders.Misc["QueenSlime"].Apply(new DrawData?(drawData));
			drawData.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0053D4C7 File Offset: 0x0053B6C7
		public PlayerQueenSlimeMountTextureContent()
		{
		}
	}
}
