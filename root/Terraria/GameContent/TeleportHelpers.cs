using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000267 RID: 615
	public class TeleportHelpers
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x00548A14 File Offset: 0x00546C14
		public static bool FindClosestTeleportSpotNoSpace(Player player, out Vector2 resultPosition)
		{
			bool flag = false;
			resultPosition = player.position;
			player.velocity = Vector2.Zero;
			Vector2 vector = new Vector2((float)player.width * 0.5f, (float)player.height);
			Vector2 bottom = player.Bottom;
			Point point = bottom.ToTileCoordinates();
			int num = point.X - 25;
			int num2 = point.X + 25;
			int num3 = point.Y - 25;
			int num4 = point.Y + 25;
			num = Utils.Clamp<int>(num, 40, Main.maxTilesX - 40);
			num2 = Utils.Clamp<int>(num2, 40, Main.maxTilesX - 40);
			num3 = Utils.Clamp<int>(num3, 40, Main.maxTilesY - 40);
			num4 = Utils.Clamp<int>(num4, 40, Main.maxTilesY - 40);
			float num5 = float.MaxValue;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Vector2 vector2 = new Vector2((float)(i * 16 + 8), (float)(j * 16 + 15)) - vector;
					Tile tile = Main.tile[i, j];
					Tile tile2 = Main.tile[i, j + 1];
					bool flag2 = WorldGen.SolidOrSlopedTile(tile) || tile.liquid > 0;
					bool flag3 = WorldGen.SolidOrSlopedTile(tile2) && tile2.liquid == 0;
					if (!TeleportHelpers.TileIsDangerous(i, j) && !flag2 && flag3 && !Collision.LavaCollision(vector2, player.width, player.height) && !Collision.AnyHurtingTiles(vector2, player.width, player.height) && !Collision.SolidCollision(vector2, player.width, player.height))
					{
						float num6 = (vector2 - bottom).Length();
						if (num6 < num5)
						{
							resultPosition = vector2;
							num5 = num6;
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00548BFC File Offset: 0x00546DFC
		public static bool RequestMagicConchTeleportPosition(Player player, int crawlOffsetX, bool rightOcean, out Point landingPoint)
		{
			landingPoint = default(Point);
			int num = 50;
			int num2 = 50;
			int num3 = 0;
			if (WorldGen.Skyblock.lowTiles)
			{
				num2 = 100;
				num3 = 50;
			}
			int num4 = (rightOcean ? (Main.maxTilesX - num) : num);
			int num5 = num2;
			int num6 = (int)Main.worldSurface - num3;
			Point point = new Point(num4, num5);
			int num7 = 1;
			int num8 = -1;
			int num9 = 1;
			int num10 = 0;
			int num11 = 5000;
			Vector2 vector = new Vector2((float)player.width * 0.5f, (float)player.height);
			int num12 = 40;
			bool flag = WorldGen.SolidOrSlopedTile(Main.tile[point.X, point.Y]);
			int num13 = 0;
			int num14 = 400;
			if (WorldGen.Skyblock.lowTiles)
			{
				num11 = num14 * ((int)Main.worldSurface - 10);
			}
			while (num10 < num11 && num13 < num14)
			{
				num10++;
				Tile tile = Main.tile[point.X, point.Y];
				Tile tile2 = Main.tile[point.X, point.Y + num9];
				bool flag2 = WorldGen.SolidOrSlopedTile(tile) || tile.liquid > 0;
				bool flag3 = WorldGen.SolidOrSlopedTile(tile2) || tile2.liquid > 0;
				if (TeleportHelpers.IsInSolidTilesExtended(new Vector2((float)(point.X * 16 + 8), (float)(point.Y * 16 + 15)) - vector, player.velocity, player.width, player.height, (int)player.gravDir))
				{
					if (flag)
					{
						point.Y += num7;
					}
					else
					{
						point.Y += num8;
					}
				}
				else if (flag2)
				{
					if (flag)
					{
						point.Y += num7;
					}
					else
					{
						point.Y += num8;
					}
				}
				else
				{
					flag = false;
					if (!TeleportHelpers.IsInSolidTilesExtended(new Vector2((float)(point.X * 16 + 8), (float)(point.Y * 16 + 15 + 16)) - vector, player.velocity, player.width, player.height, (int)player.gravDir) && !flag3 && (double)point.Y < Main.worldSurface)
					{
						point.Y += num7;
						if (WorldGen.Skyblock.lowTiles && point.Y >= num6)
						{
							point.Y = num5;
							point.X += crawlOffsetX;
							num13++;
						}
					}
					else if (tile2.liquid > 0)
					{
						point.X += crawlOffsetX;
						num13++;
					}
					else if (TeleportHelpers.TileIsDangerous(point.X, point.Y))
					{
						point.X += crawlOffsetX;
						num13++;
					}
					else if (TeleportHelpers.TileIsDangerous(point.X, point.Y + num9))
					{
						point.X += crawlOffsetX;
						num13++;
					}
					else
					{
						if (point.Y >= num12)
						{
							break;
						}
						point.Y += num7;
					}
				}
			}
			if (num10 == num11 || num13 >= num14)
			{
				return false;
			}
			if (!WorldGen.InWorld(point.X, point.Y, 40))
			{
				return false;
			}
			bool flag4 = false;
			for (int i = 0; i < 10; i++)
			{
				int num15 = point.Y + i;
				Tile tile3 = Main.tile[point.X, num15];
				if (WorldGen.SolidOrSlopedTile(tile3) || tile3.liquid > 0)
				{
					flag4 = true;
					break;
				}
			}
			if (WorldGen.Skyblock.lowTiles)
			{
				if (!flag4)
				{
					for (int j = 0; j < 10; j++)
					{
						int num16 = point.Y + j;
						Tile tile4 = Main.tile[point.X - 1, num16];
						if (WorldGen.SolidOrSlopedTile(tile4) || tile4.liquid > 0)
						{
							point.X--;
							flag4 = true;
							break;
						}
					}
				}
				if (!flag4)
				{
					for (int k = 0; k < 10; k++)
					{
						int num17 = point.Y + k;
						Tile tile5 = Main.tile[point.X + 1, num17];
						if (WorldGen.SolidOrSlopedTile(tile5) || tile5.liquid > 0)
						{
							point.X++;
							flag4 = true;
							break;
						}
					}
				}
			}
			if (!flag4)
			{
				return false;
			}
			landingPoint = point;
			return true;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00549050 File Offset: 0x00547250
		private static bool TileIsDangerous(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return (tile.liquid > 0 && tile.lava()) || (tile.wall == 87 && (double)y > Main.worldSurface && !NPC.downedPlantBoss) || (Main.wallDungeon[(int)tile.wall] && (double)y > Main.worldSurface && !NPC.downedBoss3);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x005490BC File Offset: 0x005472BC
		private static bool IsInSolidTilesExtended(Vector2 testPosition, Vector2 playerVelocity, int width, int height, int gravDir)
		{
			if (Collision.LavaCollision(testPosition, width, height))
			{
				return true;
			}
			if (Collision.AnyHurtingTiles(testPosition, width, height))
			{
				return true;
			}
			if (Collision.SolidCollision(testPosition, width, height))
			{
				return true;
			}
			Vector2 vector = Vector2.UnitX * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, true, true, gravDir, false, false, true) != vector)
			{
				return true;
			}
			vector = -Vector2.UnitX * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, true, true, gravDir, false, false, true) != vector)
			{
				return true;
			}
			vector = Vector2.UnitY * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, true, true, gravDir, false, false, true) != vector)
			{
				return true;
			}
			vector = -Vector2.UnitY * 16f;
			return Collision.TileCollision(testPosition - vector, vector, width, height, true, true, gravDir, false, false, true) != vector;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0000357B File Offset: 0x0000177B
		public TeleportHelpers()
		{
		}
	}
}
