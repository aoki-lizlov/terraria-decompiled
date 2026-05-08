using System;
using ReLogic.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004B4 RID: 1204
	public abstract class StepBasedDungeonRoomSettings : DungeonRoomSettings
	{
		// Token: 0x06003466 RID: 13414 RVA: 0x00603E23 File Offset: 0x00602023
		public override int GetBoundingRadius()
		{
			return (int)((double)this.OverrideStrength * 0.8 + 5.0 + (double)this.OverrideSteps * 0.5 * 1.4);
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x0060079F File Offset: 0x005FE99F
		protected StepBasedDungeonRoomSettings()
		{
		}

		// Token: 0x04005A2B RID: 23083
		public int OverrideStrength;

		// Token: 0x04005A2C RID: 23084
		public int OverrideSteps;

		// Token: 0x04005A2D RID: 23085
		public Vector2D OverrideStartPosition;

		// Token: 0x04005A2E RID: 23086
		public Vector2D OverrideEndPosition;

		// Token: 0x04005A2F RID: 23087
		public Vector2D OverrideVelocity;

		// Token: 0x04005A30 RID: 23088
		public double OverrideInteriorToExteriorRatio;
	}
}
