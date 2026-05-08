using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004DC RID: 1244
	public class DungeonGlobalEarlyDualDungeonFeatures : GlobalDungeonFeature
	{
		// Token: 0x060034FE RID: 13566 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalEarlyDualDungeonFeatures(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x0060D616 File Offset: 0x0060B816
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.EarlyDungeonFeatures(data);
			this.generated = true;
			return true;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x0060D630 File Offset: 0x0060B830
		public void EarlyDungeonFeatures(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = 20;
			int num2 = 8;
			int num3 = 8;
			int num4 = 6;
			int num5 = 4;
			int num6 = 4;
			int num7 = 40;
			int num8 = 40;
			switch (WorldGen.GetWorldSize())
			{
			case 0:
				num = 20;
				num2 = 8;
				num3 = 8;
				num4 = 6;
				num5 = 4;
				num6 = 4;
				num7 = 40;
				num8 = 40;
				break;
			case 1:
				num = 30;
				num2 = 14;
				num3 = 12;
				num4 = 10;
				num5 = 6;
				num6 = 8;
				num7 = 60;
				num8 = 60;
				break;
			case 2:
				num = 40;
				num2 = 18;
				num3 = 16;
				num4 = 14;
				num5 = 8;
				num6 = 12;
				num7 = 80;
				num8 = 80;
				break;
			}
			if (WorldGen.SecretSeed.Variations.actuallyNoTrapsForRealIMeanIt)
			{
				num3 = 0;
				num4 = 0;
				num5 = 0;
			}
			for (int i = 0; i < data.genVars.dungeonGenerationStyles.Count; i++)
			{
				DungeonGenerationStyleData dungeonGenerationStyleData = data.genVars.dungeonGenerationStyles[i];
				byte style = dungeonGenerationStyleData.Style;
				DungeonBounds dungeonBounds = data.outerProgressionBounds[i];
				if (style == 4 || style == 5)
				{
					bool flag = style == 5;
					int j = num;
					int num9 = 1000;
					while (j > 0)
					{
						num9--;
						if (num9 <= 0)
						{
							break;
						}
						int num10 = dungeonBounds.Left + genRand.Next(dungeonBounds.Width);
						int num11 = dungeonBounds.Top + genRand.Next(dungeonBounds.Height);
						Tile tile = Main.tile[num10, num11];
						Tile tile2 = Main.tile[num10, num11 + 1];
						while (!tile.active() && num11 < Main.maxTilesY - 10)
						{
							num11++;
							tile = Main.tile[num10, num11];
						}
						num11--;
						tile = Main.tile[num10, num11];
						tile2 = Main.tile[num10, num11 + 1];
						if (!tile.active() && tile.wall == dungeonGenerationStyleData.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile2.type);
							if (styleForTile != null && styleForTile.Style == (flag ? 5 : 4) && tile2.type != styleForTile.BrickCrackedTileType && tile2.type != styleForTile.PitTrapTileType)
							{
								WorldGen.Place3x2(num10, num11, 26, flag ? 1 : 0);
								tile = Main.tile[num10, num11];
								if (tile.active() && tile.type == 26)
								{
									j--;
								}
							}
						}
					}
				}
			}
			Dictionary<int, List<DungeonRoom>> dictionary = new Dictionary<int, List<DungeonRoom>>();
			BiomeDungeonRoom biomeDungeonRoom = null;
			for (int k = 0; k < data.dungeonRooms.Count; k++)
			{
				DungeonRoom dungeonRoom = data.dungeonRooms[k];
				byte style2 = dungeonRoom.settings.StyleData.Style;
				if (!dictionary.ContainsKey((int)style2))
				{
					dictionary.Add((int)style2, new List<DungeonRoom>());
				}
				dictionary[(int)style2].Add(dungeonRoom);
				if (dungeonRoom is BiomeDungeonRoom && dungeonRoom.settings.StyleData.Style == 10)
				{
					biomeDungeonRoom = (BiomeDungeonRoom)dungeonRoom;
				}
			}
			if (dictionary.ContainsKey(4))
			{
				int num12 = num2;
				List<DungeonRoom> list = dictionary[4].ToList<DungeonRoom>();
				while (list.Count > 0 && num12 > 0)
				{
					DungeonRoom dungeonRoom2 = list[genRand.Next(list.Count)];
					Point center = dungeonRoom2.InnerBounds.Center;
					Main.tile[center.X, center.Y];
					WorldGen.AddShadowOrb(center.X, center.Y, false);
					if (Main.tile[center.X, center.Y].type == 31)
					{
						num12--;
					}
					list.Remove(dungeonRoom2);
				}
			}
			if (dictionary.ContainsKey(5))
			{
				int num13 = num2;
				List<DungeonRoom> list2 = dictionary[5].ToList<DungeonRoom>();
				while (list2.Count > 0 && num13 > 0)
				{
					DungeonRoom dungeonRoom3 = list2[genRand.Next(list2.Count)];
					Point center2 = dungeonRoom3.InnerBounds.Center;
					Main.tile[center2.X, center2.Y];
					WorldGen.AddShadowOrb(center2.X, center2.Y, true);
					if (Main.tile[center2.X, center2.Y].type == 31)
					{
						num13--;
					}
					list2.Remove(dungeonRoom3);
				}
			}
			if (dictionary.ContainsKey(9))
			{
				List<DungeonRoom> list3 = dictionary[9].ToList<DungeonRoom>();
				while (list3.Count > 0)
				{
					DungeonRoom dungeonRoom4 = list3[0];
					Point center3 = dungeonRoom4.InnerBounds.Center;
					WorldGen.AddBeeLarva(center3.X - 1, center3.Y - 3);
					list3.Remove(dungeonRoom4);
				}
			}
			if (data.Type == DungeonType.DualDungeon)
			{
				for (int l = 0; l < data.genVars.dungeonGenerationStyles.Count; l++)
				{
					DungeonGenerationStyleData dungeonGenerationStyleData2 = data.genVars.dungeonGenerationStyles[l];
					List<DungeonRoom> list4 = dictionary[(int)dungeonGenerationStyleData2.Style];
					int num14 = num6;
					int num15;
					switch (WorldGen.GetWorldSize())
					{
					default:
						num15 = 2;
						break;
					case 1:
						num15 = 4;
						break;
					case 2:
						num15 = 6;
						break;
					}
					if (list4 != null)
					{
						while (list4.Count > 0 && num14 > 0)
						{
							DungeonRoom dungeonRoom5 = list4[genRand.Next(list4.Count)];
							if (dungeonRoom5 is BiomeDungeonRoom)
							{
								list4.Remove(dungeonRoom5);
							}
							else
							{
								int x = dungeonRoom5.InnerBounds.Center.X;
								int num16 = dungeonRoom5.InnerBounds.Bottom - 5;
								int num17 = dungeonRoom5.InnerBounds.Width / 2;
								int num18 = (int)((float)dungeonRoom5.InnerBounds.Height * 0.75f);
								bool flag2 = num15 > 0 || genRand.Next(8) == 0;
								DungeonGenerationStyleData styleData = dungeonRoom5.settings.StyleData;
								DungeonPitTrap dungeonPitTrap = new DungeonPitTrap(new DungeonPitTrapSettings
								{
									Style = styleData,
									Width = num17,
									Height = num18,
									EdgeWidth = 2,
									EdgeHeight = 2,
									TopDensity = 8,
									ConnectedRoom = dungeonRoom5,
									Flooded = flag2
								}, false);
								if (!dungeonRoom5.settings.StyleData.CanGenerateFeatureAt(data, dungeonRoom5, dungeonPitTrap, x, num16))
								{
									list4.Remove(dungeonRoom5);
								}
								else
								{
									if (dungeonPitTrap.GenerateFeature(data, x, num16))
									{
										DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonPitTrap);
										if (flag2 && num15 > 0)
										{
											num15--;
										}
										num14--;
									}
									else
									{
										num18 = Math.Max(10, (int)((float)dungeonRoom5.InnerBounds.Height * 0.5f));
										dungeonPitTrap = new DungeonPitTrap(new DungeonPitTrapSettings
										{
											Style = styleData,
											Width = num17,
											Height = num18,
											EdgeWidth = 2,
											EdgeHeight = 2,
											TopDensity = 8,
											ConnectedRoom = dungeonRoom5,
											Flooded = flag2
										}, false);
										if (!dungeonRoom5.settings.StyleData.CanGenerateFeatureAt(data, dungeonRoom5, dungeonPitTrap, x, num16))
										{
											list4.Remove(dungeonRoom5);
											continue;
										}
										if (dungeonPitTrap.GenerateFeature(data, x, num16))
										{
											DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonPitTrap);
											if (flag2 && num15 > 0)
											{
												num15--;
											}
											num14--;
										}
										else
										{
											num17 = (int)((float)(dungeonRoom5.InnerBounds.Width / 2) * 0.75f);
											dungeonPitTrap = new DungeonPitTrap(new DungeonPitTrapSettings
											{
												Style = styleData,
												Width = num17,
												Height = num18,
												EdgeWidth = 2,
												EdgeHeight = 2,
												TopDensity = 8,
												ConnectedRoom = dungeonRoom5,
												Flooded = flag2
											}, false);
											if (!dungeonRoom5.settings.StyleData.CanGenerateFeatureAt(data, dungeonRoom5, dungeonPitTrap, x, num16))
											{
												list4.Remove(dungeonRoom5);
												continue;
											}
											if (dungeonPitTrap.GenerateFeature(data, x, num16))
											{
												DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonPitTrap);
												if (flag2 && num15 > 0)
												{
													num15--;
												}
												num14--;
											}
										}
									}
									list4.Remove(dungeonRoom5);
								}
							}
						}
					}
				}
			}
			if (dictionary.ContainsKey(3))
			{
				List<DungeonRoom> list5 = dictionary[3].ToList<DungeonRoom>();
				while (list5.Count > 0 && num3 > 0)
				{
					DungeonRoom dungeonRoom6 = list5[genRand.Next(list5.Count)];
					int num19 = 20;
					while (num19 > 0 && num3 > 0)
					{
						num19--;
						int num20 = dungeonRoom6.InnerBounds.Left + genRand.Next(dungeonRoom6.InnerBounds.Width);
						int num21 = dungeonRoom6.InnerBounds.Top + genRand.Next(dungeonRoom6.InnerBounds.Height);
						if (WorldGen.InWorld(num20, num21, 25))
						{
							Tile tile3 = Main.tile[num20, num21];
							while (num21 < Main.UnderworldLayer - 10 && !tile3.active())
							{
								num21++;
								tile3 = Main.tile[num20, num21];
							}
							if (tile3.active() && tile3.type == DungeonGenerationStyles.Desert.BrickTileType)
							{
								DungeonDropTrap dungeonDropTrap = new DungeonDropTrap(new DungeonDropTrapSettings
								{
									StyleData = DungeonGenerationStyles.Desert,
									DropTrapType = ((genRand.Next(2) == 0) ? DungeonDropTrapType.Sand : DungeonDropTrapType.Lava)
								}, false);
								if (dungeonDropTrap.GenerateFeature(data, num20, num21))
								{
									DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonDropTrap);
									num3--;
								}
							}
						}
					}
					list5.Remove(dungeonRoom6);
				}
			}
			if (dictionary.ContainsKey(2))
			{
				List<DungeonRoom> list6 = dictionary[2].ToList<DungeonRoom>();
				while (list6.Count > 0 && num4 > 0)
				{
					DungeonRoom dungeonRoom7 = list6[genRand.Next(list6.Count)];
					int num22 = 20;
					while (num22 > 0 && num4 > 0)
					{
						num22--;
						int num23 = dungeonRoom7.InnerBounds.Left + genRand.Next(dungeonRoom7.InnerBounds.Width);
						int num24 = dungeonRoom7.InnerBounds.Top + genRand.Next(dungeonRoom7.InnerBounds.Height);
						if (WorldGen.InWorld(num23, num24, 25))
						{
							Tile tile4 = Main.tile[num23, num24];
							while (num24 < Main.UnderworldLayer - 10 && !tile4.active())
							{
								num24++;
								tile4 = Main.tile[num23, num24];
							}
							if (tile4.active() && tile4.type == DungeonGenerationStyles.Snow.BrickTileType)
							{
								DungeonDropTrap dungeonDropTrap2 = new DungeonDropTrap(new DungeonDropTrapSettings
								{
									StyleData = DungeonGenerationStyles.Snow,
									DropTrapType = DungeonDropTrapType.Slush
								}, false);
								if (dungeonDropTrap2.GenerateFeature(data, num23, num24))
								{
									DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonDropTrap2);
									num4--;
								}
							}
						}
					}
					list6.Remove(dungeonRoom7);
				}
			}
			if (dictionary.ContainsKey(1))
			{
				List<DungeonRoom> list7 = dictionary[1].ToList<DungeonRoom>();
				while (list7.Count > 0 && num5 > 0)
				{
					DungeonRoom dungeonRoom8 = list7[genRand.Next(list7.Count)];
					int num25 = 20;
					while (num25 > 0 && num5 > 0)
					{
						num25--;
						int num26 = dungeonRoom8.InnerBounds.Left + genRand.Next(dungeonRoom8.InnerBounds.Width);
						int num27 = dungeonRoom8.InnerBounds.Top + genRand.Next(dungeonRoom8.InnerBounds.Height);
						if (WorldGen.InWorld(num26, num27, 25))
						{
							Tile tile5 = Main.tile[num26, num27];
							while (num27 < Main.UnderworldLayer - 10 && !tile5.active())
							{
								num27++;
								tile5 = Main.tile[num26, num27];
							}
							if (tile5.active() && tile5.type == DungeonGenerationStyles.Cavern.BrickTileType)
							{
								DungeonDropTrap dungeonDropTrap3 = new DungeonDropTrap(new DungeonDropTrapSettings
								{
									StyleData = DungeonGenerationStyles.Cavern,
									DropTrapType = DungeonDropTrapType.Silt
								}, false);
								if (dungeonDropTrap3.GenerateFeature(data, num26, num27))
								{
									DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(dungeonDropTrap3);
									num5--;
								}
							}
						}
					}
					list7.Remove(dungeonRoom8);
				}
			}
			for (int m = 0; m < data.genVars.dungeonGenerationStyles.Count; m++)
			{
				DungeonGenerationStyleData dungeonGenerationStyleData3 = data.genVars.dungeonGenerationStyles[m];
				byte style3 = dungeonGenerationStyleData3.Style;
				DungeonBounds dungeonBounds2 = data.outerProgressionBounds[m];
				if (style3 == 3)
				{
					int num28 = 1000;
					int n = num7;
					while (n > 0)
					{
						num28--;
						if (num28 <= 0)
						{
							break;
						}
						int num29 = dungeonBounds2.Left + genRand.Next(dungeonBounds2.Width);
						int num30 = dungeonBounds2.Top + genRand.Next(dungeonBounds2.Height);
						Tile tile6 = Main.tile[num29, num30];
						if (tile6.wall == dungeonGenerationStyleData3.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile2 = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile6.type);
							if (styleForTile2 != null && styleForTile2.Style == 3)
							{
								new DungeonTileClump(new DungeonTileClumpSettings
								{
									RandomSeed = genRand.Next(),
									Strength = (double)(25 + genRand.Next(10)),
									Steps = 25 + genRand.Next(10),
									TileType = 53,
									WallType = 216,
									AreaToGenerateIn = null,
									OnlyReplaceThisTileType = new ushort?(styleForTile2.BrickTileType),
									OnlyReplaceThisWallType = new ushort?(styleForTile2.BrickWallType)
								}, false).GenerateFeature(data, num29, num30);
								n--;
							}
						}
					}
					num28 = 1000;
					n = num7;
					while (n > 0)
					{
						num28--;
						if (num28 <= 0)
						{
							break;
						}
						int num31 = dungeonBounds2.Left + genRand.Next(dungeonBounds2.Width);
						int num32 = dungeonBounds2.Top + genRand.Next(dungeonBounds2.Height);
						Tile tile7 = Main.tile[num31, num32];
						if (tile7.wall == dungeonGenerationStyleData3.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile3 = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile7.type);
							if (styleForTile3 != null && styleForTile3.Style == 3)
							{
								new DungeonTileClump(new DungeonTileClumpSettings
								{
									RandomSeed = genRand.Next(),
									Strength = (double)(25 + genRand.Next(10)),
									Steps = 25 + genRand.Next(10),
									TileType = 397,
									WallType = 216,
									AreaToGenerateIn = null,
									OnlyReplaceThisTileType = new ushort?(styleForTile3.BrickTileType),
									OnlyReplaceThisWallType = new ushort?(styleForTile3.BrickWallType)
								}, false).GenerateFeature(data, num31, num32);
								n--;
							}
						}
					}
					num28 = 1000;
					n = num7 * 2;
					while (n > 0)
					{
						num28--;
						if (num28 <= 0)
						{
							break;
						}
						int num33 = dungeonBounds2.Left + genRand.Next(dungeonBounds2.Width);
						int num34 = dungeonBounds2.Top + genRand.Next(dungeonBounds2.Height);
						Tile tile8 = Main.tile[num33, num34];
						if (tile8.wall == dungeonGenerationStyleData3.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile4 = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile8.type);
							if (styleForTile4 != null && styleForTile4.Style == 3)
							{
								new DungeonTileClump(new DungeonTileClumpSettings
								{
									RandomSeed = genRand.Next(),
									Strength = (double)(15 + genRand.Next(5)),
									Steps = 15 + genRand.Next(5),
									TileType = 404,
									WallType = 223,
									AreaToGenerateIn = null,
									OnlyReplaceThisTileType = new ushort?(styleForTile4.BrickTileType),
									OnlyReplaceThisWallType = new ushort?(styleForTile4.BrickWallType)
								}, false).GenerateFeature(data, num33, num34);
								n--;
							}
						}
					}
				}
				if (style3 == 2)
				{
					int num35 = 1000;
					int num36 = num8;
					while (num36 > 0)
					{
						num35--;
						if (num35 <= 0)
						{
							break;
						}
						int num37 = dungeonBounds2.Left + genRand.Next(dungeonBounds2.Width);
						int num38 = dungeonBounds2.Top + genRand.Next(dungeonBounds2.Height);
						Tile tile9 = Main.tile[num37, num38];
						if (tile9.wall == dungeonGenerationStyleData3.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile5 = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile9.type);
							if (styleForTile5 != null && styleForTile5.Style == 2)
							{
								new DungeonTileClump(new DungeonTileClumpSettings
								{
									RandomSeed = genRand.Next(),
									Strength = (double)(25 + genRand.Next(10)),
									Steps = 25 + genRand.Next(10),
									TileType = 147,
									WallType = 40,
									AreaToGenerateIn = null,
									OnlyReplaceThisTileType = new ushort?(styleForTile5.BrickTileType),
									OnlyReplaceThisWallType = new ushort?(styleForTile5.BrickWallType)
								}, false).GenerateFeature(data, num37, num38);
								num36--;
							}
						}
					}
					num35 = 1000;
					num36 = num8;
					while (num36 > 0)
					{
						num35--;
						if (num35 <= 0)
						{
							break;
						}
						int num39 = dungeonBounds2.Left + genRand.Next(dungeonBounds2.Width);
						int num40 = dungeonBounds2.Top + genRand.Next(dungeonBounds2.Height);
						Tile tile10 = Main.tile[num39, num40];
						if (tile10.wall == dungeonGenerationStyleData3.BrickWallType)
						{
							DungeonGenerationStyleData styleForTile6 = DungeonGenerationStyles.GetStyleForTile(data.genVars.dungeonGenerationStyles, (int)tile10.type);
							if (styleForTile6 != null && styleForTile6.Style == 2)
							{
								new DungeonTileClump(new DungeonTileClumpSettings
								{
									RandomSeed = genRand.Next(),
									Strength = (double)(25 + genRand.Next(10)),
									Steps = 25 + genRand.Next(10),
									TileType = 224,
									WallType = 40,
									AreaToGenerateIn = null,
									OnlyReplaceThisTileType = new ushort?(styleForTile6.BrickTileType),
									OnlyReplaceThisWallType = new ushort?(styleForTile6.BrickWallType)
								}, false).GenerateFeature(data, num39, num40);
								num36--;
							}
						}
					}
				}
			}
			for (int num41 = 0; num41 < data.dungeonRooms.Count; num41++)
			{
				data.dungeonRooms[num41].GenerateEarlyDungeonFeaturesInRoom(data);
			}
			if (biomeDungeonRoom != null)
			{
				int x2 = biomeDungeonRoom.InnerBounds.Center.X;
				int num42 = (biomeDungeonRoom.InnerBounds.Top + biomeDungeonRoom.InnerBounds.Center.Y) / 2;
				Tile tile11 = Main.tile[x2, num42];
				while (!tile11.active())
				{
					num42++;
					tile11 = Main.tile[x2, num42];
				}
				for (int num43 = -1; num43 <= 1; num43++)
				{
					int num44 = x2 + num43;
					int num45 = num42;
					Tile tile12 = Main.tile[num44, num45];
					while (!tile12.active())
					{
						tile12.ClearTile();
						tile12.active(true);
						tile12.type = 226;
						num45++;
						tile12 = Main.tile[num44, num45];
					}
				}
				WorldGen.AddLihzahrdAltar(x2 - 1, num42 - 2);
			}
			if (data.Type == DungeonType.Default)
			{
				num6 = (int)((double)Main.maxTilesX * 2.0 * data.dungeonStepScalar);
				for (int num46 = 0; num46 < num6; num46++)
				{
					int num47 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
					int num48 = data.dungeonBounds.Top;
					if (num48 < Main.dungeonY + 25)
					{
						num48 = Main.dungeonY + 25;
					}
					if ((double)num48 < Main.worldSurface)
					{
						num48 = (int)Main.worldSurface;
					}
					int num49 = genRand.Next(num48, data.dungeonBounds.Bottom);
					bool flag3 = data.makeNextPitTrapFlooded || genRand.Next(8) == 0;
					int num50 = genRand.Next(6, 10);
					if (new DungeonPitTrap(new DungeonPitTrapSettings
					{
						Style = data.genVars.dungeonStyle,
						Width = genRand.Next(8, 19),
						Height = genRand.Next(19, 46),
						EdgeWidth = genRand.Next(6, 10),
						EdgeHeight = num50,
						TopDensity = num50,
						Flooded = flag3
					}, true).GenerateFeature(data, num47, num49))
					{
						if (flag3)
						{
							data.makeNextPitTrapFlooded = false;
						}
						num46 += 1500;
					}
					else
					{
						num46++;
					}
				}
			}
		}
	}
}
