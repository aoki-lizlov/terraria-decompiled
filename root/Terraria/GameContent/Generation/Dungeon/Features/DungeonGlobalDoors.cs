using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004D9 RID: 1241
	public class DungeonGlobalDoors : GlobalDungeonFeature
	{
		// Token: 0x060034F1 RID: 13553 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalDoors(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x0060C0D8 File Offset: 0x0060A2D8
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.Doors(data);
			this.generated = true;
			return true;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x0060C0F0 File Offset: 0x0060A2F0
		public void Doors(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			ushort brickTileType = data.genVars.brickTileType;
			ushort brickWallType = data.genVars.brickWallType;
			PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[data.doorItemType];
			for (int i = 0; i < data.dungeonDoorData.Count; i++)
			{
				DungeonDoorData dungeonDoorData = data.dungeonDoorData[i];
				if (WorldGen.InWorld(dungeonDoorData.Position, 30))
				{
					ushort num = brickTileType;
					if (dungeonDoorData.OverrideBrickTileType != null)
					{
						num = dungeonDoorData.OverrideBrickTileType.Value;
					}
					ushort num2 = brickWallType;
					if (dungeonDoorData.OverrideBrickWallType != null)
					{
						num2 = dungeonDoorData.OverrideBrickWallType.Value;
					}
					int num3 = 13;
					if (genRand.Next(3) == 0)
					{
						num3 = (int)placementDetails.tileStyle;
					}
					if (dungeonDoorData.OverrideStyle != null)
					{
						num3 = dungeonDoorData.OverrideStyle.Value;
					}
					int num4 = 20;
					int num5 = num4 + 5;
					int num6 = 10;
					if (dungeonDoorData.OverrideWidthFluff != null)
					{
						num6 = dungeonDoorData.OverrideWidthFluff.Value;
					}
					int num7 = Math.Max(num5, Math.Min(Main.maxTilesX - num5, dungeonDoorData.Position.X - num6));
					int num8 = Math.Max(num5, Math.Min(Main.maxTilesX - num5, Math.Max(num7, dungeonDoorData.Position.X + num6 - 1)));
					int num9 = 100;
					int num10 = 0;
					for (int j = num7; j <= num8; j++)
					{
						bool flag = true;
						int num11 = dungeonDoorData.Position.Y;
						while (num11 > 10 && !Main.tile[j, num11].active())
						{
							num11--;
						}
						if (!DungeonUtils.IsConsideredDungeonTile((int)Main.tile[j, num11].type, false))
						{
							flag = false;
						}
						int num12 = num11;
						num11 = dungeonDoorData.Position.Y;
						while (!Main.tile[j, num11].active())
						{
							num11++;
						}
						if (!DungeonUtils.IsConsideredDungeonTile((int)Main.tile[j, num11].type, false))
						{
							flag = false;
						}
						int num13 = num11;
						if (num13 - num12 >= 3)
						{
							if (!dungeonDoorData.SkipOtherDoorsCheck)
							{
								int num14 = j - 20;
								int num15 = j + 20;
								int num16 = num13 - 10;
								int num17 = num13 + 10;
								for (int k = num14; k < num15; k++)
								{
									for (int l = num16; l < num17; l++)
									{
										if (Main.tile[k, l].active() && Main.tile[k, l].type == 10)
										{
											flag = false;
											break;
										}
									}
								}
							}
							if (flag && !dungeonDoorData.SkipSpaceCheck)
							{
								for (int m = num13 - 3; m < num13; m++)
								{
									for (int n = j - 3; n <= j + 3; n++)
									{
										if (Main.tile[n, m].active())
										{
											flag = false;
											break;
										}
									}
								}
							}
							if (flag && num13 - num12 < num4)
							{
								bool flag2 = false;
								if (dungeonDoorData.Direction == 0 && num13 - num12 < num9)
								{
									flag2 = true;
								}
								if (dungeonDoorData.Direction == -1 && j > num10)
								{
									flag2 = true;
								}
								if (dungeonDoorData.Direction == 1 && (j < num10 || num10 == 0))
								{
									flag2 = true;
								}
								if (flag2)
								{
									num10 = j;
									num9 = num13 - num12;
								}
							}
						}
					}
					if (num9 < num4)
					{
						int num18 = num10;
						int num19 = dungeonDoorData.Position.Y;
						int num20 = num19;
						while (!Main.tile[num18, num19].active())
						{
							Main.tile[num18, num19].active(false);
							num19++;
						}
						while (!Main.tile[num18, num20].active())
						{
							num20--;
						}
						num19--;
						num20++;
						for (int num21 = num20; num21 < num19 - 2; num21++)
						{
							Main.tile[num18, num21].Clear(TileDataType.Slope);
							Main.tile[num18, num21].active(true);
							Main.tile[num18, num21].type = num;
							if (Main.tile[num18 - 1, num21].active() && WorldGen.CanKillTile(num18 - 1, num21))
							{
								Main.tile[num18 - 1, num21].active(false);
								Main.tile[num18 - 1, num21].ClearEverything();
								Main.tile[num18 - 1, num21].wall = num2;
							}
							if (Main.tile[num18 - 2, num21].active() && WorldGen.CanKillTile(num18 - 2, num21))
							{
								Main.tile[num18 - 2, num21].active(false);
								Main.tile[num18 - 2, num21].ClearEverything();
								Main.tile[num18 - 2, num21].wall = num2;
							}
							if (Main.tile[num18 + 1, num21].active() && WorldGen.CanKillTile(num18 + 1, num21))
							{
								Main.tile[num18 + 1, num21].active(false);
								Main.tile[num18 + 1, num21].ClearEverything();
								Main.tile[num18 + 1, num21].wall = num2;
							}
							if (Main.tile[num18 + 2, num21].active() && WorldGen.CanKillTile(num18 + 2, num21))
							{
								Main.tile[num18 + 2, num21].active(false);
								Main.tile[num18 + 2, num21].ClearEverything();
								Main.tile[num18 + 2, num21].wall = num2;
							}
						}
						WorldGen.PlaceTile(num18, num19, 10, true, false, -1, num3);
						num18--;
						int num22 = num19 - 3;
						while (!Main.tile[num18, num22].active())
						{
							num22--;
						}
						bool flag3 = num19 - num22 < num19 - num20 + 5 && DungeonUtils.IsConsideredDungeonTile((int)Main.tile[num18, num22].type, false);
						if (dungeonDoorData.AlwaysClearArea || flag3)
						{
							for (int num23 = num19 - 4 - genRand.Next(3); num23 > num22; num23--)
							{
								if (flag3)
								{
									Main.tile[num18, num23].Clear(TileDataType.Slope);
									Main.tile[num18, num23].active(true);
									Main.tile[num18, num23].type = num;
								}
								if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 - 1, num23].type == num)
								{
									Main.tile[num18 - 1, num23].active(false);
									Main.tile[num18 - 1, num23].ClearEverything();
									Main.tile[num18 - 1, num23].wall = num2;
								}
								if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 - 2, num23].type == num)
								{
									Main.tile[num18 - 2, num23].active(false);
									Main.tile[num18 - 2, num23].ClearEverything();
									Main.tile[num18 - 2, num23].wall = num2;
								}
							}
						}
						num18 += 2;
						num22 = num19 - 3;
						while (!Main.tile[num18, num22].active())
						{
							num22--;
						}
						flag3 = num19 - num22 < num19 - num20 + 5 && DungeonUtils.IsConsideredDungeonTile((int)Main.tile[num18, num22].type, false);
						if (dungeonDoorData.AlwaysClearArea || flag3)
						{
							for (int num24 = num19 - 4 - genRand.Next(3); num24 > num22; num24--)
							{
								if (flag3)
								{
									Main.tile[num18, num24].active(true);
									Main.tile[num18, num24].Clear(TileDataType.Slope);
									Main.tile[num18, num24].type = num;
								}
								if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 + 1, num24].type == num)
								{
									Main.tile[num18 + 1, num24].active(false);
									Main.tile[num18 + 1, num24].ClearEverything();
									Main.tile[num18 + 1, num24].wall = num2;
								}
								if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 + 2, num24].type == num)
								{
									Main.tile[num18 + 2, num24].active(false);
									Main.tile[num18 + 2, num24].ClearEverything();
									Main.tile[num18 + 2, num24].wall = num2;
								}
							}
						}
						num19++;
						num18--;
						for (int num25 = num19 - 8; num25 < num19; num25++)
						{
							if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 + 2, num25].type == num)
							{
								Main.tile[num18 + 2, num25].active(false);
								Main.tile[num18 + 2, num25].ClearEverything();
								Main.tile[num18 + 2, num25].wall = num2;
							}
							if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 + 3, num25].type == num)
							{
								Main.tile[num18 + 3, num25].active(false);
								Main.tile[num18 + 3, num25].ClearEverything();
								Main.tile[num18 + 3, num25].wall = num2;
							}
							if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 - 2, num25].type == num)
							{
								Main.tile[num18 - 2, num25].active(false);
								Main.tile[num18 - 2, num25].ClearEverything();
								Main.tile[num18 - 2, num25].wall = num2;
							}
							if (dungeonDoorData.AlwaysClearArea || Main.tile[num18 - 3, num25].type == num)
							{
								Main.tile[num18 - 3, num25].active(false);
								Main.tile[num18 - 3, num25].ClearEverything();
								Main.tile[num18 - 3, num25].wall = num2;
							}
						}
						Main.tile[num18 - 1, num19].active(true);
						Main.tile[num18 - 1, num19].type = num;
						Main.tile[num18 - 1, num19].Clear(TileDataType.Slope);
						Main.tile[num18 + 1, num19].active(true);
						Main.tile[num18 + 1, num19].type = num;
						Main.tile[num18 + 1, num19].Clear(TileDataType.Slope);
					}
				}
			}
		}
	}
}
