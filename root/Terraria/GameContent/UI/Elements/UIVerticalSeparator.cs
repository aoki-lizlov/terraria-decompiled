using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003FA RID: 1018
	public class UIVerticalSeparator : UIElement
	{
		// Token: 0x06002ED2 RID: 11986 RVA: 0x005AE738 File Offset: 0x005AC938
		public UIVerticalSeparator()
		{
			this.Color = Color.White;
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/OnePixel", 1);
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x005AE7A4 File Offset: 0x005AC9A4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.ToRectangle(), this.Color);
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x040055F7 RID: 22007
		private Asset<Texture2D> _texture;

		// Token: 0x040055F8 RID: 22008
		public Color Color;

		// Token: 0x040055F9 RID: 22009
		public int EdgeWidth;
	}
}
