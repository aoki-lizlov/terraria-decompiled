using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004DA RID: 1242
	public class DungeonGlobalPlatforms : GlobalDungeonFeature
	{
		// Token: 0x060034F4 RID: 13556 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalPlatforms(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x0060CC39 File Offset: 0x0060AE39
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.Platforms(data);
			this.generated = true;
			return true;
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x0060CC54 File Offset: 0x0060AE54
		public void Platforms(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[data.platformItemType];
			for (int i = 0; i < data.dungeonPlatformData.Count; i++)
			{
				DungeonPlatformData dungeonPlatformData = data.dungeonPlatformData[i];
				if (WorldGen.InWorld(dungeonPlatformData.Position, 30))
				{
					int num = (int)placementDetails.tileStyle;
					if (dungeonPlatformData.OverrideStyle != null)
					{
						int? overrideStyle = dungeonPlatformData.OverrideStyle;
						int num2 = 0;
						if ((overrideStyle.GetValueOrDefault() >= num2) & (overrideStyle != null))
						{
							num = dungeonPlatformData.OverrideStyle.Value;
						}
					}
					int x = dungeonPlatformData.Position.X;
					int y = dungeonPlatformData.Position.Y;
					int num3 = -1;
					bool forcePlacement = dungeonPlatformData.ForcePlacement;
					int num4 = 5;
					int num5 = 10;
					if ((double)y < Main.worldSurface + 50.0)
					{
						num5 = 20;
					}
					if (dungeonPlatformData.OverrideMaxLengthAllowed > 0)
					{
						num5 = dungeonPlatformData.OverrideMaxLengthAllowed;
					}
					if (dungeonPlatformData.OverrideHeightFluff != null)
					{
						num4 = dungeonPlatformData.OverrideHeightFluff.Value;
					}
					double num6 = (dungeonPlatformData.InAHallway ? data.HallSizeScalar : data.RoomSizeScalar);
					num5 = (int)((double)num5 * num6);
					for (int j = y - num4; j <= y + num4; j++)
					{
						int num7 = x;
						int num8 = x;
						bool flag = false;
						if (forcePlacement || !Main.tile[num7, j].active())
						{
							while (!Main.tile[num7, j].active())
							{
								num7--;
								if (!forcePlacement && ((Main.tile[num7, j].active() && !DungeonUtils.IsConsideredDungeonTile((int)Main.tile[num7, j].type, false)) || num7 == 0))
								{
									flag = true;
								}
								else if (dungeonPlatformData.canPlaceHereCallback != null && !dungeonPlatformData.canPlaceHereCallback(data, num7, j))
								{
									flag = true;
								}
								else if (num7 > 10)
								{
									continue;
								}
								IL_0251:
								while (!Main.tile[num8, j].active())
								{
									num8++;
									if (!forcePlacement && ((Main.tile[num8, j].active() && !DungeonUtils.IsConsideredDungeonTile((int)Main.tile[num8, j].type, false)) || num8 == Main.maxTilesX - 1))
									{
										flag = true;
										break;
									}
									if (dungeonPlatformData.canPlaceHereCallback != null && !dungeonPlatformData.canPlaceHereCallback(data, num8, j))
									{
										flag = true;
										break;
									}
									if (num8 >= Main.maxTilesX - 10)
									{
										break;
									}
								}
								goto IL_0269;
							}
							goto IL_0251;
						}
						flag = true;
						IL_0269:
						if (!flag && (forcePlacement || num8 - num7 <= num5))
						{
							bool flag2 = true;
							int num9 = Math.Max(0, x - num5 / 2 - 2);
							int num10 = Math.Min(Main.maxTilesX - 1, x + num5 / 2 + 2);
							int num11 = j - num4;
							int num12 = j + num4;
							if (!forcePlacement)
							{
								if (!dungeonPlatformData.SkipOtherPlatformsCheck)
								{
									for (int k = num9; k <= num10; k++)
									{
										for (int l = num11; l <= num12; l++)
										{
											if (Main.tile[k, l].active() && Main.tile[k, l].type == 19)
											{
												flag2 = false;
												break;
											}
										}
									}
								}
								if (!dungeonPlatformData.SkipSpaceCheck)
								{
									for (int m = j + 3; m >= j - 5; m--)
									{
										if (Main.tile[x, m].active())
										{
											flag2 = false;
											break;
										}
									}
								}
							}
							if (flag2)
							{
								num3 = j;
								break;
							}
						}
					}
					if ((forcePlacement && num3 > 0) || (num3 > y - num4 - 5 && num3 < y + num4 + 5))
					{
						int num13 = x;
						int num14 = num3;
						int num15 = x + 1;
						while (!Main.tile[num13, num14].active())
						{
							Main.tile[num13, num14].active(true);
							Main.tile[num13, num14].type = 19;
							Main.tile[num13, num14].Clear(TileDataType.Slope);
							Main.tile[num13, num14].frameY = (short)(18 * num);
							WorldGen.TileFrame(num13, num14, false, false);
							num13--;
							if (num13 <= 10)
							{
								IL_04AB:
								while (!Main.tile[num15, num14].active())
								{
									Main.tile[num15, num14].active(true);
									Main.tile[num15, num14].type = 19;
									Main.tile[num15, num14].Clear(TileDataType.Slope);
									Main.tile[num15, num14].frameY = (short)(18 * num);
									WorldGen.TileFrame(num15, num14, false, false);
									num15++;
									if (num15 >= Main.maxTilesX - 10)
									{
										break;
									}
								}
								if (dungeonPlatformData.IsAShelf)
								{
									for (int n = num13; n < num15; n++)
									{
										if (dungeonPlatformData.PlaceWaterCandlesChance > 0.0 && genRand.NextDouble() < dungeonPlatformData.PlaceWaterCandlesChance)
										{
											DungeonUtils.GenerateDungeonWaterCandle(n, num14 - 1);
										}
										else if (dungeonPlatformData.PlacePotsChance > 0.0 && genRand.NextDouble() < dungeonPlatformData.PlacePotsChance)
										{
											DungeonUtils.GenerateDungeonPot(n, num14 - 1);
										}
										else if (dungeonPlatformData.PlacePotionBottlesChance > 0.0 && genRand.NextDouble() < dungeonPlatformData.PlacePotionBottlesChance)
										{
											DungeonUtils.GenerateDungeonPotionBottle(n, num14 - 1);
										}
										else if (dungeonPlatformData.PlaceBooksChance > 0.0 && genRand.NextDouble() < dungeonPlatformData.PlaceBooksChance)
										{
											if (dungeonPlatformData.NoWaterbolt)
											{
												DungeonUtils.GenerateDungeonBook(n, num14 - 1, false);
											}
											else
											{
												DungeonUtils.GenerateDungeonBook(n, num14 - 1);
											}
										}
									}
									goto IL_05AE;
								}
								goto IL_05AE;
							}
						}
						goto IL_04AB;
					}
				}
				IL_05AE:;
			}
		}
	}
}
