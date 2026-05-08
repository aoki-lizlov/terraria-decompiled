using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003C7 RID: 967
	public class UIItemSlot : UIElement
	{
		// Token: 0x06002D52 RID: 11602 RVA: 0x005A3254 File Offset: 0x005A1454
		public UIItemSlot(Item[] itemArray, int itemIndex, int itemSlotContext)
		{
			this._itemArray = itemArray;
			this._itemIndex = itemIndex;
			this._itemSlotContext = itemSlotContext;
			this.Width = new StyleDimension(48f, 0f);
			this.Height = new StyleDimension(48f, 0f);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x005A32A6 File Offset: 0x005A14A6
		private void HandleItemSlotLogic()
		{
			if (!base.IsMouseHovering)
			{
				return;
			}
			Main.LocalPlayer.mouseInterface = true;
			ItemSlot.Handle(this._itemArray, this._itemSlotContext, this._itemIndex, true);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x005A32D4 File Offset: 0x005A14D4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.HandleItemSlotLogic();
			Item item = this._itemArray[this._itemIndex];
			Vector2 vector = base.GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
			ItemSlot.Draw(spriteBatch, ref item, this._itemSlotContext, vector, default(Color));
		}

		// Token: 0x040054B4 RID: 21684
		private Item[] _itemArray;

		// Token: 0x040054B5 RID: 21685
		private int _itemIndex;

		// Token: 0x040054B6 RID: 21686
		private int _itemSlotContext;
	}
}
