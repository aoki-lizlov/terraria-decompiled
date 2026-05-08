using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004E9 RID: 1257
	public abstract class GlobalDungeonFeature : IDungeonFeature
	{
		// Token: 0x0600352C RID: 13612 RVA: 0x00613AE2 File Offset: 0x00611CE2
		public GlobalDungeonFeature(DungeonFeatureSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x0600352D RID: 13613
		public abstract bool GenerateFeature(DungeonData data);

		// Token: 0x04005A9D RID: 23197
		public DungeonFeatureSettings settings;

		// Token: 0x04005A9E RID: 23198
		public bool generated;
	}
}
