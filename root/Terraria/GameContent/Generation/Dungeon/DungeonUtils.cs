using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x0200049F RID: 1183
	public class DungeonUtils
	{
		// Token: 0x060033D9 RID: 13273 RVA: 0x005FCB64 File Offset: 0x005FAD64
		public static void CalculatePlatformsAndDoorsOnEdgesOfRoom(DungeonData dungeonData, DungeonBounds innerBounds, DungeonGenerationStyleData styleData, int? doorFluff = null, int? platformFluff = null)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			if (styleData == null)
			{
				if (!WorldGen.InWorld(innerBounds.Center.X, innerBounds.Center.Y, 5))
				{
					return;
				}
				Tile tile = Main.tile[innerBounds.Center.X, innerBounds.Center.Y];
				styleData = DungeonGenerationStyles.GetStyleForWall(dungeonData.genVars.dungeonGenerationStyles, (int)tile.wall);
				if (styleData == null && tile.active())
				{
					styleData = DungeonGenerationStyles.GetStyleForTile(dungeonData.genVars.dungeonGenerationStyles, (int)tile.type);
				}
				if (styleData == null)
				{
					styleData = dungeonData.genVars.dungeonStyle;
				}
			}
			bool flag = styleData.Style == 0;
			ushort brickTileType = styleData.BrickTileType;
			ushort brickWallType = styleData.BrickWallType;
			int num = Math.Max(5, innerBounds.Left);
			int num2 = Math.Min(Main.maxTilesX - 5, innerBounds.Right);
			int num3 = Math.Max(5, innerBounds.Top);
			int num4 = Math.Min(Main.maxTilesY - 5, innerBounds.Bottom);
			bool flag2 = false;
			bool flag3 = false;
			for (int i = num; i <= num2; i++)
			{
				if (!flag2 && !Main.tile[i, num3 - 1].active())
				{
					DungeonPlatformData dungeonPlatformData = new DungeonPlatformData
					{
						Position = new Point(i, num3 - 1),
						InAHallway = false,
						OverrideStyle = new int?(styleData.GetPlatformStyle(genRand))
					};
					if (platformFluff != null)
					{
						dungeonPlatformData.OverrideHeightFluff = new int?(platformFluff.Value);
					}
					dungeonData.dungeonPlatformData.Add(dungeonPlatformData);
					flag2 = true;
				}
				if (!flag3 && !Main.tile[i, num4 + 1].active())
				{
					DungeonPlatformData dungeonPlatformData2 = new DungeonPlatformData
					{
						Position = new Point(i, num4 + 1),
						InAHallway = false,
						OverrideStyle = new int?(styleData.GetPlatformStyle(genRand))
					};
					if (platformFluff != null)
					{
						dungeonPlatformData2.OverrideHeightFluff = new int?(platformFluff.Value);
					}
					dungeonData.dungeonPlatformData.Add(dungeonPlatformData2);
					flag3 = true;
				}
				if (flag2 && flag3)
				{
					break;
				}
			}
			if (styleData.DoorItemTypes != null)
			{
				int num5 = ((flag || styleData.DoorItemTypes.Length == 0) ? (-1) : styleData.DoorItemTypes[genRand.Next(styleData.DoorItemTypes.Length)]);
				bool flag4 = false;
				bool flag5 = false;
				for (int j = num3; j <= num4; j++)
				{
					if (!flag4 && !Main.tile[num - 1, j].active())
					{
						bool flag6 = doorFluff != null && doorFluff.Value == 0;
						DungeonDoorData dungeonDoorData = new DungeonDoorData
						{
							OverrideBrickTileType = new ushort?(brickTileType),
							OverrideBrickWallType = new ushort?(brickWallType),
							Position = new Point(num - 1, j),
							Direction = -1,
							InAHallway = false,
							SkipOtherDoorsCheck = flag6,
							SkipSpaceCheck = flag6,
							AlwaysClearArea = true
						};
						if (doorFluff != null)
						{
							dungeonDoorData.OverrideWidthFluff = new int?(doorFluff.Value);
						}
						if (num5 >= 0)
						{
							PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[num5];
							dungeonDoorData.OverrideStyle = new int?((int)placementDetails.tileStyle);
						}
						dungeonData.dungeonDoorData.Add(dungeonDoorData);
						flag4 = true;
					}
					if (!flag5 && !Main.tile[num2 + 1, j].active())
					{
						bool flag7 = doorFluff != null && doorFluff.Value == 0;
						DungeonDoorData dungeonDoorData2 = new DungeonDoorData
						{
							OverrideBrickTileType = new ushort?(brickTileType),
							OverrideBrickWallType = new ushort?(brickWallType),
							Position = new Point(num2 + 1, j),
							Direction = 1,
							InAHallway = false,
							SkipOtherDoorsCheck = flag7,
							SkipSpaceCheck = flag7,
							AlwaysClearArea = true
						};
						if (doorFluff != null)
						{
							dungeonDoorData2.OverrideWidthFluff = new int?(doorFluff.Value);
						}
						if (num5 >= 0)
						{
							PlacementDetails placementDetails2 = ItemID.Sets.DerivedPlacementDetails[num5];
							dungeonDoorData2.OverrideStyle = new int?((int)placementDetails2.tileStyle);
						}
						dungeonData.dungeonDoorData.Add(dungeonDoorData2);
						flag5 = true;
					}
					if (flag4 && flag5)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x005FCFC0 File Offset: 0x005FB1C0
		public static void CalculatePlatformAndDoorsOnHallway(DungeonData dungeonData, Vector2D hallwayPoint, double hallwayDirectionY, DungeonGenerationStyleData styleData, double doorVariance = 0.1)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			if (styleData == null)
			{
				if (!WorldGen.InWorld((int)hallwayPoint.X, (int)hallwayPoint.Y, 5))
				{
					return;
				}
				Tile tile = Main.tile[(int)hallwayPoint.X, (int)hallwayPoint.Y];
				styleData = DungeonGenerationStyles.GetStyleForWall(dungeonData.genVars.dungeonGenerationStyles, (int)tile.wall);
				if (styleData == null && tile.active())
				{
					styleData = DungeonGenerationStyles.GetStyleForTile(dungeonData.genVars.dungeonGenerationStyles, (int)tile.type);
				}
				if (styleData == null)
				{
					styleData = dungeonData.genVars.dungeonStyle;
				}
			}
			bool flag = styleData.Style == 0;
			ushort brickTileType = styleData.BrickTileType;
			ushort brickWallType = styleData.BrickWallType;
			if (Math.Abs(hallwayDirectionY) <= doorVariance)
			{
				if (styleData.DoorItemTypes != null)
				{
					int num = ((flag || styleData.DoorItemTypes.Length == 0) ? (-1) : styleData.DoorItemTypes[genRand.Next(styleData.DoorItemTypes.Length)]);
					DungeonDoorData dungeonDoorData = new DungeonDoorData
					{
						OverrideBrickTileType = new ushort?(brickTileType),
						OverrideBrickWallType = new ushort?(brickWallType),
						Position = hallwayPoint.ToPoint(),
						Direction = 0,
						InAHallway = true,
						AlwaysClearArea = true
					};
					if (num >= 0)
					{
						PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[num];
						dungeonDoorData.OverrideStyle = new int?((int)placementDetails.tileStyle);
					}
					dungeonData.dungeonDoorData.Add(dungeonDoorData);
					return;
				}
			}
			else
			{
				DungeonPlatformData dungeonPlatformData = new DungeonPlatformData
				{
					Position = hallwayPoint.ToPoint(),
					InAHallway = true,
					OverrideStyle = new int?(styleData.GetPlatformStyle(genRand))
				};
				dungeonData.dungeonPlatformData.Add(dungeonPlatformData);
			}
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x005FD16C File Offset: 0x005FB36C
		public static void GenerateShimmerPool(int x, int y, int outerShapeSize = 15)
		{
			int num = 5;
			int num2 = Math.Max(1, outerShapeSize - num);
			Shapes.HalfCircle halfCircle = new Shapes.HalfCircle(outerShapeSize, true);
			Shapes.HalfCircle halfCircle2 = new Shapes.HalfCircle(num2, true);
			Point point = new Point(x, y + num2);
			WorldUtils.Gen(point, halfCircle, Actions.Chain(new GenAction[]
			{
				new Actions.SetTile(667, false, false, false)
			}));
			WorldUtils.Gen(new Point(point.X, point.Y - num), halfCircle2, Actions.Chain(new GenAction[]
			{
				new Actions.ClearTile(false),
				new Actions.SetLiquid(3, byte.MaxValue)
			}));
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x005FD204 File Offset: 0x005FB404
		public static bool GenerateDungeonBiomeChest(DungeonData data, DungeonGlobalBiomeChests feature, DungeonGenerationStyleData styleData, DungeonBounds innerBounds, bool locked = true)
		{
			int left = innerBounds.Left;
			int right = innerBounds.Right;
			int y = innerBounds.Center.Y;
			int bottom = innerBounds.Bottom;
			return DungeonUtils.GenerateDungeonBiomeChest(data, feature, styleData, left, y, right, bottom, locked);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x005FD240 File Offset: 0x005FB440
		public static bool GenerateDungeonBiomeChest(DungeonData data, DungeonGlobalBiomeChests feature, DungeonGenerationStyleData styleData, int minX, int minY, int maxX, int maxY, bool locked = true)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = (int)Utils.Lerp((double)minX, (double)maxX, genRand.NextDouble());
			int num2 = (int)Utils.Lerp((double)minY, (double)maxY, genRand.NextDouble());
			if (!data.CanGenerateFeatureInArea(feature, num, num2, 1))
			{
				return false;
			}
			int num3 = 0;
			ushort num4 = 21;
			int num5 = 2;
			if (styleData.BiomeChestLootItemType >= 0)
			{
				num3 = styleData.BiomeChestLootItemType;
			}
			if (styleData.BiomeChestItemType >= 0)
			{
				PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[styleData.BiomeChestItemType];
				num4 = (ushort)placementDetails.tileType;
				num5 = (int)placementDetails.tileStyle;
			}
			if (locked && styleData.LockedBiomeChestStyle >= 0)
			{
				num5 = styleData.LockedBiomeChestStyle;
			}
			if (num3 == 0)
			{
				return false;
			}
			Point zero = Point.Zero;
			return WorldGen.AddBuriedChest(num, num2, out zero, num3, false, num5, false, num4);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x005FD2FC File Offset: 0x005FB4FC
		public static bool GenerateDungeonRegularChest(DungeonData data, DungeonGlobalBasicChests feature, DungeonGenerationStyleData styleData, DungeonBounds innerBounds)
		{
			int left = innerBounds.Left;
			int right = innerBounds.Right;
			int y = innerBounds.Center.Y;
			int bottom = innerBounds.Bottom;
			return DungeonUtils.GenerateDungeonRegularChest(data, feature, styleData, left, y, right, bottom);
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x005FD338 File Offset: 0x005FB538
		public static bool GenerateDungeonRegularChest(DungeonData data, DungeonGlobalBasicChests feature, DungeonGenerationStyleData styleData, int minX, int minY, int maxX, int maxY)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = (int)Utils.Lerp((double)minX, (double)maxX, genRand.NextDouble());
			int num2 = (int)Utils.Lerp((double)minY, (double)maxY, genRand.NextDouble());
			if (!data.CanGenerateFeatureInArea(feature, num, num2, 1))
			{
				return false;
			}
			int num3 = -1;
			ushort num4 = 21;
			int num5 = 2;
			bool flag = false;
			byte style = styleData.Style;
			if (style != 0)
			{
				switch (style)
				{
				case 8:
				case 9:
				case 14:
					num3 = WorldGen.GetNextJungleChestItem();
					break;
				case 10:
					num3 = 1293;
					break;
				case 13:
					num3 = 832;
					if (genRand.Next(3) == 0)
					{
						num3 = 4281;
					}
					break;
				}
			}
			else
			{
				WorldGen.GetDungeonLootAndChestStyle(num, num2, ref num3, ref num5);
				flag = true;
			}
			if (!flag && styleData.ChestItemTypes.Length != 0)
			{
				PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[styleData.ChestItemTypes[genRand.Next(styleData.ChestItemTypes.Length)]];
				num4 = (ushort)placementDetails.tileType;
				num5 = (int)placementDetails.tileStyle;
			}
			if (num3 == 0 && genRand.Next(2) == 0)
			{
				return true;
			}
			bool flag2 = WorldGen.AddBuriedChest(num, num2, num3, false, num5, false, num4);
			if (flag2 && styleData.Style == 0)
			{
				GenVars.CurrentDungeonGenVars.dungeonLootStyle++;
			}
			return flag2;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x005FD467 File Offset: 0x005FB667
		public static void GenerateDungeonWaterCandle(int placeX, int placeY)
		{
			WorldGen.PlaceTile(placeX, placeY, 49, true, false, -1, 0);
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x005FD478 File Offset: 0x005FB678
		public static void GenerateDungeonPotionBottle(int placeX, int placeY)
		{
			WorldGen.PlaceTile(placeX, placeY, 13, true, false, -1, 0);
			if (Main.tile[placeX, placeY].type == 13)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					Main.tile[placeX, placeY].frameX = 18;
					return;
				}
				Main.tile[placeX, placeY].frameX = 36;
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x005FD4DC File Offset: 0x005FB6DC
		public static void GenerateDungeonPot(int placeX, int placeY)
		{
			int num = WorldGen.genRand.Next(10, 13);
			WorldGen.PlacePot(placeX, placeY, 28, num);
			WorldGen.SquareTileFrame(placeX, placeY, true);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x005FD50B File Offset: 0x005FB70B
		public static void GenerateDungeonBook(int placeX, int placeY)
		{
			DungeonUtils.GenerateDungeonBook(placeX, placeY, WorldGen.genRand.Next(50) == 0);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x005FD524 File Offset: 0x005FB724
		public static void GenerateDungeonBook(int placeX, int placeY, bool waterbolt)
		{
			short num = 90;
			WorldGen.PlaceTile(placeX, placeY, 50, true, false, -1, 0);
			if (waterbolt && (double)placeY > (Main.worldSurface + Main.rockLayer) / 2.0 && Main.tile[placeY, placeY].type == 50)
			{
				Main.tile[placeX, placeY].frameX = num;
			}
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x005FD588 File Offset: 0x005FB788
		public static void GenerateBottomWedge(int placeX, int placeY, int pillarWidth, ushort pillarType, bool left = true, bool wall = false, bool actuated = false, bool crowningBottom = false, int paint = -1)
		{
			if (crowningBottom)
			{
				pillarWidth += 2;
			}
			int num = 0;
			for (int i = 0; i <= pillarWidth; i++)
			{
				int num2 = placeX + i - pillarWidth / 2;
				int num3 = (left ? (i + 1) : (pillarWidth - (i - 1)));
				DungeonUtils.GenerateTileStrip(false, out num, out num, num2, placeY, num3, pillarType, wall, actuated, paint, false, false, false);
			}
			for (int j = 0; j <= pillarWidth; j++)
			{
				int num4 = pillarWidth / 2;
				int num5 = (left ? (j + 1) : (pillarWidth - (j - 1)));
				Tile.SmoothSlope(placeX, placeY + num5, false, false);
			}
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x005FD60C File Offset: 0x005FB80C
		private static void GenerateTileStrip(bool upwards, out int topY, out int bottomY, int placeX, int placeY, int pillarHeight, ushort pillarType, bool wall = false, bool actuated = false, int paint = -1, bool smoothTop = false, bool smoothBottom = false, bool solidTop = false)
		{
			topY = placeY;
			bottomY = placeY;
			int num = pillarHeight;
			if (num == -1)
			{
				int num2 = 0;
				if (upwards)
				{
					while (num2 > -100 && WorldGen.InWorld(placeX, placeY + num2, 10) && !Main.tile[placeX, placeY + num2].active())
					{
						num2--;
					}
					num = -num2;
				}
				else
				{
					while (num2 < 100 && WorldGen.InWorld(placeX, placeY + num2, 10) && !Main.tile[placeX, placeY + num2].active())
					{
						num2++;
					}
					num = num2;
				}
			}
			if (num == 0)
			{
				return;
			}
			int num3 = -num + 1;
			int num4 = 0;
			if (!upwards)
			{
				num3 = 0;
				num4 = num - 1;
			}
			for (int i = num3; i <= num4; i++)
			{
				int num5 = placeY + i;
				if (WorldGen.InWorld(placeX, num5, 10))
				{
					Tile tile = Main.tile[placeX, num5];
					if (wall)
					{
						tile.wall = pillarType;
						if (paint >= 0)
						{
							tile.wallColor((byte)paint);
						}
					}
					else
					{
						tile.ClearTile();
						tile.active(true);
						tile.type = pillarType;
						if (paint >= 0)
						{
							tile.color((byte)paint);
						}
						if ((i == num3 && smoothTop) || (i == num4 && smoothBottom))
						{
							Tile.SmoothSlope(placeX, num5, false, false);
						}
						if ((!solidTop || i >= num3 + 2) && actuated)
						{
							tile.inActive(true);
						}
					}
					if (num5 < topY)
					{
						topY = num5;
					}
					if (num5 > bottomY)
					{
						bottomY = num5;
					}
				}
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x005FD770 File Offset: 0x005FB970
		public static Point FirstSolid(bool ceiling, Point currentPoint, DungeonBounds bounds)
		{
			int num = Main.maxTilesX * 5;
			for (;;)
			{
				num--;
				if (num <= 0)
				{
					return currentPoint;
				}
				if (ceiling && WorldGen.SolidTileAllowTopSlope(currentPoint.X, currentPoint.Y))
				{
					break;
				}
				if (!ceiling && WorldGen.SolidTileAllowBottomSlope(currentPoint.X, currentPoint.Y))
				{
					return currentPoint;
				}
				if (ceiling)
				{
					currentPoint.Y--;
				}
				else
				{
					currentPoint.Y++;
				}
				if (currentPoint.Y <= 10 || currentPoint.Y >= Main.maxTilesY - 10 || (bounds != null && !bounds.Contains(currentPoint)))
				{
					return currentPoint;
				}
			}
			return currentPoint;
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x005FD804 File Offset: 0x005FBA04
		public static void GenerateHangingLeafCluster(DungeonData data, UnifiedRandom genRand, DungeonBounds bounds, Point startPoint, int growthLength, int branchDensity, int leafDensity, ushort leafType, ushort woodType, int leafPaintColor = 0, int woodPaintColor = 0, bool goDown = true, bool includeVines = true)
		{
			Point point = new Point(startPoint.X, startPoint.Y);
			int num = (goDown ? 1 : (-1));
			point = DungeonUtils.FirstSolid(goDown, point, bounds);
			Point point2 = point;
			Point? point3 = null;
			int num2 = growthLength;
			while (growthLength > 0)
			{
				int y = point.Y;
				float num3 = (float)growthLength / (float)num2;
				if (point3 == null && num3 > 0.65f)
				{
					point3 = new Point?(point);
				}
				int num4 = (int)Utils.Lerp(0.0, (double)((float)branchDensity), (double)num3);
				for (int i = -num4; i <= num4; i++)
				{
					int num5 = point.X + i;
					DungeonUtils.ChangeTileType(Main.tile[num5, y], woodType, false, woodPaintColor);
				}
				point.Y += num;
				growthLength--;
			}
			int num6 = genRand.Next(3);
			if (!goDown)
			{
				num6 *= -1;
			}
			if (point3 != null)
			{
				point = new Point(point3.Value.X, point3.Value.Y + num6);
			}
			else
			{
				point = new Point(point2.X, point2.Y + (int)((float)num2 * 0.65f) + num6);
			}
			int j = leafDensity * 2 + 3;
			int num7 = j;
			while (j > 0)
			{
				int y2 = point.Y;
				float num8 = (float)j / (float)num7;
				int num9 = (int)Utils.WrappedLerp(1f, (float)leafDensity, num8);
				for (int k = -num9; k <= num9; k++)
				{
					int num10 = point.X + k;
					Tile tile = Main.tile[num10, y2];
					if (!tile.active())
					{
						DungeonUtils.ChangeTileType(tile, leafType, false, leafPaintColor);
					}
				}
				point.Y += num;
				j--;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x005FD9CC File Offset: 0x005FBBCC
		public static void GenerateDungeonTree(DungeonData data, int x, int y, int attachmentY, bool generateRoots = true)
		{
			if (!WorldGen.InWorld(x, y, 20))
			{
				return;
			}
			int num = y;
			while (Main.tile[x, num].active() || Main.tile[x, num].wall > 0 || Main.tile[x, num - 1].active() || Main.tile[x, num - 1].wall > 0 || Main.tile[x, num - 2].active() || Main.tile[x, num - 2].wall > 0 || Main.tile[x, num - 3].active() || Main.tile[x, num - 3].wall > 0 || Main.tile[x, num - 4].active() || Main.tile[x, num - 4].wall > 0)
			{
				num--;
				if (num < 50)
				{
					break;
				}
			}
			if (num > 50)
			{
				DungeonUtils.GrowDungeonTree(data, x, num, attachmentY, false, generateRoots);
			}
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x005FDAEC File Offset: 0x005FBCEC
		private static bool GrowDungeonTree(DungeonData data, int i, int j, int attachmentY, bool patch = false, bool generateRoots = true)
		{
			byte b = 28;
			byte b2 = 28;
			if (WorldGen.notTheBees)
			{
				b = 0;
				b2 = 0;
			}
			UnifiedRandom genRand = WorldGen.genRand;
			int num = 0;
			int[] array = new int[1000];
			int[] array2 = new int[1000];
			int[] array3 = new int[1000];
			int[] array4 = new int[1000];
			int num2 = 0;
			int[] array5 = new int[2000];
			int[] array6 = new int[2000];
			bool[] array7 = new bool[2000];
			int num3 = i - genRand.Next(2, 3);
			int num4 = i + genRand.Next(2, 3);
			if (genRand.Next(5) == 0)
			{
				if (genRand.Next(2) == 0)
				{
					num3--;
				}
				else
				{
					num4++;
				}
			}
			int num5 = num4 - num3;
			int num6 = num3;
			int num7 = num4;
			int num8 = num3;
			int num9 = num4;
			bool flag = true;
			int num10 = genRand.Next(-8, -4);
			int num11 = genRand.Next(2);
			int num12 = j;
			int num13 = genRand.Next(5, 15);
			Main.tileSolid[48] = false;
			while (flag)
			{
				num10++;
				if (num10 > num13)
				{
					num13 = genRand.Next(5, 15);
					num10 = 0;
					array2[num] = num12 + genRand.Next(5);
					if (genRand.Next(5) == 0)
					{
						if (num11 == 0)
						{
							num11 = 1;
						}
						else
						{
							num11 = 0;
						}
					}
					if (num11 == 0)
					{
						array3[num] = -1;
						array[num] = num3;
						array4[num] = num4 - num3;
						if (genRand.Next(2) == 0)
						{
							num3++;
						}
						num6++;
						num11 = 1;
					}
					else
					{
						array3[num] = 1;
						array[num] = num4;
						array4[num] = num4 - num3;
						if (genRand.Next(2) == 0)
						{
							num4--;
						}
						num7--;
						num11 = 0;
					}
					if (num6 == num7)
					{
						flag = false;
					}
					num++;
				}
				for (int k = num3; k <= num4; k++)
				{
					Main.tile[k, num12].type = 191;
					Main.tile[k, num12].active(true);
					Main.tile[k, num12].Clear(TileDataType.Slope);
					if (b != 0)
					{
						Main.tile[k, num12].color(b);
					}
				}
				num12--;
			}
			for (int l = 0; l < num - 1; l++)
			{
				int num14 = array[l] + array3[l];
				int num15 = array2[l];
				int m = (int)((double)array4[l] * (1.0 + (double)genRand.Next(20, 30) * 0.1));
				Main.tile[num14, num15 + 1].type = 191;
				Main.tile[num14, num15 + 1].active(true);
				Main.tile[num14, num15 + 1].Clear(TileDataType.Slope);
				if (b != 0)
				{
					Main.tile[num14, num15 + 1].color(b);
				}
				int num16 = genRand.Next(3, 5);
				while (m > 0)
				{
					m--;
					Main.tile[num14, num15].type = 191;
					Main.tile[num14, num15].active(true);
					Main.tile[num14, num15].Clear(TileDataType.Slope);
					if (b != 0)
					{
						Main.tile[num14, num15].color(b);
					}
					if (genRand.Next(10) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							num15--;
						}
						else
						{
							num15++;
						}
					}
					else
					{
						num14 += array3[l];
					}
					if (num16 > 0)
					{
						num16--;
					}
					else if (genRand.Next(2) == 0)
					{
						num16 = genRand.Next(2, 5);
						if (genRand.Next(2) == 0)
						{
							Main.tile[num14, num15].type = 191;
							Main.tile[num14, num15].active(true);
							Main.tile[num14, num15].Clear(TileDataType.Slope);
							if (b != 0)
							{
								Main.tile[num14, num15].color(b);
							}
							Main.tile[num14, num15 - 1].type = 191;
							Main.tile[num14, num15 - 1].Clear(TileDataType.Slope);
							if (b != 0)
							{
								Main.tile[num14, num15 - 1].color(b);
							}
							array5[num2] = num14;
							array6[num2] = num15;
							num2++;
						}
						else
						{
							Main.tile[num14, num15].type = 191;
							Main.tile[num14, num15].active(true);
							Main.tile[num14, num15].Clear(TileDataType.Slope);
							if (b != 0)
							{
								Main.tile[num14, num15].color(b);
							}
							Main.tile[num14, num15 + 1].type = 191;
							Main.tile[num14, num15 + 1].active(true);
							Main.tile[num14, num15 + 1].Clear(TileDataType.Slope);
							if (b != 0)
							{
								Main.tile[num14, num15 + 1].color(b);
							}
							array5[num2] = num14;
							array6[num2] = num15;
							num2++;
						}
					}
					if (m == 0)
					{
						array5[num2] = num14;
						array6[num2] = num15;
						num2++;
					}
				}
			}
			int num17 = (num3 + num4) / 2;
			int num18 = num12;
			int n = genRand.Next(num5 * 3, num5 * 5);
			int num19 = 0;
			int num20 = 0;
			while (n > 0)
			{
				Main.tile[num17, num18].type = 191;
				Main.tile[num17, num18].active(true);
				Main.tile[num17, num18].Clear(TileDataType.Slope);
				if (b != 0)
				{
					Main.tile[num17, num18].color(b);
				}
				if (num19 > 0)
				{
					num19--;
				}
				if (num20 > 0)
				{
					num20--;
				}
				for (int num21 = -1; num21 < 2; num21++)
				{
					if (num21 != 0 && ((num21 < 0 && num19 == 0) || (num21 > 0 && num20 == 0)) && genRand.Next(2) == 0)
					{
						int num22 = num17;
						int num23 = num18;
						int num24 = genRand.Next(num5, num5 * 3);
						if (num21 < 0)
						{
							num19 = genRand.Next(3, 5);
						}
						if (num21 > 0)
						{
							num20 = genRand.Next(3, 5);
						}
						int num25 = 0;
						while (num24 > 0)
						{
							num24--;
							num22 += num21;
							Main.tile[num22, num23].type = 191;
							Main.tile[num22, num23].active(true);
							Main.tile[num22, num23].Clear(TileDataType.Slope);
							if (b != 0)
							{
								Main.tile[num22, num23].color(b);
							}
							if (num24 == 0)
							{
								array5[num2] = num22;
								array6[num2] = num23;
								array7[num2] = true;
								num2++;
							}
							if (genRand.Next(5) == 0)
							{
								if (genRand.Next(2) == 0)
								{
									num23--;
								}
								else
								{
									num23++;
								}
								Main.tile[num22, num23].type = 191;
								Main.tile[num22, num23].active(true);
								Main.tile[num22, num23].Clear(TileDataType.Slope);
								if (b != 0)
								{
									Main.tile[num22, num23].color(b);
								}
							}
							if (num25 > 0)
							{
								num25--;
							}
							else if (genRand.Next(3) == 0)
							{
								num25 = genRand.Next(2, 4);
								int num26 = num22;
								int num27 = num23;
								if (genRand.Next(2) == 0)
								{
									num27--;
								}
								else
								{
									num27++;
								}
								Main.tile[num26, num27].type = 191;
								Main.tile[num26, num27].active(true);
								Main.tile[num26, num27].Clear(TileDataType.Slope);
								if (b != 0)
								{
									Main.tile[num26, num27].color(b);
								}
								array5[num2] = num26;
								array6[num2] = num27;
								array7[num2] = true;
								num2++;
								array5[num2] = num26 + genRand.Next(-5, 6);
								array6[num2] = num27 + genRand.Next(-5, 6);
								array7[num2] = true;
								num2++;
							}
						}
					}
				}
				array5[num2] = num17;
				array6[num2] = num18;
				num2++;
				if (genRand.Next(4) == 0)
				{
					if (genRand.Next(2) == 0)
					{
						num17--;
					}
					else
					{
						num17++;
					}
					Main.tile[num17, num18].type = 191;
					Main.tile[num17, num18].active(true);
					Main.tile[num17, num18].Clear(TileDataType.Slope);
					if (b != 0)
					{
						Main.tile[num17, num18].color(b);
					}
				}
				num18--;
				n--;
			}
			if (generateRoots)
			{
				for (int num28 = num8; num28 <= num9; num28++)
				{
					int num29 = genRand.Next(1, 6);
					int num30 = j + 1;
					while (num29 > 0)
					{
						if (WorldGen.SolidTile(num28, num30, false))
						{
							num29--;
						}
						Main.tile[num28, num30].type = 191;
						Main.tile[num28, num30].active(true);
						Main.tile[num28, num30].Clear(TileDataType.Slope);
						num30++;
					}
					int num31 = num30;
					int num32 = genRand.Next(2, num5 + 1);
					for (int num33 = 0; num33 < num32; num33++)
					{
						num30 = num31;
						int num34 = (num8 + num9) / 2;
						int num35 = 1;
						int num36;
						if (num28 < num34)
						{
							num36 = -1;
						}
						else
						{
							num36 = 1;
						}
						if (num28 == num34 || (num5 > 6 && (num28 == num34 - 1 || num28 == num34 + 1)))
						{
							num36 = 0;
						}
						int num37 = num36;
						int num38 = num28;
						num29 = genRand.Next((int)((double)num5 * 3.5), num5 * 6);
						while (num29 > 0)
						{
							num29--;
							num38 += num36;
							if (Main.tile[num38, num30].wall != 244)
							{
								Main.tile[num38, num30].type = 191;
								Main.tile[num38, num30].active(true);
								Main.tile[num38, num30].Clear(TileDataType.Slope);
							}
							num30 += num35;
							if (Main.tile[num38, num30].wall != 244)
							{
								Main.tile[num38, num30].type = 191;
								Main.tile[num38, num30].active(true);
								Main.tile[num38, num30].Clear(TileDataType.Slope);
							}
							if (!Main.tile[num38, num30 + 1].active())
							{
								num36 = 0;
								num35 = 1;
							}
							if (genRand.Next(3) == 0)
							{
								if (num37 < 0)
								{
									if (num36 == 0)
									{
										num36 = -1;
									}
									else
									{
										num36 = 0;
									}
								}
								else if (num37 > 0)
								{
									if (num36 == 0)
									{
										num36 = 1;
									}
									else
									{
										num36 = 0;
									}
								}
								else
								{
									num36 = genRand.Next(-1, 2);
								}
							}
							if (genRand.Next(3) == 0)
							{
								if (num35 == 0)
								{
									num35 = 1;
								}
								else
								{
									num35 = 0;
								}
							}
						}
					}
				}
			}
			if (!WorldGen.remixWorldGen)
			{
				for (int num39 = 0; num39 < num2; num39++)
				{
					int num40 = genRand.Next(5, 8);
					num40 = (int)((double)num40 * (1.0 + (double)num5 * 0.05));
					if (array7[num39])
					{
						num40 = genRand.Next(6, 12) + num5;
					}
					int num41 = array5[num39] - num40 * 2;
					int num42 = array5[num39] + num40 * 2;
					int num43 = array6[num39] - num40 * 2;
					int num44 = array6[num39] + num40 * 2;
					double num45 = 2.0 - (double)genRand.Next(5) * 0.1;
					for (int num46 = num41; num46 <= num42; num46++)
					{
						for (int num47 = num43; num47 <= num44; num47++)
						{
							if (Main.tile[num46, num47].type != 191)
							{
								if (array7[num39])
								{
									if ((new Vector2D((double)array5[num39], (double)array6[num39]) - new Vector2D((double)num46, (double)num47)).Length() < (double)num40 * 0.9)
									{
										Main.tile[num46, num47].type = 192;
										Main.tile[num46, num47].active(true);
										Main.tile[num46, num47].Clear(TileDataType.Slope);
										if (b2 != 0)
										{
											Main.tile[num46, num47].color(b2);
										}
									}
								}
								else if ((double)Math.Abs(array5[num39] - num46) + (double)Math.Abs(array6[num39] - num47) * num45 < (double)num40)
								{
									Main.tile[num46, num47].type = 192;
									Main.tile[num46, num47].active(true);
									Main.tile[num46, num47].Clear(TileDataType.Slope);
									if (b2 != 0)
									{
										Main.tile[num46, num47].color(b2);
									}
								}
							}
						}
					}
				}
			}
			DungeonUtils.GrowDungeonTree_MakePassage(data, j, attachmentY, num5, ref num8, ref num9, patch);
			Main.tileSolid[48] = true;
			return true;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x005FE890 File Offset: 0x005FCA90
		private static void GrowDungeonTree_MakePassage(DungeonData data, int j, int attachmentY, int width, ref int minimumLeft, ref int minimumRight, bool noSecretRoom = false)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = minimumLeft;
			int num2 = minimumRight;
			int num3 = (minimumLeft + minimumRight) / 2;
			int num4 = 5;
			int num5 = j - 6;
			int num6 = 0;
			bool flag = true;
			genRand.Next(5, 16);
			PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[data.platformItemType];
			int tileType = placementDetails.tileType;
			short tileStyle = placementDetails.tileStyle;
			for (;;)
			{
				num5++;
				if (num5 > attachmentY)
				{
					break;
				}
				int num7 = (minimumLeft + minimumRight) / 2;
				int num8 = 1;
				if (num5 > j && width <= 4)
				{
					num8++;
				}
				for (int i = minimumLeft - num8; i <= minimumRight + num8; i++)
				{
					if (i > num7 - 2 && i <= num7 + 1)
					{
						if (num5 > j - 4)
						{
							if (Main.tile[i, num5].type != 19 && Main.tile[i, num5].type != 15 && Main.tile[i, num5].type != 304 && Main.tile[i, num5].type != 21 && Main.tile[i, num5].type != 10 && Main.tile[i, num5 - 1].type != 15 && Main.tile[i, num5 - 1].type != 304 && Main.tile[i, num5 - 1].type != 21 && Main.tile[i, num5 - 1].type != 10 && Main.tile[i, num5 + 1].type != 10)
							{
								Main.tile[i, num5].ClearTileAndPaint();
							}
							if (!DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i, num5].wall, false))
							{
								Main.tile[i, num5].wall = 244;
							}
							if (!DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i - 1, num5].wall, false) && (Main.tile[i - 1, num5].wall > 0 || (double)num5 >= Main.worldSurface))
							{
								Main.tile[i - 1, num5].wall = 244;
							}
							if (!DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i + 1, num5].wall, false) && (Main.tile[i + 1, num5].wall > 0 || (double)num5 >= Main.worldSurface))
							{
								Main.tile[i + 1, num5].wall = 244;
							}
							if (num5 == j && i > num7 - 2 && i <= num7 + 1)
							{
								Main.tile[i, num5 + 1].ClearTileAndPaint();
								WorldGen.PlaceTile(i, num5 + 1, 19, true, false, -1, 23);
							}
						}
					}
					else
					{
						if (Main.tile[i, num5].type != 15 && Main.tile[i, num5].type != 304 && Main.tile[i, num5].type != 21 && Main.tile[i, num5].type != 10 && Main.tile[i - 1, num5].type != 10 && Main.tile[i + 1, num5].type != 10)
						{
							if (!DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i, num5].wall, false))
							{
								Main.tile[i, num5].type = 191;
								Main.tile[i, num5].active(true);
								Main.tile[i, num5].Clear(TileDataType.Slope);
							}
							if (Main.tile[i - 1, num5].type == 40)
							{
								Main.tile[i - 1, num5].type = 0;
							}
							if (Main.tile[i + 1, num5].type == 40)
							{
								Main.tile[i + 1, num5].type = 0;
							}
						}
						if (num5 <= j && num5 > j - 4 && i > minimumLeft - num8 && i <= minimumRight + num8 - 1)
						{
							Main.tile[i, num5].wall = 244;
						}
					}
					if (!WorldGen.isGeneratingOrLoadingWorld)
					{
						WorldGen.SquareTileFrame(i, num5, true);
						WorldGen.SquareWallFrame(i, num5, true);
					}
				}
				num6++;
				if (num6 >= 6)
				{
					num6 = 0;
					int num9 = genRand.Next(3);
					if (num9 == 0)
					{
						num9 = -1;
					}
					if (flag)
					{
						num9 = 2;
					}
					if (num9 == -1 && Main.tile[minimumLeft - num4, num5].wall == 244)
					{
						num9 = 1;
					}
					else if (num9 == 1 && Main.tile[minimumRight + num4, num5].wall == 244)
					{
						num9 = -1;
					}
					if (num9 == 2)
					{
						flag = false;
						ushort num10 = 19;
						int num11 = 23;
						bool flag2 = false;
						if (DungeonUtils.IsConsideredDungeonWall((int)Main.tile[minimumLeft, num5 + 1].wall, false) || DungeonUtils.IsConsideredDungeonWall((int)Main.tile[minimumLeft + 1, num5 + 1].wall, false) || DungeonUtils.IsConsideredDungeonWall((int)Main.tile[minimumLeft + 2, num5 + 1].wall, false))
						{
							flag2 = true;
							num10 = (ushort)tileType;
							num11 = (int)tileStyle;
						}
						if (WorldGen.SolidTile(minimumLeft - 1, num5 + 1, false) || WorldGen.SolidTile(minimumRight + 1, num5 + 1, false) || !flag2)
						{
							for (int k = minimumLeft; k <= minimumRight; k++)
							{
								if (k > num7 - 2 && k <= num7 + 1)
								{
									Main.tile[k, num5 + 1].ClearTileAndPaint();
									WorldGen.PlaceTile(k, num5 + 1, (int)num10, true, false, -1, num11);
								}
							}
						}
					}
					else
					{
						minimumLeft += num9;
						minimumRight += num9;
					}
				}
			}
			minimumLeft = num;
			minimumRight = num2;
			for (int l = minimumLeft; l <= minimumRight; l++)
			{
				for (int m = j - 3; m <= j; m++)
				{
					Tile tile = Main.tile[l, m];
					tile.ClearTileAndPaint();
					if (!DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false) && !DungeonUtils.IsConsideredDungeonWallGlass((int)tile.wall, false))
					{
						tile.wall = 244;
					}
				}
			}
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x005FEF4C File Offset: 0x005FD14C
		public static void GenerateDungeonStairs(DungeonData data, int i, int j, int direction, ushort tileType, ushort wallType, int depth = 100)
		{
			if (!WorldGen.InWorld(i, j, 20))
			{
				return;
			}
			int num = depth;
			int num2 = depth;
			int num3 = ((direction == 1) ? 1 : (-1));
			int num4 = 0;
			int num5 = i;
			while ((direction == 1) ? (num5 < i + num2) : (num5 > i - num2))
			{
				num4++;
				for (int k = j + num4; k < j + num; k++)
				{
					if (WorldGen.InWorld(num5, k, 10) && !DungeonUtils.GenerateDungeonStairs_CanPlaceTile(num5, k + 5) && num > k)
					{
						num = k;
						break;
					}
				}
				num5 += num3;
			}
			num2 = num;
			depth = num;
			num4 = 0;
			int num6 = i;
			while ((direction == 1) ? (num6 < i + num2) : (num6 > i - num2))
			{
				num4++;
				for (int l = j + num4; l < j + depth; l++)
				{
					if (WorldGen.InWorld(num6, l, 10) && l < DungeonCrawler.CurrentDungeonData.genVars.outerPotentialDungeonBounds.Top - 5)
					{
						Tile tile = Main.tile[num6, l];
						tile.liquid = 0;
						Main.tile[num6, l - 1].liquid = 0;
						Main.tile[num6, l - 2].liquid = 0;
						Main.tile[num6, l - 3].liquid = 0;
						if (DungeonUtils.GenerateDungeonStairs_CanPlaceTile(num6, l))
						{
							bool flag = data.genVars.dungeonStyle.WallIsInStyle((int)tile.wall, false);
							if (!flag)
							{
								using (List<DungeonGenerationStyleData>.Enumerator enumerator = data.genVars.dungeonGenerationStyles.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current.WallIsInStyle((int)tile.wall, false))
										{
											flag = true;
											break;
										}
									}
								}
							}
							if (flag)
							{
								if (tile.active())
								{
									tile.active(true);
									tile.type = tileType;
									tile.Clear(TileDataType.Slope);
								}
								tile.wall = wallType;
							}
							else
							{
								tile.active(true);
								tile.type = tileType;
								tile.Clear(TileDataType.Slope);
								if (l != j + num4)
								{
									tile.wall = wallType;
								}
							}
						}
					}
				}
				num6 += num3;
			}
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x005FF190 File Offset: 0x005FD390
		private static bool GenerateDungeonStairs_CanPlaceTile(int x, int y)
		{
			if (y >= DungeonCrawler.CurrentDungeonData.genVars.outerPotentialDungeonBounds.Top - 5)
			{
				return false;
			}
			Tile tile = Main.tile[x, y];
			if (tile.active())
			{
				if (!WorldGen.CanKillTile(x, y))
				{
					return false;
				}
				if (tile.type >= 0 && Main.tileFrameImportant[(int)tile.type])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x005FF1F4 File Offset: 0x005FD3F4
		public static void GenerateFloatingRocksInArea(DungeonData data, DungeonGenerationStyleData styleData, DungeonBounds bounds, ushort tileType, bool includePlatform = true, int paint = -1)
		{
			DungeonUtils.GenerateFloatingRocksInArea(data, styleData, bounds.Left, bounds.Top, bounds.Width, bounds.Height, tileType, includePlatform, paint);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x005FF228 File Offset: 0x005FD428
		public static void GenerateFloatingRocksInArea(DungeonData data, DungeonGenerationStyleData styleData, int x, int y, int width, int height, ushort tileType, bool includePlatform = true, int paint = -1)
		{
			int num = -1;
			if (includePlatform)
			{
				UnifiedRandom genRand = WorldGen.genRand;
				num = styleData.GetPlatformStyle(genRand);
			}
			DungeonUtils.GenerateFloatingRocksInArea(data, x, y, width, height, tileType, includePlatform, num, paint, 7);
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x005FF260 File Offset: 0x005FD460
		public static void GenerateFloatingRocksInArea(DungeonData data, DungeonBounds bounds, ushort tileType, bool includePlatform = true, int platformStyle = -1, int paint = -1, int platformDistance = 7)
		{
			DungeonUtils.GenerateFloatingRocksInArea(data, bounds.Left, bounds.Top, bounds.Width, bounds.Height, tileType, includePlatform, platformStyle, paint, platformDistance);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x005FF294 File Offset: 0x005FD494
		public static void GenerateFloatingRocksInArea(DungeonData data, int x, int y, int width, int height, ushort tileType, bool includePlatform = true, int platformStyle = -1, int paint = -1, int platformDistance = 7)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = Math.Max(2, (width + height) / 2 / platformDistance);
			int i = 0;
			int num2 = 100;
			int num3 = x + width / 2;
			int num4 = 8;
			bool flag = true;
			Func<DungeonData, int, int, bool> <>9__0;
			while (i < num)
			{
				num2--;
				if (num2 <= 0)
				{
					break;
				}
				int num5 = 4 + genRand.Next(3);
				int num6 = x;
				int num7 = y;
				if (i % 2 == 0)
				{
					int num8 = width / 2;
					if (flag)
					{
						num6 = x + num4 + genRand.Next(Math.Max(1, num8 - num4));
					}
					else
					{
						num6 = x + num4 + num8 + genRand.Next(Math.Max(1, num8 - num4 * 2));
					}
					flag = !flag;
				}
				else
				{
					num6 = x + num4 + genRand.Next(width - num4 * 2);
				}
				num7 = y + num4 + (int)((float)(height - num4 * 2) * ((float)i / (float)num));
				DungeonUtils.GenerateRockPlatform(genRand, num6, num7, num5, tileType, paint);
				if (includePlatform)
				{
					int num9 = ((num6 < num3) ? (num6 - 5) : (num6 + 5));
					int num10 = ((num6 < num3) ? (num6 - x) : (x + width - num6));
					DungeonPlatformData dungeonPlatformData = default(DungeonPlatformData);
					dungeonPlatformData.Position = new Point(num9, num7);
					dungeonPlatformData.InAHallway = false;
					dungeonPlatformData.OverrideHeightFluff = new int?(0);
					dungeonPlatformData.ForcePlacement = true;
					dungeonPlatformData.OverrideMaxLengthAllowed = 5 + num10;
					Func<DungeonData, int, int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (DungeonData dungeonData, int platformX, int platformY) => platformX >= x && platformX <= x + width && platformY >= y && platformY <= y + height);
					}
					dungeonPlatformData.canPlaceHereCallback = func;
					DungeonPlatformData dungeonPlatformData2 = dungeonPlatformData;
					if (platformStyle > -1)
					{
						dungeonPlatformData2.OverrideStyle = new int?(platformStyle);
					}
					data.dungeonPlatformData.Add(dungeonPlatformData2);
				}
				i++;
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x005FF4AC File Offset: 0x005FD6AC
		public static void GenerateRockPlatform(UnifiedRandom genRand, int x, int y, int width, ushort tileType, int paint = -1)
		{
			int num = width / 2;
			int num2 = Math.Max(2, num + genRand.Next(2));
			for (int i = 0; i < width; i++)
			{
				int num3 = x + i - num;
				int num4 = num2;
				if (i == 0 || i == width - 1)
				{
					num4 = Math.Max(1, num4 / 2);
				}
				else if (i == 1 || i == width - 2)
				{
					num4 = Math.Max(2, (int)((float)num4 * 0.66f));
				}
				for (int j = 0; j < num4; j++)
				{
					int num5 = y + j;
					DungeonUtils.ChangeTileType(Main.tile[num3, num5], tileType, false, paint);
				}
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x005FF544 File Offset: 0x005FD744
		public static void GenerateSpeleothemsInArea(DungeonData data, DungeonGenerationStyleData styleData, DungeonBounds bounds, int maxSpeleothems, ushort tileType, int paint = -1, int speleothemWidth = -1, int speleothemHeight = -1)
		{
			DungeonUtils.GenerateSpeleothemsInArea(data, styleData, bounds.Left, bounds.Top, bounds.Width, bounds.Height, maxSpeleothems, tileType, paint, -1, -1);
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x005FF578 File Offset: 0x005FD778
		public static void GenerateSpeleothemsInArea(DungeonData data, DungeonGenerationStyleData styleData, int x, int y, int width, int height, int maxSpeleothems, ushort tileType, int paint = -1, int speleothemWidth = -1, int speleothemHeight = -1)
		{
			DungeonUtils.GenerateSpeleothemsInArea(data, x, y, width, height, maxSpeleothems, tileType, paint, -1, -1);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x005FF59C File Offset: 0x005FD79C
		public static void GenerateSpeleothemsInArea(DungeonData data, int x, int y, int width, int height, int maxSpeleothems, ushort tileType, int paint = -1, int speleothemWidth = -1, int speleothemHeight = -1)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int i = 1;
			int num = maxSpeleothems - 1;
			int num2 = 100;
			int num3 = width / 2;
			int num4 = y + height;
			int num5 = y + height / 2;
			int num6 = genRand.Next(2);
			if (speleothemWidth <= -1)
			{
				speleothemWidth = (int)Math.Max(5f, (float)width / (float)num);
			}
			if (speleothemHeight <= -1)
			{
				speleothemHeight = height / 4;
			}
			while (i < num)
			{
				num2--;
				if (num2 <= 0)
				{
					break;
				}
				int num7 = speleothemWidth + genRand.Next(3);
				int num8 = x + (int)((float)width * ((float)i / (float)(num - 1)));
				int num9 = num5;
				int num10 = num8;
				int num11 = num9;
				bool flag = (i + num6) % 2 == 0;
				if (genRand.Next(3) == 0)
				{
					flag = !flag;
				}
				if (flag)
				{
					Tile tile = Main.tile[num8, num9];
					int num12 = Math.Max((int)((float)width * 1.5f), (int)((float)height * 1.5f));
					while (!tile.active())
					{
						if (num9 >= num4)
						{
							break;
						}
						num12--;
						if (num12 <= 0)
						{
							break;
						}
						num9++;
						tile = Main.tile[num8, num9];
					}
				}
				else
				{
					Tile tile2 = Main.tile[num8, num9];
					int num13 = Math.Max((int)((float)width * 1.5f), (int)((float)height * 1.5f));
					while (!tile2.active() && num9 > y)
					{
						num13--;
						if (num13 <= 0)
						{
							break;
						}
						num9--;
						tile2 = Main.tile[num8, num9];
					}
				}
				bool flag2 = true;
				if (flag2 && Math.Abs(num5 - num9) < 10)
				{
					flag2 = false;
				}
				if (flag2)
				{
					DungeonUtils.GenerateSpeleothem(data, genRand, num8, num9, num7, speleothemHeight + genRand.Next(3), tileType, paint);
				}
				if (genRand.Next(3) == 0)
				{
					num7 = speleothemWidth + genRand.Next(3);
					num8 = num10;
					num9 = num11;
					if (!flag)
					{
						Tile tile3 = Main.tile[num8, num9];
						int num14 = Math.Max((int)((float)width * 1.5f), (int)((float)height * 1.5f));
						while (!tile3.active())
						{
							if (num9 >= num4)
							{
								break;
							}
							num14--;
							if (num14 <= 0)
							{
								break;
							}
							num9++;
							tile3 = Main.tile[num8, num9];
						}
					}
					else
					{
						Tile tile4 = Main.tile[num8, num9];
						int num15 = Math.Max((int)((float)width * 1.5f), (int)((float)height * 1.5f));
						while (!tile4.active() && num9 > y)
						{
							num15--;
							if (num15 <= 0)
							{
								break;
							}
							num9--;
							tile4 = Main.tile[num8, num9];
						}
					}
					flag2 = true;
					if (flag2 && Math.Abs(num5 - num9) < 10)
					{
						flag2 = false;
					}
					if (flag2)
					{
						DungeonUtils.GenerateSpeleothem(data, genRand, num8, num9, num7, speleothemHeight + genRand.Next(3), tileType, paint);
					}
				}
				i++;
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x005FF86C File Offset: 0x005FDA6C
		public static void GenerateSpeleothem(DungeonData data, UnifiedRandom genRand, int x, int y, int width, int height = -1, ushort tileType = 1, int paint = -1)
		{
			if (width % 2 == 1)
			{
				width++;
			}
			int num = width / 2;
			if (height <= -1)
			{
				height = width * 2 + genRand.Next(2);
			}
			int num2 = height / 2;
			y -= num2;
			if (!Main.tile[x, y].active())
			{
				y++;
			}
			for (int i = 0; i < data.dungeonDoorData.Count; i++)
			{
				if (data.dungeonDoorData[i].Position.ToVector2().Distance(new Vector2((float)x, (float)y)) <= 5f)
				{
					return;
				}
			}
			for (int j = 0; j <= width; j++)
			{
				int num3 = x + j - num;
				int num4 = (int)Utils.WrappedLerp(1f, (float)height, (float)j / (float)width);
				if (genRand.Next(2) == 0)
				{
					num4 += 2;
				}
				int num5 = (height - num4) / 2;
				for (int k = 0; k < num4; k++)
				{
					int num6 = y + k + num5;
					DungeonUtils.ChangeTileType(Main.tile[num3, num6], tileType, false, paint);
				}
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x005FF976 File Offset: 0x005FDB76
		public static void ChangeTileType(Tile tile, ushort tileType, bool resetTile, int paint = -1)
		{
			if (resetTile)
			{
				tile.ClearEverything();
			}
			tile.active(true);
			tile.Clear(TileDataType.Slope);
			tile.type = tileType;
			if (paint > -1)
			{
				tile.color((byte)paint);
			}
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x005FF9A6 File Offset: 0x005FDBA6
		public static void ChangeWallType(Tile tile, ushort wallType, bool resetTile, int paint = -1)
		{
			if (resetTile)
			{
				tile.ClearEverything();
			}
			tile.wall = wallType;
			if (paint > -1)
			{
				tile.wallColor((byte)paint);
			}
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x005FF9C4 File Offset: 0x005FDBC4
		public static int GetDualDungeonBrickSupportCutoffY(DungeonData data)
		{
			if (SpecialSeedFeatures.DungeonEntranceIsUnderground)
			{
				return data.genVars.outerPotentialDungeonBounds.Top - 5;
			}
			return data.genVars.outerPotentialDungeonBounds.Top - 10;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x005FF9F3 File Offset: 0x005FDBF3
		public static void UpdateDungeonProgress(GenerationProgress progress, float percentile, string debugString, bool noFormatting = false)
		{
			Main.statusText = debugString;
			if (progress == null)
			{
				return;
			}
			if (noFormatting)
			{
				progress.MessageNoFormatting = debugString;
			}
			else
			{
				progress.Message = debugString;
			}
			progress.Set((double)percentile);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x005FFA1C File Offset: 0x005FDC1C
		public static Point SetOldManSpawnAndSpawnOldManIfDefaultDungeon(int x, int y, bool generating = false)
		{
			Point point = new Point(x, y);
			if (GenVars.CurrentDungeon == 0)
			{
				Main.dungeonX = point.X;
				Main.dungeonY = point.Y;
				if (generating)
				{
					int num = NPC.NewNPC(new EntitySource_WorldGen(), Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num].homeless = false;
					Main.npc[num].homeTileX = Main.dungeonX;
					Main.npc[num].homeTileY = Main.dungeonY;
					if (Main.onlyShimmerOceanWorldsGeneration)
					{
						Main.npc[num].GivenName = "Old Man James";
					}
				}
			}
			return point;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x005FFADC File Offset: 0x005FDCDC
		public static bool IsPointOfProtectionType(int i2, int j2, List<DungeonRoom> roomsInArea, ProtectionType protectionToCheck)
		{
			ProtectionType highestProtectionTypeFromPoint = DungeonUtils.GetHighestProtectionTypeFromPoint(i2, j2, roomsInArea);
			switch (protectionToCheck)
			{
			default:
				return highestProtectionTypeFromPoint == protectionToCheck;
			case ProtectionType.Tiles:
			case ProtectionType.Walls:
				return highestProtectionTypeFromPoint == protectionToCheck || highestProtectionTypeFromPoint == ProtectionType.TilesAndWalls;
			case ProtectionType.TilesAndWalls:
				return highestProtectionTypeFromPoint == protectionToCheck || highestProtectionTypeFromPoint == ProtectionType.Tiles || highestProtectionTypeFromPoint == ProtectionType.Walls;
			}
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x005FFB28 File Offset: 0x005FDD28
		public static ProtectionType GetHighestProtectionTypeFromPoint(int i2, int j2, List<DungeonRoom> roomsInArea)
		{
			ProtectionType protectionType = ProtectionType.None;
			for (int k = 0; k < roomsInArea.Count; k++)
			{
				switch (roomsInArea[k].GetProtectionTypeFromPoint(i2, j2))
				{
				case ProtectionType.Tiles:
					if (protectionType == ProtectionType.Walls)
					{
						protectionType = ProtectionType.TilesAndWalls;
					}
					else
					{
						protectionType = ProtectionType.Tiles;
					}
					break;
				case ProtectionType.Walls:
					if (protectionType == ProtectionType.Tiles)
					{
						protectionType = ProtectionType.TilesAndWalls;
					}
					else
					{
						protectionType = ProtectionType.Walls;
					}
					break;
				case ProtectionType.TilesAndWalls:
					protectionType = ProtectionType.TilesAndWalls;
					break;
				}
				if (protectionType == ProtectionType.TilesAndWalls)
				{
					break;
				}
			}
			return protectionType;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x005FFB8E File Offset: 0x005FDD8E
		public static DungeonRoom GetClosestRoomTo(List<DungeonRoom> roomsToCheck, Point point, DungeonRoomSearchSettings settings)
		{
			return DungeonUtils.GetClosestRoomTo(roomsToCheck, point.X, point.Y, settings);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x005FFBA4 File Offset: 0x005FDDA4
		public static DungeonRoom GetClosestRoomTo(List<DungeonRoom> roomsToCheck, int x, int y, DungeonRoomSearchSettings settings)
		{
			Vector2 vector = new Vector2((float)x, (float)y);
			DungeonRoom dungeonRoom = null;
			float num = 999999f;
			for (int i = 0; i < roomsToCheck.Count; i++)
			{
				DungeonRoom dungeonRoom2 = roomsToCheck[i];
				if (DungeonUtils.RoomCanBeChosen(dungeonRoom2, settings))
				{
					if (dungeonRoom2.OuterBounds.ContainsWithFluff(x, y, settings.Fluff))
					{
						return dungeonRoom2;
					}
					float num2 = Vector2.Distance(vector, dungeonRoom2.Center.ToVector2());
					if (num2 < num)
					{
						dungeonRoom = dungeonRoom2;
						num = num2;
					}
				}
			}
			return dungeonRoom;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x005FFC24 File Offset: 0x005FDE24
		public static List<DungeonRoom> GetAllRoomsNearSpot(List<DungeonRoom> roomsToCheck, int x, int y, DungeonRoomSearchSettings settings)
		{
			List<DungeonRoom> list = new List<DungeonRoom>();
			for (int i = 0; i < roomsToCheck.Count; i++)
			{
				DungeonRoom dungeonRoom = roomsToCheck[i];
				if (DungeonUtils.RoomCanBeChosen(dungeonRoom, settings) && dungeonRoom.OuterBounds.ContainsWithFluff(x, y, settings.Fluff))
				{
					list.Add(dungeonRoom);
				}
			}
			return list;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x005FFC78 File Offset: 0x005FDE78
		public static List<DungeonRoom> GetAllRoomsInSpots(List<DungeonRoom> roomsToCheck, Vector2D startPos, Vector2D endPos, DungeonRoomSearchSettings settings)
		{
			Point point = startPos.ToPoint();
			Point point2 = ((endPos - startPos) / 2.0).ToPoint();
			Point point3 = endPos.ToPoint();
			List<DungeonRoom> list = new List<DungeonRoom>();
			for (int i = 0; i < roomsToCheck.Count; i++)
			{
				DungeonRoom dungeonRoom = roomsToCheck[i];
				if (DungeonUtils.RoomCanBeChosen(dungeonRoom, settings) && (dungeonRoom.OuterBounds.ContainsWithFluff(point, settings.Fluff) || dungeonRoom.OuterBounds.ContainsWithFluff(point2, settings.Fluff) || dungeonRoom.OuterBounds.ContainsWithFluff(point3, settings.Fluff)))
				{
					list.Add(dungeonRoom);
				}
			}
			return list;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x005FFD28 File Offset: 0x005FDF28
		public static bool RoomCanBeChosen(DungeonRoom room, DungeonRoomSearchSettings settings)
		{
			if (room == null)
			{
				return false;
			}
			if (settings.ProgressionStage != null)
			{
				int progressionStage = room.settings.ProgressionStage;
				int value = settings.ProgressionStage.Value;
				switch (settings.ProgressionStageCheck)
				{
				case ProgressionStageCheck.Equals:
					if (progressionStage != value)
					{
						return false;
					}
					break;
				case ProgressionStageCheck.GreaterThenOrEqualTo:
					if (progressionStage < value)
					{
						return false;
					}
					break;
				case ProgressionStageCheck.LesserThenOrEqualTo:
					if (progressionStage > value)
					{
						return false;
					}
					break;
				}
			}
			DungeonRoom excludedRoom = settings.ExcludedRoom;
			if (excludedRoom != null)
			{
				if (excludedRoom == room)
				{
					return false;
				}
				if (settings.MaximumDistance != null && room.Center.ToVector2().Distance(excludedRoom.Center.ToVector2()) >= (float)settings.MaximumDistance.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x005FFDDC File Offset: 0x005FDFDC
		public static bool IsConsideredDungeonTile(int tileType, bool allDungeons = false)
		{
			if (tileType > 0 && Main.tileDungeon[tileType])
			{
				return true;
			}
			if (allDungeons)
			{
				for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
				{
					if (GenVars.dungeonGenVars[i].isDungeonTile[tileType])
					{
						return true;
					}
				}
			}
			else if (GenVars.CurrentDungeonGenVars.isDungeonTile[tileType])
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x005FFE3C File Offset: 0x005FE03C
		public static bool IsConsideredCrackedDungeonTile(int tileType, bool allDungeons = false)
		{
			if (allDungeons)
			{
				for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
				{
					if (GenVars.dungeonGenVars[i].isCrackedBrick[tileType])
					{
						return true;
					}
				}
			}
			else if (GenVars.CurrentDungeonGenVars.isCrackedBrick[tileType])
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x005FFE8C File Offset: 0x005FE08C
		public static bool IsConsideredPitTrapTile(int tileType, bool allDungeons = false)
		{
			if (allDungeons)
			{
				for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
				{
					if (GenVars.dungeonGenVars[i].isPitTrapTile[tileType])
					{
						return true;
					}
				}
			}
			else if (GenVars.CurrentDungeonGenVars.isPitTrapTile[tileType])
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x005FFEDC File Offset: 0x005FE0DC
		public static bool IsConsideredDungeonWall(int wallType, bool allDungeons = false)
		{
			if (wallType > 0 && Main.wallDungeon[wallType])
			{
				return true;
			}
			if (allDungeons)
			{
				for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
				{
					if (GenVars.dungeonGenVars[i].isDungeonWall[wallType])
					{
						return true;
					}
				}
			}
			else if (GenVars.CurrentDungeonGenVars.isDungeonWall[wallType])
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x005FFF3C File Offset: 0x005FE13C
		public static bool IsConsideredDungeonWallGlass(int wallType, bool allDungeons = false)
		{
			if (allDungeons)
			{
				for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
				{
					if (GenVars.dungeonGenVars[i].isDungeonWallGlass[wallType])
					{
						return true;
					}
				}
			}
			else if (GenVars.CurrentDungeonGenVars.isDungeonWallGlass[wallType])
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x005FFF8C File Offset: 0x005FE18C
		public static bool IsHigherOrEqualTieredDungeonTile(DungeonData data, int currentTileType, int newTileType)
		{
			double tierForDungeonTile = DungeonUtils.GetTierForDungeonTile(data.genVars, currentTileType);
			double tierForDungeonTile2 = DungeonUtils.GetTierForDungeonTile(data.genVars, newTileType);
			return tierForDungeonTile >= tierForDungeonTile2;
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x005FFFB8 File Offset: 0x005FE1B8
		public static bool IsHigherOrEqualTieredDungeonWall(DungeonData data, int currentWallType, int newWallType)
		{
			double tierForDungeonWall = DungeonUtils.GetTierForDungeonWall(data.genVars, currentWallType);
			double tierForDungeonWall2 = DungeonUtils.GetTierForDungeonWall(data.genVars, newWallType);
			return tierForDungeonWall >= tierForDungeonWall2;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x005FFFE4 File Offset: 0x005FE1E4
		public static double GetTierForDungeonTile(DungeonGenVars genVars, int tileType)
		{
			if (!WorldGen.SecretSeed.dualDungeons.Enabled)
			{
				return (double)((tileType > 0 && Main.tileDungeon[tileType]) ? 1f : (-1f));
			}
			for (int i = 0; i < genVars.dungeonGenerationStyles.Count; i++)
			{
				DungeonGenerationStyleData dungeonGenerationStyleData = genVars.dungeonGenerationStyles[i];
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.Spider.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.25;
				}
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.LivingWood.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.5;
				}
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.Shimmer.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.75;
				}
				if (dungeonGenerationStyleData.Style == 8 && DungeonGenerationStyles.LivingMahogany.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.33;
				}
				if (dungeonGenerationStyleData.Style == 8 && DungeonGenerationStyles.Beehive.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.66;
				}
				if (dungeonGenerationStyleData.Style == 6 && DungeonGenerationStyles.Crystal.TileIsInStyle(tileType, true))
				{
					return (double)i + 0.5;
				}
				if (dungeonGenerationStyleData.Style == 0 && Main.tileDungeon[tileType])
				{
					return (double)i;
				}
				if (dungeonGenerationStyleData.TileIsInStyle(tileType, true))
				{
					return (double)i;
				}
			}
			if (Main.tileDungeon[tileType])
			{
				return -0.5;
			}
			return -1.0;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x0060015C File Offset: 0x005FE35C
		public static double GetTierForDungeonWall(DungeonGenVars genVars, int wallType)
		{
			if (!WorldGen.SecretSeed.dualDungeons.Enabled)
			{
				return (double)((wallType > 0 && Main.wallDungeon[wallType]) ? 1f : (-1f));
			}
			for (int i = 0; i < genVars.dungeonGenerationStyles.Count; i++)
			{
				DungeonGenerationStyleData dungeonGenerationStyleData = genVars.dungeonGenerationStyles[i];
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.Spider.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.25;
				}
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.LivingWood.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.5;
				}
				if (dungeonGenerationStyleData.Style == 1 && DungeonGenerationStyles.Shimmer.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.75;
				}
				if (dungeonGenerationStyleData.Style == 8 && DungeonGenerationStyles.LivingMahogany.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.33;
				}
				if (dungeonGenerationStyleData.Style == 8 && DungeonGenerationStyles.Beehive.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.66;
				}
				if (dungeonGenerationStyleData.Style == 6 && DungeonGenerationStyles.Crystal.WallIsInStyle(wallType, false))
				{
					return (double)i + 0.5;
				}
				if (dungeonGenerationStyleData.Style == 0 && Main.wallDungeon[wallType])
				{
					return (double)i;
				}
				if (dungeonGenerationStyleData.WallIsInStyle(wallType, false))
				{
					return (double)i;
				}
			}
			if (Main.wallDungeon[wallType])
			{
				return -0.5;
			}
			return -1.0;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x006002D4 File Offset: 0x005FE4D4
		public static void CreatePotentialDungeonBounds(out DungeonBounds innerBounds, out DungeonBounds outerBounds, bool leftDungeon, double percentInMiddle = 0.02, double percentOnEdges = 0.02, double percentOnTop = -1.0, double percentOnBottom = -1.0, int innerBuffer = 10)
		{
			if (percentOnTop == -1.0)
			{
				if (SpecialSeedFeatures.DungeonEntranceIsUnderground)
				{
					percentOnTop = (GenVars.worldSurfaceHigh + 10.0) / (double)Main.maxTilesY;
				}
				else
				{
					percentOnTop = (Main.worldSurface + 10.0) / (double)Main.maxTilesY;
				}
			}
			if (percentOnBottom == -1.0)
			{
				percentOnBottom = ((double)Main.UnderworldLayer - 10.0) / (double)Main.maxTilesY;
			}
			double num = percentInMiddle / 2.0;
			double num2 = (double)Main.maxTilesX / 4200.0;
			int num3 = (leftDungeon ? ((int)((double)Main.maxTilesX * percentOnEdges)) : ((int)((double)Main.maxTilesX * (0.5 + num))));
			int num4 = (leftDungeon ? ((int)((double)Main.maxTilesX * (0.5 - num))) : (Main.maxTilesX - (int)((double)Main.maxTilesX * percentOnEdges)));
			int num5 = (int)((double)Main.maxTilesY * percentOnTop);
			int num6 = (int)((double)Main.maxTilesY * percentOnBottom);
			outerBounds = new DungeonBounds();
			outerBounds.SetBounds(num3, num5, num4, num6);
			innerBounds = new DungeonBounds();
			innerBounds.SetBounds(num3 + innerBuffer, num5 + innerBuffer, num4 - innerBuffer, num6 - innerBuffer);
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x00600404 File Offset: 0x005FE604
		public static bool InAnyPotentialDungeonBounds(int x, int y, int fluff = 0, bool inner = false)
		{
			int num;
			return DungeonUtils.InAnyPotentialDungeonBounds(out num, x, y, fluff, inner);
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x0060041C File Offset: 0x005FE61C
		public static bool InAnyPotentialDungeonBounds(out int iteration, int x, int y, int fluff = 0, bool inner = false)
		{
			iteration = -1;
			for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
			{
				DungeonGenVars dungeonGenVars = GenVars.dungeonGenVars[i];
				if ((inner && dungeonGenVars.innerPotentialDungeonBounds.ContainsWithFluff(x, y, fluff)) || (!inner && dungeonGenVars.outerPotentialDungeonBounds.ContainsWithFluff(x, y, fluff)))
				{
					iteration = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x0060047C File Offset: 0x005FE67C
		public static bool IntersectsAnyPotentialDungeonBounds(Rectangle rect, bool inner = false)
		{
			int num;
			return DungeonUtils.IntersectsAnyPotentialDungeonBounds(out num, rect, inner);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x00600494 File Offset: 0x005FE694
		public static bool IntersectsAnyPotentialDungeonBounds(out int iteration, Rectangle rect, bool inner = false)
		{
			iteration = -1;
			for (int i = 0; i < GenVars.dungeonGenVars.Count; i++)
			{
				DungeonGenVars dungeonGenVars = GenVars.dungeonGenVars[i];
				if ((inner && dungeonGenVars.innerPotentialDungeonBounds.Intersects(rect)) || (!inner && dungeonGenVars.outerPotentialDungeonBounds.Intersects(rect)))
				{
					iteration = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x006004F0 File Offset: 0x005FE6F0
		public static Rectangle GetExpandedDungeonAreaFromPoint(int x, int y)
		{
			int num;
			int num2;
			int num3;
			int num4;
			for (int i = 0; i < 2; i++)
			{
				num = x;
				num2 = x;
				while (!Main.tile[num, y].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num, y].wall, false))
				{
					num--;
				}
				num++;
				while (!Main.tile[num2, y].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num2, y].wall, false))
				{
					num2++;
				}
				num2--;
				x = (num + num2) / 2;
				num3 = y;
				num4 = y;
				while (!Main.tile[x, num3].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[x, num3].wall, false))
				{
					num3--;
				}
				num3++;
				while (!Main.tile[x, num4].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[x, num4].wall, false))
				{
					num4++;
				}
				num4--;
				y = (num3 + num4) / 2;
			}
			num = x;
			num2 = x;
			while (!Main.tile[num, y].active() && !Main.tile[num, y - 1].active() && !Main.tile[num, y + 1].active())
			{
				num--;
			}
			num++;
			while (!Main.tile[num2, y].active() && !Main.tile[num2, y - 1].active() && !Main.tile[num2, y + 1].active())
			{
				num2++;
			}
			num2--;
			num3 = y;
			num4 = y;
			while (!Main.tile[x, num3].active() && !Main.tile[x - 1, num3].active() && !Main.tile[x + 1, num3].active())
			{
				num3--;
			}
			num3++;
			while (!Main.tile[x, num4].active() && !Main.tile[x - 1, num4].active() && !Main.tile[x + 1, num4].active())
			{
				num4++;
			}
			num4--;
			return new Rectangle(num, num3, num4 - num3, num2 - num);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x0060073C File Offset: 0x005FE93C
		public static int CalculateFloodedTileCountFromShapeData(DungeonBounds innerBounds, ShapeData data)
		{
			Point16[] array = data.GetData().ToArray<Point16>();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if ((int)array[i].Y > innerBounds.Center.Y)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x0000357B File Offset: 0x0000177B
		public DungeonUtils()
		{
		}

		// Token: 0x040059B0 RID: 22960
		public const int DOORSTYLE_WOODEN = 13;

		// Token: 0x040059B1 RID: 22961
		public const int DOORSTYLE_BLUEBRICK = 16;

		// Token: 0x040059B2 RID: 22962
		public const int DOORSTYLE_GREENBRICK = 17;

		// Token: 0x040059B3 RID: 22963
		public const int DOORSTYLE_PINKBRICK = 18;

		// Token: 0x040059B4 RID: 22964
		public const int POTSTYLE_NORMAL_1 = 0;

		// Token: 0x040059B5 RID: 22965
		public const int POTSTYLE_NORMAL_2 = 1;

		// Token: 0x040059B6 RID: 22966
		public const int POTSTYLE_NORMAL_3 = 2;

		// Token: 0x040059B7 RID: 22967
		public const int POTSTYLE_NORMAL_4 = 3;

		// Token: 0x040059B8 RID: 22968
		public const int POTSTYLE_SKULL_1 = 10;

		// Token: 0x040059B9 RID: 22969
		public const int POTSTYLE_SKULL_2 = 11;

		// Token: 0x040059BA RID: 22970
		public const int POTSTYLE_SKULL_3 = 12;

		// Token: 0x040059BB RID: 22971
		public const int CHANDELIERSTYLE_BLUEBRICK = 27;

		// Token: 0x040059BC RID: 22972
		public const int CHANDELIERSTYLE_GREENBRICK = 28;

		// Token: 0x040059BD RID: 22973
		public const int CHANDELIERSTYLE_PINKBRICK = 29;

		// Token: 0x040059BE RID: 22974
		public const int PLATFORMSTYLE_BLUEBRICK = 6;

		// Token: 0x040059BF RID: 22975
		public const int PLATFORMSTYLE_GREENBRICK = 8;

		// Token: 0x040059C0 RID: 22976
		public const int PLATFORMSTYLE_PINKBRICK = 7;

		// Token: 0x040059C1 RID: 22977
		public const int PLATFORMSTYLE_METALSHELF = 9;

		// Token: 0x040059C2 RID: 22978
		public const int PLATFORMSTYLE_BRASSSHELF = 10;

		// Token: 0x040059C3 RID: 22979
		public const int PLATFORMSTYLE_WOODSHELF = 11;

		// Token: 0x040059C4 RID: 22980
		public const int PLATFORMSTYLE_DUNGEONSHELF = 12;

		// Token: 0x040059C5 RID: 22981
		public const int BANNERSTYLE_BRICK_MARCHINGBONES = 10;

		// Token: 0x040059C6 RID: 22982
		public const int BANNERSTYLE_BRICK_NECROMANTICSIGN = 11;

		// Token: 0x040059C7 RID: 22983
		public const int BANNERSTYLE_SLAB_RUGGEDCOMPANY = 12;

		// Token: 0x040059C8 RID: 22984
		public const int BANNERSTYLE_SLAB_RAGGEDBROTHERHOOD = 13;

		// Token: 0x040059C9 RID: 22985
		public const int BANNERSTYLE_TILES_MOLTENLEGION = 14;

		// Token: 0x040059CA RID: 22986
		public const int BANNERSTYLE_TILES_DIABOLICSIGIL = 15;

		// Token: 0x040059CB RID: 22987
		public const int TRAPTYPE_DART = 0;

		// Token: 0x040059CC RID: 22988
		public const double HALLWAY_DOOR_PLACEMENT_VARIANCE = 0.25;

		// Token: 0x040059CD RID: 22989
		public const int DUNGEONHALL_DEFAULT_INNER_AREA_DEPTH = 3;

		// Token: 0x040059CE RID: 22990
		public const int DUNGEONHALL_DEFAULT_OUTER_WALL_DEPTH = 8;

		// Token: 0x040059CF RID: 22991
		public const int DUNGEONROOM_DEFAULT_INNER_AREA_DEPTH = 6;

		// Token: 0x040059D0 RID: 22992
		public const int DUNGEONROOM_DEFAULT_OUTER_WALL_DEPTH = 8;

		// Token: 0x040059D1 RID: 22993
		public const int MOSAIC_NONE = 0;

		// Token: 0x040059D2 RID: 22994
		public const int MOSAIC_SKELETRON = 1;

		// Token: 0x040059D3 RID: 22995
		public const int MOSAIC_MOONLORD = 2;

		// Token: 0x02000983 RID: 2435
		// (Invoke) Token: 0x06004949 RID: 18761
		public delegate ConnectionPointQuality GetHallwayConnectionPoint(DungeonRoom room, Vector2D otherRoomPos, out Vector2D connectionPoint);

		// Token: 0x02000984 RID: 2436
		[CompilerGenerated]
		private sealed class <>c__DisplayClass61_0
		{
			// Token: 0x0600494C RID: 18764 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass61_0()
			{
			}

			// Token: 0x0600494D RID: 18765 RVA: 0x006D0DC6 File Offset: 0x006CEFC6
			internal bool <GenerateFloatingRocksInArea>b__0(DungeonData dungeonData, int platformX, int platformY)
			{
				return platformX >= this.x && platformX <= this.x + this.width && platformY >= this.y && platformY <= this.y + this.height;
			}

			// Token: 0x0400762D RID: 30253
			public int x;

			// Token: 0x0400762E RID: 30254
			public int width;

			// Token: 0x0400762F RID: 30255
			public int y;

			// Token: 0x04007630 RID: 30256
			public int height;

			// Token: 0x04007631 RID: 30257
			public Func<DungeonData, int, int, bool> <>9__0;
		}
	}
}
