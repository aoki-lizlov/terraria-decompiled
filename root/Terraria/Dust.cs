using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000049 RID: 73
	public class Dust
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x0034C5F0 File Offset: 0x0034A7F0
		public static Dust NewDustPerfect(Vector2 Position, int Type, Vector2? Velocity = null, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			Dust dust = Main.dust[Dust.NewDust(Position, 0, 0, Type, 0f, 0f, Alpha, newColor, Scale)];
			dust.position = Position;
			if (Velocity != null)
			{
				dust.velocity = Velocity.Value;
			}
			return dust;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0034C63C File Offset: 0x0034A83C
		public static Dust NewDustDirect(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0f, float SpeedY = 0f, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			Dust dust = Main.dust[Dust.NewDust(Position, Width, Height, Type, SpeedX, SpeedY, Alpha, newColor, Scale)];
			if (dust.velocity.HasNaNs())
			{
				dust.velocity = Vector2.Zero;
			}
			return dust;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0034C67C File Offset: 0x0034A87C
		public static int NewDust(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0f, float SpeedY = 0f, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			if (Main.gameMenu)
			{
				return 6000;
			}
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
			}
			if (Main.gamePaused)
			{
				return 6000;
			}
			if (WorldGen.isGeneratingOrLoadingWorld)
			{
				return 6000;
			}
			if (Main.netMode == 2)
			{
				return 6000;
			}
			int num = (int)(400f * (1f - Dust.dCount));
			Rectangle rectangle = new Rectangle((int)(Main.screenPosition.X - (float)num), (int)(Main.screenPosition.Y - (float)num), Main.screenWidth + num * 2, Main.screenHeight + num * 2);
			Rectangle rectangle2 = new Rectangle((int)Position.X, (int)Position.Y, 10, 10);
			if (!rectangle.Intersects(rectangle2))
			{
				return 6000;
			}
			int num2 = 6000;
			int i = 0;
			while (i < 6000)
			{
				Dust dust = Main.dust[i];
				if (!dust.active)
				{
					if (Main.NoPooling)
					{
						dust = (Main.dust[i] = new Dust());
					}
					if ((double)i > (double)Main.maxDustToDraw * 0.9)
					{
						if (Main.rand.Next(4) != 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.8)
					{
						if (Main.rand.Next(3) != 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.7)
					{
						if (Main.rand.Next(2) == 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.6)
					{
						if (Main.rand.Next(4) == 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.5)
					{
						if (Main.rand.Next(5) == 0)
						{
							return 6000;
						}
					}
					else
					{
						Dust.dCount = 0f;
					}
					int num3 = Width;
					int num4 = Height;
					if (num3 < 5)
					{
						num3 = 5;
					}
					if (num4 < 5)
					{
						num4 = 5;
					}
					num2 = i;
					dust.fadeIn = 0f;
					dust.active = true;
					dust.type = Type;
					dust.noGravity = false;
					dust.color = newColor;
					dust.alpha = Alpha;
					dust.position.X = Position.X + (float)Main.rand.Next(num3 - 4) + 4f;
					dust.position.Y = Position.Y + (float)Main.rand.Next(num4 - 4) + 4f;
					dust.velocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedX;
					dust.velocity.Y = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedY;
					dust.frame.X = 10 * Type;
					dust.frame.Y = 10 * Main.rand.Next(3);
					dust.shader = null;
					dust.customData = null;
					dust.noLightEmittance = false;
					dust.fullBright = false;
					int j = Type;
					while (j >= 100)
					{
						j -= 100;
						Dust dust2 = dust;
						dust2.frame.X = dust2.frame.X - 1000;
						Dust dust3 = dust;
						dust3.frame.Y = dust3.frame.Y + 30;
					}
					dust.frame.Width = 8;
					dust.frame.Height = 8;
					dust.rotation = 0f;
					dust.scale = 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
					dust.scale *= Scale;
					dust.noLight = false;
					dust.firstFrame = true;
					if (!ChildSafety.Disabled && ChildSafety.DangerousDust(dust.type))
					{
						if (Main.rand.Next(2) != 0)
						{
							dust.active = false;
							return 6000;
						}
						dust.firstFrame = false;
						dust.type = 16;
						dust.scale = Main.rand.NextFloat() * 1.6f + 0.3f;
						dust.color = Color.Transparent;
						dust.frame.X = 10 * dust.type;
						dust.frame.Y = 10 * Main.rand.Next(3);
						dust.shader = null;
						dust.customData = null;
						int num5 = dust.type / 100;
						Dust dust4 = dust;
						dust4.frame.X = dust4.frame.X - 1000 * num5;
						Dust dust5 = dust;
						dust5.frame.Y = dust5.frame.Y + 30 * num5;
						dust.noGravity = true;
					}
					if (dust.type == 228 || dust.type == 279 || dust.type == 269 || dust.type == 135 || dust.type == 6 || dust.type == 242 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158 || dust.type == 293 || dust.type == 294 || dust.type == 295 || dust.type == 296 || dust.type == 297 || dust.type == 298 || dust.type == 302 || dust.type == 307 || dust.type == 310)
					{
						dust.velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
						Dust dust6 = dust;
						dust6.velocity.X = dust6.velocity.X * 0.3f;
						dust.scale *= 0.7f;
					}
					if (dust.type == 127 || dust.type == 187)
					{
						dust.velocity *= 0.3f;
						dust.scale *= 0.7f;
					}
					if (dust.type == 308)
					{
						dust.velocity *= 0.5f;
						Dust dust7 = dust;
						dust7.velocity.Y = dust7.velocity.Y + 1f;
					}
					if (dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105)
					{
						dust.alpha = 170;
						dust.velocity *= 0.5f;
						Dust dust8 = dust;
						dust8.velocity.Y = dust8.velocity.Y + 1f;
					}
					if (dust.type == 41)
					{
						dust.velocity *= 0f;
					}
					if (dust.type == 80)
					{
						dust.alpha = 50;
					}
					if (dust.type != 34 && dust.type != 35 && dust.type != 152)
					{
						break;
					}
					dust.velocity *= 0.1f;
					dust.velocity.Y = -0.5f;
					if (dust.type == 34 && !Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
					{
						dust.active = false;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return num2;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0034CEB3 File Offset: 0x0034B0B3
		public static Dust CloneDust(int dustIndex)
		{
			return Dust.CloneDust(Main.dust[dustIndex]);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0034CEC4 File Offset: 0x0034B0C4
		public static Dust CloneDust(Dust rf)
		{
			if (rf.dustIndex == Main.maxDustToDraw)
			{
				return rf;
			}
			int num = Dust.NewDust(rf.position, 0, 0, rf.type, 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[num];
			dust.position = rf.position;
			dust.velocity = rf.velocity;
			dust.fadeIn = rf.fadeIn;
			dust.noGravity = rf.noGravity;
			dust.scale = rf.scale;
			dust.rotation = rf.rotation;
			dust.noLight = rf.noLight;
			dust.active = rf.active;
			dust.type = rf.type;
			dust.color = rf.color;
			dust.alpha = rf.alpha;
			dust.frame = rf.frame;
			dust.shader = rf.shader;
			dust.customData = rf.customData;
			return dust;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0034CFBC File Offset: 0x0034B1BC
		public static Dust QuickDust(int x, int y, Color color)
		{
			return Dust.QuickDust(new Point(x, y), color);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0034CFCB File Offset: 0x0034B1CB
		public static Dust QuickDust(Point tileCoords, Color color)
		{
			return Dust.QuickDust(tileCoords.ToWorldCoordinates(8f, 8f), color);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0034CFE4 File Offset: 0x0034B1E4
		public void HackFrame(int Type)
		{
			this.frame.X = 10 * Type;
			this.frame.Y = 10 * Main.rand.Next(3);
			int i = Type;
			while (i >= 100)
			{
				i -= 100;
				this.frame.X = this.frame.X - 1000;
				this.frame.Y = this.frame.Y + 30;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0034D04C File Offset: 0x0034B24C
		public static void QuickBox(Vector2 topLeft, Vector2 bottomRight, int divisions, Color color, float fadein = 1f, float dustScale = 1f, Action<Dust> manipulator = null)
		{
			float num = (float)(divisions + 2);
			for (float num2 = 0f; num2 <= (float)(divisions + 2); num2 += 1f)
			{
				Dust dust = Dust.QuickDust(new Vector2(MathHelper.Lerp(topLeft.X, bottomRight.X, num2 / num), topLeft.Y), color);
				dust.scale = dustScale;
				dust.fadeIn = fadein;
				if (manipulator != null)
				{
					manipulator(dust);
				}
				dust = Dust.QuickDust(new Vector2(MathHelper.Lerp(topLeft.X, bottomRight.X, num2 / num), bottomRight.Y), color);
				dust.scale = dustScale;
				dust.fadeIn = fadein;
				if (manipulator != null)
				{
					manipulator(dust);
				}
				dust = Dust.QuickDust(new Vector2(topLeft.X, MathHelper.Lerp(topLeft.Y, bottomRight.Y, num2 / num)), color);
				dust.scale = dustScale;
				dust.fadeIn = fadein;
				if (manipulator != null)
				{
					manipulator(dust);
				}
				dust = Dust.QuickDust(new Vector2(bottomRight.X, MathHelper.Lerp(topLeft.Y, bottomRight.Y, num2 / num)), color);
				dust.scale = dustScale;
				dust.fadeIn = fadein;
				if (manipulator != null)
				{
					manipulator(dust);
				}
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0034D184 File Offset: 0x0034B384
		public static void QuickCircle(Vector2 center, float radius, int divisions, Color color, Action<Dust> manipulator)
		{
			float num = 1f / Math.Max(1f, (float)divisions);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float num3 = num2 * 6.2831855f;
				Dust dust = Dust.QuickDust(center + new Vector2(radius, 0f).RotatedBy((double)num3, Vector2.Zero), color);
				if (manipulator != null)
				{
					manipulator(dust);
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0034D1F0 File Offset: 0x0034B3F0
		public static void DrawDebugBox(Rectangle itemRectangle, Color color = default(Color))
		{
			if (color == default(Color))
			{
				color = Color.White;
			}
			Vector2 vector = itemRectangle.TopLeft();
			itemRectangle.BottomRight();
			for (int i = 0; i <= itemRectangle.Width; i++)
			{
				for (int j = 0; j <= itemRectangle.Height; j++)
				{
					if (i == 0 || j == 0 || i == itemRectangle.Width - 1 || j == itemRectangle.Height - 1)
					{
						Dust dust = Dust.QuickDust(vector + new Vector2((float)i, (float)j), color);
						dust.scale = 0.31f;
						dust.fadeIn = 0f;
					}
				}
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0034D28C File Offset: 0x0034B48C
		public static Dust QuickDust(Vector2 pos, Color color)
		{
			Dust dust = Main.dust[Dust.NewDust(pos, 0, 0, 267, 0f, 0f, 0, default(Color), 1f)];
			dust.position = pos;
			dust.velocity = Vector2.Zero;
			dust.fadeIn = 1f;
			dust.noLight = true;
			dust.noGravity = true;
			dust.color = color;
			return dust;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0034D2F8 File Offset: 0x0034B4F8
		public static Dust QuickDustSmall(Vector2 pos, Color color, bool floorPositionValues = false)
		{
			Dust dust = Dust.QuickDust(pos, color);
			dust.fadeIn = 0f;
			dust.scale = 0.35f;
			if (floorPositionValues)
			{
				dust.position = dust.position.Floor();
			}
			return dust;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0034D338 File Offset: 0x0034B538
		public static void QuickDustLine(Vector2 start, Vector2 end, float splits, Color color)
		{
			Dust.QuickDust(start, color).scale = 0.3f;
			Dust.QuickDust(end, color).scale = 0.3f;
			float num = 1f / splits;
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				Dust.QuickDust(Vector2.Lerp(start, end, num2), color).scale = 0.3f;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0034D39C File Offset: 0x0034B59C
		public static int dustWater()
		{
			switch (Main.waterStyle)
			{
			case 2:
				return 98;
			case 3:
				return 99;
			case 4:
				return 100;
			case 5:
				return 101;
			case 6:
				return 102;
			case 7:
				return 103;
			case 8:
				return 104;
			case 9:
				return 105;
			case 10:
				return 123;
			case 12:
				return 288;
			}
			return 33;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0034D408 File Offset: 0x0034B608
		public static void UpdateDust()
		{
			if (Main.netMode == 2)
			{
				return;
			}
			int num = 0;
			Dust.lavaBubbles = 0;
			Main.snowDust = 0;
			Dust.SandStormCount = 0;
			bool flag = Sandstorm.ShowSandstormVisuals();
			for (int i = 0; i < 6000; i++)
			{
				Dust dust = Main.dust[i];
				if (i < Main.maxDustToDraw)
				{
					if (dust.active)
					{
						Dust.dCount += 1f;
						if (dust.scale > 10f)
						{
							dust.active = false;
						}
						if (dust.firstFrame && !ChildSafety.Disabled && ChildSafety.DangerousDust(dust.type))
						{
							if (Main.rand.Next(2) == 0)
							{
								dust.firstFrame = false;
								dust.type = 16;
								dust.scale = Main.rand.NextFloat() * 1.6f + 0.3f;
								dust.color = Color.Transparent;
								dust.frame.X = 10 * dust.type;
								dust.frame.Y = 10 * Main.rand.Next(3);
								dust.shader = null;
								dust.customData = null;
								int num2 = dust.type / 100;
								Dust dust2 = dust;
								dust2.frame.X = dust2.frame.X - 1000 * num2;
								Dust dust3 = dust;
								dust3.frame.Y = dust3.frame.Y + 30 * num2;
								dust.noGravity = true;
							}
							else
							{
								dust.active = false;
							}
						}
						int num3 = dust.type;
						if (num3 - 299 <= 2 || num3 == 305)
						{
							dust.scale *= 0.96f;
							Dust dust4 = dust;
							dust4.velocity.Y = dust4.velocity.Y - 0.01f;
						}
						if (dust.type == 35)
						{
							Dust.lavaBubbles++;
						}
						dust.position += dust.velocity;
						if (dust.type == 258)
						{
							dust.noGravity = true;
							dust.scale += 0.015f;
						}
						if (dust.type == 309)
						{
							float num4 = (float)dust.color.R / 255f * dust.scale;
							float num5 = (float)dust.color.G / 255f * dust.scale;
							float num6 = (float)dust.color.B / 255f * dust.scale;
							Lighting.AddLight(dust.position, num4, num5, num6);
							dust.scale *= 0.97f;
						}
						if (dust.type == 325)
						{
							if (!dust.noLight && !dust.noLightEmittance)
							{
								float num7 = dust.scale * 0.6f;
								if (num7 > 1f)
								{
									num7 = 1f;
								}
								float num8 = num7;
								float num9 = num7;
								float num10 = num7;
								num8 *= 1.05f;
								num9 *= 0.1f;
								num10 *= 0.4f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num7 * num8, num7 * num9, num7 * num10);
							}
							if (dust.customData != null && dust.customData is Player)
							{
								Player player = (Player)dust.customData;
								dust.position += player.position - player.oldPosition;
							}
							else if (dust.customData != null && dust.customData is Projectile)
							{
								Projectile projectile = (Projectile)dust.customData;
								if (projectile.active)
								{
									dust.position += projectile.position - projectile.oldPosition;
								}
							}
						}
						if (((dust.type >= 86 && dust.type <= 92) || dust.type == 286) && !dust.noLight && !dust.noLightEmittance)
						{
							float num11 = dust.scale * 0.6f;
							if (num11 > 1f)
							{
								num11 = 1f;
							}
							int num12 = dust.type - 85;
							float num13 = num11;
							float num14 = num11;
							float num15 = num11;
							if (num12 == 3)
							{
								num13 *= 0f;
								num14 *= 0.1f;
								num15 *= 1.3f;
							}
							else if (num12 == 5)
							{
								num13 *= 1f;
								num14 *= 0.1f;
								num15 *= 0.1f;
							}
							else if (num12 == 4)
							{
								num13 *= 0f;
								num14 *= 1f;
								num15 *= 0.1f;
							}
							else if (num12 == 1)
							{
								num13 *= 0.9f;
								num14 *= 0f;
								num15 *= 0.9f;
							}
							else if (num12 == 6)
							{
								num13 *= 1.3f;
								num14 *= 1.3f;
								num15 *= 1.3f;
							}
							else if (num12 == 2)
							{
								num13 *= 0.9f;
								num14 *= 0.9f;
								num15 *= 0f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num11 * num13, num11 * num14, num11 * num15);
						}
						if ((dust.type >= 86 && dust.type <= 92) || dust.type == 286)
						{
							if (dust.customData != null && dust.customData is Player)
							{
								Player player2 = (Player)dust.customData;
								dust.position += player2.position - player2.oldPosition;
							}
							else if (dust.customData != null && dust.customData is Projectile)
							{
								Projectile projectile2 = (Projectile)dust.customData;
								if (projectile2.active)
								{
									dust.position += projectile2.position - projectile2.oldPosition;
								}
							}
						}
						if (dust.type == 262 && !dust.noLight)
						{
							Vector3 vector = new Vector3(0.9f, 0.6f, 0f) * dust.scale * 0.6f;
							Lighting.AddLight(dust.position, vector);
						}
						if (dust.type == 240 && dust.customData != null && dust.customData is Projectile)
						{
							Projectile projectile3 = (Projectile)dust.customData;
							if (projectile3.active)
							{
								dust.position += projectile3.position - projectile3.oldPosition;
							}
						}
						if (dust.type == 329 && Collision.SolidCollision(dust.position, 4, 4))
						{
							dust.scale *= 0.8f;
						}
						if ((dust.type == 259 || dust.type == 6 || dust.type == 158 || dust.type == 135) && dust.customData != null && dust.customData is int)
						{
							if ((int)dust.customData == 0)
							{
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
							}
							else if ((int)dust.customData == 1)
							{
								dust.scale *= 0.98f;
								Dust dust5 = dust;
								dust5.velocity.Y = dust5.velocity.Y * 0.98f;
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
							}
						}
						if (dust.type == 263 || dust.type == 264)
						{
							if (!dust.noLight)
							{
								Vector3 vector2 = dust.color.ToVector3() * dust.scale * 0.4f;
								Lighting.AddLight(dust.position, vector2);
							}
							if (dust.customData != null && dust.customData is Player)
							{
								Player player3 = (Player)dust.customData;
								dust.position += player3.position - player3.oldPosition;
								dust.customData = null;
							}
							else if (dust.customData != null && dust.customData is Projectile)
							{
								Projectile projectile4 = (Projectile)dust.customData;
								dust.position += projectile4.position - projectile4.oldPosition;
							}
						}
						if (dust.type == 230)
						{
							float num16 = dust.scale * 0.6f;
							float num17 = num16;
							float num18 = num16;
							float num19 = num16;
							num17 *= 0.5f;
							num18 *= 0.9f;
							num19 *= 1f;
							dust.scale += 0.02f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num16 * num17, num16 * num18, num16 * num19);
							if (dust.customData != null && dust.customData is Player)
							{
								Vector2 center = ((Player)dust.customData).Center;
								Vector2 vector3 = dust.position - center;
								float num20 = vector3.Length();
								vector3 /= num20;
								dust.scale = Math.Min(dust.scale, num20 / 24f - 1f);
								dust.velocity -= vector3 * (100f / Math.Max(50f, num20));
							}
						}
						if (dust.type == 154 || dust.type == 218)
						{
							dust.rotation += dust.velocity.X * 0.3f;
							dust.scale -= 0.03f;
						}
						if (dust.type == 172)
						{
							float num21 = dust.scale * 0.5f;
							if (num21 > 1f)
							{
								num21 = 1f;
							}
							float num22 = num21;
							float num23 = num21;
							float num24 = num21;
							num22 *= 0f;
							num23 *= 0.25f;
							num24 *= 1f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num21 * num22, num21 * num23, num21 * num24);
						}
						if (dust.type == 182)
						{
							dust.rotation += 1f;
							if (!dust.noLight)
							{
								float num25 = dust.scale * 0.25f;
								if (num25 > 1f)
								{
									num25 = 1f;
								}
								float num26 = num25;
								float num27 = num25;
								float num28 = num25;
								num26 *= 1f;
								num27 *= 0.2f;
								num28 *= 0.1f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num25 * num26, num25 * num27, num25 * num28);
							}
							if (dust.customData != null && dust.customData is Player)
							{
								Player player4 = (Player)dust.customData;
								dust.position += player4.position - player4.oldPosition;
								dust.customData = null;
							}
						}
						if (dust.type == 261)
						{
							if (!dust.noLight && !dust.noLightEmittance)
							{
								float num29 = dust.scale * 0.3f;
								if (num29 > 1f)
								{
									num29 = 1f;
								}
								Lighting.AddLight(dust.position, new Vector3(0.4f, 0.6f, 0.7f) * num29);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							dust.velocity *= new Vector2(0.97f, 0.99f);
							dust.scale -= 0.0025f;
							if (dust.customData != null && dust.customData is Player)
							{
								Player player5 = (Player)dust.customData;
								dust.position += player5.position - player5.oldPosition;
							}
						}
						if (dust.type == 254)
						{
							float num30 = dust.scale * 0.35f;
							if (num30 > 1f)
							{
								num30 = 1f;
							}
							float num31 = num30;
							float num32 = num30;
							float num33 = num30;
							num31 *= 0.9f;
							num32 *= 0.1f;
							num33 *= 0.75f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num30 * num31, num30 * num32, num30 * num33);
						}
						if (dust.type == 255)
						{
							float num34 = dust.scale * 0.25f;
							if (num34 > 1f)
							{
								num34 = 1f;
							}
							float num35 = num34;
							float num36 = num34;
							float num37 = num34;
							num35 *= 0.9f;
							num36 *= 0.1f;
							num37 *= 0.75f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num34 * num35, num34 * num36, num34 * num37);
						}
						if (dust.type == 211 && dust.noLight && Collision.SolidCollision(dust.position, 4, 4))
						{
							dust.active = false;
						}
						if (dust.type == 284 && Collision.SolidCollision(dust.position - Vector2.One * 4f, 8, 8) && dust.fadeIn == 0f)
						{
							dust.velocity *= 0.25f;
						}
						if (dust.type == 213 || dust.type == 260)
						{
							dust.rotation = 0f;
							float num38 = dust.scale / 2.5f * 0.2f;
							Vector3 vector4 = Vector3.Zero;
							num3 = dust.type;
							if (num3 != 213)
							{
								if (num3 == 260)
								{
									vector4 = new Vector3(255f, 48f, 48f);
								}
							}
							else
							{
								vector4 = new Vector3(255f, 217f, 48f);
							}
							vector4 /= 255f;
							if (num38 > 1f)
							{
								num38 = 1f;
							}
							vector4 *= num38;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), vector4.X, vector4.Y, vector4.Z);
						}
						if (dust.type == 157)
						{
							float num39 = dust.scale * 0.2f;
							float num40 = num39;
							float num41 = num39;
							float num42 = num39;
							num40 *= 0.25f;
							num41 *= 1f;
							num42 *= 0.5f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num39 * num40, num39 * num41, num39 * num42);
						}
						if (dust.type == 206)
						{
							dust.scale -= 0.1f;
							float num43 = dust.scale * 0.4f;
							float num44 = num43;
							float num45 = num43;
							float num46 = num43;
							num44 *= 0.1f;
							num45 *= 0.6f;
							num46 *= 1f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num43 * num44, num43 * num45, num43 * num46);
						}
						if (dust.type == 163)
						{
							float num47 = dust.scale * 0.25f;
							float num48 = num47;
							float num49 = num47;
							float num50 = num47;
							num48 *= 0.25f;
							num49 *= 1f;
							num50 *= 0.05f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num47 * num48, num47 * num49, num47 * num50);
						}
						if (dust.type == 205)
						{
							float num51 = dust.scale * 0.25f;
							float num52 = num51;
							float num53 = num51;
							float num54 = num51;
							num52 *= 1f;
							num53 *= 0.05f;
							num54 *= 1f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num51 * num52, num51 * num53, num51 * num54);
						}
						if (dust.type == 170)
						{
							float num55 = dust.scale * 0.5f;
							float num56 = num55;
							float num57 = num55;
							float num58 = num55;
							num56 *= 1f;
							num57 *= 1f;
							num58 *= 0.05f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55 * num56, num55 * num57, num55 * num58);
						}
						if (dust.type == 156)
						{
							float num59 = dust.scale * 0.6f;
							int num60 = dust.type;
							float num61 = num59;
							float num62 = num59;
							num61 *= 0.9f;
							num62 *= 1f;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 12, num59);
						}
						if (dust.type == 234 && !dust.noLightEmittance)
						{
							float num63 = dust.scale * 0.6f;
							int num64 = dust.type;
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 13, num63);
						}
						if (dust.type == 175)
						{
							dust.scale -= 0.05f;
						}
						if (dust.type == 174)
						{
							dust.scale -= 0.01f;
							float num65 = dust.scale * 1f;
							if (num65 > 0.6f)
							{
								num65 = 0.6f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num65, num65 * 0.4f, 0f);
						}
						if (dust.type == 235)
						{
							Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							vector5.Normalize();
							vector5 *= 15f;
							dust.scale -= 0.01f;
						}
						else if (dust.type == 228 || dust.type == 279 || dust.type == 229 || dust.type == 6 || dust.type == 242 || dust.type == 135 || dust.type == 127 || dust.type == 187 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158 || dust.type == 293 || dust.type == 294 || dust.type == 295 || dust.type == 296 || dust.type == 297 || dust.type == 298 || dust.type == 302 || dust.type == 307 || dust.type == 310)
						{
							if (!dust.noGravity)
							{
								Dust dust6 = dust;
								dust6.velocity.Y = dust6.velocity.Y + 0.05f;
							}
							if (dust.type == 229 || dust.type == 228 || dust.type == 279)
							{
								if (dust.customData != null && dust.customData is NPC)
								{
									NPC npc = (NPC)dust.customData;
									dust.position += npc.position - npc.oldPos[1];
								}
								else if (dust.customData != null && dust.customData is Player)
								{
									Player player6 = (Player)dust.customData;
									dust.position += player6.position - player6.oldPosition;
								}
								else if (dust.customData != null && dust.customData is Vector2)
								{
									Vector2 vector6 = (Vector2)dust.customData - dust.position;
									if (vector6 != Vector2.Zero)
									{
										vector6.Normalize();
									}
									dust.velocity = (dust.velocity * 4f + vector6 * dust.velocity.Length()) / 5f;
								}
							}
							if (!dust.noLight && !dust.noLightEmittance)
							{
								float num66 = dust.scale * 1.4f;
								if (dust.type == 29)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66 * 0.1f, num66 * 0.4f, num66);
								}
								else if (dust.type == 75)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									if (dust.customData is float)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 8, num66 * (float)dust.customData);
									}
									else
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 8, num66);
									}
								}
								else if (dust.type == 169)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 11, num66);
								}
								else if (dust.type == 135)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 9, num66);
								}
								else if (dust.type == 158)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 10, num66);
								}
								else if (dust.type == 228)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66 * 0.7f, num66 * 0.65f, num66 * 0.3f);
								}
								else if (dust.type == 229)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66 * 0.3f, num66 * 0.65f, num66 * 0.7f);
								}
								else if (dust.type == 242)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 15, num66);
								}
								else if (dust.type == 293)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									num66 *= 0.95f;
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 16, num66);
								}
								else if (dust.type == 294)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 17, num66);
								}
								else if (dust.type >= 59 && dust.type <= 65)
								{
									if (num66 > 0.8f)
									{
										num66 = 0.8f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1 + dust.type - 59, num66);
								}
								else if (dust.type == 127)
								{
									num66 *= 1.3f;
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66, num66 * 0.45f, num66 * 0.2f);
								}
								else if (dust.type == 187)
								{
									num66 *= 1.3f;
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66 * 0.2f, num66 * 0.45f, num66);
								}
								else if (dust.type == 295)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 18, num66);
								}
								else if (dust.type == 296)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 19, num66);
								}
								else if (dust.type == 297)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 20, num66);
								}
								else if (dust.type == 298)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 21, num66);
								}
								else if (dust.type == 307)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 22, num66);
								}
								else if (dust.type == 310)
								{
									if (num66 > 1f)
									{
										num66 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 23, num66);
								}
								else
								{
									if (num66 > 0.6f)
									{
										num66 = 0.6f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66, num66 * 0.65f, num66 * 0.4f);
								}
							}
						}
						else if (dust.type == 306)
						{
							if (!dust.noGravity)
							{
								Dust dust7 = dust;
								dust7.velocity.Y = dust7.velocity.Y + 0.05f;
							}
							dust.scale -= 0.04f;
							if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
							{
								dust.scale *= 0.9f;
								dust.velocity *= 0.25f;
							}
						}
						else if (dust.type == 269)
						{
							if (!dust.noLight)
							{
								float num67 = dust.scale * 1.4f;
								if (num67 > 1f)
								{
									num67 = 1f;
								}
								Vector3 vector7 = new Vector3(0.7f, 0.65f, 0.3f);
								Lighting.AddLight(dust.position, vector7 * num67);
							}
							if (dust.customData != null && dust.customData is Vector2)
							{
								Vector2 vector8 = (Vector2)dust.customData - dust.position;
								Dust dust8 = dust;
								dust8.velocity.X = dust8.velocity.X + 1f * (float)Math.Sign(vector8.X) * dust.scale;
							}
						}
						else if (dust.type == 159)
						{
							float num68 = dust.scale * 1.3f;
							if (num68 > 1f)
							{
								num68 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num68, num68, num68 * 0.1f);
							if (dust.noGravity)
							{
								if (dust.scale < 0.7f)
								{
									dust.velocity *= 1.075f;
								}
								else if (Main.rand.Next(2) == 0)
								{
									dust.velocity *= -0.95f;
								}
								else
								{
									dust.velocity *= 1.05f;
								}
								dust.scale -= 0.03f;
							}
							else
							{
								dust.scale += 0.005f;
								dust.velocity *= 0.9f;
								Dust dust9 = dust;
								dust9.velocity.X = dust9.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
								Dust dust10 = dust;
								dust10.velocity.Y = dust10.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
								if (Main.rand.Next(5) == 0)
								{
									int num69 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
									Main.dust[num69].noGravity = true;
									Main.dust[num69].scale = dust.scale * 2.5f;
								}
							}
						}
						else if (dust.type == 164)
						{
							float num70 = dust.scale;
							if (num70 > 1f)
							{
								num70 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num70, num70 * 0.1f, num70 * 0.8f);
							if (dust.noGravity)
							{
								if (dust.scale < 0.7f)
								{
									dust.velocity *= 1.075f;
								}
								else if (Main.rand.Next(2) == 0)
								{
									dust.velocity *= -0.95f;
								}
								else
								{
									dust.velocity *= 1.05f;
								}
								dust.scale -= 0.03f;
							}
							else
							{
								dust.scale -= 0.005f;
								dust.velocity *= 0.9f;
								Dust dust11 = dust;
								dust11.velocity.X = dust11.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
								Dust dust12 = dust;
								dust12.velocity.Y = dust12.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
								if (Main.rand.Next(5) == 0)
								{
									int num71 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
									Main.dust[num71].noGravity = true;
									Main.dust[num71].scale = dust.scale * 2.5f;
								}
							}
						}
						else if (dust.type == 173)
						{
							float num72 = dust.scale;
							if (num72 > 1f)
							{
								num72 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num72 * 0.4f, num72 * 0.1f, num72);
							if (dust.noGravity)
							{
								dust.velocity *= 0.8f;
								Dust dust13 = dust;
								dust13.velocity.X = dust13.velocity.X + (float)Main.rand.Next(-20, 21) * 0.01f;
								Dust dust14 = dust;
								dust14.velocity.Y = dust14.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.01f;
								dust.scale -= 0.01f;
							}
							else
							{
								dust.scale -= 0.015f;
								dust.velocity *= 0.8f;
								Dust dust15 = dust;
								dust15.velocity.X = dust15.velocity.X + (float)Main.rand.Next(-10, 11) * 0.005f;
								Dust dust16 = dust;
								dust16.velocity.Y = dust16.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.005f;
								if (Main.rand.Next(10) == 10)
								{
									int num73 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
									Main.dust[num73].noGravity = true;
									Main.dust[num73].scale = dust.scale;
								}
							}
						}
						else if (dust.type == 304)
						{
							dust.velocity.Y = (float)Math.Sin((double)dust.rotation) / 5f;
							dust.rotation += 0.015f;
							if (dust.scale < 1.15f)
							{
								dust.alpha = Math.Max(0, dust.alpha - 20);
								dust.scale += 0.0015f;
							}
							else
							{
								dust.alpha += 6;
								if (dust.alpha >= 255)
								{
									dust.active = false;
								}
							}
							if (dust.customData != null && dust.customData is Player)
							{
								Player player7 = (Player)dust.customData;
								float num74 = Utils.Remap(dust.scale, 1f, 1.05f, 1f, 0f, true);
								if (num74 > 0f)
								{
									dust.position += player7.velocity * num74;
								}
								float num75 = player7.Center.X - dust.position.X;
								if (Math.Abs(num75) > 20f)
								{
									float num76 = num75 * 0.01f;
									dust.velocity.X = MathHelper.Lerp(dust.velocity.X, num76, num74 * 0.2f);
								}
							}
						}
						else if (dust.type == 184)
						{
							if (!dust.noGravity)
							{
								dust.velocity *= 0f;
								dust.scale -= 0.01f;
							}
						}
						else if (dust.type == 160 || dust.type == 162)
						{
							float num77 = dust.scale * 1.3f;
							if (num77 > 1f)
							{
								num77 = 1f;
							}
							if (dust.type == 162)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num77, num77 * 0.7f, num77 * 0.1f);
							}
							else
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num77 * 0.1f, num77, num77);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.8f;
								Dust dust17 = dust;
								dust17.velocity.X = dust17.velocity.X + (float)Main.rand.Next(-20, 21) * 0.04f;
								Dust dust18 = dust;
								dust18.velocity.Y = dust18.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.04f;
								dust.scale -= 0.1f;
							}
							else
							{
								dust.scale -= 0.1f;
								Dust dust19 = dust;
								dust19.velocity.X = dust19.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
								Dust dust20 = dust;
								dust20.velocity.Y = dust20.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
								if ((double)dust.scale > 0.3 && Main.rand.Next(50) == 0)
								{
									int num78 = Dust.NewDust(new Vector2(dust.position.X - 4f, dust.position.Y - 4f), 1, 1, dust.type, 0f, 0f, 0, default(Color), 1f);
									Main.dust[num78].noGravity = true;
									Main.dust[num78].scale = dust.scale * 1.5f;
								}
							}
						}
						else if (dust.type == 168)
						{
							float num79 = dust.scale * 0.8f;
							if ((double)num79 > 0.55)
							{
								num79 = 0.55f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num79, 0f, num79 * 0.8f);
							dust.scale += 0.03f;
							Dust dust21 = dust;
							dust21.velocity.X = dust21.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
							Dust dust22 = dust;
							dust22.velocity.Y = dust22.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
							dust.velocity *= 0.99f;
						}
						else if (dust.type >= 139 && dust.type < 143)
						{
							Dust dust23 = dust;
							dust23.velocity.X = dust23.velocity.X * 0.98f;
							Dust dust24 = dust;
							dust24.velocity.Y = dust24.velocity.Y * 0.98f;
							if (dust.velocity.Y < 1f)
							{
								Dust dust25 = dust;
								dust25.velocity.Y = dust25.velocity.Y + 0.05f;
							}
							dust.scale += 0.009f;
							dust.rotation -= dust.velocity.X * 0.4f;
							if (dust.velocity.X > 0f)
							{
								dust.rotation += 0.005f;
							}
							else
							{
								dust.rotation -= 0.005f;
							}
						}
						else if (dust.type == 326 || dust.type == 327 || dust.type == 328 || dust.type == 14 || dust.type == 16 || dust.type == 31 || dust.type == 46 || dust.type == 124 || dust.type == 186 || dust.type == 188 || dust.type == 303)
						{
							Dust dust26 = dust;
							dust26.velocity.Y = dust26.velocity.Y * 0.98f;
							Dust dust27 = dust;
							dust27.velocity.X = dust27.velocity.X * 0.98f;
							if (dust.type == 31)
							{
								if (dust.customData != null && dust.customData is float)
								{
									float num80 = (float)dust.customData;
									Dust dust28 = dust;
									dust28.velocity.Y = dust28.velocity.Y + num80;
								}
								if (dust.customData != null && dust.customData is NPC)
								{
									NPC npc2 = (NPC)dust.customData;
									dust.position += npc2.position - npc2.oldPosition;
									if (dust.noGravity)
									{
										dust.velocity *= 1.02f;
									}
									dust.alpha -= 70;
									if (dust.alpha < 0)
									{
										dust.alpha = 0;
									}
									dust.scale *= 0.97f;
									if (dust.scale <= 0.01f)
									{
										dust.scale = 0.0001f;
										dust.alpha = 255;
									}
								}
								else if (dust.noGravity)
								{
									dust.velocity *= 1.02f;
									dust.scale += 0.02f;
									dust.alpha += 4;
									if (dust.alpha > 255)
									{
										dust.scale = 0.0001f;
										dust.alpha = 255;
									}
								}
							}
							if (dust.type == 303 && dust.noGravity)
							{
								dust.velocity *= 1.02f;
								dust.scale += 0.03f;
								if (dust.alpha < 90)
								{
									dust.alpha = 90;
								}
								dust.alpha += 4;
								if (dust.alpha > 255)
								{
									dust.scale = 0.0001f;
									dust.alpha = 255;
								}
							}
						}
						else if (dust.type == 32)
						{
							dust.scale -= 0.01f;
							Dust dust29 = dust;
							dust29.velocity.X = dust29.velocity.X * 0.96f;
							if (!dust.noGravity)
							{
								Dust dust30 = dust;
								dust30.velocity.Y = dust30.velocity.Y + 0.1f;
							}
						}
						else if (dust.type >= 244 && dust.type <= 247)
						{
							dust.rotation += 0.1f * dust.scale;
							Color color = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
							byte b = (color.R + color.G + color.B) / 3;
							float num81 = ((float)b / 270f + 1f) / 2f;
							float num82 = ((float)b / 270f + 1f) / 2f;
							float num83 = ((float)b / 270f + 1f) / 2f;
							num81 *= dust.scale * 0.9f;
							num82 *= dust.scale * 0.9f;
							num83 *= dust.scale * 0.9f;
							if (dust.alpha < 255)
							{
								dust.scale += 0.09f;
								if (dust.scale >= 1f)
								{
									dust.scale = 1f;
									dust.alpha = 255;
								}
							}
							else
							{
								if ((double)dust.scale < 0.8)
								{
									dust.scale -= 0.01f;
								}
								if ((double)dust.scale < 0.5)
								{
									dust.scale -= 0.01f;
								}
							}
							float num84 = 1f;
							if (dust.type == 244)
							{
								num81 *= 0.8862745f;
								num82 *= 0.4627451f;
								num83 *= 0.29803923f;
								num84 = 0.9f;
							}
							else if (dust.type == 245)
							{
								num81 *= 0.5137255f;
								num82 *= 0.6745098f;
								num83 *= 0.6784314f;
								num84 = 1f;
							}
							else if (dust.type == 246)
							{
								num81 *= 0.8f;
								num82 *= 0.70980394f;
								num83 *= 0.28235295f;
								num84 = 1.1f;
							}
							else if (dust.type == 247)
							{
								num81 *= 0.6f;
								num82 *= 0.6745098f;
								num83 *= 0.7254902f;
								num84 = 1.2f;
							}
							num81 *= num84;
							num82 *= num84;
							num83 *= num84;
							if (!dust.noLightEmittance)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num81, num82, num83);
							}
						}
						else if (dust.type == 43)
						{
							dust.rotation += 0.1f * dust.scale;
							Color color2 = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
							float num85 = (float)color2.R / 270f;
							float num86 = (float)color2.G / 270f;
							float num87 = (float)color2.B / 270f;
							float num88 = (float)dust.color.R / 255f;
							float num89 = (float)dust.color.G / 255f;
							float num90 = (float)dust.color.B / 255f;
							num85 *= dust.scale * 1.07f * num88;
							num86 *= dust.scale * 1.07f * num89;
							num87 *= dust.scale * 1.07f * num90;
							if (dust.alpha < 255)
							{
								dust.scale += 0.09f;
								if (dust.scale >= 1f)
								{
									dust.scale = 1f;
									dust.alpha = 255;
								}
							}
							else
							{
								if ((double)dust.scale < 0.8)
								{
									dust.scale -= 0.01f;
								}
								if ((double)dust.scale < 0.5)
								{
									dust.scale -= 0.01f;
								}
							}
							if ((double)num85 < 0.05 && (double)num86 < 0.05 && (double)num87 < 0.05)
							{
								dust.active = false;
							}
							else if (!dust.noLightEmittance)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num85, num86, num87);
							}
							if (dust.customData != null && dust.customData is Player)
							{
								Player player8 = (Player)dust.customData;
								dust.position += player8.position - player8.oldPosition;
							}
						}
						else if (dust.type == 15 || dust.type == 57 || dust.type == 58 || dust.type == 274 || dust.type == 292)
						{
							Dust dust31 = dust;
							dust31.velocity.Y = dust31.velocity.Y * 0.98f;
							Dust dust32 = dust;
							dust32.velocity.X = dust32.velocity.X * 0.98f;
							if (!dust.noLightEmittance)
							{
								float num91 = dust.scale;
								if (dust.type != 15)
								{
									num91 = dust.scale * 0.8f;
								}
								if (dust.noLight)
								{
									dust.velocity *= 0.95f;
								}
								if (num91 > 1f)
								{
									num91 = 1f;
								}
								if (dust.type == 15)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num91 * 0.45f, num91 * 0.55f, num91);
								}
								else if (dust.type == 57)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num91 * 0.95f, num91 * 0.95f, num91 * 0.45f);
								}
								else if (dust.type == 58)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num91, num91 * 0.55f, num91 * 0.75f);
								}
							}
						}
						else if (dust.type == 204)
						{
							if (dust.fadeIn > dust.scale)
							{
								dust.scale += 0.02f;
							}
							else
							{
								dust.scale -= 0.02f;
							}
							dust.velocity *= 0.95f;
						}
						else if (dust.type == 110)
						{
							float num92 = dust.scale * 0.1f;
							if (num92 > 1f)
							{
								num92 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92 * 0.2f, num92, num92 * 0.5f);
						}
						else if (dust.type == 111)
						{
							float num93 = dust.scale * 0.125f;
							if (num93 > 1f)
							{
								num93 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num93 * 0.2f, num93 * 0.7f, num93);
						}
						else if (dust.type == 112)
						{
							float num94 = dust.scale * 0.1f;
							if (num94 > 1f)
							{
								num94 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94 * 0.8f, num94 * 0.2f, num94 * 0.8f);
						}
						else if (dust.type == 113)
						{
							float num95 = dust.scale * 0.1f;
							if (num95 > 1f)
							{
								num95 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num95 * 0.2f, num95 * 0.3f, num95 * 1.3f);
						}
						else if (dust.type == 114)
						{
							float num96 = dust.scale * 0.1f;
							if (num96 > 1f)
							{
								num96 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96 * 1.2f, num96 * 0.5f, num96 * 0.4f);
						}
						else if (dust.type == 311)
						{
							float num97 = dust.scale * 0.1f;
							if (num97 > 1f)
							{
								num97 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 16, num97);
						}
						else if (dust.type == 312)
						{
							float num98 = dust.scale * 0.1f;
							if (num98 > 1f)
							{
								num98 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 9, num98);
						}
						else if (dust.type == 313)
						{
							float num99 = dust.scale * 0.25f;
							if (num99 > 1f)
							{
								num99 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num99 * 1f, num99 * 0.8f, num99 * 0.6f);
						}
						else if (dust.type == 66)
						{
							if (dust.velocity.X < 0f)
							{
								dust.rotation -= 1f;
							}
							else
							{
								dust.rotation += 1f;
							}
							Dust dust33 = dust;
							dust33.velocity.Y = dust33.velocity.Y * 0.98f;
							Dust dust34 = dust;
							dust34.velocity.X = dust34.velocity.X * 0.98f;
							dust.scale += 0.02f;
							float num100 = dust.scale;
							if (dust.type != 15)
							{
								num100 = dust.scale * 0.8f;
							}
							if (num100 > 1f)
							{
								num100 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num100 * ((float)dust.color.R / 255f), num100 * ((float)dust.color.G / 255f), num100 * ((float)dust.color.B / 255f));
						}
						else if (dust.type == 267)
						{
							if (dust.velocity.X < 0f)
							{
								dust.rotation -= 1f;
							}
							else
							{
								dust.rotation += 1f;
							}
							Dust dust35 = dust;
							dust35.velocity.Y = dust35.velocity.Y * 0.98f;
							Dust dust36 = dust;
							dust36.velocity.X = dust36.velocity.X * 0.98f;
							dust.scale += 0.02f;
							float num101 = dust.scale * 0.8f;
							if (num101 > 1f)
							{
								num101 = 1f;
							}
							if (dust.noLight)
							{
								dust.noLight = false;
							}
							if (!dust.noLight && !dust.noLightEmittance)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num101 * ((float)dust.color.R / 255f), num101 * ((float)dust.color.G / 255f), num101 * ((float)dust.color.B / 255f));
							}
						}
						else if (dust.type == 20 || dust.type == 21 || dust.type == 231)
						{
							dust.scale += 0.005f;
							Dust dust37 = dust;
							dust37.velocity.Y = dust37.velocity.Y * 0.94f;
							Dust dust38 = dust;
							dust38.velocity.X = dust38.velocity.X * 0.94f;
							float num102 = dust.scale * 0.8f;
							if (num102 > 1f)
							{
								num102 = 1f;
							}
							if (dust.type == 21 && !dust.noLightEmittance)
							{
								num102 = dust.scale * 0.4f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num102 * 0.8f, num102 * 0.3f, num102);
							}
							else if (dust.type == 231)
							{
								num102 = dust.scale * 0.4f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num102, num102 * 0.5f, num102 * 0.3f);
							}
							else
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num102 * 0.3f, num102 * 0.6f, num102);
							}
						}
						else if (dust.type == 27 || dust.type == 45)
						{
							if (dust.type == 27 && dust.fadeIn >= 100f)
							{
								if ((double)dust.scale >= 1.5)
								{
									dust.scale -= 0.01f;
								}
								else
								{
									dust.scale -= 0.05f;
								}
								if ((double)dust.scale <= 0.5)
								{
									dust.scale -= 0.05f;
								}
								if ((double)dust.scale <= 0.25)
								{
									dust.scale -= 0.05f;
								}
							}
							dust.velocity *= 0.94f;
							dust.scale += 0.002f;
							float num103 = dust.scale;
							if (dust.noLight)
							{
								num103 *= 0.1f;
								dust.scale -= 0.06f;
								if (dust.scale < 1f)
								{
									dust.scale -= 0.06f;
								}
								if (Main.player[Main.myPlayer].wet)
								{
									dust.position += Main.player[Main.myPlayer].velocity * 0.5f;
								}
								else
								{
									dust.position += Main.player[Main.myPlayer].velocity;
								}
							}
							if (num103 > 1f)
							{
								num103 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num103 * 0.6f, num103 * 0.2f, num103);
						}
						else if (dust.type == 55 || dust.type == 56 || dust.type == 73 || dust.type == 74)
						{
							dust.velocity *= 0.98f;
							if (!dust.noLightEmittance)
							{
								float num104 = dust.scale * 0.8f;
								if (dust.type == 55)
								{
									if (num104 > 1f)
									{
										num104 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num104, num104, num104 * 0.6f);
								}
								else if (dust.type == 73)
								{
									if (num104 > 1f)
									{
										num104 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num104, num104 * 0.35f, num104 * 0.5f);
								}
								else if (dust.type == 74)
								{
									if (num104 > 1f)
									{
										num104 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num104 * 0.35f, num104, num104 * 0.5f);
								}
								else
								{
									num104 = dust.scale * 1.2f;
									if (num104 > 1f)
									{
										num104 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num104 * 0.35f, num104 * 0.5f, num104);
								}
							}
						}
						else if (dust.type == 71 || dust.type == 72)
						{
							dust.velocity *= 0.98f;
							float num105 = dust.scale;
							if (num105 > 1f)
							{
								num105 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num105 * 0.2f, 0f, num105 * 0.1f);
						}
						else if (dust.type == 76)
						{
							Main.snowDust++;
							dust.scale += 0.009f;
							float y = Main.player[Main.myPlayer].velocity.Y;
							if (y > 0f && dust.fadeIn == 0f && dust.velocity.Y < y)
							{
								dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y, 0.04f);
							}
							if (!dust.noLight && y > 0f)
							{
								Dust dust39 = dust;
								dust39.position.Y = dust39.position.Y + Main.player[Main.myPlayer].velocity.Y * 0.2f;
							}
							if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
							{
								dust.scale *= 0.9f;
								dust.velocity *= 0.25f;
							}
						}
						else if (dust.type == 270)
						{
							dust.velocity *= 1.0050251f;
							dust.scale += 0.01f;
							dust.rotation = 0f;
							if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
							{
								dust.scale *= 0.95f;
								dust.velocity *= 0.25f;
							}
							else
							{
								dust.velocity.Y = (float)Math.Sin((double)(dust.position.X * 0.0043982295f)) * 2f;
								Dust dust40 = dust;
								dust40.velocity.Y = dust40.velocity.Y - 3f;
								Dust dust41 = dust;
								dust41.velocity.Y = dust41.velocity.Y / 20f;
							}
						}
						else if (dust.type == 271)
						{
							dust.velocity *= 1.0050251f;
							dust.scale += 0.003f;
							dust.rotation = 0f;
							Dust dust42 = dust;
							dust42.velocity.Y = dust42.velocity.Y - 4f;
							Dust dust43 = dust;
							dust43.velocity.Y = dust43.velocity.Y / 6f;
						}
						else if (dust.type == 268)
						{
							Dust.SandStormCount++;
							dust.velocity *= 1.0050251f;
							dust.scale += 0.01f;
							if (!flag)
							{
								dust.scale -= 0.05f;
							}
							dust.rotation = 0f;
							float y2 = Main.player[Main.myPlayer].velocity.Y;
							if (y2 > 0f && dust.fadeIn == 0f && dust.velocity.Y < y2)
							{
								dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y2, 0.04f);
							}
							if (!dust.noLight && y2 > 0f)
							{
								Dust dust44 = dust;
								dust44.position.Y = dust44.position.Y + y2 * 0.2f;
							}
							if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
							{
								dust.scale *= 0.9f;
								dust.velocity *= 0.25f;
							}
							else
							{
								dust.velocity.Y = (float)Math.Sin((double)(dust.position.X * 0.0043982295f)) * 2f;
								Dust dust45 = dust;
								dust45.velocity.Y = dust45.velocity.Y + 3f;
							}
						}
						else if (!dust.noGravity && dust.type != 41 && dust.type != 44 && dust.type != 309)
						{
							if (dust.type == 107)
							{
								dust.velocity *= 0.9f;
							}
							else
							{
								Dust dust46 = dust;
								dust46.velocity.Y = dust46.velocity.Y + 0.1f;
							}
						}
						if ((dust.type == 5 || dust.type == 273) && dust.noGravity)
						{
							dust.scale -= 0.04f;
						}
						if (dust.type == 308 || dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105 || dust.type == 123 || dust.type == 288)
						{
							if (dust.velocity.X == 0f)
							{
								if (Collision.SolidCollision(dust.position, 2, 2))
								{
									dust.scale = 0f;
								}
								dust.rotation += 0.5f;
								dust.scale -= 0.01f;
							}
							if (Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y), 4, 4))
							{
								dust.alpha += 20;
								dust.scale -= 0.1f;
							}
							dust.alpha += 2;
							dust.scale -= 0.005f;
							if (dust.alpha > 255)
							{
								dust.scale = 0f;
							}
							if (dust.velocity.Y > 4f)
							{
								dust.velocity.Y = 4f;
							}
							if (dust.noGravity)
							{
								if (dust.velocity.X < 0f)
								{
									dust.rotation -= 0.2f;
								}
								else
								{
									dust.rotation += 0.2f;
								}
								dust.scale += 0.03f;
								Dust dust47 = dust;
								dust47.velocity.X = dust47.velocity.X * 1.05f;
								Dust dust48 = dust;
								dust48.velocity.Y = dust48.velocity.Y + 0.15f;
							}
						}
						if (dust.type == 35 && dust.noGravity)
						{
							dust.scale += 0.03f;
							if (dust.scale < 1f)
							{
								Dust dust49 = dust;
								dust49.velocity.Y = dust49.velocity.Y + 0.075f;
							}
							Dust dust50 = dust;
							dust50.velocity.X = dust50.velocity.X * 1.08f;
							if (dust.velocity.X > 0f)
							{
								dust.rotation += 0.01f;
							}
							else
							{
								dust.rotation -= 0.01f;
							}
							float num106 = dust.scale * 0.6f;
							if (num106 > 1f)
							{
								num106 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f + 1f), num106, num106 * 0.3f, num106 * 0.1f);
						}
						else if (dust.type == 152 && dust.noGravity)
						{
							dust.scale += 0.03f;
							if (dust.scale < 1f)
							{
								Dust dust51 = dust;
								dust51.velocity.Y = dust51.velocity.Y + 0.075f;
							}
							Dust dust52 = dust;
							dust52.velocity.X = dust52.velocity.X * 1.08f;
							if (dust.velocity.X > 0f)
							{
								dust.rotation += 0.01f;
							}
							else
							{
								dust.rotation -= 0.01f;
							}
						}
						else if (dust.type == 67 || dust.type == 92)
						{
							float num107 = dust.scale;
							if (num107 > 1f)
							{
								num107 = 1f;
							}
							if (dust.noLight)
							{
								num107 *= 0.1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, num107 * 0.8f, num107);
						}
						else if (dust.type == 185)
						{
							float num108 = dust.scale;
							if (num108 > 1f)
							{
								num108 = 1f;
							}
							if (dust.noLight)
							{
								num108 *= 0.1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num108 * 0.1f, num108 * 0.7f, num108);
						}
						else if (dust.type == 107)
						{
							float num109 = dust.scale * 0.5f;
							if (num109 > 1f)
							{
								num109 = 1f;
							}
							if (!dust.noLightEmittance)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num109 * 0.1f, num109, num109 * 0.4f);
							}
						}
						else if (dust.type == 34 || dust.type == 35 || dust.type == 152)
						{
							if (!Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
							{
								dust.scale = 0f;
							}
							else
							{
								dust.alpha += Main.rand.Next(2);
								if (dust.alpha > 255)
								{
									dust.scale = 0f;
								}
								dust.velocity.Y = -0.5f;
								if (dust.type == 34)
								{
									dust.scale += 0.005f;
								}
								else
								{
									dust.alpha++;
									dust.scale -= 0.01f;
									dust.velocity.Y = -0.2f;
								}
								Dust dust53 = dust;
								dust53.velocity.X = dust53.velocity.X + (float)Main.rand.Next(-10, 10) * 0.002f;
								if ((double)dust.velocity.X < -0.25)
								{
									dust.velocity.X = -0.25f;
								}
								if ((double)dust.velocity.X > 0.25)
								{
									dust.velocity.X = 0.25f;
								}
							}
							if (dust.type == 35)
							{
								float num110 = dust.scale * 0.3f + 0.4f;
								if (num110 > 1f)
								{
									num110 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110, num110 * 0.5f, num110 * 0.3f);
							}
						}
						if (dust.type == 68)
						{
							float num111 = dust.scale * 0.3f;
							if (num111 > 1f)
							{
								num111 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num111 * 0.1f, num111 * 0.2f, num111);
						}
						if (dust.type == 70)
						{
							float num112 = dust.scale * 0.3f;
							if (num112 > 1f)
							{
								num112 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num112 * 0.5f, 0f, num112);
						}
						if (dust.type == 41)
						{
							Dust dust54 = dust;
							dust54.velocity.X = dust54.velocity.X + (float)Main.rand.Next(-10, 11) * 0.01f;
							Dust dust55 = dust;
							dust55.velocity.Y = dust55.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.01f;
							if ((double)dust.velocity.X > 0.75)
							{
								dust.velocity.X = 0.75f;
							}
							if ((double)dust.velocity.X < -0.75)
							{
								dust.velocity.X = -0.75f;
							}
							if ((double)dust.velocity.Y > 0.75)
							{
								dust.velocity.Y = 0.75f;
							}
							if ((double)dust.velocity.Y < -0.75)
							{
								dust.velocity.Y = -0.75f;
							}
							dust.scale += 0.007f;
							float num113 = dust.scale * 0.7f;
							if (num113 > 1f)
							{
								num113 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num113 * 0.4f, num113 * 0.9f, num113);
						}
						else if (dust.type == 44)
						{
							Dust dust56 = dust;
							dust56.velocity.X = dust56.velocity.X + (float)Main.rand.Next(-10, 11) * 0.003f;
							Dust dust57 = dust;
							dust57.velocity.Y = dust57.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.003f;
							if ((double)dust.velocity.X > 0.35)
							{
								dust.velocity.X = 0.35f;
							}
							if ((double)dust.velocity.X < -0.35)
							{
								dust.velocity.X = -0.35f;
							}
							if ((double)dust.velocity.Y > 0.35)
							{
								dust.velocity.Y = 0.35f;
							}
							if ((double)dust.velocity.Y < -0.35)
							{
								dust.velocity.Y = -0.35f;
							}
							dust.scale += 0.0085f;
							float num114 = dust.scale * 0.7f;
							if (num114 > 1f)
							{
								num114 = 1f;
							}
							Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num114 * 0.7f, num114, num114 * 0.8f);
						}
						else if (dust.type != 304)
						{
							Dust dust58 = dust;
							dust58.velocity.X = dust58.velocity.X * 0.99f;
						}
						if (dust.type == 322 && !dust.noGravity)
						{
							dust.scale *= 0.98f;
						}
						if (dust.type != 79 && dust.type != 268 && dust.type != 304)
						{
							dust.rotation += dust.velocity.X * 0.5f;
						}
						if (dust.fadeIn > 0f && dust.fadeIn < 100f)
						{
							if (dust.type == 235)
							{
								dust.scale += 0.007f;
								int num115 = (int)dust.fadeIn - 1;
								if (num115 >= 0 && num115 <= 255)
								{
									Vector2 vector9 = dust.position - Main.player[num115].Center;
									float num116 = vector9.Length();
									num116 = 100f - num116;
									if (num116 > 0f)
									{
										dust.scale -= num116 * 0.0015f;
									}
									vector9.Normalize();
									float num117 = (1f - dust.scale) * 20f;
									vector9 *= -num117;
									dust.velocity = (dust.velocity * 4f + vector9) / 5f;
								}
							}
							else if (dust.type == 46)
							{
								dust.scale += 0.1f;
							}
							else if (dust.type == 213 || dust.type == 260)
							{
								dust.scale += 0.1f;
							}
							else
							{
								dust.scale += 0.03f;
							}
							if (dust.scale > dust.fadeIn)
							{
								dust.fadeIn = 0f;
							}
						}
						else if (dust.type != 304)
						{
							if (dust.type == 213 || dust.type == 260)
							{
								dust.scale -= 0.2f;
							}
							else
							{
								dust.scale -= 0.01f;
							}
						}
						if (dust.type >= 130 && dust.type <= 134)
						{
							float num118 = dust.scale;
							if (num118 > 1f)
							{
								num118 = 1f;
							}
							if (dust.type == 130)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num118 * 1f, num118 * 0.5f, num118 * 0.4f);
							}
							if (dust.type == 131)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num118 * 0.4f, num118 * 1f, num118 * 0.6f);
							}
							if (dust.type == 132)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num118 * 0.3f, num118 * 0.5f, num118 * 1f);
							}
							if (dust.type == 133)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num118 * 0.9f, num118 * 0.9f, num118 * 0.3f);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							else if (dust.type == 131)
							{
								dust.velocity *= 0.98f;
								Dust dust59 = dust;
								dust59.velocity.Y = dust59.velocity.Y - 0.1f;
								dust.scale += 0.0025f;
							}
							else
							{
								dust.velocity *= 0.95f;
								dust.scale -= 0.0025f;
							}
						}
						else if (dust.type == 278)
						{
							float num119 = dust.scale;
							if (num119 > 1f)
							{
								num119 = 1f;
							}
							if (!dust.noLight && !dust.noLightEmittance)
							{
								Lighting.AddLight(dust.position, dust.color.ToVector3() * num119);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							else
							{
								dust.velocity *= 0.95f;
								dust.scale -= 0.0025f;
							}
							if (WorldGen.SolidTile(Framing.GetTileSafely(dust.position)) && dust.fadeIn == 0f && !dust.noGravity)
							{
								dust.scale *= 0.9f;
								dust.velocity *= 0.25f;
							}
						}
						else if (dust.type >= 219 && dust.type <= 223)
						{
							float num120 = dust.scale;
							if (num120 > 1f)
							{
								num120 = 1f;
							}
							if (!dust.noLight)
							{
								if (dust.type == 219)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num120 * 1f, num120 * 0.5f, num120 * 0.4f);
								}
								if (dust.type == 220)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num120 * 0.4f, num120 * 1f, num120 * 0.6f);
								}
								if (dust.type == 221)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num120 * 0.3f, num120 * 0.5f, num120 * 1f);
								}
								if (dust.type == 222)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num120 * 0.9f, num120 * 0.9f, num120 * 0.3f);
								}
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							dust.velocity *= new Vector2(0.97f, 0.99f);
							dust.scale -= 0.0025f;
							if (dust.customData != null && dust.customData is Player)
							{
								Player player9 = (Player)dust.customData;
								dust.position += player9.position - player9.oldPosition;
							}
						}
						else if (dust.type == 226)
						{
							float num121 = dust.scale;
							if (num121 > 1f)
							{
								num121 = 1f;
							}
							if (!dust.noLight)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num121 * 0.2f, num121 * 0.7f, num121 * 1f);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							dust.velocity *= new Vector2(0.97f, 0.99f);
							if (dust.customData != null && dust.customData is Player)
							{
								Player player10 = (Player)dust.customData;
								dust.position += player10.position - player10.oldPosition;
							}
							if (dust.customData != null && dust.customData is Color)
							{
								Color color3 = (Color)dust.customData;
								if (!dust.noLightEmittance)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num121 * (float)color3.R / 255f, num121 * (float)color3.G / 255f, num121 * (float)color3.B / 255f);
								}
							}
							dust.scale -= 0.01f;
						}
						else if (dust.type == 272)
						{
							float num122 = dust.scale;
							if (num122 > 1f)
							{
								num122 = 1f;
							}
							if (!dust.noLight)
							{
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num122 * 0.5f, num122 * 0.2f, num122 * 0.8f);
							}
							if (dust.noGravity)
							{
								dust.velocity *= 0.93f;
								if (dust.fadeIn == 0f)
								{
									dust.scale += 0.0025f;
								}
							}
							dust.velocity *= new Vector2(0.97f, 0.99f);
							if (dust.customData != null && dust.customData is Player)
							{
								Player player11 = (Player)dust.customData;
								dust.position += player11.position - player11.oldPosition;
							}
							if (dust.customData != null && dust.customData is NPC)
							{
								NPC npc3 = (NPC)dust.customData;
								dust.position += npc3.position - npc3.oldPosition;
							}
							dust.scale -= 0.01f;
						}
						else if (dust.type != 304 && dust.noGravity)
						{
							dust.velocity *= 0.92f;
							if (dust.fadeIn == 0f)
							{
								dust.scale -= 0.04f;
							}
						}
						if (dust.position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
						{
							dust.active = false;
						}
						float num123 = 0.1f;
						if ((double)Dust.dCount == 0.5)
						{
							dust.scale -= 0.001f;
						}
						if ((double)Dust.dCount == 0.6)
						{
							dust.scale -= 0.0025f;
						}
						if ((double)Dust.dCount == 0.7)
						{
							dust.scale -= 0.005f;
						}
						if ((double)Dust.dCount == 0.8)
						{
							dust.scale -= 0.01f;
						}
						if ((double)Dust.dCount == 0.9)
						{
							dust.scale -= 0.02f;
						}
						if ((double)Dust.dCount == 0.5)
						{
							num123 = 0.11f;
						}
						if ((double)Dust.dCount == 0.6)
						{
							num123 = 0.13f;
						}
						if ((double)Dust.dCount == 0.7)
						{
							num123 = 0.16f;
						}
						if ((double)Dust.dCount == 0.8)
						{
							num123 = 0.22f;
						}
						if ((double)Dust.dCount == 0.9)
						{
							num123 = 0.25f;
						}
						if (dust.scale < num123)
						{
							dust.active = false;
						}
					}
				}
				else
				{
					dust.active = false;
				}
			}
			int num124 = num;
			if ((double)num124 > (double)Main.maxDustToDraw * 0.9)
			{
				Dust.dCount = 0.9f;
				return;
			}
			if ((double)num124 > (double)Main.maxDustToDraw * 0.8)
			{
				Dust.dCount = 0.8f;
				return;
			}
			if ((double)num124 > (double)Main.maxDustToDraw * 0.7)
			{
				Dust.dCount = 0.7f;
				return;
			}
			if ((double)num124 > (double)Main.maxDustToDraw * 0.6)
			{
				Dust.dCount = 0.6f;
				return;
			}
			if ((double)num124 > (double)Main.maxDustToDraw * 0.5)
			{
				Dust.dCount = 0.5f;
				return;
			}
			Dust.dCount = 0f;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00352F34 File Offset: 0x00351134
		public Color GetAlpha(Color newColor)
		{
			if (this.fullBright)
			{
				return Color.White;
			}
			float num = (float)(255 - this.alpha) / 255f;
			int num2 = this.type;
			switch (num2)
			{
			case 299:
			case 300:
			case 301:
			case 305:
			{
				Color color = default(Color);
				switch (this.type)
				{
				case 299:
					color = new Color(50, 255, 50, 200);
					break;
				case 300:
					color = new Color(50, 200, 255, 255);
					break;
				case 301:
					color = new Color(255, 50, 125, 200);
					break;
				default:
					color = new Color(255, 150, 150, 200);
					break;
				case 305:
					color = new Color(200, 50, 200, 200);
					break;
				}
				return color;
			}
			case 302:
			case 303:
			case 304:
			case 306:
			case 307:
				break;
			case 308:
			case 309:
				return new Color(225, 200, 250, 190);
			default:
				if (num2 == 323)
				{
					return Color.White;
				}
				if (num2 == 324)
				{
					return new Color(225, 200, 250, 190) * num;
				}
				break;
			}
			if (this.type == 304)
			{
				return Color.White * num;
			}
			if (this.type == 306)
			{
				return this.color * num;
			}
			if (this.type == 292)
			{
				return Color.White;
			}
			if (this.type == 259)
			{
				return new Color(230, 230, 230, 230);
			}
			if (this.type == 261)
			{
				return new Color(230, 230, 230, 115);
			}
			if (this.type == 254 || this.type == 255)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 258)
			{
				return new Color(150, 50, 50, 0);
			}
			if (this.type == 263 || this.type == 264)
			{
				return new Color((int)(this.color.R / 2 + 127), (int)(this.color.G + 127), (int)(this.color.B + 127), (int)(this.color.A / 8)) * 0.5f;
			}
			if (this.type == 235)
			{
				return new Color(255, 255, 255, 0);
			}
			if (((this.type >= 86 && this.type <= 91) || this.type == 262 || this.type == 286 || this.type == 138 || this.type == 325) && !this.noLight)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 213 || this.type == 260)
			{
				int num3 = (int)(this.scale / 2.5f * 255f);
				return new Color(num3, num3, num3, num3);
			}
			if (this.type == 64 && this.alpha == 255 && this.noLight)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 197)
			{
				return new Color(250, 250, 250, 150);
			}
			if ((this.type >= 110 && this.type <= 114) || this.type == 311 || this.type == 312 || this.type == 313)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 204)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 181)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 182 || this.type == 206)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 159)
			{
				return new Color(250, 250, 250, 50);
			}
			if (this.type == 163 || this.type == 205)
			{
				return new Color(250, 250, 250, 0);
			}
			if (this.type == 170)
			{
				return new Color(200, 200, 200, 100);
			}
			if (this.type == 180)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 175)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 183)
			{
				return new Color(50, 0, 0, 0);
			}
			if (this.type == 172)
			{
				return new Color(250, 250, 250, 150);
			}
			if (this.type == 160 || this.type == 162 || this.type == 164 || this.type == 173)
			{
				int num4 = (int)(250f * this.scale);
				return new Color(num4, num4, num4, 0);
			}
			if (this.type == 92 || this.type == 106 || this.type == 107)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 185)
			{
				return new Color(200, 200, 255, 125);
			}
			if (this.type == 127 || this.type == 187)
			{
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 156 || this.type == 230 || this.type == 234)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 270)
			{
				return new Color((int)(newColor.R / 2 + 127), (int)(newColor.G / 2 + 127), (int)(newColor.B / 2 + 127), 25);
			}
			if (this.type == 271)
			{
				return new Color((int)(newColor.R / 2 + 127), (int)(newColor.G / 2 + 127), (int)(newColor.B / 2 + 127), 127);
			}
			if (this.type == 6 || this.type == 242 || this.type == 174 || this.type == 135 || this.type == 75 || this.type == 20 || this.type == 21 || this.type == 231 || this.type == 169 || (this.type >= 130 && this.type <= 134) || this.type == 158 || this.type == 293 || this.type == 294 || this.type == 295 || this.type == 296 || this.type == 297 || this.type == 298 || this.type == 307 || this.type == 310)
			{
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 278)
			{
				return new Color(newColor.ToVector3() * this.color.ToVector3())
				{
					A = 25
				};
			}
			if (this.type >= 219 && this.type <= 223)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.5f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 226 || this.type == 272)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 228)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 279)
			{
				int a = (int)newColor.A;
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, a) * MathHelper.Min(this.scale, 1f);
			}
			if (this.type == 229 || this.type == 269)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.6f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if ((this.type == 68 || this.type == 70) && this.noGravity)
			{
				return new Color(255, 255, 255, 0);
			}
			int num7;
			int num6;
			int num5;
			if (this.type == 157)
			{
				num5 = (num6 = (num7 = 255));
				float num8 = (float)Main.mouseTextColor / 100f - 1.6f;
				num6 = (int)((float)num6 * num8);
				num5 = (int)((float)num5 * num8);
				num7 = (int)((float)num7 * num8);
				int num9 = (int)(100f * num8);
				num6 += 50;
				if (num6 > 255)
				{
					num6 = 255;
				}
				num5 += 50;
				if (num5 > 255)
				{
					num5 = 255;
				}
				num7 += 50;
				if (num7 > 255)
				{
					num7 = 255;
				}
				return new Color(num6, num5, num7, num9);
			}
			if (this.type == 284)
			{
				return new Color(newColor.ToVector4() * this.color.ToVector4())
				{
					A = this.color.A
				};
			}
			if (this.type == 327 && !Main.dayTime)
			{
				num6 = (int)(Main.rand.NextFloat() * 0.04f + 0.1f + (float)Main.DiscoR / 800f) * 255;
				num5 = (int)(Main.rand.NextFloat() * 0.04f + 0.1f + (float)Main.DiscoG / 800f) * 255;
				num7 = (int)(Main.rand.NextFloat() * 0.04f + 0.1f + (float)Main.DiscoB / 800f) * 255;
				if (num6 > (int)newColor.R)
				{
					newColor.R = (byte)num6;
				}
				if (num5 > (int)newColor.G)
				{
					newColor.G = (byte)num5;
				}
				if (num7 > (int)newColor.B)
				{
					newColor.B = (byte)num7;
				}
				return newColor;
			}
			if (this.type == 58)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 15 || this.type == 274 || this.type == 20 || this.type == 21 || this.type == 29 || this.type == 35 || this.type == 41 || this.type == 44 || this.type == 27 || this.type == 45 || this.type == 55 || this.type == 56 || this.type == 57 || this.type == 58 || this.type == 73 || this.type == 74)
			{
				num = (num + 3f) / 4f;
			}
			else if (this.type == 43)
			{
				num = (num + 9f) / 10f;
			}
			else
			{
				if (this.type >= 244 && this.type <= 247)
				{
					return new Color(255, 255, 255, 0);
				}
				if (this.type == 66)
				{
					return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 0);
				}
				if (this.type == 267)
				{
					return new Color((int)this.color.R, (int)this.color.G, (int)this.color.B, 0);
				}
				if (this.type == 71)
				{
					return new Color(200, 200, 200, 0);
				}
				if (this.type == 72)
				{
					return new Color(200, 200, 200, 200);
				}
			}
			num6 = (int)((float)newColor.R * num);
			num5 = (int)((float)newColor.G * num);
			num7 = (int)((float)newColor.B * num);
			int num10 = (int)newColor.A - this.alpha;
			if (num10 < 0)
			{
				num10 = 0;
			}
			if (num10 > 255)
			{
				num10 = 255;
			}
			return new Color(num6, num5, num7, num10);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00353D2C File Offset: 0x00351F2C
		public Color GetColor(Color newColor)
		{
			int num = this.type;
			if (num == 284)
			{
				return Color.Transparent;
			}
			int num2 = (int)(this.color.R - (byte.MaxValue - newColor.R));
			int num3 = (int)(this.color.G - (byte.MaxValue - newColor.G));
			int num4 = (int)(this.color.B - (byte.MaxValue - newColor.B));
			int num5 = (int)(this.color.A - (byte.MaxValue - newColor.A));
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			return new Color(num2, num3, num4, num5);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00353E11 File Offset: 0x00352011
		public float GetVisualRotation()
		{
			if (this.type == 304)
			{
				return 0f;
			}
			return this.rotation;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00353E2C File Offset: 0x0035202C
		public float GetVisualScale()
		{
			if (this.type == 304)
			{
				return 1f;
			}
			return this.scale;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0000357B File Offset: 0x0000177B
		public Dust()
		{
		}

		// Token: 0x04000994 RID: 2452
		public static float dCount;

		// Token: 0x04000995 RID: 2453
		public static int lavaBubbles;

		// Token: 0x04000996 RID: 2454
		public static int SandStormCount;

		// Token: 0x04000997 RID: 2455
		public int dustIndex;

		// Token: 0x04000998 RID: 2456
		public Vector2 position;

		// Token: 0x04000999 RID: 2457
		public Vector2 velocity;

		// Token: 0x0400099A RID: 2458
		public float fadeIn;

		// Token: 0x0400099B RID: 2459
		public bool noGravity;

		// Token: 0x0400099C RID: 2460
		public float scale;

		// Token: 0x0400099D RID: 2461
		public float rotation;

		// Token: 0x0400099E RID: 2462
		public bool noLight;

		// Token: 0x0400099F RID: 2463
		public bool noLightEmittance;

		// Token: 0x040009A0 RID: 2464
		public bool fullBright;

		// Token: 0x040009A1 RID: 2465
		public bool active;

		// Token: 0x040009A2 RID: 2466
		public int type;

		// Token: 0x040009A3 RID: 2467
		public Color color;

		// Token: 0x040009A4 RID: 2468
		public int alpha;

		// Token: 0x040009A5 RID: 2469
		public Rectangle frame;

		// Token: 0x040009A6 RID: 2470
		public ArmorShaderData shader;

		// Token: 0x040009A7 RID: 2471
		public object customData;

		// Token: 0x040009A8 RID: 2472
		public bool firstFrame;
	}
}
