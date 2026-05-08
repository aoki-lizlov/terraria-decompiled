using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200056D RID: 1389
	public class EntitySource_TileInteraction : AEntitySource_Tile
	{
		// Token: 0x060037F0 RID: 14320 RVA: 0x006306A0 File Offset: 0x0062E8A0
		public EntitySource_TileInteraction(IEntitySourceTarget entity, int tileCoordsX, int tileCoordsY)
			: base(tileCoordsX, tileCoordsY)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C2B RID: 23595
		public readonly IEntitySourceTarget Entity;
	}
}
