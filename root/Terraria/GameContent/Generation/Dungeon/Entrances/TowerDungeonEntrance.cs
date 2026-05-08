using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Entrances
{
	// Token: 0x020004ED RID: 1261
	public class TowerDungeonEntrance : DungeonEntrance
	{
		// Token: 0x06003535 RID: 13621 RVA: 0x0061463C File Offset: 0x0061283C
		public TowerDungeonEntrance(DungeonEntranceSettings settings)
			: base(settings)
		{
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x00614645 File Offset: 0x00612845
		public override void CalculateEntrance(DungeonData data, int x, int y)
		{
			this.calculated = false;
			this.TowerEntrance(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x0061465F File Offset: 0x0061285F
		public override bool GenerateEntrance(DungeonData data, int x, int y)
		{
			this.generated = false;
			this.TowerEntrance(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x0061467A File Offset: 0x0061287A
		public override bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return !(feature is DungeonGlobalBookshelves) && !(feature is DungeonGlobalPaintings) && !(feature is DungeonGlobalSpikes) && base.CanGenerateFeatureAt(data, feature, x, y);
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x006146A4 File Offset: 0x006128A4
		public void TowerEntrance(DungeonData data, int i, int j, bool generating)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(((TowerDungeonEntranceSettings)this.settings).RandomSeed);
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			WindowType windowType;
			switch (unifiedRandom.Next(3))
			{
			default:
				windowType = WindowType.RegularWindows;
				break;
			case 1:
				windowType = WindowType.SkeletronMosaic;
				break;
			case 2:
				windowType = WindowType.MoonLordMosaic;
				break;
			}
			bool dungeonEntranceIsBuried = SpecialSeedFeatures.DungeonEntranceIsBuried;
			bool dungeonEntranceIsUnderground = SpecialSeedFeatures.DungeonEntranceIsUnderground;
			bool flag = data.genVars.dungeonSide == (int)DungeonSide.Left;
			if (Main.drunkWorld)
			{
				flag = !flag;
			}
			this.Bounds.SetBounds(i, j, i, j);
			if (generating)
			{
				int num = 60;
				for (int k = i - num; k < i + num; k++)
				{
					for (int l = j - num; l < j + num; l++)
					{
						if (WorldGen.InWorld(k, l, 0))
						{
							Main.tile[k, l].liquid = 0;
							Main.tile[k, l].lava(false);
							Main.tile[k, l].Clear(TileDataType.Slope);
						}
					}
				}
			}
			int num2 = 5;
			int num3 = 35;
			int num4 = num3 + num2;
			int num5 = 100;
			int num6 = 30;
			int num7 = j - num6;
			int num8 = 30;
			int num9 = 25;
			int num10 = num9 + num2;
			int num11 = 20;
			int num12 = num8 + num11;
			int num13 = 15;
			int num14 = num13 + num2;
			int num15 = 40;
			int num16 = num8 + num11 + num15;
			int num17 = num7 - num16;
			int num18 = num7 + 10;
			int m = 10;
			int num19 = 50;
			if (data.Type == DungeonType.DualDungeon)
			{
				num5 = DungeonUtils.GetDualDungeonBrickSupportCutoffY(data) - num7;
			}
			else if (dungeonEntranceIsUnderground)
			{
				num5 = num19 - m + 5;
			}
			if (generating && !dungeonEntranceIsBuried && !dungeonEntranceIsUnderground)
			{
				int num20 = i - num3 + 1;
				if (flag)
				{
					num20 = i + num3 - 1;
				}
				WorldUtils.Gen(new Point(num20, num7 - 15), new Shapes.Circle(15, 15), Actions.Chain(new GenAction[]
				{
					new Actions.Clear()
				}));
			}
			this.Bounds.UpdateBounds(i - num4, num17, i + num4 + 1, num18);
			if (generating)
			{
				int num21 = -5;
				int num22 = num5;
				for (int n = -num4; n <= num4; n++)
				{
					for (int num23 = num21; num23 < num22; num23++)
					{
						int num24 = i + n;
						int num25 = num7 + num23;
						if (WorldGen.InWorld(num24, num25, 0))
						{
							Tile tile = Main.tile[num24, num25];
							bool flag2 = tile.active() && !this.settings.StyleData.TileIsInStyle((int)tile.type, true);
							bool flag3 = !this.settings.StyleData.WallIsInStyle((int)tile.wall, false);
							bool flag4 = DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false);
							if (num23 < 0)
							{
								tile.ClearEverything();
							}
							else if (num23 >= 0 && num23 < 5)
							{
								if ((n >= -num4 + num2 && n <= -num4 + num2 * 2 - 1) || (n >= num4 - num2 * 2 + 1 && n <= num4 - num2))
								{
									tile.ClearEverything();
									if (!flag4)
									{
										tile.wall = brickWallType;
									}
								}
								else if (!flag4)
								{
									tile.liquid = 0;
									tile.active(true);
									tile.type = brickTileType;
									if (n != -num4 && n != num4)
									{
										tile.wall = brickWallType;
									}
								}
							}
							else if (num23 >= 5 && num23 < 10)
							{
								if (n >= -num4 + num2 && n <= num4 - num2)
								{
									tile.ClearEverything();
									tile.wall = brickWallType;
								}
								else if (!flag4)
								{
									tile.liquid = 0;
									tile.active(true);
									tile.type = brickTileType;
									if (n != -num4 && n != num4)
									{
										tile.wall = brickWallType;
									}
								}
							}
							else if ((tile.active() && flag2) || !flag4)
							{
								tile.liquid = 0;
								tile.active(true);
								tile.type = brickTileType;
								if (n != -num4 && n != num4)
								{
									tile.wall = brickWallType;
								}
							}
							else if (flag3)
							{
								tile.liquid = 0;
								if (n != -num4 && n != num4)
								{
									tile.wall = brickWallType;
								}
							}
							if (num23 == 1 && (n == -num4 + num2 || n == num4 - num2 * 2))
							{
								DungeonPlatformData dungeonPlatformData = new DungeonPlatformData
								{
									Position = new Point(num24, num25),
									OverrideHeightFluff = new int?(0),
									ForcePlacement = true,
									PlacePotsChance = 0.33000001311302185
								};
								data.dungeonPlatformData.Add(dungeonPlatformData);
							}
							if (num23 == 10 && n == 0)
							{
								DungeonPlatformData dungeonPlatformData2 = new DungeonPlatformData
								{
									Position = new Point(num24, num25),
									OverrideHeightFluff = new int?(0),
									ForcePlacement = true,
									PlacePotsChance = 0.33000001311302185
								};
								data.dungeonPlatformData.Add(dungeonPlatformData2);
							}
						}
					}
				}
				int num26 = -1;
				int num27 = 6;
				while (m < num19)
				{
					Tile tile2 = Main.tile[i, num7 + m];
					if (num26 == -1 && !tile2.active())
					{
						num26 = 15;
					}
					if (num26 > 0)
					{
						num26--;
						if (num26 <= 0)
						{
							break;
						}
						if (num26 <= 5)
						{
							num27--;
						}
					}
					for (int num28 = -num27; num28 <= num27; num28++)
					{
						Tile tile3 = Main.tile[i + num28, num7 + m];
						tile3.ClearEverything();
						if (!DungeonUtils.IsConsideredDungeonWall((int)tile3.wall, false))
						{
							tile3.wall = brickWallType;
						}
					}
					m++;
				}
			}
			if (generating)
			{
				for (int num29 = -num4; num29 <= num4; num29++)
				{
					int num30 = i + num29;
					for (int num31 = 0; num31 <= num16; num31++)
					{
						int num32 = num7 - num31;
						if (WorldGen.InWorld(num30, num32, 5))
						{
							Tile tile4 = Main.tile[num30, num32];
							if (num31 >= 0 && num31 <= num8)
							{
								if (num29 >= -num3 && num29 <= num3)
								{
									DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
								}
								else
								{
									if (num29 > -num4 && num29 < num4)
									{
										DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
									}
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
								if (num31 >= num8 - num2 && (num29 < -num9 || num29 > num9))
								{
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
							}
							else if (num31 >= num8 - num2 && num31 <= num12 && num29 >= -num10 && num29 <= num10)
							{
								if (num29 >= -num9 && num29 <= num9)
								{
									DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
								}
								else
								{
									if (num29 > -num10 && num29 < num10)
									{
										DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
									}
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
								if (num31 >= num12 - num2 && (num29 < -num13 || num29 > num13))
								{
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
							}
							else if (num31 >= num12 - num2 && num31 <= num16 && num29 >= -num14 && num29 <= num14)
							{
								if (num29 >= -num13 && num29 <= num13)
								{
									DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
								}
								else
								{
									if (num29 > -num14 && num29 < num14)
									{
										DungeonUtils.ChangeWallType(tile4, brickWallType, true, -1);
									}
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
								if (num31 >= num16 - num2)
								{
									DungeonUtils.ChangeTileType(tile4, brickTileType, false, -1);
								}
							}
						}
					}
				}
			}
			DungeonPillarSettings dungeonPillarSettings = new DungeonPillarSettings
			{
				Style = this.settings.StyleData,
				PillarType = PillarType.Block,
				Width = 3,
				Height = 0,
				CrowningOnTop = true,
				CrowningOnBottom = true,
				CrowningStopsAtPillar = false,
				AlwaysPlaceEntirePillar = true
			};
			if (generating)
			{
				dungeonPillarSettings.PillarType = PillarType.BlockActuated;
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num9 - 3, num7);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num9 + 3, num7);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num13 - 3, num7);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num13 + 3, num7);
			}
			if (generating)
			{
				DungeonUtils.GenerateBottomWedge(i - num4 - 4, num7 - num8, 5, brickTileType, true, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i - num4 - 4, num7 - num8, brickTileType);
				DungeonUtils.GenerateBottomWedge(i - num10 - 4, num7 - num12, 5, brickTileType, true, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i - num10 - 4, num7 - num12, brickTileType);
				DungeonUtils.GenerateBottomWedge(i - num14 - 4, num7 - num16, 5, brickTileType, true, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i - num14 - 4, num7 - num16, brickTileType);
				DungeonUtils.GenerateBottomWedge(i + num4 + 3, num7 - num8, 5, brickTileType, false, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i + num4 + 4, num7 - num8, brickTileType);
				DungeonUtils.GenerateBottomWedge(i + num10 + 3, num7 - num12, 5, brickTileType, false, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i + num10 + 4, num7 - num12, brickTileType);
				DungeonUtils.GenerateBottomWedge(i + num14 + 3, num7 - num16, 5, brickTileType, false, false, false, true, -1);
				this.TowerEntrance_OuterPillar(data, i + num14 + 4, num7 - num16, brickTileType);
			}
			if (generating)
			{
				dungeonPillarSettings.PillarType = PillarType.Block;
				dungeonPillarSettings.CrowningOnTop = false;
				dungeonPillarSettings.CrowningOnBottom = false;
				dungeonPillarSettings.Width = 5;
				dungeonPillarSettings.Height = 2;
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num4 + 5, num7 - num8 - 1);
				this.TowerEntrance_LineOfFence(i - num4 - 2, i - num10 + 1, num7 - num8 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num10 + 5, num7 - num12 - 1);
				this.TowerEntrance_LineOfFence(i - num10 - 2, i - num14 + 1, num7 - num12 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num14 + 5, num7 - num16 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i - num14 + 13, num7 - num16 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num4 - 5, num7 - num8 - 1);
				this.TowerEntrance_LineOfFence(i + num10 - 1, i + num4 + 2, num7 - num8 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num10 - 5, num7 - num12 - 1);
				this.TowerEntrance_LineOfFence(i + num14 - 1, i + num10 + 2, num7 - num12 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num14 - 5, num7 - num16 - 1);
				new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, i + num14 - 13, num7 - num16 - 1);
				this.TowerEntrance_LineOfFence(i - num14 - 2, i + num14 + 2, num7 - num16 - 1);
				DungeonUtils.GenerateBottomWedge(i - num13, num7 - num16 + num2, 3, brickTileType, false, false, false, true, -1);
				DungeonUtils.GenerateBottomWedge(i + num13 - 1, num7 - num16 + num2, 3, brickTileType, true, false, false, true, -1);
			}
			if (generating)
			{
				this.TowerEntrance_AddPlatform(data, new Point(i - num10 - 2, num7 - num8 + 15));
				this.TowerEntrance_AddPlatform(data, new Point(i - num10 - 2, num7 - num8 + 21));
				this.TowerEntrance_AddPlatform(data, new Point(i - num14 - 2, num7 - num12 + 15));
				this.TowerEntrance_AddPlatform(data, new Point(i - num14 - 2, num7 - num12 + 21));
				this.TowerEntrance_AddPlatform(data, new Point(i + num10 + 2, num7 - num8 + 15));
				this.TowerEntrance_AddPlatform(data, new Point(i + num10 + 2, num7 - num8 + 21));
				this.TowerEntrance_AddPlatform(data, new Point(i + num14 + 2, num7 - num12 + 15));
				this.TowerEntrance_AddPlatform(data, new Point(i + num14 + 2, num7 - num12 + 21));
				this.TowerEntrance_AddPlatform(data, new Point(i, num7 - num12 + num2 - 3));
			}
			if (generating)
			{
				int num33 = num7 - num16 + 20;
				DungeonWindowBasicSettings dungeonWindowBasicSettings = new DungeonWindowBasicSettings
				{
					Style = this.settings.StyleData,
					Width = 5,
					Height = 24,
					Closed = dungeonEntranceIsUnderground
				};
				DungeonWindowMosaicSettings dungeonWindowMosaicSettings = new DungeonWindowMosaicSettings
				{
					Style = this.settings.StyleData,
					Closed = dungeonEntranceIsUnderground,
					MosaicType = windowType
				};
				switch (windowType)
				{
				case WindowType.RegularWindows:
					new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i - 9, num33 + 4);
					new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i + 9, num33 + 4);
					dungeonWindowBasicSettings.Height = 28;
					new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i, num33 + 3);
					break;
				case WindowType.SkeletronMosaic:
					if (!dungeonEntranceIsUnderground)
					{
						dungeonWindowMosaicSettings.OverrideGlassType = 89;
					}
					dungeonWindowMosaicSettings.OverrideGlassPaint = 26;
					new DungeonWindowMosaic(dungeonWindowMosaicSettings).GenerateFeature(data, i, num33 - 1);
					break;
				case WindowType.MoonLordMosaic:
					if (!dungeonEntranceIsUnderground)
					{
						dungeonWindowMosaicSettings.OverrideGlassType = 91;
					}
					new DungeonWindowMosaic(dungeonWindowMosaicSettings).GenerateFeature(data, i, num33 + 5);
					break;
				}
				dungeonWindowBasicSettings.Width = 9;
				dungeonWindowBasicSettings.Height = 24;
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i - 8, num7 - 16);
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i + 8, num7 - 16);
				dungeonWindowBasicSettings.Width = 7;
				dungeonWindowBasicSettings.Height = 11;
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i - 10, num7 - 37);
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i + 10, num7 - 37);
				dungeonWindowBasicSettings.Height = 13;
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, i, num7 - 39);
			}
			if (generating)
			{
				this.TowerEntrance_Door(data, i, num7, num4, num3, flag, dungeonEntranceIsBuried);
				this.TowerEntrance_Door(data, i, num7, num4, num3, !flag, dungeonEntranceIsBuried);
			}
			this.OldManSpawn = DungeonUtils.SetOldManSpawnAndSpawnOldManIfDefaultDungeon(i, num7, generating);
			if (generating && SpecialSeedFeatures.DungeonEntranceHasATree)
			{
				DungeonUtils.GenerateDungeonTree(data, i, (int)Main.worldSurface, num7 - num16 + 8, false);
			}
			if (generating && SpecialSeedFeatures.DungeonEntranceHasStairs)
			{
				int num34 = i + num4;
				DungeonUtils.GenerateDungeonStairs(data, num34, num7, 1, brickTileType, brickWallType, num5);
				num34 = i - num4;
				DungeonUtils.GenerateDungeonStairs(data, num34, num7, -1, brickTileType, brickWallType, num5);
			}
			this.Bounds.CalculateHitbox();
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x006154B0 File Offset: 0x006136B0
		public void TowerEntrance_Door(DungeonData data, int i, int entranceFloor, int outerSize, int innerSize, bool leftDungeonDoor, bool buried)
		{
			int num = (leftDungeonDoor ? (innerSize - 1) : (-outerSize - 2));
			int num2 = (leftDungeonDoor ? (outerSize + 2) : (-innerSize + 1));
			if (buried)
			{
				num += 2 * (leftDungeonDoor ? 0 : 1);
				num2 += 2 * (leftDungeonDoor ? (-1) : 0);
			}
			Point point = new Point(i + (leftDungeonDoor ? (outerSize - 1) : (-outerSize + 1)), entranceFloor);
			Point point2 = new Point(i + (leftDungeonDoor ? (innerSize + 1) : (-innerSize - 1)), entranceFloor);
			for (int j = num; j <= num2; j++)
			{
				for (int k = -3; k <= 1; k++)
				{
					int num3 = j + i;
					int num4 = k + entranceFloor;
					Tile tile = Main.tile[num3, num4];
					if (!buried && ((leftDungeonDoor && num3 >= point.X) || (!leftDungeonDoor && num3 <= point.X)))
					{
						tile.wall = 0;
					}
					if (k >= -2 && k <= 0)
					{
						tile.ClearTile();
					}
				}
			}
			WorldGen.PlaceTile(point.X, point.Y, 10, true, true, -1, 13);
			WorldGen.PlaceTile(point2.X, point2.Y, 10, true, true, -1, 13);
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x006155D8 File Offset: 0x006137D8
		public void TowerEntrance_LineOfFence(int leftX, int rightX, int y)
		{
			if (y <= 10)
			{
				return;
			}
			for (int i = leftX; i <= rightX; i++)
			{
				if (leftX >= 10 && rightX <= Main.maxTilesX - 10)
				{
					WorldGen.PlaceWall(i, y, 245, true);
				}
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x00615614 File Offset: 0x00613814
		public void TowerEntrance_OuterPillar(DungeonData data, int pillarX, int pillarY, ushort tileType)
		{
			DungeonPillarSettings dungeonPillarSettings = new DungeonPillarSettings();
			dungeonPillarSettings.Style = this.settings.StyleData;
			dungeonPillarSettings.PillarType = PillarType.Block;
			dungeonPillarSettings.Width = 7;
			dungeonPillarSettings.Height = 3;
			new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, pillarX, pillarY - 1);
			dungeonPillarSettings.Width = 5;
			dungeonPillarSettings.Height = 7;
			new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, pillarX, pillarY - 4);
			if (pillarY - 11 >= 10)
			{
				WorldGen.PlaceTile(pillarX, pillarY - 11, 215, true, false, -1, 0);
			}
			for (int i = 0; i < 5; i++)
			{
				WorldGen.PlaceWall(pillarX - 2 + i, pillarY - 11, 245, true);
			}
			if (pillarY - 12 >= 10)
			{
				WorldGen.PlaceWall(pillarX - 2, pillarY - 12, 245, true);
				WorldGen.PlaceWall(pillarX + 2, pillarY - 12, 245, true);
			}
			if (pillarY - 10 >= 10)
			{
				WorldGen.PlaceWall(pillarX - 2, pillarY - 10, 245, true);
				WorldGen.PlaceWall(pillarX + 2, pillarY - 10, 245, true);
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x00615710 File Offset: 0x00613910
		public void TowerEntrance_TreeOnPillar(UnifiedRandom genRand, int pillarX, int pillarY)
		{
			int num = 5;
			int num2 = num / 2;
			for (int i = 0; i < num; i++)
			{
				int num3 = pillarX + i - num2;
				for (int j = 0; j <= 3; j++)
				{
					int num4 = pillarY + j;
					if (num4 <= 10)
					{
						break;
					}
					if ((j != 1 || genRand.Next(2) != 0) && (j != 2 || genRand.Next(3) == 0) && (j != 3 || genRand.Next(4) == 0))
					{
						Tile tile = Main.tile[num3, num4];
						if (WorldGen.TileIsExposedToAir(num3, num4))
						{
							tile.type = 2;
						}
						else
						{
							tile.type = 0;
						}
					}
				}
			}
			if (pillarY > 10)
			{
				WorldGen.TryGrowingTreeByType(5, pillarX, pillarY, 0, true);
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x006157B8 File Offset: 0x006139B8
		public void TowerEntrance_AddPlatform(DungeonData data, Point position)
		{
			DungeonPlatformData dungeonPlatformData = new DungeonPlatformData
			{
				Position = position,
				OverrideHeightFluff = new int?(0),
				ForcePlacement = true,
				PlacePotsChance = 0.33000001311302185,
				PlaceBooksChance = 0.75,
				PlacePotionBottlesChance = 0.10000000149011612,
				NoWaterbolt = true
			};
			data.dungeonPlatformData.Add(dungeonPlatformData);
		}
	}
}
