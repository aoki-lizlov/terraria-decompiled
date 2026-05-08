using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200042D RID: 1069
	public class CrimsonBiome : AShoppingBiome
	{
		// Token: 0x06003091 RID: 12433 RVA: 0x005BA680 File Offset: 0x005B8880
		public CrimsonBiome()
		{
			base.NameKey = "Crimson";
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x005BA693 File Offset: 0x005B8893
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCrimson;
		}
	}
}
