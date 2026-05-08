using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000403 RID: 1027
	public class UIPanel : UIElement
	{
		// Token: 0x06002F29 RID: 12073 RVA: 0x005B1468 File Offset: 0x005AF668
		public UIPanel()
		{
			if (this._borderTexture == null)
			{
				this._borderTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBorder", 1);
			}
			if (this._backgroundTexture == null)
			{
				this._backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBackground", 1);
			}
			base.SetPadding((float)this._cornerSize);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x005B14FC File Offset: 0x005AF6FC
		public UIPanel(Asset<Texture2D> customBackground, Asset<Texture2D> customborder, int customCornerSize = 12, int customBarSize = 4)
		{
			if (this._borderTexture == null)
			{
				this._borderTexture = customborder;
			}
			if (this._backgroundTexture == null)
			{
				this._backgroundTexture = customBackground;
			}
			this._cornerSize = customCornerSize;
			this._barSize = customBarSize;
			base.SetPadding((float)this._cornerSize);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x005B1584 File Offset: 0x005AF784
		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width - this._cornerSize, point.Y + (int)dimensions.Height - this._cornerSize);
			int num = point2.X - point.X - this._cornerSize;
			int num2 = point2.Y - point.Y - this._cornerSize;
			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(0, 0, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, 0, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(0, this._cornerSize + this._barSize, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, this._cornerSize + this._barSize, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point.Y, num, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize, 0, this._barSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point2.Y, num, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize, this._cornerSize + this._barSize, this._barSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + this._cornerSize, this._cornerSize, num2), new Rectangle?(new Rectangle(0, this._cornerSize, this._cornerSize, this._barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + this._cornerSize, this._cornerSize, num2), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, this._cornerSize, this._cornerSize, this._barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point.Y + this._cornerSize, num, num2), new Rectangle?(new Rectangle(this._cornerSize, this._cornerSize, this._barSize, this._barSize)), color);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x005B18A4 File Offset: 0x005AFAA4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._backgroundTexture != null)
			{
				this.DrawPanel(spriteBatch, this._backgroundTexture.Value, this.BackgroundColor);
			}
			if (this._borderTexture != null)
			{
				this.DrawPanel(spriteBatch, this._borderTexture.Value, this.BorderColor);
			}
		}

		// Token: 0x0400563E RID: 22078
		private int _cornerSize = 12;

		// Token: 0x0400563F RID: 22079
		private int _barSize = 4;

		// Token: 0x04005640 RID: 22080
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04005641 RID: 22081
		private Asset<Texture2D> _backgroundTexture;

		// Token: 0x04005642 RID: 22082
		public Color BorderColor = Color.Black;

		// Token: 0x04005643 RID: 22083
		public Color BackgroundColor = new Color(63, 82, 151) * 0.7f;
	}
}
