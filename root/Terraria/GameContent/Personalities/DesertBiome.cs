using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000426 RID: 1062
	public class DesertBiome : AShoppingBiome
	{
		// Token: 0x06003083 RID: 12419 RVA: 0x005BA5C3 File Offset: 0x005B87C3
		public DesertBiome()
		{
			base.NameKey = "Desert";
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x005BA5D6 File Offset: 0x005B87D6
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDesert;
		}
	}
}
