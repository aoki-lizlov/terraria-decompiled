using System;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004D1 RID: 1233
	public abstract class DungeonWindow : DungeonFeature
	{
		// Token: 0x060034E1 RID: 13537 RVA: 0x0060B00F File Offset: 0x0060920F
		public DungeonWindow(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0060B028 File Offset: 0x00609228
		public override bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return feature is DungeonGlobalWallVariants;
		}
	}
}
