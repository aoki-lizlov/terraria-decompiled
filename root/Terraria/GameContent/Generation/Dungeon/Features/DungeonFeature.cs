using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004E8 RID: 1256
	public abstract class DungeonFeature : IDungeonFeature
	{
		// Token: 0x06003529 RID: 13609 RVA: 0x00613AC8 File Offset: 0x00611CC8
		public DungeonFeature(DungeonFeatureSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x0600352A RID: 13610
		public abstract bool GenerateFeature(DungeonData data, int x, int y);

		// Token: 0x0600352B RID: 13611 RVA: 0x000379E9 File Offset: 0x00035BE9
		public virtual bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return true;
		}

		// Token: 0x04005A9A RID: 23194
		public DungeonFeatureSettings settings;

		// Token: 0x04005A9B RID: 23195
		public DungeonBounds Bounds = new DungeonBounds();

		// Token: 0x04005A9C RID: 23196
		public bool generated;
	}
}
