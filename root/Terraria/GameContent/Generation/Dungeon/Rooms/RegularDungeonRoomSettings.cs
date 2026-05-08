using System;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004AF RID: 1199
	public class RegularDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x0600345E RID: 13406 RVA: 0x00603BAC File Offset: 0x00601DAC
		public override int GetBoundingRadius()
		{
			return (this.OverrideInnerBoundsSize + this.OverrideOuterBoundsSize) * 142 / 100;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x0060079F File Offset: 0x005FE99F
		public RegularDungeonRoomSettings()
		{
		}

		// Token: 0x04005A09 RID: 23049
		public int OverrideInnerBoundsSize;

		// Token: 0x04005A0A RID: 23050
		public int OverrideOuterBoundsSize;
	}
}
