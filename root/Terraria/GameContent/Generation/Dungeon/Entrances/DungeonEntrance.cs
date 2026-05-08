using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation.Dungeon.Features;

namespace Terraria.GameContent.Generation.Dungeon.Entrances
{
	// Token: 0x020004F1 RID: 1265
	public abstract class DungeonEntrance
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x00615830 File Offset: 0x00613A30
		public bool Processed
		{
			get
			{
				return this.calculated || this.generated;
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x00615842 File Offset: 0x00613A42
		public DungeonEntrance(DungeonEntranceSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x06003543 RID: 13635
		public abstract void CalculateEntrance(DungeonData data, int x, int y);

		// Token: 0x06003544 RID: 13636
		public abstract bool GenerateEntrance(DungeonData data, int x, int y);

		// Token: 0x06003545 RID: 13637 RVA: 0x0061585C File Offset: 0x00613A5C
		public virtual bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return !(feature is DungeonGlobalBiomeChests);
		}

		// Token: 0x04005AB4 RID: 23220
		public DungeonEntranceSettings settings;

		// Token: 0x04005AB5 RID: 23221
		public bool calculated;

		// Token: 0x04005AB6 RID: 23222
		public bool generated;

		// Token: 0x04005AB7 RID: 23223
		public DungeonBounds Bounds = new DungeonBounds();

		// Token: 0x04005AB8 RID: 23224
		public Point OldManSpawn;
	}
}
