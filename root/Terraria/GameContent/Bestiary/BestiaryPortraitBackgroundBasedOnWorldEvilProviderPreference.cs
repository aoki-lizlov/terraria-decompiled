using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000351 RID: 849
	public class BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x0057393F File Offset: 0x00571B3F
		public BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProviderCorrupt, IBestiaryBackgroundImagePathAndColorProvider preferredProviderCrimson)
		{
			this._preferredProviderCorrupt = preferredProviderCorrupt;
			this._preferredProviderCrimson = preferredProviderCrimson;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00573955 File Offset: 0x00571B55
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return provider == this._preferredProviderCorrupt;
			}
			return provider == this._preferredProviderCrimson;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x00573978 File Offset: 0x00571B78
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return this._preferredProviderCorrupt;
			}
			return this._preferredProviderCrimson;
		}

		// Token: 0x04005152 RID: 20818
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCorrupt;

		// Token: 0x04005153 RID: 20819
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCrimson;
	}
}
