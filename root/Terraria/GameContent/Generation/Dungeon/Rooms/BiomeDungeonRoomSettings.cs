using System;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A3 RID: 1187
	public class BiomeDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x06003418 RID: 13336 RVA: 0x00600792 File Offset: 0x005FE992
		public override int GetBoundingRadius()
		{
			return BiomeDungeonRoom.GetBiomeRoomOuterSize(this.StyleData);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x0060079F File Offset: 0x005FE99F
		public BiomeDungeonRoomSettings()
		{
		}
	}
}
