using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000357 RID: 855
	public class ItemDropBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060028AA RID: 10410 RVA: 0x00573AAE File Offset: 0x00571CAE
		public ItemDropBestiaryInfoElement(DropRateInfo info)
		{
			this._droprateInfo = info;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x00573AC0 File Offset: 0x00571CC0
		public virtual UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			bool flag = ItemDropBestiaryInfoElement.ShouldShowItem(ref this._droprateInfo);
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			return new UIBestiaryInfoItemLine(this._droprateInfo, info, 1f);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x00573AFC File Offset: 0x00571CFC
		private static bool ShouldShowItem(ref DropRateInfo dropRateInfo)
		{
			bool flag = true;
			if (dropRateInfo.conditions != null && dropRateInfo.conditions.Count > 0)
			{
				for (int i = 0; i < dropRateInfo.conditions.Count; i++)
				{
					if (!dropRateInfo.conditions[i].CanShowItemDropInUI())
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00573B50 File Offset: 0x00571D50
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			bool flag = ItemDropBestiaryInfoElement.ShouldShowItem(ref this._droprateInfo);
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			return ContentSamples.ItemsByType[this._droprateInfo.itemId].Name;
		}

		// Token: 0x0400515E RID: 20830
		protected DropRateInfo _droprateInfo;
	}
}
