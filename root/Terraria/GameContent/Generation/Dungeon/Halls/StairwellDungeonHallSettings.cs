using System;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004BE RID: 1214
	public class StairwellDungeonHallSettings : DungeonHallSettings
	{
		// Token: 0x060034A7 RID: 13479 RVA: 0x00606E7D File Offset: 0x0060507D
		public StairwellDungeonHallSettings()
		{
		}

		// Token: 0x04005A43 RID: 23107
		public int MaxDistFromLine = 20;

		// Token: 0x04005A44 RID: 23108
		public int PointVariance = 4;

		// Token: 0x04005A45 RID: 23109
		public int InnerBoundsSize = 3;

		// Token: 0x04005A46 RID: 23110
		public int OuterBoundsSize = 8;

		// Token: 0x04005A47 RID: 23111
		public double Gradient = 0.35;

		// Token: 0x04005A48 RID: 23112
		public bool IsEntranceHall;
	}
}
