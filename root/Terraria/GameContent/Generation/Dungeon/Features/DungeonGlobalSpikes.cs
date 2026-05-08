using System;
using Terraria.DataStructures;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004DD RID: 1245
	public class DungeonGlobalSpikes : GlobalDungeonFeature
	{
		// Token: 0x06003501 RID: 13569 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalSpikes(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x0060EB57 File Offset: 0x0060CD57
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.Spikes(data);
			this.generated = true;
			return true;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x0060EB70 File Offset: 0x0060CD70
		public void Spikes(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = data.wallVariants[0];
			float num2 = (float)Main.maxTilesX / 4200f;
			int num3 = 0;
			int num4 = 1000;
			int i = 0;
			double num5 = Math.Max(1.0, data.globalFeatureScalar * 0.25);
			int num6 = (int)((double)(42f * num2) * num5);
			if (WorldGen.getGoodWorldGen)
			{
				num6 *= 3;
			}
			while (i < num6)
			{
				num3++;
				int num7 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
				int num8 = genRand.Next((int)Main.worldSurface + 25, data.dungeonBounds.Bottom);
				if (WorldGen.drunkWorldGen || WorldGen.SecretSeed.noSurface.Enabled)
				{
					num8 = genRand.Next(data.genVars.generatingDungeonPositionY + 25, data.dungeonBounds.Bottom);
				}
				int num9 = num7;
				ushort num10 = 48;
				bool flag = true;
				bool flag2 = (int)Main.tile[num7, num8].wall == num;
				if (data.Type == DungeonType.DualDungeon)
				{
					flag2 = DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num7, num8].wall, false);
					if (Main.tile[num7, num8].wall == 87)
					{
						num10 = 232;
						flag = false;
					}
				}
				if (flag2 && !Main.tile[num7, num8].active())
				{
					int num11 = 1;
					if (genRand.Next(2) == 0)
					{
						num11 = -1;
					}
					while (!Main.tile[num7, num8].active())
					{
						num8 += num11;
					}
					if (Main.tile[num7 - 1, num8].active() && Main.tile[num7 + 1, num8].active() && this.Spikes_CanSupportSpike(num7 - 1, num8) && !Main.tile[num7 - 1, num8 - num11].active() && !Main.tile[num7 + 1, num8 - num11].active())
					{
						i++;
						int num12 = genRand.Next(5, 13);
						while (Main.tile[num7 - 1, num8].active() && this.Spikes_CanSupportSpike(num7 - 1, num8) && Main.tile[num7, num8 + num11].active() && Main.tile[num7, num8].active() && !Main.tile[num7, num8 - num11].active() && num12 > 0)
						{
							if (!data.CanGenerateFeatureAt(this, num7, num8) || !data.CanGenerateFeatureAt(this, num7, num8 - num11))
							{
								num7--;
								num12 = 0;
							}
							else
							{
								Main.tile[num7, num8].type = num10;
								if (!Main.tile[num7 - 1, num8 - num11].active() && !Main.tile[num7 + 1, num8 - num11].active())
								{
									Main.tile[num7, num8 - num11].Clear(TileDataType.Slope);
									Main.tile[num7, num8 - num11].type = num10;
									Main.tile[num7, num8 - num11].active(true);
									if (flag)
									{
										Main.tile[num7, num8 - num11 * 2].Clear(TileDataType.Slope);
										Main.tile[num7, num8 - num11 * 2].type = num10;
										Main.tile[num7, num8 - num11 * 2].active(true);
									}
								}
								num7--;
								num12--;
							}
						}
						num12 = genRand.Next(5, 13);
						num7 = num9 + 1;
						while (Main.tile[num7 + 1, num8].active() && this.Spikes_CanSupportSpike(num7 + 1, num8) && Main.tile[num7, num8 + num11].active() && Main.tile[num7, num8].active() && !Main.tile[num7, num8 - num11].active() && num12 > 0)
						{
							if (!data.CanGenerateFeatureAt(this, num7, num8) || !data.CanGenerateFeatureAt(this, num7, num8 - num11))
							{
								num7++;
								num12 = 0;
							}
							else
							{
								Main.tile[num7, num8].type = num10;
								if (!Main.tile[num7 - 1, num8 - num11].active() && !Main.tile[num7 + 1, num8 - num11].active())
								{
									Main.tile[num7, num8 - num11].Clear(TileDataType.Slope);
									Main.tile[num7, num8 - num11].type = num10;
									Main.tile[num7, num8 - num11].active(true);
									if (flag)
									{
										Main.tile[num7, num8 - num11 * 2].Clear(TileDataType.Slope);
										Main.tile[num7, num8 - num11 * 2].type = num10;
										Main.tile[num7, num8 - num11 * 2].active(true);
									}
								}
								num7++;
								num12--;
							}
						}
					}
				}
				if (num3 > num4)
				{
					num3 = 0;
					i++;
				}
			}
			num3 = 0;
			num4 = 1000;
			i = 0;
			while (i < num6)
			{
				num3++;
				int num13 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
				int num14 = genRand.Next((int)Main.worldSurface + 25, data.dungeonBounds.Bottom);
				if (WorldGen.SecretSeed.noSurface.Enabled)
				{
					num14 = genRand.Next(data.genVars.generatingDungeonPositionY + 25, data.dungeonBounds.Bottom);
				}
				int num15 = num14;
				ushort num16 = 48;
				bool flag3 = true;
				bool flag4 = (int)Main.tile[num13, num14].wall == num;
				if (data.Type == DungeonType.DualDungeon)
				{
					flag4 = DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num13, num14].wall, false);
					if (Main.tile[num13, num14].wall == 87)
					{
						num16 = 232;
						flag3 = false;
					}
				}
				if (flag4 && !Main.tile[num13, num14].active())
				{
					int num17 = 1;
					if (genRand.Next(2) == 0)
					{
						num17 = -1;
					}
					while (num13 > 5 && num13 < Main.maxTilesX - 5 && !Main.tile[num13, num14].active())
					{
						num13 += num17;
					}
					if (Main.tile[num13, num14 - 1].active() && Main.tile[num13, num14 + 1].active() && this.Spikes_CanSupportSpike(num13, num14 - 1) && !Main.tile[num13 - num17, num14 - 1].active() && !Main.tile[num13 - num17, num14 + 1].active())
					{
						i++;
						int num18 = genRand.Next(5, 13);
						while (Main.tile[num13, num14 - 1].active() && this.Spikes_CanSupportSpike(num13, num14 - 1) && Main.tile[num13 + num17, num14].active() && Main.tile[num13, num14].active() && !Main.tile[num13 - num17, num14].active() && num18 > 0)
						{
							if (!data.CanGenerateFeatureAt(this, num13, num14) || !data.CanGenerateFeatureAt(this, num13 - num17, num14))
							{
								num14--;
								num18 = 0;
							}
							else
							{
								Main.tile[num13, num14].type = num16;
								if (!Main.tile[num13 - num17, num14 - 1].active() && !Main.tile[num13 - num17, num14 + 1].active())
								{
									Main.tile[num13 - num17, num14].type = num16;
									Main.tile[num13 - num17, num14].active(true);
									Main.tile[num13 - num17, num14].Clear(TileDataType.Slope);
									if (flag3)
									{
										Main.tile[num13 - num17 * 2, num14].type = num16;
										Main.tile[num13 - num17 * 2, num14].active(true);
										Main.tile[num13 - num17 * 2, num14].Clear(TileDataType.Slope);
									}
								}
								num14--;
								num18--;
							}
						}
						num18 = genRand.Next(5, 13);
						num14 = num15 + 1;
						while (Main.tile[num13, num14 + 1].active() && this.Spikes_CanSupportSpike(num13, num14 + 1) && Main.tile[num13 + num17, num14].active() && Main.tile[num13, num14].active() && !Main.tile[num13 - num17, num14].active() && num18 > 0)
						{
							if (!data.CanGenerateFeatureAt(this, num13, num14) || !data.CanGenerateFeatureAt(this, num13 - num17, num14))
							{
								num14++;
								num18 = 0;
							}
							else
							{
								Main.tile[num13, num14].type = num16;
								if (!Main.tile[num13 - num17, num14 - 1].active() && !Main.tile[num13 - num17, num14 + 1].active())
								{
									Main.tile[num13 - num17, num14].type = num16;
									Main.tile[num13 - num17, num14].active(true);
									Main.tile[num13 - num17, num14].Clear(TileDataType.Slope);
									if (flag3)
									{
										Main.tile[num13 - num17 * 2, num14].type = num16;
										Main.tile[num13 - num17 * 2, num14].active(true);
										Main.tile[num13 - num17 * 2, num14].Clear(TileDataType.Slope);
									}
								}
								num14++;
								num18--;
							}
						}
					}
				}
				if (num3 > num4)
				{
					num3 = 0;
					i++;
				}
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0060F628 File Offset: 0x0060D828
		private bool Spikes_CanSupportSpike(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.active() && (tile.type < 0 || (!Main.tileFrameImportant[(int)tile.type] && !Main.tileCut[(int)tile.type])) && !DungeonUtils.IsConsideredCrackedDungeonTile((int)tile.type, false);
		}
	}
}
