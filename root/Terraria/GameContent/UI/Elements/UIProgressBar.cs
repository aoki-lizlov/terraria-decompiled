using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200040B RID: 1035
	public class UIProgressBar : UIElement
	{
		// Token: 0x06002F95 RID: 12181 RVA: 0x005B45D1 File Offset: 0x005B27D1
		public UIProgressBar()
		{
			this._progressBar.Height.Precent = 1f;
			this._progressBar.Recalculate();
			base.Append(this._progressBar);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x005B4610 File Offset: 0x005B2810
		public void SetProgress(float value)
		{
			this._targetProgress = value;
			if (value < this._visualProgress)
			{
				this._visualProgress = value;
			}
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x005B462C File Offset: 0x005B282C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this._visualProgress = this._visualProgress * 0.95f + 0.05f * this._targetProgress;
			this._progressBar.Width.Precent = this._visualProgress;
			this._progressBar.Recalculate();
		}

		// Token: 0x0400568E RID: 22158
		private UIProgressBar.UIInnerProgressBar _progressBar = new UIProgressBar.UIInnerProgressBar();

		// Token: 0x0400568F RID: 22159
		private float _visualProgress;

		// Token: 0x04005690 RID: 22160
		private float _targetProgress;

		// Token: 0x02000938 RID: 2360
		private class UIInnerProgressBar : UIElement
		{
			// Token: 0x06004825 RID: 18469 RVA: 0x006CC9E0 File Offset: 0x006CABE0
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(dimensions.X, dimensions.Y), null, Color.Blue, 0f, Vector2.Zero, new Vector2(dimensions.Width, dimensions.Height / 1000f), SpriteEffects.None, 0f);
			}

			// Token: 0x06004826 RID: 18470 RVA: 0x005A2DAD File Offset: 0x005A0FAD
			public UIInnerProgressBar()
			{
			}
		}
	}
}
