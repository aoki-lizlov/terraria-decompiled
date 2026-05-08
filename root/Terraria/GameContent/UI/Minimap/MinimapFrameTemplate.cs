using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.Minimap
{
	// Token: 0x020003C2 RID: 962
	public class MinimapFrameTemplate
	{
		// Token: 0x06002D28 RID: 11560 RVA: 0x005A28FD File Offset: 0x005A0AFD
		public MinimapFrameTemplate(string name, Vector2 frameOffset, Vector2 resetPosition, Vector2 zoomInPosition, Vector2 zoomOutPosition)
		{
			this.name = name;
			this.frameOffset = frameOffset;
			this.resetPosition = resetPosition;
			this.zoomInPosition = zoomInPosition;
			this.zoomOutPosition = zoomOutPosition;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x005A292C File Offset: 0x005A0B2C
		public MinimapFrame CreateInstance(AssetRequestMode mode)
		{
			MinimapFrame minimapFrame = new MinimapFrame(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapFrame", mode), this.frameOffset);
			minimapFrame.NameKey = this.name;
			minimapFrame.ConfigKey = this.name;
			minimapFrame.SetResetButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_Reset", mode), this.resetPosition);
			minimapFrame.SetZoomOutButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomOut", mode), this.zoomOutPosition);
			minimapFrame.SetZoomInButton(MinimapFrameTemplate.LoadAsset<Texture2D>("Images\\UI\\Minimap\\" + this.name + "\\MinimapButton_ZoomIn", mode), this.zoomInPosition);
			return minimapFrame;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x005A29EC File Offset: 0x005A0BEC
		private static Asset<T> LoadAsset<T>(string assetName, AssetRequestMode mode) where T : class
		{
			return Main.Assets.Request<T>(assetName, mode);
		}

		// Token: 0x0400549A RID: 21658
		private string name;

		// Token: 0x0400549B RID: 21659
		private Vector2 frameOffset;

		// Token: 0x0400549C RID: 21660
		private Vector2 resetPosition;

		// Token: 0x0400549D RID: 21661
		private Vector2 zoomInPosition;

		// Token: 0x0400549E RID: 21662
		private Vector2 zoomOutPosition;
	}
}
