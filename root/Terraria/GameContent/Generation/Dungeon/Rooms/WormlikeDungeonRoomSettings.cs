using System;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004AA RID: 1194
	public class WormlikeDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x06003446 RID: 13382 RVA: 0x00602D7A File Offset: 0x00600F7A
		public override int GetBoundingRadius()
		{
			return (int)((16.200000000000003 + (double)(this.FirstSideIterations + this.SecondSideIterations) * 0.5 * 1.4) * 0.5);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x0060079F File Offset: 0x005FE99F
		public WormlikeDungeonRoomSettings()
		{
		}

		// Token: 0x040059F4 RID: 23028
		public int FirstSideIterations;

		// Token: 0x040059F5 RID: 23029
		public int SecondSideIterations;
	}
}
