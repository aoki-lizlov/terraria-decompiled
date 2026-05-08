using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000425 RID: 1061
	public class SnowBiome : AShoppingBiome
	{
		// Token: 0x06003081 RID: 12417 RVA: 0x005BA5A8 File Offset: 0x005B87A8
		public SnowBiome()
		{
			base.NameKey = "Snow";
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x005BA5BB File Offset: 0x005B87BB
		public override bool IsInBiome(Player player)
		{
			return player.ZoneSnow;
		}
	}
}
