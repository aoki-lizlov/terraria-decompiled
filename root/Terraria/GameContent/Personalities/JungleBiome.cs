using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000427 RID: 1063
	public class JungleBiome : AShoppingBiome
	{
		// Token: 0x06003085 RID: 12421 RVA: 0x005BA5DE File Offset: 0x005B87DE
		public JungleBiome()
		{
			base.NameKey = "Jungle";
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x005BA5F1 File Offset: 0x005B87F1
		public override bool IsInBiome(Player player)
		{
			return player.ZoneJungle;
		}
	}
}
