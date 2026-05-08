using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004D3 RID: 1235
	public class DungeonPillarSettings : DungeonFeatureSettings
	{
		// Token: 0x060034E3 RID: 13539 RVA: 0x0060B035 File Offset: 0x00609235
		public DungeonPillarSettings()
		{
		}

		// Token: 0x04005A86 RID: 23174
		public DungeonGenerationStyleData Style;

		// Token: 0x04005A87 RID: 23175
		public PillarType PillarType;

		// Token: 0x04005A88 RID: 23176
		public int Width;

		// Token: 0x04005A89 RID: 23177
		public int Height;

		// Token: 0x04005A8A RID: 23178
		public bool Wall;

		// Token: 0x04005A8B RID: 23179
		public int OverridePaintTile = -1;

		// Token: 0x04005A8C RID: 23180
		public int OverridePaintWall = -1;

		// Token: 0x04005A8D RID: 23181
		public bool CrowningOnTop;

		// Token: 0x04005A8E RID: 23182
		public bool CrowningOnBottom;

		// Token: 0x04005A8F RID: 23183
		public bool CrowningStopsAtPillar;

		// Token: 0x04005A90 RID: 23184
		public bool AlwaysPlaceEntirePillar = true;
	}
}
