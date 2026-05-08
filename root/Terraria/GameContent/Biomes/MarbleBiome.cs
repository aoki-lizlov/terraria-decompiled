using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200050B RID: 1291
	public class MarbleBiome : MicroBiome
	{
		// Token: 0x0600363F RID: 13887 RVA: 0x00623EEC File Offset: 0x006220EC
		private void SmoothSlope(int x, int y)
		{
			MarbleBiome.Slab slab = this._slabs[x, y];
			if (!slab.IsSolid)
			{
				return;
			}
			bool isSolid = this._slabs[x, y - 1].IsSolid;
			bool isSolid2 = this._slabs[x, y + 1].IsSolid;
			bool isSolid3 = this._slabs[x - 1, y].IsSolid;
			bool isSolid4 = this._slabs[x + 1, y].IsSolid;
			switch (((isSolid ? 1 : 0) << 3) | ((isSolid2 ? 1 : 0) << 2) | ((isSolid3 ? 1 : 0) << 1) | (isSolid4 ? 1 : 0))
			{
			case 4:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.HalfBrick));
				return;
			case 5:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomRightFilled));
				return;
			case 6:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomLeftFilled));
				return;
			case 9:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopRightFilled));
				return;
			case 10:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopLeftFilled));
				return;
			}
			this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid));
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x0062407C File Offset: 0x0062227C
		private void PlaceSlab(MarbleBiome.Slab slab, int originX, int originY, int scale)
		{
			ushort num = 367;
			ushort num2 = 178;
			if (WorldGen.drunkWorldGen)
			{
				num = 368;
				num2 = 180;
			}
			int num3 = -1;
			int num4 = scale + 1;
			int num5 = 0;
			int num6 = scale;
			for (int i = num3; i < num4; i++)
			{
				if ((i != num3 && i != num4 - 1) || WorldGen.genRand.Next(2) != 0)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						num5--;
					}
					if (WorldGen.genRand.Next(2) == 0)
					{
						num6++;
					}
					for (int j = num5; j < num6; j++)
					{
						Tile tile = GenBase._tiles[originX + i, originY + j];
						tile.ResetToType(TileID.Sets.Ore[(int)tile.type] ? tile.type : num);
						bool flag = slab.State(i, j, scale);
						tile.active(flag);
						if (slab.HasWall)
						{
							tile.wall = num2;
						}
						WorldUtils.TileFrame(originX + i, originY + j, true);
						WorldGen.SquareWallFrame(originX + i, originY + j, true);
						Tile.SmoothSlope(originX + i, originY + j, true, false);
						if (WorldGen.SolidTile(originX + i, originY + j - 1, false) && GenBase._random.Next(4) == 0)
						{
							WorldGen.PlaceTight(originX + i, originY + j, false);
						}
						if (WorldGen.SolidTile(originX + i, originY + j, false) && GenBase._random.Next(4) == 0)
						{
							WorldGen.PlaceTight(originX + i, originY + j - 1, false);
						}
					}
				}
			}
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x0062420C File Offset: 0x0062240C
		private static bool IsGroupSolid(int x, int y, int scale)
		{
			int num = 0;
			for (int i = 0; i < scale; i++)
			{
				for (int j = 0; j < scale; j++)
				{
					if (WorldGen.SolidOrSlopedTile(x + i, y + j))
					{
						num++;
					}
				}
			}
			return num > scale / 4 * 3;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x0062424C File Offset: 0x0062244C
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			if (WorldGen.BiomeTileCheck(origin.X, origin.Y))
			{
				return false;
			}
			if (this._slabs == null)
			{
				this._slabs = new MarbleBiome.Slab[56, 26];
			}
			int num = GenBase._random.Next(80, 150) / 3;
			int num2 = GenBase._random.Next(40, 60) / 3;
			int num3 = (num2 * 3 - GenBase._random.Next(20, 30)) / 3;
			origin.X -= num * 3 / 2;
			origin.Y -= num2 * 3 / 2;
			for (int i = -1; i < num + 1; i++)
			{
				double num4 = (double)(i - num / 2) / (double)num + 0.5;
				int num5 = (int)((0.5 - Math.Abs(num4 - 0.5)) * 5.0) - 2;
				for (int j = -1; j < num2 + 1; j++)
				{
					bool flag = true;
					bool flag2 = false;
					bool flag3 = MarbleBiome.IsGroupSolid(i * 3 + origin.X, j * 3 + origin.Y, 3);
					int num6 = Math.Abs(j - num2 / 2) - num3 / 4 + num5;
					if (num6 > 3)
					{
						flag2 = flag3;
						flag = false;
					}
					else if (num6 > 0)
					{
						flag2 = j - num2 / 2 > 0 || flag3;
						flag = j - num2 / 2 < 0 || num6 <= 2;
					}
					else if (num6 == 0)
					{
						flag2 = GenBase._random.Next(2) == 0 && (j - num2 / 2 > 0 || flag3);
					}
					if (Math.Abs(num4 - 0.5) > 0.35 + GenBase._random.NextDouble() * 0.1 && !flag3)
					{
						flag = false;
						flag2 = false;
					}
					this._slabs[i + 1, j + 1] = MarbleBiome.Slab.Create(flag2 ? new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid) : new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty), flag);
				}
			}
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < num2; l++)
				{
					this.SmoothSlope(k + 1, l + 1);
				}
			}
			int num7 = num / 2;
			int num8 = num2 / 2;
			int num9 = (num8 + 1) * (num8 + 1);
			double num10 = GenBase._random.NextDouble() * 2.0 - 1.0;
			double num11 = GenBase._random.NextDouble() * 2.0 - 1.0;
			double num12 = GenBase._random.NextDouble() * 2.0 - 1.0;
			double num13 = 0.0;
			for (int m = 0; m <= num; m++)
			{
				double num14 = (double)num8 / (double)num7 * (double)(m - num7);
				int num15 = Math.Min(num8, (int)Math.Sqrt(Math.Max(0.0, (double)num9 - num14 * num14)));
				if (m < num / 2)
				{
					num13 += Utils.Lerp(num10, num11, (double)m / (double)(num / 2));
				}
				else
				{
					num13 += Utils.Lerp(num11, num12, (double)m / (double)(num / 2) - 1.0);
				}
				for (int n = num8 - num15; n <= num8 + num15; n++)
				{
					this.PlaceSlab(this._slabs[m + 1, n + 1], m * 3 + origin.X, n * 3 + origin.Y + (int)num13, 3);
				}
			}
			structures.AddStructure(new Rectangle(origin.X, origin.Y, num * 3, num2 * 3), 8);
			return true;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0061F6EC File Offset: 0x0061D8EC
		public MarbleBiome()
		{
		}

		// Token: 0x04005B1A RID: 23322
		private const int SCALE = 3;

		// Token: 0x04005B1B RID: 23323
		private MarbleBiome.Slab[,] _slabs;

		// Token: 0x02000999 RID: 2457
		// (Invoke) Token: 0x06004997 RID: 18839
		private delegate bool SlabState(int x, int y, int scale);

		// Token: 0x0200099A RID: 2458
		private static class SlabStates
		{
			// Token: 0x0600499A RID: 18842 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public static bool Empty(int x, int y, int scale)
			{
				return false;
			}

			// Token: 0x0600499B RID: 18843 RVA: 0x000379E9 File Offset: 0x00035BE9
			public static bool Solid(int x, int y, int scale)
			{
				return true;
			}

			// Token: 0x0600499C RID: 18844 RVA: 0x006D2431 File Offset: 0x006D0631
			public static bool HalfBrick(int x, int y, int scale)
			{
				return y >= scale / 2;
			}

			// Token: 0x0600499D RID: 18845 RVA: 0x006D243C File Offset: 0x006D063C
			public static bool BottomRightFilled(int x, int y, int scale)
			{
				return x >= scale - y;
			}

			// Token: 0x0600499E RID: 18846 RVA: 0x006D2447 File Offset: 0x006D0647
			public static bool BottomLeftFilled(int x, int y, int scale)
			{
				return x < y;
			}

			// Token: 0x0600499F RID: 18847 RVA: 0x006D244D File Offset: 0x006D064D
			public static bool TopRightFilled(int x, int y, int scale)
			{
				return x > y;
			}

			// Token: 0x060049A0 RID: 18848 RVA: 0x006D2453 File Offset: 0x006D0653
			public static bool TopLeftFilled(int x, int y, int scale)
			{
				return x < scale - y;
			}
		}

		// Token: 0x0200099B RID: 2459
		private struct Slab
		{
			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060049A1 RID: 18849 RVA: 0x006D245B File Offset: 0x006D065B
			public bool IsSolid
			{
				get
				{
					return this.State != new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty);
				}
			}

			// Token: 0x060049A2 RID: 18850 RVA: 0x006D2474 File Offset: 0x006D0674
			private Slab(MarbleBiome.SlabState state, bool hasWall)
			{
				this.State = state;
				this.HasWall = hasWall;
			}

			// Token: 0x060049A3 RID: 18851 RVA: 0x006D2484 File Offset: 0x006D0684
			public MarbleBiome.Slab WithState(MarbleBiome.SlabState state)
			{
				return new MarbleBiome.Slab(state, this.HasWall);
			}

			// Token: 0x060049A4 RID: 18852 RVA: 0x006D2492 File Offset: 0x006D0692
			public static MarbleBiome.Slab Create(MarbleBiome.SlabState state, bool hasWall)
			{
				return new MarbleBiome.Slab(state, hasWall);
			}

			// Token: 0x04007676 RID: 30326
			public readonly MarbleBiome.SlabState State;

			// Token: 0x04007677 RID: 30327
			public readonly bool HasWall;
		}
	}
}
