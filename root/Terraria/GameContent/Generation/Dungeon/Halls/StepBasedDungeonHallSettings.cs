using System;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C4 RID: 1220
	public abstract class StepBasedDungeonHallSettings : DungeonHallSettings
	{
		// Token: 0x060034B7 RID: 13495 RVA: 0x006073D8 File Offset: 0x006055D8
		protected StepBasedDungeonHallSettings()
		{
		}

		// Token: 0x04005A5B RID: 23131
		public int OverrideStrength;

		// Token: 0x04005A5C RID: 23132
		public int OverrideSteps;

		// Token: 0x04005A5D RID: 23133
		public bool ForceHorizontal;

		// Token: 0x04005A5E RID: 23134
		public double OverrideInteriorToExteriorRatio;
	}
}
