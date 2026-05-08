using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200040C RID: 1036
	public class UIHeader : UIElement
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x005B4679 File Offset: 0x005B2879
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x005B4684 File Offset: 0x005B2884
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this._text != value)
				{
					this._text = value;
					if (!Main.dedServ)
					{
						Vector2 vector = FontAssets.DeathText.Value.MeasureString(this.Text);
						this.Width.Pixels = vector.X;
						this.Height.Pixels = vector.Y;
					}
					this.Width.Precent = 0f;
					this.Height.Precent = 0f;
					this.Recalculate();
				}
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x005B470B File Offset: 0x005B290B
		public UIHeader()
		{
			this.Text = "";
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x005B471E File Offset: 0x005B291E
		public UIHeader(string text)
		{
			this.Text = text;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x005B4730 File Offset: 0x005B2930
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			float num = 1.2f;
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X - num, dimensions.Y - num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X + num, dimensions.Y - num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X - num, dimensions.Y + num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X + num, dimensions.Y + num), Color.Black);
			if (WorldGen.tenthAnniversaryWorldGen && !Main.zenithWorld)
			{
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X, dimensions.Y), Color.HotPink);
				return;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X, dimensions.Y), Color.White);
		}

		// Token: 0x04005691 RID: 22161
		private string _text;
	}
}
