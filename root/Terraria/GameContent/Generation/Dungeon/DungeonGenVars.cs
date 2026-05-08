using System;
using System.Collections.Generic;
using ReLogic.Utilities;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation.Dungeon.Entrances;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x02000499 RID: 1177
	public class DungeonGenVars
	{
		// Token: 0x060033C2 RID: 13250 RVA: 0x005FA08C File Offset: 0x005F828C
		public DungeonGenVars()
		{
		}

		// Token: 0x04005923 RID: 22819
		public int dungeonSide;

		// Token: 0x04005924 RID: 22820
		public int dungeonLocation;

		// Token: 0x04005925 RID: 22821
		public DungeonColor dungeonColor;

		// Token: 0x04005926 RID: 22822
		public ushort brickTileType = 41;

		// Token: 0x04005927 RID: 22823
		public ushort brickWallType = 7;

		// Token: 0x04005928 RID: 22824
		public ushort brickCrackedTileType = 481;

		// Token: 0x04005929 RID: 22825
		public ushort windowGlassWallType = 91;

		// Token: 0x0400592A RID: 22826
		public ushort windowClosedGlassWallType = 149;

		// Token: 0x0400592B RID: 22827
		public ushort windowEdgeWallType = 8;

		// Token: 0x0400592C RID: 22828
		public int[] windowPlatformItemTypes;

		// Token: 0x0400592D RID: 22829
		public int generatingDungeonPositionX;

		// Token: 0x0400592E RID: 22830
		public int generatingDungeonPositionY;

		// Token: 0x0400592F RID: 22831
		public int generatingDungeonTopX;

		// Token: 0x04005930 RID: 22832
		public int dungeonLootStyle;

		// Token: 0x04005931 RID: 22833
		public DungeonBounds outerPotentialDungeonBounds = new DungeonBounds();

		// Token: 0x04005932 RID: 22834
		public DungeonBounds innerPotentialDungeonBounds = new DungeonBounds();

		// Token: 0x04005933 RID: 22835
		public DungeonGenerationStyleData dungeonStyle;

		// Token: 0x04005934 RID: 22836
		public List<DungeonGenerationStyleData> dungeonGenerationStyles = new List<DungeonGenerationStyleData>();

		// Token: 0x04005935 RID: 22837
		public DitherSnake dungeonDitherSnake = new DitherSnake();

		// Token: 0x04005936 RID: 22838
		public bool[] isCrackedBrick;

		// Token: 0x04005937 RID: 22839
		public bool[] isPitTrapTile;

		// Token: 0x04005938 RID: 22840
		public bool[] isDungeonTile;

		// Token: 0x04005939 RID: 22841
		public bool[] isDungeonWall;

		// Token: 0x0400593A RID: 22842
		public bool[] isDungeonWallGlass;

		// Token: 0x0400593B RID: 22843
		public bool GeneratingDungeon;

		// Token: 0x0400593C RID: 22844
		public PreGenDungeonEntranceSettings preGenDungeonEntranceSettings;

		// Token: 0x0400593D RID: 22845
		public Vector2D dungeonEntrancePosition;

		// Token: 0x0400593E RID: 22846
		public bool desertChestLootState;
	}
}
