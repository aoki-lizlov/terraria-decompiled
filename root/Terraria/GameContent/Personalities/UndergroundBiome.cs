using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000428 RID: 1064
	public class UndergroundBiome : AShoppingBiome
	{
		// Token: 0x06003087 RID: 12423 RVA: 0x005BA5F9 File Offset: 0x005B87F9
		public UndergroundBiome()
		{
			base.NameKey = "NormalUnderground";
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x005BA60C File Offset: 0x005B880C
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_BelowSurface;
		}
	}
}
