using System;

namespace Terraria.GameContent.Generation.Dungeon.Entrances
{
	// Token: 0x020004F0 RID: 1264
	public abstract class DungeonEntranceSettings
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x0000357B File Offset: 0x0000177B
		protected DungeonEntranceSettings()
		{
		}

		// Token: 0x04005AB0 RID: 23216
		public DungeonEntranceType EntranceType;

		// Token: 0x04005AB1 RID: 23217
		public int RandomSeed;

		// Token: 0x04005AB2 RID: 23218
		public DungeonGenerationStyleData StyleData;

		// Token: 0x04005AB3 RID: 23219
		public bool PrecalculateEntrancePosition;
	}
}
