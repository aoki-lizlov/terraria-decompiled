using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200042A RID: 1066
	public class MushroomBiome : AShoppingBiome
	{
		// Token: 0x0600308B RID: 12427 RVA: 0x005BA62F File Offset: 0x005B882F
		public MushroomBiome()
		{
			base.NameKey = "Mushroom";
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x005BA642 File Offset: 0x005B8842
		public override bool IsInBiome(Player player)
		{
			return player.ZoneGlowshroom;
		}
	}
}
