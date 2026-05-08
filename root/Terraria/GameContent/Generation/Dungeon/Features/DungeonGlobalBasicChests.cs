using System;
using Terraria.GameContent.Generation.Dungeon.Rooms;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004D5 RID: 1237
	public class DungeonGlobalBasicChests : GlobalDungeonFeature
	{
		// Token: 0x060034E9 RID: 13545 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalBasicChests(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x0060B4FB File Offset: 0x006096FB
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.BasicChests(data);
			this.generated = true;
			return true;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x0060B514 File Offset: 0x00609714
		private void BasicChests(DungeonData data)
		{
			for (int i = 0; i < data.dungeonRooms.Count; i++)
			{
				DungeonRoom dungeonRoom = data.dungeonRooms[i];
				int num = 0;
				while (num < 1000 && !dungeonRoom.TryGenerateChestInRoom(data, this))
				{
					num++;
				}
			}
		}
	}
}
