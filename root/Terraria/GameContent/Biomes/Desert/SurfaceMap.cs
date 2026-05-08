using System;
using Terraria.ID;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x0200051C RID: 1308
	public class SurfaceMap
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x00628728 File Offset: 0x00626928
		public int Width
		{
			get
			{
				return this._heights.Length;
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x00628734 File Offset: 0x00626934
		private SurfaceMap(short[] heights, int x)
		{
			this._heights = heights;
			this.X = x;
			int num = 0;
			int num2 = int.MaxValue;
			int num3 = 0;
			for (int i = 0; i < heights.Length; i++)
			{
				num3 += (int)heights[i];
				num = Math.Max(num, (int)heights[i]);
				num2 = Math.Min(num2, (int)heights[i]);
			}
			if ((double)num > Main.worldSurface - 10.0)
			{
				num = (int)Main.worldSurface - 10;
			}
			this.Bottom = num;
			this.Top = num2;
			this.Average = (double)num3 / (double)this._heights.Length;
		}

		// Token: 0x17000459 RID: 1113
		public short this[int absoluteX]
		{
			get
			{
				return this._heights[absoluteX - this.X];
			}
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x006287D8 File Offset: 0x006269D8
		public static SurfaceMap FromArea(int startX, int width)
		{
			int num = Main.maxTilesY / 2;
			short[] array = new short[width];
			for (int i = startX; i < startX + width; i++)
			{
				bool flag = false;
				int num2 = 0;
				for (int j = 50; j < 50 + num; j++)
				{
					if (Main.tile[i, j].active())
					{
						if (TileID.Sets.Clouds[(int)Main.tile[i, j].type])
						{
							flag = false;
						}
						else if (!flag)
						{
							num2 = j;
							flag = true;
						}
					}
					if (!flag)
					{
						num2 = num + 50;
					}
				}
				array[i - startX] = (short)num2;
			}
			return new SurfaceMap(array, startX);
		}

		// Token: 0x04005B33 RID: 23347
		public readonly double Average;

		// Token: 0x04005B34 RID: 23348
		public readonly int Bottom;

		// Token: 0x04005B35 RID: 23349
		public readonly int Top;

		// Token: 0x04005B36 RID: 23350
		public readonly int X;

		// Token: 0x04005B37 RID: 23351
		private readonly short[] _heights;
	}
}
