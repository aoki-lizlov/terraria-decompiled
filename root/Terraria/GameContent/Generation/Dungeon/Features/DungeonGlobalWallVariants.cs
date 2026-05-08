using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004E5 RID: 1253
	public class DungeonGlobalWallVariants : GlobalDungeonFeature
	{
		// Token: 0x06003524 RID: 13604 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalWallVariants(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x00613726 File Offset: 0x00611926
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.WallVariants(data);
			this.generated = true;
			return true;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x00613740 File Offset: 0x00611940
		public void WallVariants(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int[] wallVariants = data.wallVariants;
			int num = wallVariants.Length;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num2 = genRand.Next(40, 240);
					int num3 = genRand.Next(data.dungeonBounds.Left, data.dungeonBounds.Right);
					int num4 = genRand.Next(data.dungeonBounds.Top, data.dungeonBounds.Bottom);
					for (int k = num3 - num2; k < num3 + num2; k++)
					{
						for (int l = num4 - num2; l < num4 + num2; l++)
						{
							if ((double)l > Main.worldSurface && WorldGen.InWorld(k, l, 2))
							{
								int num5 = Math.Abs(num3 - k);
								int num6 = Math.Abs(num4 - l);
								if (Math.Sqrt((double)(num5 * num5 + num6 * num6)) < (double)((float)num2 * 0.4f) && Main.wallDungeon[(int)Main.tile[k, l].wall])
								{
									this.SpreadWallDungeon(data, k, l, (ushort)wallVariants[j], true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x0061387C File Offset: 0x00611A7C
		public void SpreadWallDungeon(DungeonData data, int x, int y, ushort wallType, bool dungeonWallOnly = true)
		{
			if (!WorldGen.InWorld(x, y, 0))
			{
				return;
			}
			List<Point> list = new List<Point>();
			List<Point> list2 = new List<Point>();
			HashSet<Point> hashSet = new HashSet<Point>();
			list2.Add(new Point(x, y));
			while (list2.Count > 0)
			{
				list.Clear();
				list.AddRange(list2);
				list2.Clear();
				while (list.Count > 0)
				{
					Point point = list[0];
					if (!WorldGen.InWorld(point.X, point.Y, 1))
					{
						list.Remove(point);
					}
					else
					{
						hashSet.Add(point);
						list.Remove(point);
						Tile tile = Main.tile[point.X, point.Y];
						if (tile.wall != 0 && tile.wall != wallType && tile.wall != 244 && tile.wall != 62 && data.CanGenerateFeatureAt(this, point.X, point.Y))
						{
							if (data.dungeonEntrance.Bounds.Contains(point.X, point.Y))
							{
								if (tile.wall != data.dungeonEntrance.settings.StyleData.BrickWallType)
								{
									continue;
								}
							}
							else if (dungeonWallOnly && tile.wall != data.genVars.brickWallType)
							{
								continue;
							}
							if (!WorldGen.SolidTile(point.X, point.Y, false))
							{
								tile.wall = wallType;
								Point point2 = new Point(point.X - 1, point.Y);
								if (!hashSet.Contains(point2))
								{
									list2.Add(point2);
								}
								point2 = new Point(point.X + 1, point.Y);
								if (!hashSet.Contains(point2))
								{
									list2.Add(point2);
								}
								point2 = new Point(point.X, point.Y - 1);
								if (!hashSet.Contains(point2))
								{
									list2.Add(point2);
								}
								point2 = new Point(point.X, point.Y + 1);
								if (!hashSet.Contains(point2))
								{
									list2.Add(point2);
								}
							}
							else if (tile.active())
							{
								tile.wall = wallType;
							}
						}
					}
				}
			}
		}
	}
}
