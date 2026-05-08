using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004CA RID: 1226
	public class DungeonTileClumpSettings : DungeonFeatureSettings
	{
		// Token: 0x060034CD RID: 13517 RVA: 0x00609B30 File Offset: 0x00607D30
		public DungeonTileClumpSettings()
		{
		}

		// Token: 0x04005A6F RID: 23151
		public int RandomSeed;

		// Token: 0x04005A70 RID: 23152
		public double Strength;

		// Token: 0x04005A71 RID: 23153
		public int Steps;

		// Token: 0x04005A72 RID: 23154
		public ushort TileType;

		// Token: 0x04005A73 RID: 23155
		public ushort WallType;

		// Token: 0x04005A74 RID: 23156
		public DungeonBounds AreaToGenerateIn;

		// Token: 0x04005A75 RID: 23157
		public ushort? OnlyReplaceThisTileType;

		// Token: 0x04005A76 RID: 23158
		public ushort? OnlyReplaceThisWallType;
	}
}
