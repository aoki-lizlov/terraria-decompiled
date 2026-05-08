using System;
using Terraria.GameContent.Generation.Dungeon.Rooms;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004EA RID: 1258
	public class DungeonPitTrapSettings : DungeonFeatureSettings
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x00609B30 File Offset: 0x00607D30
		public DungeonPitTrapSettings()
		{
		}

		// Token: 0x04005A9F RID: 23199
		public DungeonGenerationStyleData Style;

		// Token: 0x04005AA0 RID: 23200
		public int Width;

		// Token: 0x04005AA1 RID: 23201
		public int Height;

		// Token: 0x04005AA2 RID: 23202
		public int EdgeWidth;

		// Token: 0x04005AA3 RID: 23203
		public int EdgeHeight;

		// Token: 0x04005AA4 RID: 23204
		public int TopDensity;

		// Token: 0x04005AA5 RID: 23205
		public bool Flooded;

		// Token: 0x04005AA6 RID: 23206
		public DungeonRoom ConnectedRoom;
	}
}
