using System;
using Terraria.GameContent.Generation.Dungeon.Rooms;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x02000495 RID: 1173
	public struct DungeonRoomSearchSettings
	{
		// Token: 0x04005913 RID: 22803
		public int Fluff;

		// Token: 0x04005914 RID: 22804
		public DungeonRoom ExcludedRoom;

		// Token: 0x04005915 RID: 22805
		public ProgressionStageCheck ProgressionStageCheck;

		// Token: 0x04005916 RID: 22806
		public int? ProgressionStage;

		// Token: 0x04005917 RID: 22807
		public int? MaximumDistance;
	}
}
