using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200056E RID: 1390
	public class EntitySource_OverfullChest : AEntitySource_Tile
	{
		// Token: 0x060037F1 RID: 14321 RVA: 0x006306B1 File Offset: 0x0062E8B1
		public EntitySource_OverfullChest(int tileCoordsX, int tileCoordsY, Chest chest)
			: base(tileCoordsX, tileCoordsY)
		{
			this.Chest = chest;
		}

		// Token: 0x04005C2C RID: 23596
		public readonly Chest Chest;
	}
}
