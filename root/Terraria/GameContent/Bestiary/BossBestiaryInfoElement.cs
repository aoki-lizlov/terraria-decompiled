using System;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200034C RID: 844
	public class BossBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x06002878 RID: 10360 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0057368D File Offset: 0x0057188D
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowPortraitOnly_1)
			{
				return null;
			}
			return Language.GetText("BestiaryInfo.IsBoss").Value;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0000357B File Offset: 0x0000177B
		public BossBestiaryInfoElement()
		{
		}
	}
}
