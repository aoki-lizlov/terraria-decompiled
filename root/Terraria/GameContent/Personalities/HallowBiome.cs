using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000429 RID: 1065
	public class HallowBiome : AShoppingBiome
	{
		// Token: 0x06003089 RID: 12425 RVA: 0x005BA614 File Offset: 0x005B8814
		public HallowBiome()
		{
			base.NameKey = "Hallow";
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x005BA627 File Offset: 0x005B8827
		public override bool IsInBiome(Player player)
		{
			return player.ZoneHallow;
		}
	}
}
