using System;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000358 RID: 856
	public class ItemFromCatchingNPCBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060028AE RID: 10414 RVA: 0x00573B93 File Offset: 0x00571D93
		public ItemFromCatchingNPCBestiaryInfoElement(int itemId)
		{
			this._itemType = itemId;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00573BA2 File Offset: 0x00571DA2
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return new UIBestiaryInfoLine<string>(("catch item #" + this._itemType) ?? "", 1f);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00573BD7 File Offset: 0x00571DD7
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return ContentSamples.ItemsByType[this._itemType].Name;
		}

		// Token: 0x0400515F RID: 20831
		private int _itemType;
	}
}
