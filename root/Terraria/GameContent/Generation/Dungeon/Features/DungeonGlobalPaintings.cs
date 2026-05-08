using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004DF RID: 1247
	public class DungeonGlobalPaintings : GlobalDungeonFeature
	{
		// Token: 0x06003508 RID: 13576 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalPaintings(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x0060FA23 File Offset: 0x0060DC23
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.Paintings(data);
			this.generated = true;
			return true;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x0060FA3C File Offset: 0x0060DC3C
		public void Paintings(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			float num = (float)Main.maxTilesX / 4200f;
			DungeonGlobalPaintings.lihzahrdPaintingsPlaced = 0;
			switch (WorldGen.GetWorldSize())
			{
			default:
				DungeonGlobalPaintings.lihzahrdPaintingsMax = 1;
				break;
			case 1:
				DungeonGlobalPaintings.lihzahrdPaintingsMax = 2;
				break;
			case 2:
				DungeonGlobalPaintings.lihzahrdPaintingsMax = 2 + genRand.Next(2);
				break;
			}
			int num2 = data.wallVariants[0];
			double num3 = Math.Max(1.0, data.globalFeatureScalar * 0.75);
			int num4 = (int)((double)(100f * num) * num3);
			int num5 = num4 * 3;
			int i = 0;
			while (i < num4)
			{
				num5--;
				if (num5 <= 0)
				{
					break;
				}
				int num6 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
				int num7 = genRand.Next((int)Main.worldSurface, data.dungeonBounds.Bottom);
				int num8 = 1000;
				while (!DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num6, num7].wall, false) || Main.tile[num6, num7].active())
				{
					num8--;
					if (num8 <= 0)
					{
						break;
					}
					num6 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
					num7 = genRand.Next((int)Main.worldSurface, data.dungeonBounds.Bottom);
				}
				if (!WorldGen.InWorld(num6, num7, 5) || Main.tile[num6, num7] == null)
				{
					goto IL_01BB;
				}
				DungeonGenerationStyleData styleForWall = DungeonGenerationStyles.GetStyleForWall(data.genVars.dungeonGenerationStyles, (int)Main.tile[num6, num7].wall);
				if (styleForWall == null || styleForWall.Style != 10 || DungeonGlobalPaintings.lihzahrdPaintingsPlaced < DungeonGlobalPaintings.lihzahrdPaintingsMax)
				{
					goto IL_01BB;
				}
				i--;
				IL_0B4D:
				i++;
				continue;
				IL_01BB:
				int num9;
				int num10;
				int num11;
				int num12;
				for (int j = 0; j < 2; j++)
				{
					num9 = num6;
					num10 = num6;
					while (num9 > 20 && !Main.tile[num9, num7].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num9, num7].wall, false))
					{
						num9--;
					}
					num9++;
					while (num10 < Main.maxTilesX - 20 && !Main.tile[num10, num7].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num10, num7].wall, false))
					{
						num10++;
					}
					num10--;
					num6 = (num9 + num10) / 2;
					num11 = num7;
					num12 = num7;
					while (num11 > 20 && !Main.tile[num6, num11].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num6, num11].wall, false))
					{
						num11--;
					}
					num11++;
					while (num12 < Main.maxTilesY - 20 && !Main.tile[num6, num12].active() && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num6, num12].wall, false))
					{
						num12++;
					}
					num12--;
					num7 = (num11 + num12) / 2;
				}
				num9 = num6;
				num10 = num6;
				while (num9 > 20 && !Main.tile[num9, num7].active() && !Main.tile[num9, num7 - 1].active() && !Main.tile[num9, num7 + 1].active())
				{
					num9--;
				}
				num9++;
				while (num10 < Main.maxTilesX - 20 && !Main.tile[num10, num7].active() && !Main.tile[num10, num7 - 1].active() && !Main.tile[num10, num7 + 1].active())
				{
					num10++;
				}
				num10--;
				num11 = num7;
				num12 = num7;
				while (num11 > 20 && !Main.tile[num6, num11].active() && !Main.tile[num6 - 1, num11].active() && !Main.tile[num6 + 1, num11].active())
				{
					num11--;
				}
				num11++;
				while (num12 < Main.maxTilesY - 20 && !Main.tile[num6, num12].active() && !Main.tile[num6 - 1, num12].active() && !Main.tile[num6 + 1, num12].active())
				{
					num12++;
				}
				num12--;
				num6 = (num9 + num10) / 2;
				num7 = (num11 + num12) / 2;
				int num13 = num10 - num9;
				int num14 = num12 - num11;
				if (num13 <= 7 || num14 <= 5)
				{
					goto IL_0B4D;
				}
				bool[] array = new bool[3];
				array[0] = true;
				if (num13 > num14 * 3 && num13 > 21)
				{
					array[1] = true;
				}
				if (num14 > num13 * 3 && num14 > 21)
				{
					array[2] = true;
				}
				int num15 = genRand.Next(3);
				if ((int)Main.tile[num6, num7].wall == num2)
				{
					num15 = 0;
				}
				while (!array[num15])
				{
					num15 = genRand.Next(3);
				}
				if (WorldGen.nearPicture2(num6, num7))
				{
					num15 = -1;
				}
				if (num15 == 0)
				{
					PaintingEntry paintingEntry = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num6, num7].wall);
					new DungeonBounds();
					if (data.CanGenerateFeatureInArea(this, num6, num7, 3) && !WorldGen.nearPicture(num6, num7))
					{
						DungeonGlobalPaintings.Paintings_PlacePainting(num6, num7, paintingEntry);
						goto IL_0B4D;
					}
					goto IL_0B4D;
				}
				else if (num15 == 1)
				{
					PaintingEntry paintingEntry2 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num6, num7].wall);
					if (!data.CanGenerateFeatureInArea(this, num6, num7, 3))
					{
						goto IL_0B4D;
					}
					if (!Main.tile[num6, num7].active())
					{
						DungeonGlobalPaintings.Paintings_PlacePainting(num6, num7, paintingEntry2);
					}
					if (!Main.tile[num6, num7].active())
					{
						int num16 = num6;
						int num17 = num7;
						int num18 = num7;
						for (int k = 0; k < 2; k++)
						{
							num6 += 7;
							num11 = num18;
							num12 = num18;
							while (num11 > 0 && !Main.tile[num6, num11].active() && !Main.tile[num6 - 1, num11].active() && !Main.tile[num6 + 1, num11].active())
							{
								num11--;
							}
							num11++;
							while (num12 < Main.maxTilesY - 1 && !Main.tile[num6, num12].active() && !Main.tile[num6 - 1, num12].active() && !Main.tile[num6 + 1, num12].active())
							{
								num12++;
							}
							num12--;
							num18 = (num11 + num12) / 2;
							if (data.CanGenerateFeatureInArea(this, num6, num18, 3))
							{
								paintingEntry2 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num6, num18].wall);
								if (Math.Abs(num17 - num18) >= 4 || WorldGen.nearPicture(num6, num18))
								{
									break;
								}
								DungeonGlobalPaintings.Paintings_PlacePainting(num6, num18, paintingEntry2);
							}
						}
						num18 = num7;
						num6 = num16;
						for (int l = 0; l < 2; l++)
						{
							num6 -= 7;
							num11 = num18;
							num12 = num18;
							while (num11 > 0 && !Main.tile[num6, num11].active() && !Main.tile[num6 - 1, num11].active() && !Main.tile[num6 + 1, num11].active())
							{
								num11--;
							}
							num11++;
							while (num12 < Main.maxTilesY - 1 && !Main.tile[num6, num12].active() && !Main.tile[num6 - 1, num12].active() && !Main.tile[num6 + 1, num12].active())
							{
								num12++;
							}
							num12--;
							num18 = (num11 + num12) / 2;
							if (data.CanGenerateFeatureInArea(this, num6, num18, 3))
							{
								paintingEntry2 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num6, num18].wall);
								if (Math.Abs(num17 - num18) >= 4 || WorldGen.nearPicture(num6, num18))
								{
									break;
								}
								DungeonGlobalPaintings.Paintings_PlacePainting(num6, num18, paintingEntry2);
							}
						}
						goto IL_0B4D;
					}
					goto IL_0B4D;
				}
				else
				{
					if (num15 != 2)
					{
						goto IL_0B4D;
					}
					PaintingEntry paintingEntry3 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num6, num7].wall);
					if (!data.CanGenerateFeatureInArea(this, num6, num7, 3))
					{
						goto IL_0B4D;
					}
					if (!Main.tile[num6, num7].active())
					{
						DungeonGlobalPaintings.Paintings_PlacePainting(num6, num7, paintingEntry3);
					}
					if (!Main.tile[num6, num7].active())
					{
						int num19 = num7;
						int num20 = num6;
						int num21 = num6;
						for (int m = 0; m < 3; m++)
						{
							num7 += 7;
							num9 = num21;
							num10 = num21;
							while (num9 > 0 && !Main.tile[num9, num7].active() && !Main.tile[num9, num7 - 1].active() && !Main.tile[num9, num7 + 1].active())
							{
								num9--;
							}
							num9++;
							while (num10 < Main.maxTilesX - 1 && !Main.tile[num10, num7].active() && !Main.tile[num10, num7 - 1].active() && !Main.tile[num10, num7 + 1].active())
							{
								num10++;
							}
							num10--;
							num21 = (num9 + num10) / 2;
							if (data.CanGenerateFeatureInArea(this, num21, num7, 3))
							{
								paintingEntry3 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num21, num7].wall);
								if (Math.Abs(num20 - num21) >= 4 || WorldGen.nearPicture(num21, num7))
								{
									break;
								}
								DungeonGlobalPaintings.Paintings_PlacePainting(num21, num7, paintingEntry3);
							}
						}
						num21 = num6;
						num7 = num19;
						for (int n = 0; n < 3; n++)
						{
							num7 -= 7;
							num9 = num21;
							num10 = num21;
							while (num9 > 0 && !Main.tile[num9, num7].active() && !Main.tile[num9, num7 - 1].active() && !Main.tile[num9, num7 + 1].active())
							{
								num9--;
							}
							num9++;
							while (num10 < Main.maxTilesX - 1 && !Main.tile[num10, num7].active() && !Main.tile[num10, num7 - 1].active() && !Main.tile[num10, num7 + 1].active())
							{
								num10++;
							}
							num10--;
							num21 = (num9 + num10) / 2;
							if (data.CanGenerateFeatureInArea(this, num21, num7, 3))
							{
								paintingEntry3 = DungeonGlobalPaintings.Paintings_GetPaintingEntry(data, (int)Main.tile[num21, num7].wall);
								if (Math.Abs(num20 - num21) >= 4 || WorldGen.nearPicture(num21, num7))
								{
									break;
								}
								DungeonGlobalPaintings.Paintings_PlacePainting(num21, num7, paintingEntry3);
							}
						}
						goto IL_0B4D;
					}
					goto IL_0B4D;
				}
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x006105A5 File Offset: 0x0060E7A5
		private static void Paintings_PlacePainting(int x, int y, PaintingEntry entry)
		{
			WorldGen.PlaceTile(x, y, entry.tileType, true, false, -1, entry.style);
			if (Main.tile[x, y].wall == 87)
			{
				DungeonGlobalPaintings.lihzahrdPaintingsPlaced++;
			}
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x006105E0 File Offset: 0x0060E7E0
		private static PaintingEntry Paintings_GetPaintingEntry(DungeonData data, int currentWall)
		{
			int num = data.wallVariants[0];
			DungeonGenerationStyleData styleForWall = DungeonGenerationStyles.GetStyleForWall(data.genVars.dungeonGenerationStyles, currentWall);
			int num2 = (int)((styleForWall == null) ? 0 : styleForWall.Style);
			switch (num2)
			{
			case 0:
				if (currentWall != num)
				{
					return DungeonGlobalPaintings.Paintings_RandomBonePainting();
				}
				return DungeonGlobalPaintings.Paintings_RandomDungeonPainting();
			case 1:
			case 2:
				break;
			case 3:
				return WorldGen.RandHousePictureDesert();
			case 4:
			case 5:
				return DungeonGlobalPaintings.Paintings_RandomBonePainting();
			default:
				if (num2 == 10)
				{
					PlacementDetails placementDetails = ItemID.Sets.DerivedPlacementDetails[5230];
					return new PaintingEntry
					{
						tileType = placementDetails.tileType,
						style = (int)placementDetails.tileStyle
					};
				}
				break;
			}
			return WorldGen.RandHousePicture();
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x00610690 File Offset: 0x0060E890
		private static PaintingEntry Paintings_RandomDungeonPainting()
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = genRand.Next(3);
			int num2 = 0;
			if (num <= 1)
			{
				int num3 = 7;
				num = 240;
				num2 = genRand.Next(num3);
				if (num2 == 6)
				{
					num2 = genRand.Next(num3);
				}
				if (num2 == 0)
				{
					num2 = 12;
				}
				else if (num2 == 1)
				{
					num2 = 13;
				}
				else if (num2 == 2)
				{
					num2 = 14;
				}
				else if (num2 == 3)
				{
					num2 = 15;
				}
				else if (num2 == 4)
				{
					num2 = 18;
				}
				else if (num2 == 5)
				{
					num2 = 19;
				}
				else if (num2 == 6)
				{
					num2 = 23;
				}
			}
			else if (num == 2)
			{
				num = 242;
				int num4 = 17;
				num2 = genRand.Next(num4);
				if (num2 > 13)
				{
					if (num2 == 14)
					{
						num2 = 15;
					}
					else if (num2 == 15)
					{
						num2 = 16;
					}
					else if (num2 == 16)
					{
						num2 = 30;
					}
				}
			}
			return new PaintingEntry
			{
				tileType = num,
				style = num2
			};
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x00610760 File Offset: 0x0060E960
		private static PaintingEntry Paintings_RandomBonePainting()
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = genRand.Next(2);
			int num2 = 0;
			if (num == 0)
			{
				num = 240;
				num2 = genRand.Next(2);
				if (num2 == 0)
				{
					num2 = 16;
				}
				else if (num2 == 1)
				{
					num2 = 17;
				}
			}
			else if (num == 1)
			{
				num = 241;
				num2 = genRand.Next(9);
			}
			return new PaintingEntry
			{
				tileType = num,
				style = num2
			};
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x006107CB File Offset: 0x0060E9CB
		// Note: this type is marked as 'beforefieldinit'.
		static DungeonGlobalPaintings()
		{
		}

		// Token: 0x04005A98 RID: 23192
		public static int lihzahrdPaintingsPlaced = 0;

		// Token: 0x04005A99 RID: 23193
		public static int lihzahrdPaintingsMax = 1;
	}
}
