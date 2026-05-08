using System;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004AD RID: 1197
	public class GenShapeDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x06003450 RID: 13392 RVA: 0x00603569 File Offset: 0x00601769
		public override int GetBoundingRadius()
		{
			return this.BoundingRadius;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x0060079F File Offset: 0x005FE99F
		public GenShapeDungeonRoomSettings()
		{
		}

		// Token: 0x04005A02 RID: 23042
		public GenShapeType ShapeType;

		// Token: 0x04005A03 RID: 23043
		public GenShape InnerShape;

		// Token: 0x04005A04 RID: 23044
		public GenShape OuterShape;

		// Token: 0x04005A05 RID: 23045
		public int BoundingRadius;
	}
}
