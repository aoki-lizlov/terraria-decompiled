using System;
using Microsoft.Xna.Framework;
using Terraria.Testing;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000C2 RID: 194
	public static class WorldUtils
	{
		// Token: 0x060017DA RID: 6106 RVA: 0x004E052C File Offset: 0x004DE72C
		public static Rectangle ClampToWorld(Rectangle tileRectangle, int fluff = 0)
		{
			int num = Math.Max(fluff, Math.Min(tileRectangle.Left, Main.maxTilesX - fluff));
			int num2 = Math.Max(fluff, Math.Min(tileRectangle.Top, Main.maxTilesY - fluff));
			int num3 = Math.Max(fluff, Math.Min(tileRectangle.Right, Main.maxTilesX - fluff));
			int num4 = Math.Max(fluff, Math.Min(tileRectangle.Bottom, Main.maxTilesY - fluff));
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x004E05B0 File Offset: 0x004DE7B0
		public static Rectangle GetWorldPlayArea()
		{
			int num = 640;
			Point point = new Point((int)Main.leftWorld + num, (int)Main.topWorld + num);
			Point point2 = new Point((int)Main.rightWorld - num, (int)Main.bottomWorld - num);
			return new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x004E061C File Offset: 0x004DE81C
		public static Rectangle ClampToWorldBorders(Rectangle worldRect)
		{
			if (DebugOptions.noLimits)
			{
				return worldRect;
			}
			return Utils.Clamp(worldRect, WorldUtils.GetWorldPlayArea());
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x004E0632 File Offset: 0x004DE832
		public static bool Gen(Point origin, GenShape shape, GenAction action)
		{
			return shape.Perform(origin, action);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x004E063C File Offset: 0x004DE83C
		public static bool Gen(Point origin, GenShapeActionPair pair)
		{
			return pair.Shape.Perform(origin, pair.Action);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x004E0650 File Offset: 0x004DE850
		public static bool Find(Point origin, GenSearch search, out Point result)
		{
			result = search.Find(origin);
			return !(result == GenSearch.NOT_FOUND);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x004E0674 File Offset: 0x004DE874
		public static void ClearTile(int x, int y, bool frameNeighbors = false)
		{
			Main.tile[x, y].ClearTile();
			if (frameNeighbors)
			{
				WorldGen.TileFrame(x + 1, y, false, false);
				WorldGen.TileFrame(x - 1, y, false, false);
				WorldGen.TileFrame(x, y + 1, false, false);
				WorldGen.TileFrame(x, y - 1, false, false);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x004E06C1 File Offset: 0x004DE8C1
		public static void ClearWall(int x, int y, bool frameNeighbors = false)
		{
			Main.tile[x, y].wall = 0;
			if (frameNeighbors)
			{
				WorldGen.SquareWallFrame(x + 1, y, true);
				WorldGen.SquareWallFrame(x - 1, y, true);
				WorldGen.SquareWallFrame(x, y + 1, true);
				WorldGen.SquareWallFrame(x, y - 1, true);
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x004E0700 File Offset: 0x004DE900
		public static void TileFrame(int x, int y, bool frameNeighbors = false)
		{
			WorldGen.TileFrame(x, y, true, false);
			if (frameNeighbors)
			{
				WorldGen.TileFrame(x + 1, y, true, false);
				WorldGen.TileFrame(x - 1, y, true, false);
				WorldGen.TileFrame(x, y + 1, true, false);
				WorldGen.TileFrame(x, y - 1, true, false);
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x004E073A File Offset: 0x004DE93A
		public static void WallFrame(int x, int y, bool frameNeighbors = false)
		{
			Framing.WallFrame(x, y, true);
			if (frameNeighbors)
			{
				Framing.WallFrame(x + 1, y, true);
				Framing.WallFrame(x - 1, y, true);
				Framing.WallFrame(x, y + 1, true);
				Framing.WallFrame(x, y - 1, true);
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x004E076F File Offset: 0x004DE96F
		public static void ClearChestLocation(int x, int y)
		{
			WorldUtils.ClearTile(x, y, true);
			WorldUtils.ClearTile(x - 1, y, true);
			WorldUtils.ClearTile(x, y - 1, true);
			WorldUtils.ClearTile(x - 1, y - 1, true);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x004E079C File Offset: 0x004DE99C
		public static void WireLine(Point start, Point end)
		{
			Point point = start;
			Point point2 = end;
			if (end.X < start.X)
			{
				Utils.Swap<int>(ref end.X, ref start.X);
			}
			if (end.Y < start.Y)
			{
				Utils.Swap<int>(ref end.Y, ref start.Y);
			}
			for (int i = start.X; i <= end.X; i++)
			{
				WorldGen.PlaceWire(i, point.Y);
			}
			for (int j = start.Y; j <= end.Y; j++)
			{
				WorldGen.PlaceWire(point2.X, j);
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x004E0835 File Offset: 0x004DEA35
		public static void DebugRegen()
		{
			WorldGen.GenerateWorld(null, null);
			Main.NewText("World Regen Complete.", byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x004E0858 File Offset: 0x004DEA58
		public static void DebugRotate()
		{
			int num = 0;
			int num2 = 0;
			int maxTilesY = Main.maxTilesY;
			for (int i = 0; i < Main.maxTilesX / Main.maxTilesY; i++)
			{
				for (int j = 0; j < maxTilesY / 2; j++)
				{
					for (int k = j; k < maxTilesY - j; k++)
					{
						Tile tile = Main.tile[k + num, j + num2];
						Main.tile[k + num, j + num2] = Main.tile[j + num, maxTilesY - k + num2];
						Main.tile[j + num, maxTilesY - k + num2] = Main.tile[maxTilesY - k + num, maxTilesY - j + num2];
						Main.tile[maxTilesY - k + num, maxTilesY - j + num2] = Main.tile[maxTilesY - j + num, k + num2];
						Main.tile[maxTilesY - j + num, k + num2] = tile;
					}
				}
				num += maxTilesY;
			}
		}
	}
}
