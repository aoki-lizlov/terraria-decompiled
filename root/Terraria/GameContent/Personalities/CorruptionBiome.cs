using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200042C RID: 1068
	public class CorruptionBiome : AShoppingBiome
	{
		// Token: 0x0600308F RID: 12431 RVA: 0x005BA665 File Offset: 0x005B8865
		public CorruptionBiome()
		{
			base.NameKey = "Corruption";
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x005BA678 File Offset: 0x005B8878
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCorrupt;
		}
	}
}
