using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F1 RID: 1009
	public class UIColoredSliderSimple : UIElement
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x005AC2F8 File Offset: 0x005AA4F8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.DrawValueBarDynamicWidth(spriteBatch);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x005AC304 File Offset: 0x005AA504
		private void DrawValueBarDynamicWidth(SpriteBatch sb)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2 = new Rectangle(5, 4, 4, 4);
			Utils.DrawSplicedPanel(sb, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle rectangle4 = new Rectangle(0, 0, 1, 1);
			sb.Draw(value2, rectangle3, new Rectangle?(rectangle4), this.EmptyColor);
			Rectangle rectangle5 = rectangle3;
			rectangle5.Width = (int)((float)rectangle5.Width * this.FillPercent);
			sb.Draw(value2, rectangle5, new Rectangle?(rectangle4), this.FilledColor);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x005AC41B File Offset: 0x005AA61B
		public UIColoredSliderSimple()
		{
		}

		// Token: 0x040055C8 RID: 21960
		public float FillPercent;

		// Token: 0x040055C9 RID: 21961
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x040055CA RID: 21962
		public Color EmptyColor = Color.Black;
	}
}
