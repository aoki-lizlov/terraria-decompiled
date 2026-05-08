using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003C4 RID: 964
	public class UICyclingImage : UIImage
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x005A2D0E File Offset: 0x005A0F0E
		public UICyclingImage(List<Asset<Texture2D>> textureAssets)
		{
			this.FramesPerCycle = 45;
			this._textureAssets = textureAssets;
			base.SetImage(this._textureAssets[this._currentTextureIndex]);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x005A2D3C File Offset: 0x005A0F3C
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			int num = this._framesCounted + 1;
			this._framesCounted = num;
			if (num < this.FramesPerCycle)
			{
				return;
			}
			this._framesCounted = 0;
			num = this._currentTextureIndex + 1;
			this._currentTextureIndex = num;
			if (num >= this._textureAssets.Count)
			{
				this._currentTextureIndex = 0;
			}
			base.SetImage(this._textureAssets[this._currentTextureIndex]);
		}

		// Token: 0x040054AA RID: 21674
		public int FramesPerCycle;

		// Token: 0x040054AB RID: 21675
		private List<Asset<Texture2D>> _textureAssets;

		// Token: 0x040054AC RID: 21676
		private int _currentTextureIndex;

		// Token: 0x040054AD RID: 21677
		private int _framesCounted;
	}
}
