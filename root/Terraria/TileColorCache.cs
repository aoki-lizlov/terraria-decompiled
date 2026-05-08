using System;

namespace Terraria
{
	// Token: 0x0200004D RID: 77
	public struct TileColorCache
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0035615B File Offset: 0x0035435B
		public void ApplyToBlock(Tile tile)
		{
			tile.color(this.Color);
			tile.fullbrightBlock(this.FullBright);
			tile.invisibleBlock(this.Invisible);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00356181 File Offset: 0x00354381
		public void ApplyToWall(Tile tile)
		{
			tile.wallColor(this.Color);
			tile.fullbrightWall(this.FullBright);
			tile.invisibleWall(this.Invisible);
		}

		// Token: 0x040009D3 RID: 2515
		public byte Color;

		// Token: 0x040009D4 RID: 2516
		public bool FullBright;

		// Token: 0x040009D5 RID: 2517
		public bool Invisible;
	}
}
