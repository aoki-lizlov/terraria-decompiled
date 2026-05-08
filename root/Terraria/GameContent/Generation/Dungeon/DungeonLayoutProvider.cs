using System;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x020004A2 RID: 1186
	public abstract class DungeonLayoutProvider
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x00600783 File Offset: 0x005FE983
		public DungeonLayoutProvider(DungeonLayoutProviderSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x06003417 RID: 13335
		public abstract void ProvideLayout(DungeonData data, GenerationProgress progress, UnifiedRandom genRand, ref int roomDelay);

		// Token: 0x040059D5 RID: 22997
		public DungeonLayoutProviderSettings settings;
	}
}
