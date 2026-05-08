using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000361 RID: 865
	public class SearchAliasInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060028D2 RID: 10450 RVA: 0x005751B5 File Offset: 0x005733B5
		public SearchAliasInfoElement(string alias)
		{
			this._alias = alias;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x005751C4 File Offset: 0x005733C4
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return this._alias;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005172 RID: 20850
		private readonly string _alias;
	}
}
