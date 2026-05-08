using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000423 RID: 1059
	public class OceanBiome : AShoppingBiome
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x005BA572 File Offset: 0x005B8772
		public OceanBiome()
		{
			base.NameKey = "Ocean";
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x005BA585 File Offset: 0x005B8785
		public override bool IsInBiome(Player player)
		{
			return player.ZoneBeach;
		}
	}
}
