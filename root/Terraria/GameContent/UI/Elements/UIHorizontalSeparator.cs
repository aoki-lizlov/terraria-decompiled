using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F3 RID: 1011
	public class UIHorizontalSeparator : UIElement
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x005AC950 File Offset: 0x005AAB50
		public UIHorizontalSeparator(int EdgeWidth = 2, bool highlightSideUp = true)
		{
			this.Color = Color.White;
			if (highlightSideUp)
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator1", 1);
			}
			else
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator2", 1);
			}
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x005AC9D8 File Offset: 0x005AABD8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawPanel(this._texture.Value, this.EdgeWidth, 0, spriteBatch, dimensions.Position(), dimensions.Width, this.Color);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x040055D1 RID: 21969
		private Asset<Texture2D> _texture;

		// Token: 0x040055D2 RID: 21970
		public Color Color;

		// Token: 0x040055D3 RID: 21971
		public int EdgeWidth;
	}
}
