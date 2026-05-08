using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004D0 RID: 1232
	public abstract class DungeonWindowSettings : DungeonFeatureSettings
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x0060AFF9 File Offset: 0x006091F9
		protected DungeonWindowSettings()
		{
		}

		// Token: 0x04005A7B RID: 23163
		public DungeonGenerationStyleData Style;

		// Token: 0x04005A7C RID: 23164
		public int OverrideGlassPaint = -1;

		// Token: 0x04005A7D RID: 23165
		public int OverrideGlassType = -1;

		// Token: 0x04005A7E RID: 23166
		public bool Closed;
	}
}
