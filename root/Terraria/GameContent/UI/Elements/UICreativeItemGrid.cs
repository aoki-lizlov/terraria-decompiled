using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003CD RID: 973
	public class UICreativeItemGrid : UIDynamicItemCollection<Item>
	{
		// Token: 0x06002D9D RID: 11677 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		protected override Item GetItem(Item entry)
		{
			return entry;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x005A5040 File Offset: 0x005A3240
		protected override void DrawSlot(SpriteBatch spriteBatch, Item item, Vector2 pos, bool hovering)
		{
			ItemsSacrificedUnlocksTracker itemSacrifices = Main.LocalPlayerCreativeTracker.ItemSacrifices;
			int num = (itemSacrifices.IsFullyResearched(item.type) ? 29 : 34);
			if (hovering)
			{
				this._item.SetDefaults(item.type, null);
				item = this._item;
				Main.LocalPlayer.mouseInterface = true;
				ItemSlot.Handle(ref item, num, true);
				itemSacrifices.ClearNewlyResearchedStatus(item.type);
			}
			UILinkPointNavigator.Shortcuts.ItemSlotShouldHighlightAsSelected = hovering;
			item.newAndShiny = itemSacrifices.IsNewlyResearched(item.type);
			ItemSlot.Draw(spriteBatch, ref item, num, pos, default(Color));
			item.newAndShiny = false;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x005A50DE File Offset: 0x005A32DE
		public UICreativeItemGrid()
		{
		}

		// Token: 0x040054E8 RID: 21736
		private Item _item = new Item();
	}
}
