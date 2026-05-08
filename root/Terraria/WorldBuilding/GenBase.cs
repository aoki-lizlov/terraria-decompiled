using System;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A6 RID: 166
	public class GenBase
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x004DDDC9 File Offset: 0x004DBFC9
		protected static UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x004DDDD0 File Offset: 0x004DBFD0
		protected static Tile[,] _tiles
		{
			get
			{
				return Main.tile;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x004DDDD7 File Offset: 0x004DBFD7
		protected static int _worldWidth
		{
			get
			{
				return Main.maxTilesX;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x004DDDDE File Offset: 0x004DBFDE
		protected static int _worldHeight
		{
			get
			{
				return Main.maxTilesY;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0000357B File Offset: 0x0000177B
		public GenBase()
		{
		}

		// Token: 0x020006B0 RID: 1712
		// (Invoke) Token: 0x06003EE2 RID: 16098
		public delegate bool CustomPerUnitAction(int x, int y, params object[] args);
	}
}
