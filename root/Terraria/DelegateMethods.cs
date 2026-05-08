using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000037 RID: 55
	public static class DelegateMethods
	{
		// Token: 0x0600033B RID: 827 RVA: 0x000454EE File Offset: 0x000436EE
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00045500 File Offset: 0x00043700
		public static Color ColorLerp_HSL_H(float percent)
		{
			return Main.hslToRgb(percent, 1f, 0.5f, byte.MaxValue);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00045517 File Offset: 0x00043717
		public static Color ColorLerp_HSL_S(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, percent, DelegateMethods.v3_1.Z, byte.MaxValue);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00045538 File Offset: 0x00043738
		public static Color ColorLerp_HSL_L(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, 0.15f + 0.85f * percent, byte.MaxValue);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00045565 File Offset: 0x00043765
		public static Color ColorLerp_HSL_O(float percent)
		{
			return Color.Lerp(Color.White, Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z, byte.MaxValue), percent);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0004559A File Offset: 0x0004379A
		public static bool SpreadIceBlocksOverWater(int x, int y)
		{
			return DelegateMethods.SpreadTile(x, y, 161, 80, false, 0);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000455AC File Offset: 0x000437AC
		public static bool SpreadDirt(int x, int y)
		{
			return DelegateMethods.SpreadTile(x, y, 0, 0, false, -1);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000455B9 File Offset: 0x000437B9
		public static bool SpreadPoopPyramid(int x, int y)
		{
			return DelegateMethods.SpreadTile(x, y, 666, 322, true, -1);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000455D0 File Offset: 0x000437D0
		public static bool SpreadTile(int x, int y, int tileType, int dustType, bool pyramidShape = false, int overLiquidType = -1)
		{
			float num = Math.Max(0.1f, DelegateMethods.f_1);
			if (pyramidShape)
			{
				float num2 = num;
				float num3 = DelegateMethods.v2_1.Y + num / 2f;
				float num4 = Math.Abs((float)y - num3) / num;
				num2 *= 1f - num4;
				if (Math.Abs((float)x - DelegateMethods.v2_1.X) > num2 || Math.Abs((float)y - DelegateMethods.v2_1.Y) > num)
				{
					return false;
				}
			}
			else if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > num)
			{
				return false;
			}
			if (overLiquidType >= 0 && (Main.tile[x, y].liquid == 0 || (int)Main.tile[x, y].liquidType() != overLiquidType))
			{
				return !WorldGen.SolidTile(x, y, false) || (int)Main.tile[x, y].type == tileType;
			}
			WorldGen.TryKillingReplaceableTile(x, y, tileType);
			if (WorldGen.PlaceTile(x, y, tileType, false, false, -1, 0))
			{
				if (overLiquidType >= 0)
				{
					Main.tile[x, y].Clear(TileDataType.Liquid);
					WorldGen.SquareTileFrame(x, y, false);
					if (Main.netMode != 0)
					{
						NetMessage.sendWater(x, y);
					}
					else
					{
						Liquid.AddWater(x, y);
					}
				}
				if (Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, (float)tileType, 0, 0, 0);
				}
				Vector2 vector = new Vector2((float)(x * 16), (float)(y * 16));
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(vector, 16, 16, dustType, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.noGravity = true;
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 4f;
					Dust dust2 = Dust.NewDustDirect(vector, 16, 16, dustType, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 2f;
				}
				int num5 = y + 1;
				if (Main.tile[x, num5] != null && !TileID.Sets.Platforms[(int)Main.tile[x, num5].type] && (Main.tile[x, num5].topSlope() || Main.tile[x, num5].halfBrick()))
				{
					WorldGen.SlopeTile(x, num5, 0, false, true);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)num5, 0f, 0, 0, 0);
					}
				}
				num5 = y - 1;
				if (Main.tile[x, num5] != null && !TileID.Sets.Platforms[(int)Main.tile[x, num5].type] && Main.tile[x, num5].bottomSlope())
				{
					WorldGen.SlopeTile(x, num5, 0, false, true);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)num5, 0f, 0, 0, 0);
					}
				}
				for (int j = x - 1; j <= x + 1; j++)
				{
					for (int k = y - 1; k <= y + 1; k++)
					{
						Tile tile = Main.tile[j, k];
						if (tile.active() && tileType != (int)tile.type && (tile.type == 2 || tile.type == 23 || tile.type == 60 || tile.type == 70 || tile.type == 109 || tile.type == 199 || tile.type == 477 || tile.type == 492))
						{
							bool flag = true;
							for (int l = j - 1; l <= j + 1; l++)
							{
								for (int m = k - 1; m <= k + 1; m++)
								{
									if (!WorldGen.SolidTile(l, m, false))
									{
										flag = false;
									}
								}
							}
							if (flag)
							{
								WorldGen.KillTile(j, k, true, false, false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)j, (float)k, 1f, 0, 0, 0);
								}
							}
						}
					}
				}
				return true;
			}
			if (overLiquidType >= 0 && !Collision.EmptyTile(x, y, true))
			{
				return true;
			}
			Tile tile2 = Main.tile[x, y];
			return tile2 != null && tile2.type >= 0 && tile2.type < TileID.Count && (!Main.tileSolid[(int)tile2.type] || TileID.Sets.Platforms[(int)tile2.type] || tile2.type == 380);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00045A74 File Offset: 0x00043C74
		public static bool SpreadWater(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 0, 255))
			{
				Vector2 vector = new Vector2((float)(x * 16), (float)(y * 16));
				int num = Dust.dustWater();
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.noGravity = true;
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 7f;
					Dust dust2 = Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00045B78 File Offset: 0x00043D78
		public static bool SpreadHoney(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 2, 255))
			{
				Vector2 vector = new Vector2((float)(x * 16), (float)(y * 16));
				int num = 152;
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 7f;
					Dust dust2 = Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00045C74 File Offset: 0x00043E74
		public static bool SpreadLava(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 1, 255))
			{
				Vector2 vector = new Vector2((float)(x * 16), (float)(y * 16));
				int num = 35;
				for (int i = 0; i < 3; i++)
				{
					Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.2f).velocity *= 7f;
					Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 0.8f).velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00045D40 File Offset: 0x00043F40
		public static bool SpreadDry(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.EmptyLiquid(x, y))
			{
				Vector2 vector = new Vector2((float)(x * 16), (float)(y * 16));
				int num = 31;
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.2f);
					dust.noGravity = true;
					dust.velocity *= 7f;
					Dust.NewDustDirect(vector, 16, 16, num, 0f, 0f, 100, Color.Transparent, 0.8f).velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00045E0C File Offset: 0x0004400C
		public static bool SpreadTest(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (WorldGen.SolidTile(x, y, false) || tile.wall != 0)
			{
				tile.active();
				return false;
			}
			return true;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00045E44 File Offset: 0x00044044
		public static bool TestDust(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			int num = Dust.NewDust(new Vector2((float)x, (float)y) * 16f + new Vector2(8f), 0, 0, 6, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num].noGravity = true;
			Main.dust[num].noLight = true;
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00045ECC File Offset: 0x000440CC
		public static bool CastLight(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00045F2C File Offset: 0x0004412C
		public static bool CastLightOpen(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
			}
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00045FE4 File Offset: 0x000441E4
		public static bool CheckStopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int)Main.tile[x, y].type] && Main.tileSolid[(int)Main.tile[x, y].type])
			{
				DelegateMethods.CheckResultOut = true;
				return false;
			}
			return true;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0004607C File Offset: 0x0004427C
		public static bool CastLightOpen_StopForSolids_ScaleWithDistance(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				Vector2 vector2 = new Vector2((float)x, (float)y);
				float num = Vector2.Distance(DelegateMethods.v2_1, vector2);
				vector *= MathHelper.Lerp(0.65f, 1f, num / DelegateMethods.f_1);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00046164 File Offset: 0x00044364
		public static bool CastLightOpen_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00046224 File Offset: 0x00044424
		public static bool SpreadLightOpen_StopForSolids(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			Tile tile = Main.tile[x, y];
			if (tile == null || tile.active() || tile.inActive() || Main.tileSolidTop[(int)tile.type] || !Main.tileSolid[(int)tile.type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000462B8 File Offset: 0x000444B8
		public static bool EmitGolfCartDust_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Dust.NewDustPerfect(new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)), 260, new Vector2?(Vector2.UnitY * -0.2f), 0, default(Color), 1f);
				return true;
			}
			return false;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00046390 File Offset: 0x00044590
		public static bool NotSolidOrPlatforms(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && (!tile.active() || (!Main.tileSolid[(int)tile.type] && !TileID.Sets.Platforms[(int)tile.type]));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000463D8 File Offset: 0x000445D8
		public static bool NotDoorStand(int x, int y)
		{
			return Main.tile[x, y] == null || !Main.tile[x, y].active() || Main.tile[x, y].type != 11 || (Main.tile[x, y].frameX >= 18 && Main.tile[x, y].frameX < 54);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0004644C File Offset: 0x0004464C
		public static bool CutTiles(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 1))
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tileCut[(int)Main.tile[x, y].type])
			{
				return true;
			}
			if (DelegateMethods.tileCutIgnore[(int)Main.tile[x, y].type])
			{
				return true;
			}
			if (WorldGen.CanCutTile(x, y, DelegateMethods.tilecut_0))
			{
				WorldGen.KillTile(x, y, false, false, false);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
				}
			}
			return true;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000464E4 File Offset: 0x000446E4
		public static bool SearchAvoidedByNPCs(int x, int y)
		{
			return WorldGen.InWorld(x, y, 1) && Main.tile[x, y] != null && (!Main.tile[x, y].active() || !TileID.Sets.AvoidedByNPCs[(int)Main.tile[x, y].type]);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0004653C File Offset: 0x0004473C
		public static void RainbowLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			if (stage == 0)
			{
				distCovered = 33f;
				frame = new Rectangle(0, 0, 26, 22);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 25, 26, 28);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 22f;
				frame = new Rectangle(0, 56, 26, 22);
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00046638 File Offset: 0x00044838
		public static void TurretLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			if (stage == 0)
			{
				distCovered = 32f;
				frame = new Rectangle(0, 0, 22, 20);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				DelegateMethods.i_1++;
				int num = DelegateMethods.i_1 % 5;
				frame = new Rectangle(0, 22 * (num + 1), 22, 20);
				distCovered = (float)(frame.Height - 1);
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				frame = new Rectangle(0, 154, 22, 30);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00046754 File Offset: 0x00044954
		public static void LightningLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1 * DelegateMethods.f_1;
			if (stage == 0)
			{
				distCovered = 0f;
				frame = new Rectangle(0, 0, 21, 8);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 8, 21, 6);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 8f;
				frame = new Rectangle(0, 14, 21, 8);
				origin = new Vector2((float)(frame.Width / 2), 2f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00046853 File Offset: 0x00044A53
		public static int CompareYReverse(Point a, Point b)
		{
			return b.Y.CompareTo(a.Y);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00046867 File Offset: 0x00044A67
		public static int CompareDrawSorterByYScale(DrawData a, DrawData b)
		{
			return a.scale.Y.CompareTo(b.scale.Y);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00046885 File Offset: 0x00044A85
		// Note: this type is marked as 'beforefieldinit'.
		static DelegateMethods()
		{
		}

		// Token: 0x04000261 RID: 609
		public static Vector3 v3_1 = Vector3.Zero;

		// Token: 0x04000262 RID: 610
		public static Vector2 v2_1 = Vector2.Zero;

		// Token: 0x04000263 RID: 611
		public static float f_1 = 0f;

		// Token: 0x04000264 RID: 612
		public static Color c_1 = Color.Transparent;

		// Token: 0x04000265 RID: 613
		public static int i_1;

		// Token: 0x04000266 RID: 614
		public static bool CheckResultOut;

		// Token: 0x04000267 RID: 615
		public static TileCuttingContext tilecut_0 = TileCuttingContext.Unknown;

		// Token: 0x04000268 RID: 616
		public static bool[] tileCutIgnore = null;

		// Token: 0x02000608 RID: 1544
		public static class CharacterPreview
		{
			// Token: 0x06003BC2 RID: 15298 RVA: 0x0065B748 File Offset: 0x00659948
			public static void EtsyPet(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				if (walking)
				{
					float num = (float)Main.timeForVisualEffects % 90f / 90f;
					proj.localAI[1] = 6.2831855f * num;
					return;
				}
				proj.localAI[1] = 0f;
			}

			// Token: 0x06003BC3 RID: 15299 RVA: 0x0065B790 File Offset: 0x00659990
			public static void CompanionCubePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					float num = (float)Main.timeForVisualEffects % 120f;
					int num2 = (int)(num / 30f);
					float num3 = num % 30f / 30f;
					float num4 = Utils.MultiLerp(num3, new float[] { 0f, 0f, 16f, 20f, 20f, 16f, 0f, 0f });
					float num5 = Utils.MultiLerp(num3, new float[] { 0f, 0f, 0.2f, 0.5f, 0.75f, 0.8f, 1f, 1f });
					proj.position.Y = proj.position.Y - num4;
					proj.rotation = (float)num2 * 1.5707964f + num5 * 1.5707964f;
					return;
				}
				proj.rotation = 0f;
			}

			// Token: 0x06003BC4 RID: 15300 RVA: 0x0065B824 File Offset: 0x00659A24
			public static void BerniePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.position.X = proj.position.X + 6f;
				}
			}

			// Token: 0x06003BC5 RID: 15301 RVA: 0x0065B840 File Offset: 0x00659A40
			public static void SlimePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					float num = (float)Main.timeForVisualEffects % 30f / 30f;
					proj.position.Y = proj.position.Y - Utils.MultiLerp(num, new float[] { 0f, 0f, 16f, 20f, 20f, 16f, 0f, 0f });
				}
			}

			// Token: 0x06003BC6 RID: 15302 RVA: 0x0065B88C File Offset: 0x00659A8C
			public static void WormPet(Projectile proj, bool walking)
			{
				float num = -0.3985988f;
				Vector2 vector = (Vector2.UnitY * 2f).RotatedBy((double)num, default(Vector2));
				Vector2 vector2 = proj.position;
				int num2 = proj.oldPos.Length;
				if (proj.type == 893)
				{
					num2 = proj.oldPos.Length - 30;
				}
				for (int i = 0; i < proj.oldPos.Length; i++)
				{
					vector2 -= vector;
					if (i < num2)
					{
						proj.oldPos[i] = vector2;
					}
					else if (i > 0)
					{
						proj.oldPos[i] = proj.oldPos[i - 1];
					}
					vector = vector.RotatedBy(-0.05235987901687622, default(Vector2));
				}
				proj.rotation = vector.ToRotation() + 0.31415927f + 3.1415927f;
				if (proj.type == 887)
				{
					proj.rotation += 0.3926991f;
				}
				if (proj.type == 893)
				{
					proj.rotation += 1.5707964f;
				}
			}

			// Token: 0x06003BC7 RID: 15303 RVA: 0x0065B9AE File Offset: 0x00659BAE
			public static void FloatAndSpinWhenWalking(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				if (walking)
				{
					proj.rotation = 6.2831855f * ((float)Main.timeForVisualEffects % 20f / 20f);
					return;
				}
				proj.rotation = 0f;
			}

			// Token: 0x06003BC8 RID: 15304 RVA: 0x0065B9E4 File Offset: 0x00659BE4
			public static void FloatAndRotateForwardWhenWalking(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				DelegateMethods.CharacterPreview.RotateForwardWhenWalking(proj, walking);
			}

			// Token: 0x06003BC9 RID: 15305 RVA: 0x0065B9F4 File Offset: 0x00659BF4
			public static void Float(Projectile proj, bool walking)
			{
				float num = 0.5f;
				float num2 = (float)Main.timeForVisualEffects % 60f / 60f;
				proj.position.Y = proj.position.Y + (-num + (float)(Math.Cos((double)(num2 * 6.2831855f * 2f)) * (double)(num * 2f)));
			}

			// Token: 0x06003BCA RID: 15306 RVA: 0x0065BA49 File Offset: 0x00659C49
			public static void RotateForwardWhenWalking(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.rotation = 0.5235988f;
					return;
				}
				proj.rotation = 0f;
			}

			// Token: 0x06003BCB RID: 15307 RVA: 0x0065BA65 File Offset: 0x00659C65
			public static void SpinWhenWalking(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.rotation = 6.2831855f * ((float)Main.timeForVisualEffects % 20f / 20f);
					return;
				}
				proj.rotation = 0f;
			}
		}

		// Token: 0x02000609 RID: 1545
		public static class Mount
		{
			// Token: 0x06003BCC RID: 15308 RVA: 0x0065BA94 File Offset: 0x00659C94
			public static Dust BatDashDust(Player player, int currentDustCount, Dust dust)
			{
				if (currentDustCount % 2 == 0)
				{
					dust.active = false;
				}
				else
				{
					dust.position = Main.rand.NextVector2FromRectangle(player.Hitbox);
					dust.scale *= 0.75f;
				}
				return dust;
			}

			// Token: 0x06003BCD RID: 15309 RVA: 0x0065BACD File Offset: 0x00659CCD
			public static bool BatPlayerSize(Player player, out Vector2? size)
			{
				size = new Vector2?(new Vector2(20f, 18f));
				return true;
			}

			// Token: 0x06003BCE RID: 15310 RVA: 0x0065BAEA File Offset: 0x00659CEA
			public static bool PixiePlayerSize(Player player, out Vector2? size)
			{
				size = new Vector2?(new Vector2(8f, 14f));
				return true;
			}

			// Token: 0x06003BCF RID: 15311 RVA: 0x0065BA94 File Offset: 0x00659C94
			public static Dust PixieDashDust(Player player, int currentDustCount, Dust dust)
			{
				if (currentDustCount % 2 == 0)
				{
					dust.active = false;
				}
				else
				{
					dust.position = Main.rand.NextVector2FromRectangle(player.Hitbox);
					dust.scale *= 0.75f;
				}
				return dust;
			}

			// Token: 0x06003BD0 RID: 15312 RVA: 0x0065BB07 File Offset: 0x00659D07
			public static bool RatPlayerSize(Player player, out Vector2? size)
			{
				size = new Vector2?(new Vector2(14f, 14f));
				return true;
			}

			// Token: 0x06003BD1 RID: 15313 RVA: 0x0065BB24 File Offset: 0x00659D24
			public static bool NoPosition(Player player, out Vector2? position)
			{
				position = null;
				return true;
			}

			// Token: 0x06003BD2 RID: 15314 RVA: 0x0065BB30 File Offset: 0x00659D30
			public static bool WolfMouthPosition(Player player, out Vector2? position)
			{
				Vector2 vector = new Vector2((float)(player.direction * 22), player.gravDir * -6f);
				position = new Vector2?(player.RotatedRelativePoint(player.MountedCenter, false, false) + vector.RotatedBy((double)player.fullRotation, default(Vector2)));
				return true;
			}

			// Token: 0x06003BD3 RID: 15315 RVA: 0x0065BB90 File Offset: 0x00659D90
			public static bool VelociraptorMouthPosition(Player player, out Vector2? position)
			{
				Vector2 vector = new Vector2((float)(player.direction * 24), player.gravDir * -12f);
				position = new Vector2?(player.RotatedRelativePoint(player.MountedCenter, false, false) + vector.RotatedBy((double)player.fullRotation, default(Vector2)));
				return true;
			}
		}

		// Token: 0x0200060A RID: 1546
		public static class Minecart
		{
			// Token: 0x06003BD4 RID: 15316 RVA: 0x0065BBF0 File Offset: 0x00659DF0
			public static void Sparks(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : (-13)), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 213, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 1f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x06003BD5 RID: 15317 RVA: 0x00009E46 File Offset: 0x00008046
			public static void JumpingSound(Player Player, Vector2 Position, int Width, int Height)
			{
			}

			// Token: 0x06003BD6 RID: 15318 RVA: 0x0065BD8A File Offset: 0x00659F8A
			public static void LandingSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item53, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
			}

			// Token: 0x06003BD7 RID: 15319 RVA: 0x0065BDB7 File Offset: 0x00659FB7
			public static void BumperSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item56, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
			}

			// Token: 0x06003BD8 RID: 15320 RVA: 0x0065BDE4 File Offset: 0x00659FE4
			public static void SpawnFartCloud(Player Player, Vector2 Position, int Width, int Height, bool useDelay = true)
			{
				if (useDelay)
				{
					if (Player.fartKartCloudDelay > 0)
					{
						return;
					}
					Player.fartKartCloudDelay = 20;
				}
				float num = 10f;
				float num2 = -4f;
				Vector2 vector = Position + new Vector2((float)(Width / 2 - 18), (float)(Height - 16));
				Vector2 vector2 = Player.velocity * 0.1f;
				if (vector2.Length() > 2f)
				{
					vector2 = vector2.SafeNormalize(Vector2.Zero) * 2f;
				}
				int num3 = Gore.NewGore(vector + new Vector2(0f, num2), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num3].velocity *= 0.2f;
				Main.gore[num3].velocity += vector2;
				Gore gore = Main.gore[num3];
				gore.velocity.Y = gore.velocity.Y * 0.75f;
				num3 = Gore.NewGore(vector + new Vector2(-num, num2), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num3].velocity *= 0.2f;
				Main.gore[num3].velocity += vector2;
				Gore gore2 = Main.gore[num3];
				gore2.velocity.Y = gore2.velocity.Y * 0.75f;
				num3 = Gore.NewGore(vector + new Vector2(num, num2), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num3].velocity *= 0.2f;
				Main.gore[num3].velocity += vector2;
				Gore gore3 = Main.gore[num3];
				gore3.velocity.Y = gore3.velocity.Y * 0.75f;
				if (Player.mount.Active && Player.mount.Type == 53)
				{
					Vector2 vector3 = Position + new Vector2((float)(Width / 2), (float)(Height + 10));
					float num4 = 30f;
					float num5 = -16f;
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustPerfect(vector3 + new Vector2(-num4 + num4 * 2f * Main.rand.NextFloat(), num5 * Main.rand.NextFloat()), 107, new Vector2?(Vector2.Zero), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.6f);
						dust.velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
						dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
						dust.velocity.Y = dust.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
						dust.velocity += vector2;
						dust.velocity.Y = dust.velocity.Y * 0.75f;
						dust.fadeIn = 0.2f + Main.rand.NextFloat() * 0.1f;
						dust.noGravity = Main.rand.Next(3) == 0;
						dust.noLightEmittance = true;
					}
				}
			}

			// Token: 0x06003BD9 RID: 15321 RVA: 0x0065C177 File Offset: 0x0065A377
			public static void JumpingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06003BDA RID: 15322 RVA: 0x0065C1B0 File Offset: 0x0065A3B0
			public static void LandingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
				SoundEngine.PlaySound(SoundID.Item53, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06003BDB RID: 15323 RVA: 0x0065C220 File Offset: 0x0065A420
			public static void BumperSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
				SoundEngine.PlaySound(SoundID.Item56, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 0f, 1f);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, true);
			}

			// Token: 0x06003BDC RID: 15324 RVA: 0x0065C290 File Offset: 0x0065A490
			public static void SparksFart(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : (-13)), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 211, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 50, default(Color), 0.8f);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				Main.dust[num].noLight = true;
				Main.dust[num].noGravity = Main.rand.Next(3) == 0;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
			}

			// Token: 0x06003BDD RID: 15325 RVA: 0x0065C418 File Offset: 0x0065A618
			public static void SparksTerraFart(Vector2 dustPosition)
			{
				if (Main.rand.Next(2) == 0)
				{
					DelegateMethods.Minecart.SparksFart(dustPosition);
					return;
				}
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : (-13)), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 107, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.8f);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				Main.dust[num].noLightEmittance = true;
				Main.dust[num].noGravity = Main.rand.Next(3) == 0;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
			}

			// Token: 0x06003BDE RID: 15326 RVA: 0x0065C5D0 File Offset: 0x0065A7D0
			public static void SparksMech(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : (-13)), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 260, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 0.5f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x06003BDF RID: 15327 RVA: 0x0065C76C File Offset: 0x0065A96C
			public static void SparksMeow(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : (-13)), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 213, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].shader = GameShaders.Armor.GetShaderFromItemId(2870);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 1f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x0400643F RID: 25663
			public static Vector2 rotationOrigin;

			// Token: 0x04006440 RID: 25664
			public static float rotation;
		}
	}
}
