using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Biomes;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004B3 RID: 1203
	public abstract class DungeonRoomSettings
	{
		// Token: 0x06003464 RID: 13412
		public abstract int GetBoundingRadius();

		// Token: 0x06003465 RID: 13413 RVA: 0x00603E0D File Offset: 0x0060200D
		protected DungeonRoomSettings()
		{
		}

		// Token: 0x04005A1D RID: 23069
		public DungeonControlLine ControlLine;

		// Token: 0x04005A1E RID: 23070
		public Point RoomPosition;

		// Token: 0x04005A1F RID: 23071
		public DungeonRoomType RoomType;

		// Token: 0x04005A20 RID: 23072
		public int RandomSeed;

		// Token: 0x04005A21 RID: 23073
		public DungeonGenerationStyleData StyleData;

		// Token: 0x04005A22 RID: 23074
		public int ProgressionStage;

		// Token: 0x04005A23 RID: 23075
		public bool StartingRoom;

		// Token: 0x04005A24 RID: 23076
		public int OverridePaintTile = -1;

		// Token: 0x04005A25 RID: 23077
		public int OverridePaintWall = -1;

		// Token: 0x04005A26 RID: 23078
		public bool ForceStyleForDoorsAndPlatforms;

		// Token: 0x04005A27 RID: 23079
		public bool OnCurvedLine;

		// Token: 0x04005A28 RID: 23080
		public SnakeOrientation Orientation;

		// Token: 0x04005A29 RID: 23081
		public DungeonUtils.GetHallwayConnectionPoint HallwayConnectionPointOverride;

		// Token: 0x04005A2A RID: 23082
		public int? HallwayPointAdjuster;
	}
}
