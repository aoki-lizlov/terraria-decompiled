using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200056C RID: 1388
	public abstract class AEntitySource_Tile : IEntitySource
	{
		// Token: 0x060037EF RID: 14319 RVA: 0x0063068B File Offset: 0x0062E88B
		public AEntitySource_Tile(int tileCoordsX, int tileCoordsY)
		{
			this.TileCoords = new Point(tileCoordsX, tileCoordsY);
		}

		// Token: 0x04005C2A RID: 23594
		public readonly Point TileCoords;
	}
}
