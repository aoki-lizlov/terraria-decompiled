using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200034F RID: 847
	public interface IPreferenceProviderElement : IBestiaryInfoElement
	{
		// Token: 0x06002888 RID: 10376
		IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider();

		// Token: 0x06002889 RID: 10377
		bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider);
	}
}
