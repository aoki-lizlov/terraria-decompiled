using System;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C3 RID: 1219
	public abstract class DungeonHallSettings
	{
		// Token: 0x060034B6 RID: 13494 RVA: 0x0060773D File Offset: 0x0060593D
		protected DungeonHallSettings()
		{
		}

		// Token: 0x04005A51 RID: 23121
		public DungeonHallType HallType;

		// Token: 0x04005A52 RID: 23122
		public int RandomSeed;

		// Token: 0x04005A53 RID: 23123
		public DungeonGenerationStyleData StyleData;

		// Token: 0x04005A54 RID: 23124
		public int OverridePaintTile = -1;

		// Token: 0x04005A55 RID: 23125
		public int OverridePaintWall = -1;

		// Token: 0x04005A56 RID: 23126
		public double CrackedBrickChance = 0.166;

		// Token: 0x04005A57 RID: 23127
		public bool PlaceOverProtectedBricks;

		// Token: 0x04005A58 RID: 23128
		public double ZigzagChance = 0.66;

		// Token: 0x04005A59 RID: 23129
		public bool ForceStyleForDoorsAndPlatforms;

		// Token: 0x04005A5A RID: 23130
		public bool CarveOnly;
	}
}
