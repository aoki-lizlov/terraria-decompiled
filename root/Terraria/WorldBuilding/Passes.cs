using System;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AE RID: 174
	public static class Passes
	{
		// Token: 0x020006D8 RID: 1752
		public class Clear : GenPass
		{
			// Token: 0x06003F39 RID: 16185 RVA: 0x00699B09 File Offset: 0x00697D09
			public Clear()
				: base("clear", 1.0)
			{
			}

			// Token: 0x06003F3A RID: 16186 RVA: 0x00699B20 File Offset: 0x00697D20
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				for (int i = 0; i < GenBase._worldWidth; i++)
				{
					for (int j = 0; j < GenBase._worldHeight; j++)
					{
						if (GenBase._tiles[i, j] == null)
						{
							GenBase._tiles[i, j] = new Tile();
						}
						else
						{
							GenBase._tiles[i, j].ClearEverything();
						}
					}
				}
			}
		}

		// Token: 0x020006D9 RID: 1753
		public class ScatterCustom : GenPass
		{
			// Token: 0x06003F3B RID: 16187 RVA: 0x00699B7F File Offset: 0x00697D7F
			public ScatterCustom(string name, double loadWeight, int count, GenBase.CustomPerUnitAction perUnit = null)
				: base(name, loadWeight)
			{
				this._perUnit = perUnit;
				this._count = count;
			}

			// Token: 0x06003F3C RID: 16188 RVA: 0x00699B98 File Offset: 0x00697D98
			public void SetCustomAction(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x06003F3D RID: 16189 RVA: 0x00699BA4 File Offset: 0x00697DA4
			protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
			{
				int i = this._count;
				while (i > 0)
				{
					if (this._perUnit(GenBase._random.Next(1, GenBase._worldWidth), GenBase._random.Next(1, GenBase._worldHeight), new object[0]))
					{
						i--;
					}
				}
			}

			// Token: 0x040067C7 RID: 26567
			private GenBase.CustomPerUnitAction _perUnit;

			// Token: 0x040067C8 RID: 26568
			private int _count;
		}
	}
}
