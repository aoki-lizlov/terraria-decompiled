using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200042B RID: 1067
	public class DungeonBiome : AShoppingBiome
	{
		// Token: 0x0600308D RID: 12429 RVA: 0x005BA64A File Offset: 0x005B884A
		public DungeonBiome()
		{
			base.NameKey = "Dungeon";
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x005BA65D File Offset: 0x005B885D
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDungeon;
		}
	}
}
