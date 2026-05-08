using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Events
{
	// Token: 0x02000500 RID: 1280
	public class CultistRitual
	{
		// Token: 0x060035DA RID: 13786 RVA: 0x0061DD24 File Offset: 0x0061BF24
		public static void UpdateTime()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			CultistRitual.delay -= Main.dayRate;
			if (CultistRitual.delay < 0)
			{
				CultistRitual.delay = 0;
			}
			CultistRitual.recheck -= Main.dayRate;
			if (CultistRitual.recheck < 0)
			{
				CultistRitual.recheck = 0;
			}
			if (CultistRitual.delay == 0 && CultistRitual.recheck == 0)
			{
				CultistRitual.recheck = 600;
				if (NPC.AnyDanger(false, false))
				{
					CultistRitual.recheck *= 6;
					return;
				}
				CultistRitual.TrySpawning(Main.dungeonX, Main.dungeonY, false);
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0061DDB5 File Offset: 0x0061BFB5
		public static void CultistSlain()
		{
			CultistRitual.delay -= 3600;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0061DDC7 File Offset: 0x0061BFC7
		public static void TabletDestroyed()
		{
			CultistRitual.delay = 43200;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x0061DDD4 File Offset: 0x0061BFD4
		public static bool TrySpawning(int x, int y, bool force = false)
		{
			if (x < 0 || y < 0 || x >= Main.maxTilesX || y >= Main.maxTilesY)
			{
				return false;
			}
			if (!force && (WorldGen.PlayerLOS(x - 6, y) || WorldGen.PlayerLOS(x + 6, y)))
			{
				return false;
			}
			if (!CultistRitual.CheckRitual(x, y, force))
			{
				return false;
			}
			NPC.NewNPC(new EntitySource_WorldEvent(), x * 16 + 8, (y - 4) * 16 - 8, 437, 0, 0f, 0f, 0f, 0f, 255);
			return true;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0061DE5C File Offset: 0x0061C05C
		private static bool CheckRitual(int x, int y, bool force = false)
		{
			if (!force && (CultistRitual.delay != 0 || !Main.hardMode || !NPC.downedGolemBoss || !NPC.downedBoss3))
			{
				return false;
			}
			if (y < 7 || WorldGen.SolidTile(Main.tile[x, y - 7]))
			{
				return false;
			}
			if (!force && NPC.AnyNPCs(437))
			{
				return false;
			}
			Vector2 vector = new Vector2((float)(x * 16 + 8), (float)(y * 16 - 64 - 8 - 27));
			Point[] array = null;
			return CultistRitual.CheckFloor(vector, out array);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0061DEE0 File Offset: 0x0061C0E0
		public static bool CheckFloor(Vector2 Center, out Point[] spawnPoints)
		{
			Point[] array = new Point[4];
			int num = 0;
			Point point = Center.ToTileCoordinates();
			for (int i = -5; i <= 5; i += 2)
			{
				if (i != -1 && i != 1)
				{
					for (int j = -5; j < 12; j++)
					{
						int num2 = point.X + i * 2;
						int num3 = point.Y + j;
						if ((WorldGen.SolidTile(num2, num3, false) || TileID.Sets.Platforms[(int)Framing.GetTileSafely(num2, num3).type]) && (!Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1) || (!Collision.SolidTiles(num2, num2, num3 - 3, num3 - 1) && !Collision.SolidTiles(num2 + 1, num2 + 1, num3 - 3, num3 - 2) && !Collision.SolidTiles(num2 - 1, num2 - 1, num3 - 3, num3 - 2))))
						{
							array[num++] = new Point(num2, num3);
							break;
						}
					}
				}
			}
			if (num != 4)
			{
				spawnPoints = null;
				return false;
			}
			spawnPoints = array;
			return true;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0061DFEC File Offset: 0x0061C1EC
		public static bool CheckFloor2(Vector2 Center, out Point[] spawnPoints)
		{
			Point[] array = new Point[2];
			int num = 0;
			Point point = Center.ToTileCoordinates();
			for (int i = -3; i <= 3; i += 2)
			{
				if (i != -1 && i != 1)
				{
					for (int j = -5; j < 12; j++)
					{
						int num2 = point.X + i * 2;
						int num3 = point.Y + j;
						if ((WorldGen.SolidTile(num2, num3, false) || TileID.Sets.Platforms[(int)Framing.GetTileSafely(num2, num3).type]) && (!Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1) || (!Collision.SolidTiles(num2, num2, num3 - 3, num3 - 1) && !Collision.SolidTiles(num2 + 1, num2 + 1, num3 - 3, num3 - 2) && !Collision.SolidTiles(num2 - 1, num2 - 1, num3 - 3, num3 - 2))))
						{
							array[num++] = new Point(num2, num3);
							break;
						}
					}
				}
			}
			if (num != 2)
			{
				spawnPoints = null;
				return false;
			}
			spawnPoints = array;
			return true;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0000357B File Offset: 0x0000177B
		public CultistRitual()
		{
		}

		// Token: 0x04005AEF RID: 23279
		public const int delayStart = 86400;

		// Token: 0x04005AF0 RID: 23280
		public const int respawnDelay = 43200;

		// Token: 0x04005AF1 RID: 23281
		private const int timePerCultist = 3600;

		// Token: 0x04005AF2 RID: 23282
		private const int recheckStart = 600;

		// Token: 0x04005AF3 RID: 23283
		public static int delay;

		// Token: 0x04005AF4 RID: 23284
		public static int recheck;
	}
}
