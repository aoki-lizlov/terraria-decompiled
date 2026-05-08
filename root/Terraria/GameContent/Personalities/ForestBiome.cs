using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000424 RID: 1060
	public class ForestBiome : AShoppingBiome
	{
		// Token: 0x0600307F RID: 12415 RVA: 0x005BA58D File Offset: 0x005B878D
		public ForestBiome()
		{
			base.NameKey = "Forest";
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x005BA5A0 File Offset: 0x005B87A0
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_Forest;
		}
	}
}
