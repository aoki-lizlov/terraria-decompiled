using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003DE RID: 990
	public class UIItemIcon : UIElement
	{
		// Token: 0x06002E0A RID: 11786 RVA: 0x005A7D93 File Offset: 0x005A5F93
		public UIItemIcon(Item item, bool blackedOut)
		{
			this._item = item;
			this.Width.Set(32f, 0f);
			this.Height.Set(32f, 0f);
			this._blackedOut = blackedOut;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x005A7DD4 File Offset: 0x005A5FD4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			ItemSlot.DrawItemIcon(this._item, 31, spriteBatch, dimensions.Center(), this._item.scale, 32f, this._blackedOut ? Color.Black : Color.White, 1f, false);
		}

		// Token: 0x04005520 RID: 21792
		private Item _item;

		// Token: 0x04005521 RID: 21793
		private bool _blackedOut;
	}
}
