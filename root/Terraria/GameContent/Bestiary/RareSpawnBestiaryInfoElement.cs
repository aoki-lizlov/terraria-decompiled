using System;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200034B RID: 843
	public class RareSpawnBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x00573652 File Offset: 0x00571852
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x0057365A File Offset: 0x0057185A
		public int RarityLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<RarityLevel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RarityLevel>k__BackingField = value;
			}
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00573663 File Offset: 0x00571863
		public RareSpawnBestiaryInfoElement(int rarityLevel)
		{
			this.RarityLevel = rarityLevel;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00573672 File Offset: 0x00571872
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText("BestiaryInfo.IsRare").Value;
		}

		// Token: 0x0400514A RID: 20810
		[CompilerGenerated]
		private int <RarityLevel>k__BackingField;
	}
}
