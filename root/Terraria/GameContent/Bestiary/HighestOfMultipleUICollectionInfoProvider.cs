using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000348 RID: 840
	public class HighestOfMultipleUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x00573355 File Offset: 0x00571555
		public HighestOfMultipleUICollectionInfoProvider(params IBestiaryUICollectionInfoProvider[] providers)
		{
			this._providers = providers;
			this._mainProviderIndex = 0;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0057336C File Offset: 0x0057156C
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryUICollectionInfo entryUICollectionInfo = this._providers[this._mainProviderIndex].GetEntryUICollectionInfo();
			BestiaryEntryUnlockState bestiaryEntryUnlockState = entryUICollectionInfo.UnlockState;
			for (int i = 0; i < this._providers.Length; i++)
			{
				BestiaryUICollectionInfo entryUICollectionInfo2 = this._providers[i].GetEntryUICollectionInfo();
				if (bestiaryEntryUnlockState < entryUICollectionInfo2.UnlockState)
				{
					bestiaryEntryUnlockState = entryUICollectionInfo2.UnlockState;
				}
			}
			entryUICollectionInfo.UnlockState = bestiaryEntryUnlockState;
			return entryUICollectionInfo;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005144 RID: 20804
		private IBestiaryUICollectionInfoProvider[] _providers;

		// Token: 0x04005145 RID: 20805
		private int _mainProviderIndex;
	}
}
