using System;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A8 RID: 1192
	public class LivingTreeDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x0600343A RID: 13370 RVA: 0x00602472 File Offset: 0x00600672
		public override int GetBoundingRadius()
		{
			return this.BoundingRadius;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x0060079F File Offset: 0x005FE99F
		public LivingTreeDungeonRoomSettings()
		{
		}

		// Token: 0x040059EC RID: 23020
		public int InnerWidth;

		// Token: 0x040059ED RID: 23021
		public int InnerHeight;

		// Token: 0x040059EE RID: 23022
		public int Depth;

		// Token: 0x040059EF RID: 23023
		public int BoundingRadius;
	}
}
