using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000350 RID: 848
	public class BestiaryPortraitBackgroundProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x0057391D File Offset: 0x00571B1D
		public BestiaryPortraitBackgroundProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProvider)
		{
			this._preferredProvider = preferredProvider;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0057392C File Offset: 0x00571B2C
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			return provider == this._preferredProvider;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00573937 File Offset: 0x00571B37
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			return this._preferredProvider;
		}

		// Token: 0x04005151 RID: 20817
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProvider;
	}
}
