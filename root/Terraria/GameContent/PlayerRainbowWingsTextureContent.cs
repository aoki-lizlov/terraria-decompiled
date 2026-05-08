using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x02000255 RID: 597
	public class PlayerRainbowWingsTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x0053D424 File Offset: 0x0053B624
		protected override void HandleUseRequest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[171];
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData drawData = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			GameShaders.Misc["HallowBoss"].Apply(new DrawData?(drawData));
			drawData.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0053D4C7 File Offset: 0x0053B6C7
		public PlayerRainbowWingsTextureContent()
		{
		}
	}
}
