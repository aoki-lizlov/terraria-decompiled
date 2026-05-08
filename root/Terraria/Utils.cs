using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using ReLogic.Graphics;
using ReLogic.OS;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Utilities;
using Terraria.Utilities.Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000053 RID: 83
	public static class Utils
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x000454EE File Offset: 0x000436EE
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0040B36E File Offset: 0x0040956E
		public static double Lerp(double value1, double value2, double amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0040B377 File Offset: 0x00409577
		public static Vector2 Round(Vector2 input)
		{
			return new Vector2((float)Math.Round((double)input.X), (float)Math.Round((double)input.Y));
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0040B398 File Offset: 0x00409598
		public static bool IsPowerOfTwo(int x)
		{
			return x != 0 && (x & (x - 1)) == 0;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0040B3A7 File Offset: 0x004095A7
		public static float SmoothStep(float min, float max, float x)
		{
			return MathHelper.Clamp((x - min) / (max - min), 0f, 1f);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0040B3BF File Offset: 0x004095BF
		public static double SmoothStep(double min, double max, double x)
		{
			return Utils.Clamp<double>((x - min) / (max - min), 0.0, 1.0);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0040B3DF File Offset: 0x004095DF
		public static float UnclampedSmoothStep(float min, float max, float x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0040B3DF File Offset: 0x004095DF
		public static double UnclampedSmoothStep(double min, double max, double x)
		{
			return (x - min) / (max - min);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0040B3E8 File Offset: 0x004095E8
		public static Dictionary<string, string> ParseArguements(string[] args)
		{
			string text = null;
			string text2 = "";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Length != 0)
				{
					if (args[i][0] == '-' || args[i][0] == '+')
					{
						if (text != null)
						{
							dictionary.Add(text.ToLower(), text2);
						}
						text = args[i];
						text2 = "";
					}
					else
					{
						if (text2 != "")
						{
							text2 += " ";
						}
						text2 += args[i];
					}
				}
			}
			if (text != null)
			{
				dictionary.Add(text.ToLower(), text2);
			}
			return dictionary;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0040B494 File Offset: 0x00409694
		public static void Swap<T>(ref T t1, ref T t2)
		{
			T t3 = t1;
			t1 = t2;
			t2 = t3;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0040B4BB File Offset: 0x004096BB
		public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
		{
			if (value.CompareTo(max) > 0)
			{
				return max;
			}
			if (value.CompareTo(min) < 0)
			{
				return min;
			}
			return value;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0040B4E4 File Offset: 0x004096E4
		public static Rectangle Clamp(Rectangle r, Rectangle bounds)
		{
			return new Rectangle(Utils.Clamp<int>(r.X, bounds.Left, bounds.Right - r.Width), Utils.Clamp<int>(r.Y, bounds.Top, bounds.Bottom - r.Height), r.Width, r.Height);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0040B542 File Offset: 0x00409742
		public static float Turn01ToCyclic010(float value)
		{
			return 1f - ((float)Math.Cos((double)(value * 6.2831855f)) * 0.5f + 0.5f);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0040B564 File Offset: 0x00409764
		public static float PingPongFrom01To010(float value)
		{
			value %= 1f;
			if (value < 0f)
			{
				value += 1f;
			}
			if (value >= 0.5f)
			{
				return 2f - value * 2f;
			}
			return value * 2f;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0040B5A0 File Offset: 0x004097A0
		public static void Shift<T>(T[] array, int n)
		{
			if (n == 0 || n >= array.Length || n <= -array.Length)
			{
				return;
			}
			if (n > 0)
			{
				if (n < array.Length)
				{
					Array.Copy(array, 0, array, n, array.Length - n);
					return;
				}
			}
			else if (n > -array.Length)
			{
				Array.Copy(array, -n, array, 0, array.Length + n);
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0040B5F0 File Offset: 0x004097F0
		public static float MultiLerp(float percent, params float[] floats)
		{
			float num = 1f / ((float)floats.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < floats.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return MathHelper.Lerp(floats[num3], floats[num3 + 1], (percent - num * (float)num3) / num);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0040B644 File Offset: 0x00409844
		public static Color MultiLerp(float percent, params Color[] colors)
		{
			float num = 1f / ((float)colors.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < colors.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return Color.Lerp(colors[num3], colors[num3 + 1], (percent - num * (float)num3) / num);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0040B6A0 File Offset: 0x004098A0
		public static float WrappedLerp(float value1, float value2, float percent)
		{
			float num = percent * 2f;
			if (num > 1f)
			{
				num = 2f - num;
			}
			return MathHelper.Lerp(value1, value2, num);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0040B6CD File Offset: 0x004098CD
		public static float GetLerpValue(float from, float to, float t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0f;
					}
					if (t > to)
					{
						return 1f;
					}
				}
				else
				{
					if (t < to)
					{
						return 1f;
					}
					if (t > from)
					{
						return 0f;
					}
				}
			}
			return (t - from) / (to - from);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0040B705 File Offset: 0x00409905
		public static float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax, bool clamped = true)
		{
			return MathHelper.Lerp(toMin, toMax, Utils.GetLerpValue(fromMin, fromMax, fromValue, clamped));
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0040B719 File Offset: 0x00409919
		public static double Remap(double fromValue, double fromMin, double fromMax, double toMin, double toMax, bool clamped = true)
		{
			return Utils.Lerp(toMin, toMax, Utils.GetLerpValue(fromMin, fromMax, fromValue, clamped));
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0040B72D File Offset: 0x0040992D
		public static double EaseOutBounce(double x)
		{
			return Utils.BounceEaseOut(x, 4, 2.0);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0040B740 File Offset: 0x00409940
		private static double BounceEaseOut(double t, int bounces, double elasticity)
		{
			double num = (double)bounces * 3.141592653589793;
			double num2 = Math.Pow(1.0 - t, elasticity);
			double num3 = Math.Abs(Math.Sin(t * num));
			return 1.0 - num2 * num3;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0040B787 File Offset: 0x00409987
		public static double EaseInCirc(double x)
		{
			return 1.0 - Math.Sqrt(1.0 - x * x);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0040B7A5 File Offset: 0x004099A5
		public static double EaseOutCirc(double x)
		{
			return Math.Sqrt(1.0 - Math.Pow(x - 1.0, 2.0));
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0040B7D0 File Offset: 0x004099D0
		public static void GetPortraitMovement(double t, out double offsetX, out double scaleX)
		{
			t %= 1.0;
			double num = 0.16666666666666666;
			int num2 = (int)(t / num);
			double num3 = t % num / num;
			offsetX = 0.0;
			scaleX = 1.0;
			switch (num2)
			{
			case 0:
				offsetX = 0.0;
				scaleX = 1.0 - 2.0 * num3;
				return;
			case 1:
				offsetX = 0.0 - 0.5 * Utils.EaseOutCirc(num3);
				scaleX = -1.0;
				return;
			case 2:
				offsetX = -0.5 - 0.5 * Utils.EaseOutCirc(num3);
				scaleX = -1.0;
				return;
			case 3:
				offsetX = -1.0;
				scaleX = -1.0 + 2.0 * num3;
				return;
			case 4:
				offsetX = -1.0 + 0.5 * Utils.EaseOutCirc(num3);
				scaleX = 1.0;
				return;
			case 5:
				offsetX = -0.5 + 0.5 * Utils.EaseOutCirc(num3);
				scaleX = 1.0;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0040B91C File Offset: 0x00409B1C
		public static Color ShiftHue(Color color, float hueShift, float luminanceShift, float saturationBoost)
		{
			Vector3 vector = Main.rgbToHsl(color);
			float num = (vector.X + hueShift) % 1f;
			if (num < 0f)
			{
				num += 1f;
			}
			return Main.hslToRgb(num, vector.Y + saturationBoost, Utils.Clamp<float>(vector.Z + luminanceShift, 0f, 1f), color.A);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0040B97C File Offset: 0x00409B7C
		public static Color ShiftBlueToCyanTheme(Color color)
		{
			return Utils.ShiftHue(color, -0.04f, 0.04f, 0.2f);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0040B9A0 File Offset: 0x00409BA0
		public static void ClampWithinWorld(ref int minX, ref int minY, ref int maxX, ref int maxY, bool lastValuesInclusiveToIteration = false, int fluffX = 0, int fluffY = 0)
		{
			int num = (lastValuesInclusiveToIteration ? 1 : 0);
			minX = Utils.Clamp<int>(minX, fluffX, Main.maxTilesX - num - fluffX);
			maxX = Utils.Clamp<int>(maxX, fluffX, Main.maxTilesX - num - fluffX);
			minY = Utils.Clamp<int>(minY, fluffY, Main.maxTilesY - num - fluffY);
			maxY = Utils.Clamp<int>(maxY, fluffY, Main.maxTilesY - num - fluffY);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0040BA0A File Offset: 0x00409C0A
		public static void DrawNotificationIcon(SpriteBatch spritebatch, Rectangle hitbox, float rotationMultiplier = 1f, bool worldSpace = false)
		{
			Utils.DrawNotificationIcon(spritebatch, hitbox.BottomRight() + new Vector2(-7f, -6f), rotationMultiplier, worldSpace);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0040BA30 File Offset: 0x00409C30
		public static void DrawNotificationIcon(SpriteBatch spritebatch, Vector2 position, float rotationMultiplier = 1f, bool worldSpace = false)
		{
			Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/UI_quickicon1", 1).Value;
			float num = (float)Math.Sin((double)(6.2831855f * (Main.GlobalTimeWrappedHourly % 1f / 1f))) * 0.5f + 0.5f;
			Color color = Color.White;
			float num2 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly % 2f / 2f * 6.2831855f)) * 6.2831855f * 0.035f * rotationMultiplier;
			if (worldSpace)
			{
				color = Lighting.GetColor(position.ToTileCoordinates());
				position -= Main.screenPosition;
				if (Main.LocalPlayer.gravDir == -1f)
				{
					num2 += 3.1415927f;
					position = Main.ReverseGravitySupport(position, 0f);
				}
			}
			Color color2 = color;
			color2.A /= 2;
			Color color3 = Color.Lerp(color, color2, num);
			spritebatch.Draw(value, position, null, color3, num2, new Vector2((float)(value.Width / 2), (float)(value.Height - 4)), 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0040BB48 File Offset: 0x00409D48
		public static Vector2 ConstrainedToPointInRectangle(Rectangle bounds, Vector2 centerTestPosition)
		{
			if (bounds.Contains(centerTestPosition.ToPoint()))
			{
				return centerTestPosition;
			}
			Vector2 vector = new Vector2((float)bounds.Center.X, (float)bounds.Center.Y);
			Vector2 vector2 = vector - centerTestPosition;
			float num = ((vector2.X == 0f) ? float.MaxValue : Math.Abs((vector.X - (float)(bounds.Width / 2) - centerTestPosition.X) / vector2.X));
			float num2 = ((vector2.Y == 0f) ? float.MaxValue : Math.Abs((vector.Y - (float)(bounds.Height / 2) - centerTestPosition.Y) / vector2.Y));
			float num3 = Math.Min(num, num2);
			Vector2 vector3 = centerTestPosition + vector2 * num3;
			vector3.X = MathHelper.Clamp(vector3.X, (float)bounds.Left, (float)bounds.Right);
			vector3.Y = MathHelper.Clamp(vector3.Y, (float)bounds.Top, (float)bounds.Bottom);
			return vector3;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0040BC5C File Offset: 0x00409E5C
		private static bool CheckForGoodTeleportationSpot_CheckNoInvalidTiles(int tpx, int tpy, Utils.RandomTeleportationAttemptSettings settings)
		{
			if (settings.tilesToAvoidRange > 0 && settings.tilesToAvoid != null)
			{
				int tilesToAvoidRange = settings.tilesToAvoidRange;
				for (int i = -tilesToAvoidRange; i <= tilesToAvoidRange; i++)
				{
					for (int j = -tilesToAvoidRange; j <= tilesToAvoidRange; j++)
					{
						int num = tpx + i;
						int num2 = tpy + j;
						if (WorldGen.InWorld(num, num2, 2))
						{
							Tile tile = Main.tile[num, num2];
							if (tile != null && tile.active())
							{
								ushort type = tile.type;
								for (int k = 0; k < settings.tilesToAvoid.Length; k++)
								{
									if ((int)type == settings.tilesToAvoid[k])
									{
										return false;
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0040BD04 File Offset: 0x00409F04
		public static Vector2 CheckForGoodTeleportationSpot(ref bool canSpawn, int teleportStartX, int teleportRangeX, int teleportStartY, int teleportRangeY, Utils.RandomTeleportationAttemptSettings settings)
		{
			int num = (int)settings.teleporteeSize.X;
			int num2 = (int)settings.teleporteeSize.Y;
			Vector2 teleporteeVelocity = settings.teleporteeVelocity;
			float teleporteeGravityDirection = settings.teleporteeGravityDirection;
			Rectangle rectangle = new Rectangle(teleportStartX, teleportStartY, teleportRangeX, teleportRangeY);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = num;
			Vector2 vector = new Vector2((float)num4, (float)num5) * 16f + new Vector2((float)(-(float)num6 / 2 + 8), (float)(-(float)num2));
			while (!canSpawn && num3 < settings.attemptsBeforeGivingUp)
			{
				num3++;
				num4 = teleportStartX + Main.rand.Next(teleportRangeX);
				num5 = teleportStartY + Main.rand.Next(teleportRangeY);
				int num7 = 45;
				num4 = (int)MathHelper.Clamp((float)num4, (float)num7, (float)(Main.maxTilesX - num7));
				num5 = (int)MathHelper.Clamp((float)num5, (float)num7, (float)(Main.maxTilesY - num7));
				if (!settings.strictRange || rectangle.Contains(new Point(num4, num5)))
				{
					vector = new Vector2((float)num4, (float)num5) * 16f + new Vector2((float)(-(float)num6 / 2 + 8), (float)(-(float)num2));
					if (!Collision.SolidCollision(vector, num6, num2))
					{
						if (Main.tile[num4, num5] == null)
						{
							Main.tile[num4, num5] = new Tile();
						}
						Tile tile = Main.tile[num4, num5];
						if ((!settings.avoidWalls || tile.wall <= 0) && (tile.wall != 87 || (double)num5 <= Main.worldSurface || NPC.downedPlantBoss) && (!Main.wallDungeon[(int)tile.wall] || (double)num5 <= Main.worldSurface || NPC.downedBoss3) && Utils.CheckForGoodTeleportationSpot_CheckNoInvalidTiles(num4, num5, settings))
						{
							bool flag = false;
							int i = 0;
							while (i < settings.maximumFallDistanceFromOrignalPoint)
							{
								if (settings.strictRange && !rectangle.Contains(new Point(num4, num5 + i)))
								{
									flag = true;
									break;
								}
								if (Main.tile[num4, num5 + i] == null)
								{
									Main.tile[num4, num5 + i] = new Tile();
								}
								Tile tile2 = Main.tile[num4, num5 + i];
								vector = new Vector2((float)num4, (float)(num5 + i)) * 16f + new Vector2((float)(-(float)num6 / 2 + 8), (float)(-(float)num2));
								Collision.SlopeCollision(vector, teleporteeVelocity, num6, num2, teleporteeGravityDirection, false, false);
								if (!Collision.SolidCollision(vector, num6, num2 + 1, settings.allowSolidTopFloor))
								{
									i++;
								}
								else
								{
									if (tile2.active() && !tile2.inActive() && Main.tileSolid[(int)tile2.type])
									{
										break;
									}
									i++;
								}
							}
							if (!flag)
							{
								int num8 = (int)vector.X / 16;
								int num9 = (int)vector.Y / 16;
								if (Utils.CheckForGoodTeleportationSpot_CheckNoInvalidTiles(num8, num9, settings))
								{
									int num10 = (int)(vector.X + (float)num6 * 0.5f) / 16;
									int num11 = (int)(vector.Y + (float)num2) / 16;
									Tile tileSafely = Framing.GetTileSafely(num8, num9);
									Tile tileSafely2 = Framing.GetTileSafely(num10, num11);
									if ((settings.specializedConditions == null || settings.specializedConditions(tileSafely2, num10, num11)) && (!settings.avoidAnyLiquid || tileSafely2.liquid <= 0))
									{
										if (settings.mostlySolidFloor)
										{
											Tile tileSafely3 = Framing.GetTileSafely(num10 - 1, num11);
											Tile tileSafely4 = Framing.GetTileSafely(num10 + 1, num11);
											bool flag2;
											bool flag3;
											if (settings.allowSolidTopFloor)
											{
												flag2 = !tileSafely3.inActive() && WorldGen.SolidTileAllowBottomSlope(num10 - 1, num11);
												flag3 = !tileSafely4.inActive() && WorldGen.SolidTileAllowBottomSlope(num10 + 1, num11);
											}
											else
											{
												flag2 = tileSafely3.active() && !tileSafely3.inActive() && Main.tileSolid[(int)tileSafely3.type] && !Main.tileSolidTop[(int)tileSafely3.type];
												flag3 = tileSafely4.active() && !tileSafely4.inActive() && Main.tileSolid[(int)tileSafely4.type] && !Main.tileSolidTop[(int)tileSafely4.type];
											}
											if (!flag2 && !flag3)
											{
												continue;
											}
										}
										if ((!settings.avoidWalls || tileSafely.wall <= 0) && (!settings.avoidAnyLiquid || !Collision.WetCollision(vector, num6, num2)) && (!settings.avoidLava || !Collision.LavaCollision(vector, num6, num2)) && (!settings.avoidHurtTiles || !Collision.AnyHurtingTiles(vector, num6, num2)) && !Collision.SolidCollision(vector, num6, num2, settings.allowSolidTopFloor) && i < settings.maximumFallDistanceFromOrignalPoint - 1)
										{
											Vector2 vector2 = Vector2.UnitX * 16f;
											if (!(Collision.TileCollision(vector - vector2, vector2, num, num2, false, false, (int)teleporteeGravityDirection, false, false, true) != vector2))
											{
												vector2 = -Vector2.UnitX * 16f;
												if (!(Collision.TileCollision(vector - vector2, vector2, num, num2, false, false, (int)teleporteeGravityDirection, false, false, true) != vector2))
												{
													vector2 = Vector2.UnitY * 16f;
													if (!(Collision.TileCollision(vector - vector2, vector2, num, num2, false, false, (int)teleporteeGravityDirection, false, false, true) != vector2))
													{
														vector2 = -Vector2.UnitY * 16f;
														if (!(Collision.TileCollision(vector - vector2, vector2, num, num2, false, false, (int)teleporteeGravityDirection, false, false, true) != vector2) && (!Main.dualDungeonsSeed || !UnbreakableWallScan.InsideUnbreakableWalls(new Point(num8, num9))))
														{
															canSpawn = true;
															num5 += i;
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return vector;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0040C2F0 File Offset: 0x0040A4F0
		public static Utils.ChaseResults GetChaseResults(Vector2 chaserPosition, float chaserSpeed, Vector2 runnerPosition, Vector2 runnerVelocity)
		{
			Utils.ChaseResults chaseResults = default(Utils.ChaseResults);
			if (chaserPosition == runnerPosition)
			{
				return new Utils.ChaseResults
				{
					InterceptionHappens = true,
					InterceptionPosition = chaserPosition,
					InterceptionTime = 0f,
					ChaserVelocity = Vector2.Zero
				};
			}
			if (chaserSpeed <= 0f)
			{
				return default(Utils.ChaseResults);
			}
			Vector2 vector = chaserPosition - runnerPosition;
			float num = vector.Length();
			float num2 = runnerVelocity.Length();
			if (num2 == 0f)
			{
				chaseResults.InterceptionTime = num / chaserSpeed;
				chaseResults.InterceptionPosition = runnerPosition;
			}
			else
			{
				float num3 = chaserSpeed * chaserSpeed - num2 * num2;
				float num4 = 2f * Vector2.Dot(vector, runnerVelocity);
				float num5 = -num * num;
				float num6;
				float num7;
				if (!Utils.SolveQuadratic(num3, num4, num5, out num6, out num7))
				{
					return default(Utils.ChaseResults);
				}
				if (num6 < 0f && num7 < 0f)
				{
					return default(Utils.ChaseResults);
				}
				if (num6 > 0f && num7 > 0f)
				{
					chaseResults.InterceptionTime = Math.Min(num6, num7);
				}
				else
				{
					chaseResults.InterceptionTime = Math.Max(num6, num7);
				}
				chaseResults.InterceptionPosition = runnerPosition + runnerVelocity * chaseResults.InterceptionTime;
			}
			chaseResults.ChaserVelocity = (chaseResults.InterceptionPosition - chaserPosition) / chaseResults.InterceptionTime;
			chaseResults.InterceptionHappens = true;
			return chaseResults;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0040C454 File Offset: 0x0040A654
		public static float GetJumpForce(float jumpHeight, float atGravity)
		{
			return (float)Math.Sqrt((double)(jumpHeight / atGravity * 2f)) * atGravity;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0040C478 File Offset: 0x0040A678
		public static float GetJumpTimeToApex(float jumpHeight, float atGravity)
		{
			return (float)Math.Sqrt((double)(jumpHeight / atGravity * 2f));
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0040C498 File Offset: 0x0040A698
		public static Vector2 FactorAcceleration(Vector2 currentVelocity, float timeToInterception, Vector2 descendOfProjectile, int framesOfLenience)
		{
			float num = Math.Max(0f, timeToInterception - (float)framesOfLenience);
			Vector2 vector = descendOfProjectile * (num * num) / 2f / timeToInterception;
			return currentVelocity - vector;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0040C4D8 File Offset: 0x0040A6D8
		public static bool SolveQuadratic(float a, float b, float c, out float result1, out float result2)
		{
			float num = b * b - 4f * a * c;
			result1 = 0f;
			result2 = 0f;
			if (num > 0f)
			{
				result1 = (-b + (float)Math.Sqrt((double)num)) / (2f * a);
				result2 = (-b - (float)Math.Sqrt((double)num)) / (2f * a);
				return true;
			}
			if (num < 0f)
			{
				return false;
			}
			result1 = (result2 = (-b + (float)Math.Sqrt((double)num)) / (2f * a));
			return true;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0040C560 File Offset: 0x0040A760
		public static double GetLerpValue(double from, double to, double t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0.0;
					}
					if (t > to)
					{
						return 1.0;
					}
				}
				else
				{
					if (t < to)
					{
						return 1.0;
					}
					if (t > from)
					{
						return 0.0;
					}
				}
			}
			return (t - from) / (to - from);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0040C5B4 File Offset: 0x0040A7B4
		public static float GetDayTimeAs24FloatStartingFromMidnight()
		{
			if (Main.dayTime)
			{
				return 4.5f + (float)(Main.time / 54000.0) * 15f;
			}
			return 19.5f + (float)(Main.time / 32400.0) * 9f;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0040C601 File Offset: 0x0040A801
		public static Vector2 GetDayTimeAsDirectionIn24HClock()
		{
			return Utils.GetDayTimeAsDirectionIn24HClock(Utils.GetDayTimeAs24FloatStartingFromMidnight());
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0040C610 File Offset: 0x0040A810
		public static Vector2 GetDayTimeAsDirectionIn24HClock(float timeFrom0To24)
		{
			return new Vector2(0f, -1f).RotatedBy((double)(timeFrom0To24 / 24f * 6.2831855f), default(Vector2));
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0040C648 File Offset: 0x0040A848
		public static string[] ConvertMonoArgsToDotNet(string[] brokenArgs)
		{
			ArrayList arrayList = new ArrayList();
			string text = "";
			for (int i = 0; i < brokenArgs.Length; i++)
			{
				if (brokenArgs[i].StartsWith("-"))
				{
					if (text != "")
					{
						arrayList.Add(text);
						text = "";
					}
					else
					{
						arrayList.Add("");
					}
					arrayList.Add(brokenArgs[i]);
				}
				else
				{
					if (text != "")
					{
						text += " ";
					}
					text += brokenArgs[i];
				}
			}
			arrayList.Add(text);
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0040C6F4 File Offset: 0x0040A8F4
		public static T Max<T>(params T[] args) where T : IComparable
		{
			T t = args[0];
			for (int i = 1; i < args.Length; i++)
			{
				if (t.CompareTo(args[i]) < 0)
				{
					t = args[i];
				}
			}
			return t;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0040C73C File Offset: 0x0040A93C
		public static float LineRectangleDistance(Rectangle rect, Vector2 lineStart, Vector2 lineEnd)
		{
			Vector2 vector = rect.TopLeft();
			Vector2 vector2 = rect.TopRight();
			Vector2 vector3 = rect.BottomLeft();
			Vector2 vector4 = rect.BottomRight();
			if (lineStart.Between(vector, vector4) || lineEnd.Between(vector, vector4))
			{
				return 0f;
			}
			float num = vector.Distance(vector.ClosestPointOnLine(lineStart, lineEnd));
			float num2 = vector2.Distance(vector2.ClosestPointOnLine(lineStart, lineEnd));
			float num3 = vector3.Distance(vector3.ClosestPointOnLine(lineStart, lineEnd));
			float num4 = vector4.Distance(vector4.ClosestPointOnLine(lineStart, lineEnd));
			return MathHelper.Min(num, MathHelper.Min(num2, MathHelper.Min(num3, num4)));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0040C7D4 File Offset: 0x0040A9D4
		public static List<List<TextSnippet>> WordwrapStringSmart(string text, Color c, DynamicSpriteFont font, float maxWidth = -1f, int maxLines = -1)
		{
			List<List<TextSnippet>> list = new List<List<TextSnippet>>();
			List<TextSnippet> list2 = new List<TextSnippet>();
			list.Add(list2);
			foreach (PositionedSnippet positionedSnippet in ChatManager.LayoutSnippets(font, ChatManager.ParseMessage(text, c), Vector2.One, maxWidth))
			{
				while (positionedSnippet.Line >= list.Count)
				{
					if (list.Count == maxLines)
					{
						return list;
					}
					list.Add(list2 = new List<TextSnippet>());
				}
				list2.Add(positionedSnippet.Snippet);
			}
			return list;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0040C878 File Offset: 0x0040AA78
		public static string[] WordwrapString(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
		{
			string[] array = font.CreateWrappedText(text, (float)maxWidth, Language.ActiveCulture.CultureInfo).Split(new char[] { '\n' });
			lineAmount = Math.Min(array.Length, maxLines);
			string[] array2 = new string[maxLines];
			Array.Copy(array, array2, lineAmount);
			return array2;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0040C8C8 File Offset: 0x0040AAC8
		public static string[] WordwrapStringLegacy(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
		{
			string[] array = new string[maxLines];
			int num = 0;
			List<string> list = new List<string>(text.Split(new char[] { '\n' }));
			List<string> list2 = new List<string>(list[0].Split(new char[] { ' ' }));
			int num2 = 1;
			while (num2 < list.Count && num2 < maxLines)
			{
				list2.Add("\n");
				list2.AddRange(list[num2].Split(new char[] { ' ' }));
				num2++;
			}
			bool flag = true;
			while (list2.Count > 0)
			{
				string text2 = list2[0];
				string text3 = " ";
				if (list2.Count == 1)
				{
					text3 = "";
				}
				if (text2 == "\n")
				{
					string[] array2 = array;
					int num3 = num++;
					array2[num3] += text2;
					flag = true;
					if (num >= maxLines)
					{
						break;
					}
					list2.RemoveAt(0);
				}
				else if (flag)
				{
					if (font.MeasureString(text2).X > (float)maxWidth)
					{
						string text4 = text2[0].ToString() ?? "";
						int num4 = 1;
						while (font.MeasureString(text4 + text2[num4].ToString() + "-").X <= (float)maxWidth)
						{
							text4 += text2[num4++].ToString();
						}
						text4 += "-";
						array[num++] = text4 + " ";
						if (num >= maxLines)
						{
							break;
						}
						list2.RemoveAt(0);
						list2.Insert(0, text2.Substring(num4));
					}
					else
					{
						string[] array3 = array;
						int num5 = num;
						array3[num5] = array3[num5] + text2 + text3;
						flag = false;
						list2.RemoveAt(0);
					}
				}
				else if (font.MeasureString(array[num] + text2).X > (float)maxWidth)
				{
					num++;
					if (num >= maxLines)
					{
						break;
					}
					flag = true;
				}
				else
				{
					string[] array4 = array;
					int num6 = num;
					array4[num6] = array4[num6] + text2 + text3;
					flag = false;
					list2.RemoveAt(0);
				}
			}
			lineAmount = Math.Min(num + 1, maxLines);
			return array;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0040CAFB File Offset: 0x0040ACFB
		public static Rectangle CenteredRectangle(Vector2 center, Vector2 size)
		{
			return new Rectangle((int)(center.X - size.X / 2f), (int)(center.Y - size.Y / 2f), (int)size.X, (int)size.Y);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0040CB38 File Offset: 0x0040AD38
		public static Rectangle CenteredRectangle(Point center, Point size)
		{
			return new Rectangle(center.X - size.X / 2, center.Y - size.Y / 2, size.X, size.Y);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0040CB6C File Offset: 0x0040AD6C
		public static Rectangle Including(this Rectangle rect, Point point)
		{
			int num = Math.Min(rect.Left, point.X);
			int num2 = Math.Max(rect.Right, point.X);
			int num3 = Math.Min(rect.Top, point.Y);
			int num4 = Math.Max(rect.Bottom, point.Y);
			return new Rectangle(num, num3, num2 - num, num4 - num3);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0040CBD4 File Offset: 0x0040ADD4
		public static Vector2 Vector2FromElipse(Vector2 angleVector, Vector2 elipseSizes)
		{
			if (elipseSizes == Vector2.Zero)
			{
				return Vector2.Zero;
			}
			if (angleVector == Vector2.Zero)
			{
				return Vector2.Zero;
			}
			angleVector.Normalize();
			Vector2 vector = Vector2.Normalize(elipseSizes);
			vector = Vector2.One / vector;
			angleVector *= vector;
			angleVector.Normalize();
			return angleVector * elipseSizes / 2f;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0040CC42 File Offset: 0x0040AE42
		public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
		{
			return r1StartX <= r2StartX + r2Width && r1StartY <= r2StartY + r2Height && r1StartX + r1Width >= r2StartX && r1StartY + r1Height >= r2StartY;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0040CC42 File Offset: 0x0040AE42
		public static bool DoubleIntersect(double r1StartX, double r1StartY, double r1Width, double r1Height, double r2StartX, double r2StartY, double r2Width, double r2Height)
		{
			return r1StartX <= r2StartX + r2Width && r1StartY <= r2StartY + r2Height && r1StartX + r1Width >= r2StartX && r1StartY + r1Height >= r2StartY;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0040CC68 File Offset: 0x0040AE68
		public static bool LineSegmentsIntersect(Vector2D start1, Vector2D end1, Vector2D start2, Vector2D end2)
		{
			Vector2D vector2D = end1 - start1;
			Vector2D vector2D2 = end2 - start2;
			double num = Vector2D.Cross(vector2D, vector2D2);
			if (num == 0.0)
			{
				return false;
			}
			Vector2D vector2D3 = start2 - start1;
			double num2 = Vector2D.Cross(vector2D3, vector2D) / num;
			double num3 = Vector2D.Cross(vector2D3, vector2D) / num;
			double num4 = Vector2D.Cross(vector2D3, vector2D) / num;
			return 0.0 <= num3 && num3 <= 1.0 && 0.0 <= num4 && num4 <= 1.0;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0040CCF8 File Offset: 0x0040AEF8
		public static long CoinsCount(out bool overFlowing, Item[] inv, params int[] ignoreSlots)
		{
			List<int> list = new List<int>(ignoreSlots);
			long num = 0L;
			for (int i = 0; i < inv.Length; i++)
			{
				if (!list.Contains(i))
				{
					switch (inv[i].type)
					{
					case 71:
						num += (long)inv[i].stack;
						break;
					case 72:
						num += (long)inv[i].stack * 100L;
						break;
					case 73:
						num += (long)inv[i].stack * 10000L;
						break;
					case 74:
						num += (long)inv[i].stack * 1000000L;
						break;
					}
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0040CD98 File Offset: 0x0040AF98
		public static int[] CoinsSplit(long count)
		{
			int[] array = new int[4];
			long num = 0L;
			long num2 = 1000000L;
			for (int i = 3; i >= 0; i--)
			{
				array[i] = (int)((count - num) / num2);
				num += (long)array[i] * num2;
				num2 /= 100L;
			}
			return array;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0040CDDC File Offset: 0x0040AFDC
		public static long CoinsCombineStacks(out bool overFlowing, params long[] coinCounts)
		{
			long num = 0L;
			foreach (long num2 in coinCounts)
			{
				num += num2;
				if (num >= 9999999999L)
				{
					overFlowing = true;
					return 9999999999L;
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0040CE24 File Offset: 0x0040B024
		public static void PoofOfSmoke(Vector2 position)
		{
			int num = Main.rand.Next(3, 7);
			for (int i = 0; i < num; i++)
			{
				int num2 = Gore.NewGore(position, (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.7f) * 0.7f, Main.rand.Next(11, 14), 1f);
				Main.gore[num2].scale = 0.7f;
				Main.gore[num2].velocity *= 0.5f;
			}
			for (int j = 0; j < 10; j++)
			{
				Dust dust = Main.dust[Dust.NewDust(position, 14, 14, 16, 0f, 0f, 100, default(Color), 1.5f)];
				dust.position += new Vector2(5f);
				dust.velocity = (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.7f) * 0.7f * (0.5f + 0.5f * Main.rand.NextFloat());
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0040CF75 File Offset: 0x0040B175
		public static Vector2 ToScreenPosition(this Vector2 worldPosition)
		{
			return Vector2.Transform(worldPosition - Main.screenPosition, Main.GameViewMatrix.TransformationMatrix) / Main.UIScale;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0040CF9B File Offset: 0x0040B19B
		public static Vector2 ScreenToWorldPosition(this Vector2 screenPosition)
		{
			return Vector2.Transform(screenPosition * Main.UIScale, Matrix.Invert(Main.GameViewMatrix.TransformationMatrix)) + Main.screenPosition;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0040CFC8 File Offset: 0x0040B1C8
		public static string PrettifyPercentDisplay(float percent, string originalFormat)
		{
			return percent.ToString(originalFormat, CultureInfo.InvariantCulture).TrimEnd(new char[] { '0', '%', ' ' }).TrimEnd(new char[] { '.', ' ' })
				.TrimStart(new char[] { '0', ' ' }) + "%";
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0040D02C File Offset: 0x0040B22C
		public static void TrimTextIfNeeded(ref string text, DynamicSpriteFont font, float scale, float maxWidth)
		{
			bool flag = false;
			Vector2 vector = font.MeasureString(text) * scale;
			while (vector.X > maxWidth)
			{
				text = Utils.TrimLastCharacter(text);
				flag = true;
				vector = font.MeasureString(text) * scale;
			}
			if (flag)
			{
				text = Utils.TrimLastCharacter(text) + "…";
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0040D088 File Offset: 0x0040B288
		public static string FormatWith(string original, object obj)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			return Utils._substitutionRegex.Replace(original, delegate(Match match)
			{
				if (match.Groups[1].Length != 0)
				{
					return "";
				}
				string text = match.Groups[2].ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(text, false);
				if (propertyDescriptor == null)
				{
					return "";
				}
				return (propertyDescriptor.GetValue(obj) ?? "").ToString();
			});
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0040D0D0 File Offset: 0x0040B2D0
		public static bool TryCreatingDirectory(string folderPath)
		{
			if (Directory.Exists(folderPath))
			{
				return true;
			}
			bool flag;
			try
			{
				Directory.CreateDirectory(folderPath);
				flag = true;
			}
			catch (Exception ex)
			{
				FancyErrorPrinter.ShowDirectoryCreationFailError(ex, folderPath);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0040D110 File Offset: 0x0040B310
		public static void OpenFolder(string folderPath)
		{
			if (!Utils.TryCreatingDirectory(folderPath))
			{
				return;
			}
			if (Platform.IsLinux)
			{
				Process.Start(new ProcessStartInfo(folderPath)
				{
					FileName = "open-folder",
					Arguments = folderPath,
					UseShellExecute = true,
					CreateNoWindow = true
				});
				return;
			}
			Process.Start(folderPath);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0040D161 File Offset: 0x0040B361
		public static TimeSpan SWTicksToTimeSpan(long swTicks)
		{
			return new TimeSpan((long)((double)swTicks * 10000000.0 / (double)Stopwatch.Frequency));
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0040D17C File Offset: 0x0040B37C
		public static long TimeSpanToSWTicks(TimeSpan timeSpan)
		{
			return timeSpan.Ticks * Stopwatch.Frequency / 10000000L;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0040D194 File Offset: 0x0040B394
		public static byte[] ToByteArray(this string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0040D1C1 File Offset: 0x0040B3C1
		public static float NextFloat(this UnifiedRandom r)
		{
			return (float)r.NextDouble();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0040D1CA File Offset: 0x0040B3CA
		public static float NextFloatDirection(this UnifiedRandom r)
		{
			return (float)r.NextDouble() * 2f - 1f;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0040D1DF File Offset: 0x0040B3DF
		public static float NextFloat(this UnifiedRandom random, FloatRange range)
		{
			return random.NextFloat() * (range.Maximum - range.Minimum) + range.Minimum;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0040D1FC File Offset: 0x0040B3FC
		public static T NextFromList<T>(this UnifiedRandom random, params T[] objs)
		{
			return objs[random.Next(objs.Length)];
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0040D210 File Offset: 0x0040B410
		public static bool JustBecameTrue(bool state, ref bool releasedStateHolder)
		{
			bool flag = false;
			if (state)
			{
				if (releasedStateHolder)
				{
					flag = true;
				}
				releasedStateHolder = false;
			}
			else
			{
				releasedStateHolder = true;
			}
			return flag;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0040D231 File Offset: 0x0040B431
		public static T NextFromCollection<T>(this UnifiedRandom random, List<T> objs)
		{
			return objs[random.Next(objs.Count)];
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0040D245 File Offset: 0x0040B445
		public static int Next(this UnifiedRandom random, IntRange range)
		{
			return random.Next(range.Minimum, range.Maximum + 1);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0040D25B File Offset: 0x0040B45B
		public static Point NextFromRectangle(this UnifiedRandom r, Rectangle rect)
		{
			return new Point(r.Next(rect.Left, rect.Right), r.Next(rect.Top, rect.Bottom));
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0040D28A File Offset: 0x0040B48A
		public static Vector2 NextVector2Square(this UnifiedRandom r, float min, float max)
		{
			return new Vector2((max - min) * (float)r.NextDouble() + min, (max - min) * (float)r.NextDouble() + min);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0040D2AB File Offset: 0x0040B4AB
		public static Vector2 NextVector2FromRectangle(this UnifiedRandom r, Rectangle rect)
		{
			return new Vector2((float)rect.X + r.NextFloat() * (float)rect.Width, (float)rect.Y + r.NextFloat() * (float)rect.Height);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0040D2DE File Offset: 0x0040B4DE
		public static Vector2 NextVector2Unit(this UnifiedRandom r, float startRotation = 0f, float rotationRange = 6.2831855f)
		{
			return (startRotation + rotationRange * r.NextFloat()).ToRotationVector2();
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0040D2EF File Offset: 0x0040B4EF
		public static Vector2 NextVector2Circular(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight) * r.NextFloat();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0040D318 File Offset: 0x0040B518
		public static Vector2 NextVector2CircularEdge(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
		{
			return r.NextVector2Unit(0f, 6.2831855f) * new Vector2(circleHalfWidth, circleHalfHeight);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0040D336 File Offset: 0x0040B536
		public static Vector2D NextVector2DSquare(this UnifiedRandom r, double min, double max)
		{
			return new Vector2D((max - min) * r.NextDouble() + min, (max - min) * r.NextDouble() + min);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0040D355 File Offset: 0x0040B555
		public static Vector2D NextVector2DFromRectangle(this UnifiedRandom r, Rectangle rect)
		{
			return new Vector2D((double)rect.X + r.NextDouble() * (double)rect.Width, (double)rect.Y + r.NextDouble() * (double)rect.Height);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0040D388 File Offset: 0x0040B588
		public static Vector2D NextVector2DUnit(this UnifiedRandom r, double startRotation = 0.0, double rotationRange = 6.2831854820251465)
		{
			return (startRotation + rotationRange * r.NextDouble()).ToRotationVector2D();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0040D399 File Offset: 0x0040B599
		public static Vector2D NextVector2DCircular(this UnifiedRandom r, double circleHalfWidth, double circleHalfHeight)
		{
			return r.NextVector2DUnit(0.0, 6.2831854820251465) * new Vector2D(circleHalfWidth, circleHalfHeight) * r.NextDouble();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0040D3CA File Offset: 0x0040B5CA
		public static Vector2D NextVector2DCircularEdge(this UnifiedRandom r, double circleHalfWidth, double circleHalfHeight)
		{
			return r.NextVector2DUnit(0.0, 6.2831854820251465) * new Vector2D(circleHalfWidth, circleHalfHeight);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0040D3F0 File Offset: 0x0040B5F0
		public static int Width(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Width;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0040D407 File Offset: 0x0040B607
		public static int Height(this Asset<Texture2D> asset)
		{
			if (!asset.IsLoaded)
			{
				return 0;
			}
			return asset.Value.Height;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0040D41E File Offset: 0x0040B61E
		public static Rectangle Frame(this Asset<Texture2D> tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			if (!tex.IsLoaded)
			{
				return Rectangle.Empty;
			}
			return tex.Value.Frame(horizontalFrames, verticalFrames, frameX, frameY, sizeOffsetX, sizeOffsetY);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0040D442 File Offset: 0x0040B642
		public static Rectangle OffsetSize(this Rectangle rect, int xSize, int ySize)
		{
			rect.Width += xSize;
			rect.Height += ySize;
			return rect;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0040D45D File Offset: 0x0040B65D
		public static Vector2 Size(this Asset<Texture2D> tex)
		{
			if (!tex.IsLoaded)
			{
				return Vector2.Zero;
			}
			return tex.Value.Size();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0040D478 File Offset: 0x0040B678
		public static Rectangle Frame(this Texture2D tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0, int sizeOffsetX = 0, int sizeOffsetY = 0)
		{
			int num = tex.Width / horizontalFrames;
			int num2 = tex.Height / verticalFrames;
			return new Rectangle(num * frameX, num2 * frameY, num + sizeOffsetX, num2 + sizeOffsetY);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0040D4AB File Offset: 0x0040B6AB
		public static Vector2 OriginFlip(this Rectangle rect, Vector2 origin, SpriteEffects effects)
		{
			if ((effects & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
			{
				origin.X = (float)rect.Width - origin.X;
			}
			if ((effects & SpriteEffects.FlipVertically) != SpriteEffects.None)
			{
				origin.Y = (float)rect.Height - origin.Y;
			}
			return origin;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0040D4E2 File Offset: 0x0040B6E2
		public static Vector2 Size(this Texture2D tex)
		{
			return new Vector2((float)tex.Width, (float)tex.Height);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0040D4F7 File Offset: 0x0040B6F7
		public static void WriteRGB(this BinaryWriter bb, Color c)
		{
			bb.Write(c.R);
			bb.Write(c.G);
			bb.Write(c.B);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0040D520 File Offset: 0x0040B720
		public static void WriteVector2(this BinaryWriter bb, Vector2 v)
		{
			bb.Write(v.X);
			bb.Write(v.Y);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0040D53C File Offset: 0x0040B73C
		public static void WritePackedVector2(this BinaryWriter bb, Vector2 v)
		{
			HalfVector2 halfVector = new HalfVector2(v.X, v.Y);
			bb.Write(halfVector.PackedValue);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0040D569 File Offset: 0x0040B769
		public static Color ReadRGB(this BinaryReader bb)
		{
			return new Color((int)bb.ReadByte(), (int)bb.ReadByte(), (int)bb.ReadByte());
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0040D582 File Offset: 0x0040B782
		public static Vector2 ReadVector2(this BinaryReader bb)
		{
			return new Vector2(bb.ReadSingle(), bb.ReadSingle());
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0040D598 File Offset: 0x0040B798
		public static Vector2 ReadPackedVector2(this BinaryReader bb)
		{
			HalfVector2 halfVector = default(HalfVector2);
			halfVector.PackedValue = bb.ReadUInt32();
			return halfVector.ToVector2();
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0040D5C4 File Offset: 0x0040B7C4
		public static void Write7BitEncodedInt(this BinaryWriter writer, int value)
		{
			uint num;
			for (num = (uint)value; num > 127U; num >>= 7)
			{
				writer.Write((byte)(num | 4294967168U));
			}
			writer.Write((byte)num);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0040D5F4 File Offset: 0x0040B7F4
		public static int Read7BitEncodedInt(this BinaryReader reader)
		{
			uint num = 0U;
			byte b;
			for (int i = 0; i < 28; i += 7)
			{
				b = reader.ReadByte();
				num |= (uint)((uint)(b & 127) << i);
				if (b <= 127)
				{
					return (int)num;
				}
			}
			b = reader.ReadByte();
			if (b > 15)
			{
				throw new FormatException("Bad 7bit encoded int");
			}
			return (int)(num | (uint)((uint)b << 28));
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0040D649 File Offset: 0x0040B849
		public static Vector2 Left(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0040D667 File Offset: 0x0040B867
		public static Vector2 Right(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0040D68C File Offset: 0x0040B88C
		public static Vector2 Top(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)r.Y);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0040D6AA File Offset: 0x0040B8AA
		public static Vector2 Bottom(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height));
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0040D6CF File Offset: 0x0040B8CF
		public static Vector2 TopLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)r.Y);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0040D6E4 File Offset: 0x0040B8E4
		public static Vector2 TopRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)r.Y);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0040D700 File Offset: 0x0040B900
		public static Vector2 BottomLeft(this Rectangle r)
		{
			return new Vector2((float)r.X, (float)(r.Y + r.Height));
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0040D71C File Offset: 0x0040B91C
		public static Vector2 BottomRight(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width), (float)(r.Y + r.Height));
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0040D73F File Offset: 0x0040B93F
		public static Vector2D TopLeftDouble(this Rectangle r)
		{
			return new Vector2D((double)r.X, (double)r.Y);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0040D754 File Offset: 0x0040B954
		public static Vector2D TopRightDouble(this Rectangle r)
		{
			return new Vector2D((double)(r.X + r.Width), (double)r.Y);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0040D770 File Offset: 0x0040B970
		public static Vector2D BottomLeftDouble(this Rectangle r)
		{
			return new Vector2D((double)r.X, (double)(r.Y + r.Height));
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0040D78C File Offset: 0x0040B98C
		public static Vector2D BottomRightDouble(this Rectangle r)
		{
			return new Vector2D((double)(r.X + r.Width), (double)(r.Y + r.Height));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0040D7AF File Offset: 0x0040B9AF
		public static Vector2 Center(this Rectangle r)
		{
			return new Vector2((float)(r.X + r.Width / 2), (float)(r.Y + r.Height / 2));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0040D7D6 File Offset: 0x0040B9D6
		public static Vector2 Size(this Rectangle r)
		{
			return new Vector2((float)r.Width, (float)r.Height);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0040D7EC File Offset: 0x0040B9EC
		public static float Distance(this Rectangle r, Vector2 point)
		{
			if (Utils.FloatIntersect((float)r.Left, (float)r.Top, (float)r.Width, (float)r.Height, point.X, point.Y, 0f, 0f))
			{
				return 0f;
			}
			if (point.X >= (float)r.Left && point.X <= (float)r.Right)
			{
				if (point.Y < (float)r.Top)
				{
					return (float)r.Top - point.Y;
				}
				return point.Y - (float)r.Bottom;
			}
			else if (point.Y >= (float)r.Top && point.Y <= (float)r.Bottom)
			{
				if (point.X < (float)r.Left)
				{
					return (float)r.Left - point.X;
				}
				return point.X - (float)r.Right;
			}
			else if (point.X < (float)r.Left)
			{
				if (point.Y < (float)r.Top)
				{
					return Vector2.Distance(point, r.TopLeft());
				}
				return Vector2.Distance(point, r.BottomLeft());
			}
			else
			{
				if (point.Y < (float)r.Top)
				{
					return Vector2.Distance(point, r.TopRight());
				}
				return Vector2.Distance(point, r.BottomRight());
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0040D940 File Offset: 0x0040BB40
		public static double Distance(this Rectangle r, Vector2D point)
		{
			if (Utils.DoubleIntersect((double)r.Left, (double)r.Top, (double)r.Width, (double)r.Height, point.X, point.Y, 0.0, 0.0))
			{
				return 0.0;
			}
			if (point.X >= (double)r.Left && point.X <= (double)r.Right)
			{
				if (point.Y < (double)r.Top)
				{
					return (double)r.Top - point.Y;
				}
				return point.Y - (double)r.Bottom;
			}
			else if (point.Y >= (double)r.Top && point.Y <= (double)r.Bottom)
			{
				if (point.X < (double)r.Left)
				{
					return (double)r.Left - point.X;
				}
				return point.X - (double)r.Right;
			}
			else if (point.X < (double)r.Left)
			{
				if (point.Y < (double)r.Top)
				{
					return Vector2D.Distance(point, r.TopLeftDouble());
				}
				return Vector2D.Distance(point, r.BottomLeftDouble());
			}
			else
			{
				if (point.Y < (double)r.Top)
				{
					return Vector2D.Distance(point, r.TopRightDouble());
				}
				return Vector2D.Distance(point, r.BottomRightDouble());
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0040DAA0 File Offset: 0x0040BCA0
		public static Vector2 ClosestPointInRect(this Rectangle r, Vector2 point)
		{
			Vector2 vector = point;
			if (vector.X < (float)r.Left)
			{
				vector.X = (float)r.Left;
			}
			if (vector.X > (float)r.Right)
			{
				vector.X = (float)r.Right;
			}
			if (vector.Y < (float)r.Top)
			{
				vector.Y = (float)r.Top;
			}
			if (vector.Y > (float)r.Bottom)
			{
				vector.Y = (float)r.Bottom;
			}
			return vector;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0040DB2C File Offset: 0x0040BD2C
		public static Rectangle Modified(this Rectangle r, int x, int y, int w, int h)
		{
			return new Rectangle(r.X + x, r.Y + y, r.Width + w, r.Height + h);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0040DB54 File Offset: 0x0040BD54
		public static bool IntersectsConeFastInaccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 vector = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			Vector2 vector2 = targetRect.ClosestPointInRect(vector) - coneCenter;
			float num = vector2.RotatedBy((double)(-(double)coneRotation), default(Vector2)).ToRotation();
			return num >= -maximumAngle && num <= maximumAngle && vector2.Length() < coneLength;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0040DBB4 File Offset: 0x0040BDB4
		public static bool IntersectsConeSlowMoreAccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 vector = coneCenter + coneRotation.ToRotationVector2() * coneLength;
			return Utils.DoesFitInCone(targetRect.ClosestPointInRect(vector), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.TopRight(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomLeft(), coneCenter, coneLength, coneRotation, maximumAngle) || Utils.DoesFitInCone(targetRect.BottomRight(), coneCenter, coneLength, coneRotation, maximumAngle);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0040DC3C File Offset: 0x0040BE3C
		public static bool DoesFitInCone(Vector2 point, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
		{
			Vector2 vector = point - coneCenter;
			float num = vector.RotatedBy((double)(-(double)coneRotation), default(Vector2)).ToRotation();
			return num >= -maximumAngle && num <= maximumAngle && vector.Length() < coneLength;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0040DC80 File Offset: 0x0040BE80
		public static float ToRotation(this Vector2 v)
		{
			return (float)Math.Atan2((double)v.Y, (double)v.X);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0040DC96 File Offset: 0x0040BE96
		public static double ToRotation(this Vector2D v)
		{
			return Math.Atan2(v.Y, v.X);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0040DCA9 File Offset: 0x0040BEA9
		public static Vector2 ToRotationVector2(this float f)
		{
			return new Vector2((float)Math.Cos((double)f), (float)Math.Sin((double)f));
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0040DCC0 File Offset: 0x0040BEC0
		public static Vector2D ToRotationVector2D(this double f)
		{
			return new Vector2D(Math.Cos(f), Math.Sin(f));
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0040DCD4 File Offset: 0x0040BED4
		public static Vector2 RotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default(Vector2))
		{
			float num = (float)Math.Cos(radians);
			float num2 = (float)Math.Sin(radians);
			Vector2 vector = spinningpoint - center;
			Vector2 vector2 = center;
			vector2.X += vector.X * num - vector.Y * num2;
			vector2.Y += vector.X * num2 + vector.Y * num;
			return vector2;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0040DD34 File Offset: 0x0040BF34
		public static Vector2D RotatedBy(this Vector2D spinningpoint, double radians, Vector2D center = default(Vector2D))
		{
			double num = Math.Cos(radians);
			double num2 = Math.Sin(radians);
			Vector2D vector2D = spinningpoint - center;
			Vector2D vector2D2 = center;
			vector2D2.X += vector2D.X * num - vector2D.Y * num2;
			vector2D2.Y += vector2D.X * num2 + vector2D.Y * num;
			return vector2D2;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0040DD94 File Offset: 0x0040BF94
		public static Vector2 RotatedByRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0040DDC9 File Offset: 0x0040BFC9
		public static Vector2 Floor(this Vector2 vec)
		{
			vec.X = (float)((int)vec.X);
			vec.Y = (float)((int)vec.Y);
			return vec;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0040DDEA File Offset: 0x0040BFEA
		public static bool HasNaNs(this Vector2 vec)
		{
			return float.IsNaN(vec.X) || float.IsNaN(vec.Y);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0040DE06 File Offset: 0x0040C006
		public static bool Between(this Vector2 vec, Vector2 minimum, Vector2 maximum)
		{
			return vec.X >= minimum.X && vec.X <= maximum.X && vec.Y >= minimum.Y && vec.Y <= maximum.Y;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0040DE45 File Offset: 0x0040C045
		public static Vector2 ScaledBy(this Vector2 vec, Vector2 other)
		{
			return Vector2.Multiply(vec, other);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0040DE4E File Offset: 0x0040C04E
		public static Vector2 ScaledBy(this Vector2 vec, float scaleX, float scaleY)
		{
			return Vector2.Multiply(vec, new Vector2(scaleX, scaleY));
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0040DE5D File Offset: 0x0040C05D
		public static Vector2 ToVector2(this Point p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0040DE72 File Offset: 0x0040C072
		public static Vector2 ToVector2(this Point16 p)
		{
			return new Vector2((float)p.X, (float)p.Y);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0040DE87 File Offset: 0x0040C087
		public static Vector3 ToVector3(this Vector2 v)
		{
			return new Vector3(v.X, v.Y, 0f);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0040DE9F File Offset: 0x0040C09F
		public static Vector2D ToVector2D(this Point p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0040DEB4 File Offset: 0x0040C0B4
		public static Vector2D ToVector2D(this Point16 p)
		{
			return new Vector2D((double)p.X, (double)p.Y);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0040DEC9 File Offset: 0x0040C0C9
		public static Vector2 ToWorldCoordinates(this Point p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0040DEE7 File Offset: 0x0040C0E7
		public static Vector2 ToWorldCoordinates(this Point16 p, float autoAddX = 8f, float autoAddY = 8f)
		{
			return p.ToVector2() * 16f + new Vector2(autoAddX, autoAddY);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0040DF08 File Offset: 0x0040C108
		public static Vector2 MoveTowards(this Vector2 currentPosition, Vector2 targetPosition, float maxAmountAllowedToMove)
		{
			Vector2 vector = targetPosition - currentPosition;
			if (vector.Length() < maxAmountAllowedToMove)
			{
				return targetPosition;
			}
			return currentPosition + vector.SafeNormalize(Vector2.Zero) * maxAmountAllowedToMove;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0040DF40 File Offset: 0x0040C140
		public static float MoveTowards(float original, float target, float amount)
		{
			if (original == target)
			{
				return target;
			}
			int num = Math.Sign(target - original);
			float num2 = original + amount * (float)num;
			if (Math.Sign(target - num2) != num)
			{
				return target;
			}
			return num2;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0040DF71 File Offset: 0x0040C171
		public static Point16 ToTileCoordinates16(this Vector2 vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0040DF8A File Offset: 0x0040C18A
		public static Point16 ToTileCoordinates16(this Vector2D vec)
		{
			return new Point16((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0040DFA3 File Offset: 0x0040C1A3
		public static Point ToTileCoordinates(this Vector2 vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0040DFBC File Offset: 0x0040C1BC
		public static Point ToTileCoordinates(this Vector2D vec)
		{
			return new Point((int)vec.X >> 4, (int)vec.Y >> 4);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0040DFD5 File Offset: 0x0040C1D5
		public static Point ToPoint(this Vector2 v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0040DFEA File Offset: 0x0040C1EA
		public static Point ToPoint(this Vector2D v)
		{
			return new Point((int)v.X, (int)v.Y);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0040DFFF File Offset: 0x0040C1FF
		public static Vector2 ToVector2(this Vector2D v)
		{
			return new Vector2((float)v.X, (float)v.Y);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0040E014 File Offset: 0x0040C214
		public static Vector2D ToVector2D(this Vector2 v)
		{
			return new Vector2D((double)v.X, (double)v.Y);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0040E029 File Offset: 0x0040C229
		public static Vector2 SafeNormalize(this Vector2 v, Vector2 defaultValue)
		{
			if (v == Vector2.Zero || v.HasNaNs())
			{
				return defaultValue;
			}
			return Vector2.Normalize(v);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0040E048 File Offset: 0x0040C248
		public static Vector2D SafeNormalize(this Vector2D v, Vector2D defaultValue)
		{
			if (v == Vector2D.Zero)
			{
				return defaultValue;
			}
			return Vector2D.Normalize(v);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0040E05F File Offset: 0x0040C25F
		public static Point ClampedInWorld(this Point p, int fluff = 0)
		{
			return new Point(Utils.Clamp<int>(p.X, fluff, Main.maxTilesX - fluff - 1), Utils.Clamp<int>(p.Y, fluff, Main.maxTilesX - fluff - 1));
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0040E090 File Offset: 0x0040C290
		public static Vector2 ClosestPointOnLine(this Vector2 P, Vector2 A, Vector2 B)
		{
			Vector2 vector = P - A;
			Vector2 vector2 = B - A;
			float num = vector2.LengthSquared();
			float num2 = Vector2.Dot(vector, vector2) / num;
			if (num2 < 0f)
			{
				return A;
			}
			if (num2 > 1f)
			{
				return B;
			}
			return A + vector2 * num2;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0040E0E0 File Offset: 0x0040C2E0
		public static Vector2D ClosestPointOnLine(this Vector2D P, Vector2D A, Vector2D B)
		{
			Vector2D vector2D = P - A;
			Vector2D vector2D2 = B - A;
			double num = vector2D2.LengthSquared();
			double num2 = Vector2D.Dot(vector2D, vector2D2) / num;
			if (num2 < 0.0)
			{
				return A;
			}
			if (num2 > 1.0)
			{
				return B;
			}
			return A + vector2D2 * num2;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0040E138 File Offset: 0x0040C338
		public static bool RectangleLineCollision(Vector2 rectTopLeft, Vector2 rectBottomRight, Vector2 lineStart, Vector2 lineEnd)
		{
			if (lineStart.Between(rectTopLeft, rectBottomRight) || lineEnd.Between(rectTopLeft, rectBottomRight))
			{
				return true;
			}
			Vector2 vector = new Vector2(rectBottomRight.X, rectTopLeft.Y);
			Vector2 vector2 = new Vector2(rectTopLeft.X, rectBottomRight.Y);
			Vector2[] array = new Vector2[]
			{
				rectTopLeft.ClosestPointOnLine(lineStart, lineEnd),
				vector.ClosestPointOnLine(lineStart, lineEnd),
				vector2.ClosestPointOnLine(lineStart, lineEnd),
				rectBottomRight.ClosestPointOnLine(lineStart, lineEnd)
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[0].Between(rectTopLeft, vector2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0040E1E8 File Offset: 0x0040C3E8
		public static Vector2 RotateRandom(this Vector2 spinninpoint, double maxRadians)
		{
			return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, default(Vector2));
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0040E21D File Offset: 0x0040C41D
		public static float AngleTo(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Target.Y - Origin.Y), (double)(Target.X - Origin.X));
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0040E241 File Offset: 0x0040C441
		public static float AngleFrom(this Vector2 Origin, Vector2 Target)
		{
			return (float)Math.Atan2((double)(Origin.Y - Target.Y), (double)(Origin.X - Target.X));
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0040E268 File Offset: 0x0040C468
		public static Vector2 rotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
		{
			float num = currentVelocity.Length();
			float num2 = currentPosition.AngleTo(targetPosition);
			return currentVelocity.ToRotation().AngleTowards(num2, maxChange).ToRotationVector2() * num;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0040E29D File Offset: 0x0040C49D
		public static float Distance(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Distance(Origin, Target);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0040E2A6 File Offset: 0x0040C4A6
		public static double Distance(this Vector2D Origin, Vector2D Target)
		{
			return Vector2D.Distance(Origin, Target);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0040E2AF File Offset: 0x0040C4AF
		public static float DistanceSQ(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.DistanceSquared(Origin, Target);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0040E2B8 File Offset: 0x0040C4B8
		public static Vector2 DirectionTo(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Target - Origin);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0040E2C6 File Offset: 0x0040C4C6
		public static Vector2 DirectionFrom(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Origin - Target);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0040E2D4 File Offset: 0x0040C4D4
		public static bool WithinRange(this Vector2 Origin, Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(Origin, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0040E2E5 File Offset: 0x0040C4E5
		public static Vector2 XY(this Vector4 vec)
		{
			return new Vector2(vec.X, vec.Y);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0040E2F8 File Offset: 0x0040C4F8
		public static Vector2 ZW(this Vector4 vec)
		{
			return new Vector2(vec.Z, vec.W);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0040E30B File Offset: 0x0040C50B
		public static Vector3 XZW(this Vector4 vec)
		{
			return new Vector3(vec.X, vec.Z, vec.W);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0040E324 File Offset: 0x0040C524
		public static Vector3 YZW(this Vector4 vec)
		{
			return new Vector3(vec.Y, vec.Z, vec.W);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0040E340 File Offset: 0x0040C540
		public static Color MultiplyRGB(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)));
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0040E398 File Offset: 0x0040C598
		public static Color MultiplyRGBA(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)), (int)((byte)((float)(firstColor.A * secondColor.A) / 255f)));
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0040E408 File Offset: 0x0040C608
		public static string Hex3(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).ToLower();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0040E45C File Offset: 0x0040C65C
		public static string Hex4(this Color color)
		{
			return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") + color.A.ToString("X2")).ToLower();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0040E4C3 File Offset: 0x0040C6C3
		public static int ToDirectionInt(this bool value)
		{
			if (!value)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0040E4CB File Offset: 0x0040C6CB
		public static int ToInt(this bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0040E4D3 File Offset: 0x0040C6D3
		public static int ModulusPositive(this int myInteger, int modulusNumber)
		{
			return (myInteger % modulusNumber + modulusNumber) % modulusNumber;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0040E4DC File Offset: 0x0040C6DC
		public static float AngleLerp(this float curAngle, float targetAngle, float amount)
		{
			float num2;
			if (targetAngle < curAngle)
			{
				float num = targetAngle + 6.2831855f;
				num2 = ((num - curAngle > curAngle - targetAngle) ? MathHelper.Lerp(curAngle, targetAngle, amount) : MathHelper.Lerp(curAngle, num, amount));
			}
			else
			{
				if (targetAngle <= curAngle)
				{
					return curAngle;
				}
				float num = targetAngle - 6.2831855f;
				num2 = ((targetAngle - curAngle > curAngle - num) ? MathHelper.Lerp(curAngle, num, amount) : MathHelper.Lerp(curAngle, targetAngle, amount));
			}
			return MathHelper.WrapAngle(num2);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0040E544 File Offset: 0x0040C744
		public static float AngleTowards(this float curAngle, float targetAngle, float maxChange)
		{
			curAngle = MathHelper.WrapAngle(curAngle);
			targetAngle = MathHelper.WrapAngle(targetAngle);
			if (curAngle < targetAngle)
			{
				if (targetAngle - curAngle > 3.1415927f)
				{
					curAngle += 6.2831855f;
				}
			}
			else if (curAngle - targetAngle > 3.1415927f)
			{
				curAngle -= 6.2831855f;
			}
			curAngle += MathHelper.Clamp(targetAngle - curAngle, -maxChange, maxChange);
			return MathHelper.WrapAngle(curAngle);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0040E5A4 File Offset: 0x0040C7A4
		public static float RotateUntil(this float curAngle, float targetAngle, float changePerTick)
		{
			curAngle = MathHelper.WrapAngle(curAngle);
			targetAngle = MathHelper.WrapAngle(targetAngle);
			if (curAngle < targetAngle)
			{
				if (targetAngle - curAngle > 3.1415927f)
				{
					curAngle += 6.2831855f;
				}
			}
			else if (curAngle - targetAngle > 3.1415927f)
			{
				curAngle -= 6.2831855f;
			}
			curAngle += changePerTick;
			curAngle = MathHelper.WrapAngle(curAngle);
			if (curAngle > targetAngle)
			{
				curAngle = targetAngle;
			}
			return curAngle;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0040E604 File Offset: 0x0040C804
		public static bool deepCompare(this int[] firstArray, int[] secondArray)
		{
			if (firstArray == null && secondArray == null)
			{
				return true;
			}
			if (firstArray == null || secondArray == null)
			{
				return false;
			}
			if (firstArray.Length != secondArray.Length)
			{
				return false;
			}
			for (int i = 0; i < firstArray.Length; i++)
			{
				if (firstArray[i] != secondArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0040E644 File Offset: 0x0040C844
		public static bool deepCompare(this Rectangle[,] firstArray, Rectangle[,] secondArray)
		{
			if (firstArray == null && secondArray == null)
			{
				return true;
			}
			if (firstArray == null || secondArray == null)
			{
				return false;
			}
			if (firstArray.Length != secondArray.Length)
			{
				return false;
			}
			if (firstArray.GetLength(0) != secondArray.GetLength(0))
			{
				return false;
			}
			if (firstArray.GetLength(1) != secondArray.GetLength(1))
			{
				return false;
			}
			for (int i = 0; i < firstArray.GetLength(0); i++)
			{
				for (int j = 0; j < firstArray.GetLength(1); j++)
				{
					if (firstArray[i, j] != secondArray[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0040E6D4 File Offset: 0x0040C8D4
		public static List<int> GetTrueIndexes(this bool[] array)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0040E704 File Offset: 0x0040C904
		public static List<int> GetTrueIndexes(params bool[][] arrays)
		{
			List<int> list = new List<int>();
			foreach (bool[] array in arrays)
			{
				list.AddRange(array.GetTrueIndexes());
			}
			return list.Distinct<int>().ToList<int>();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0040E744 File Offset: 0x0040C944
		public static int Count<T>(this T[] arr, T value)
		{
			int num = 0;
			foreach (T t in arr)
			{
				if (EqualityComparer<T>.Default.Equals(t, value))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0040E77E File Offset: 0x0040C97E
		public static bool PressingShift(this KeyboardState kb)
		{
			return kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0040E79C File Offset: 0x0040C99C
		public static bool PressingControl(this KeyboardState kb)
		{
			return kb.IsKeyDown(Keys.LeftControl) || kb.IsKeyDown(Keys.RightControl);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0040E7BA File Offset: 0x0040C9BA
		public static bool PressingAlt(this KeyboardState kb)
		{
			return kb.IsKeyDown(Keys.LeftAlt) || kb.IsKeyDown(Keys.RightAlt);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0040E7D8 File Offset: 0x0040C9D8
		public static R[] MapArray<T, R>(T[] array, Func<T, R> mapper)
		{
			R[] array2 = new R[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = mapper(array[i]);
			}
			return array2;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0040E811 File Offset: 0x0040CA11
		public static bool PlotLine(Point16 p0, Point16 p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine((int)p0.X, (int)p0.Y, (int)p1.X, (int)p1.Y, plot, jump);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0040E832 File Offset: 0x0040CA32
		public static bool PlotLine(Point p0, Point p1, Utils.TileActionAttempt plot, bool jump = true)
		{
			return Utils.PlotLine(p0.X, p0.Y, p1.X, p1.Y, plot, jump);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0040E854 File Offset: 0x0040CA54
		private static bool PlotLine(int x0, int y0, int x1, int y1, Utils.TileActionAttempt plot, bool jump = true)
		{
			if (x0 == x1 && y0 == y1)
			{
				return plot(x0, y0);
			}
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				Utils.Swap<int>(ref x0, ref y0);
				Utils.Swap<int>(ref x1, ref y1);
			}
			int num = Math.Abs(x1 - x0);
			int num2 = Math.Abs(y1 - y0);
			int num3 = num / 2;
			int num4 = y0;
			int num5 = ((x0 < x1) ? 1 : (-1));
			int num6 = ((y0 < y1) ? 1 : (-1));
			for (int num7 = x0; num7 != x1; num7 += num5)
			{
				if (flag)
				{
					if (!plot(num4, num7))
					{
						return false;
					}
				}
				else if (!plot(num7, num4))
				{
					return false;
				}
				num3 -= num2;
				if (num3 < 0)
				{
					num4 += num6;
					if (!jump)
					{
						if (flag)
						{
							if (!plot(num4, num7))
							{
								return false;
							}
						}
						else if (!plot(num7, num4))
						{
							return false;
						}
					}
					num3 += num;
				}
			}
			return true;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0040E933 File Offset: 0x0040CB33
		public static int RandomNext(ref ulong seed, int bits)
		{
			seed = Utils.RandomNextSeed(seed);
			return (int)(seed >> 48 - bits);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0040E949 File Offset: 0x0040CB49
		public static ulong RandomNextSeed(ulong seed)
		{
			return (seed * 25214903917UL + 11UL) & 281474976710655UL;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0040E964 File Offset: 0x0040CB64
		public static float RandomFloat(ref ulong seed)
		{
			return (float)Utils.RandomNext(ref seed, 24) / 16777216f;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0040E978 File Offset: 0x0040CB78
		public static int RandomInt(ref ulong seed, int max)
		{
			if ((max & -max) == max)
			{
				return (int)((long)max * (long)Utils.RandomNext(ref seed, 31) >> 31);
			}
			int num;
			int num2;
			do
			{
				num = Utils.RandomNext(ref seed, 31);
				num2 = num % max;
			}
			while (num - num2 + (max - 1) < 0);
			return num2;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0040E9B5 File Offset: 0x0040CBB5
		public static int RandomInt(ref ulong seed, int min, int max)
		{
			return Utils.RandomInt(ref seed, max - min) + min;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0040E9C2 File Offset: 0x0040CBC2
		public static bool PlotTileLine(Vector2 start, Vector2 end, float width, Utils.TileActionAttempt plot)
		{
			return Utils.PlotTileLine(start.ToVector2D(), end.ToVector2D(), (double)width, plot);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0040E9D8 File Offset: 0x0040CBD8
		public static bool PlotTileLine(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double num = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D vector2D3 = new Vector2D(-vector2D2.Y, vector2D2.X) * num;
			Point point = (start - vector2D3).ToTileCoordinates();
			Point point2 = (start + vector2D3).ToTileCoordinates();
			Point point3 = start.ToTileCoordinates();
			Point point4 = end.ToTileCoordinates();
			Point lineMinOffset = new Point(point.X - point3.X, point.Y - point3.Y);
			Point lineMaxOffset = new Point(point2.X - point3.X, point2.Y - point3.Y);
			return Utils.PlotLine(point3.X, point3.Y, point4.X, point4.Y, (int x, int y) => Utils.PlotLine(x + lineMinOffset.X, y + lineMinOffset.Y, x + lineMaxOffset.X, y + lineMaxOffset.Y, plot, false), true);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0040EADC File Offset: 0x0040CCDC
		public static bool PlotTileTale(Vector2D start, Vector2D end, double width, Utils.TileActionAttempt plot)
		{
			double halfWidth = width / 2.0;
			Vector2D vector2D = end - start;
			Vector2D vector2D2 = vector2D / vector2D.Length();
			Vector2D perpOffset = new Vector2D(-vector2D2.Y, vector2D2.X);
			Point pointStart = start.ToTileCoordinates();
			Point point = end.ToTileCoordinates();
			int length = 0;
			Utils.PlotLine(pointStart.X, pointStart.Y, point.X, point.Y, delegate
			{
				int length2 = length;
				length = length2 + 1;
				return true;
			}, true);
			int length3 = length;
			length = length3 - 1;
			int curLength = 0;
			return Utils.PlotLine(pointStart.X, pointStart.Y, point.X, point.Y, delegate(int x, int y)
			{
				double num = 1.0 - (double)curLength / (double)length;
				int curLength2 = curLength;
				curLength = curLength2 + 1;
				Point point2 = (start - perpOffset * halfWidth * num).ToTileCoordinates();
				Point point3 = (start + perpOffset * halfWidth * num).ToTileCoordinates();
				Point point4 = new Point(point2.X - pointStart.X, point2.Y - pointStart.Y);
				Point point5 = new Point(point3.X - pointStart.X, point3.Y - pointStart.Y);
				return Utils.PlotLine(x + point4.X, y + point4.Y, x + point5.X, y + point5.Y, plot, false);
			}, true);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0040EBE4 File Offset: 0x0040CDE4
		public static void FloodFillTile(Point point, float maxDist, Utils.TileActionAttempt plot)
		{
			if (!WorldGen.InWorld(point, 0))
			{
				return;
			}
			List<Point> floodFillQueue = Utils._floodFillQueue1;
			List<Point> floodFillQueue2 = Utils._floodFillQueue2;
			BitSet2D floodFillBitset = Utils._floodFillBitset;
			floodFillBitset.Reset(point, (int)Math.Ceiling((double)maxDist) + 1);
			floodFillQueue2.Add(point);
			floodFillBitset.Add(point);
			while (floodFillQueue2.Count > 0)
			{
				Utils.Swap<List<Point>>(ref floodFillQueue, ref floodFillQueue2);
				floodFillQueue2.Clear();
				foreach (Point point2 in floodFillQueue)
				{
					if (plot(point2.X, point2.Y))
					{
						Point point3 = new Point(point2.X - 1, point2.Y);
						if (WorldGen.InWorld(point3, 0) && floodFillBitset.Add(point3))
						{
							floodFillQueue2.Add(point3);
						}
						point3 = new Point(point2.X + 1, point2.Y);
						if (WorldGen.InWorld(point3, 0) && floodFillBitset.Add(point3))
						{
							floodFillQueue2.Add(point3);
						}
						point3 = new Point(point2.X, point2.Y - 1);
						if (WorldGen.InWorld(point3, 0) && floodFillBitset.Add(point3))
						{
							floodFillQueue2.Add(point3);
						}
						point3 = new Point(point2.X, point2.Y + 1);
						if (WorldGen.InWorld(point3, 0) && floodFillBitset.Add(point3))
						{
							floodFillQueue2.Add(point3);
						}
					}
				}
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0040ED80 File Offset: 0x0040CF80
		public static int RandomConsecutive(double random, int odds)
		{
			return (int)Math.Log(1.0 - random, 1.0 / (double)odds);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0040D28A File Offset: 0x0040B48A
		public static Vector2 RandomVector2(UnifiedRandom random, float min, float max)
		{
			return new Vector2((max - min) * (float)random.NextDouble() + min, (max - min) * (float)random.NextDouble() + min);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0040D336 File Offset: 0x0040B536
		public static Vector2D RandomVector2D(UnifiedRandom random, double min, double max)
		{
			return new Vector2D((max - min) * random.NextDouble() + min, (max - min) * random.NextDouble() + min);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0040ED9F File Offset: 0x0040CF9F
		public static bool IndexInRange<T>(this T[] t, int index)
		{
			return index >= 0 && index < t.Length;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0040EDAD File Offset: 0x0040CFAD
		public static bool IndexInRange<T>(this List<T> t, int index)
		{
			return index >= 0 && index < t.Count;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0040EDBE File Offset: 0x0040CFBE
		public static T SelectRandom<T>(UnifiedRandom random, params T[] choices)
		{
			return choices[random.Next(choices.Length)];
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0040EDD0 File Offset: 0x0040CFD0
		public static void DrawBorderStringFourWay(SpriteBatch sb, DynamicSpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f)
		{
			Color color = borderColor;
			Vector2 zero = Vector2.Zero;
			int i = 0;
			while (i < 5)
			{
				switch (i)
				{
				case 0:
					zero.X = x - 2f;
					zero.Y = y;
					break;
				case 1:
					zero.X = x + 2f;
					zero.Y = y;
					break;
				case 2:
					zero.X = x;
					zero.Y = y - 2f;
					break;
				case 3:
					zero.X = x;
					zero.Y = y + 2f;
					break;
				case 4:
					goto IL_0090;
				default:
					goto IL_0090;
				}
				IL_00A4:
				DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, zero, color, 0f, origin, scale, SpriteEffects.None, 0f, null, null);
				i++;
				continue;
				IL_0090:
				zero.X = x;
				zero.Y = y;
				color = textColor;
				goto IL_00A4;
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0040EEA8 File Offset: 0x0040D0A8
		public static Vector2 DrawBorderString(SpriteBatch sb, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
		{
			if (maxCharactersDisplayed != -1)
			{
				text = Utils.TrimUserString(text, maxCharactersDisplayed);
			}
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			Vector2 vector = value.MeasureString(text);
			ChatManager.DrawColorCodedStringWithShadow(sb, value, text, pos, color, 0f, new Vector2(anchorx, anchory) * vector, new Vector2(scale), -1f, 1.5f);
			return vector * scale;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0040EF10 File Offset: 0x0040D110
		public static Vector2 DrawBorderStringBig(SpriteBatch spriteBatch, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
		{
			if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
			{
				text.Substring(0, maxCharactersDisplayed);
			}
			DynamicSpriteFont value = FontAssets.DeathText.Value;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, value, text, pos + new Vector2((float)i, (float)j), Color.Black, 0f, new Vector2(anchorx, anchory) * value.MeasureString(text), scale, SpriteEffects.None, 0f, null, null);
				}
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, value, text, pos, color, 0f, new Vector2(anchorx, anchory) * value.MeasureString(text), scale, SpriteEffects.None, 0f, null, null);
			return value.MeasureString(text) * scale;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0040EFD5 File Offset: 0x0040D1D5
		public static void DrawInvBG(SpriteBatch sb, Rectangle R, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, R.X, R.Y, R.Width, R.Height, c);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0040EFF6 File Offset: 0x0040D1F6
		public static void DrawInvBG(SpriteBatch sb, float x, float y, float w, float h, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, (int)x, (int)y, (int)w, (int)h, c);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0040F00C File Offset: 0x0040D20C
		public static void DrawInvBG(SpriteBatch sb, int x, int y, int w, int h, Color c = default(Color))
		{
			if (c == default(Color))
			{
				c = new Color(63, 65, 151, 255) * 0.785f;
			}
			Texture2D value = TextureAssets.InventoryBack13.Value;
			if (w < 20)
			{
				w = 20;
			}
			if (h < 20)
			{
				h = 20;
			}
			sb.Draw(value, new Rectangle(x, y, 10, 10), new Rectangle?(new Rectangle(0, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y, w - 20, 10), new Rectangle?(new Rectangle(10, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y, 10, 10), new Rectangle?(new Rectangle(value.Width - 10, 0, 10, 10)), c);
			sb.Draw(value, new Rectangle(x, y + 10, 10, h - 20), new Rectangle?(new Rectangle(0, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y + 10, w - 20, h - 20), new Rectangle?(new Rectangle(10, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y + 10, 10, h - 20), new Rectangle?(new Rectangle(value.Width - 10, 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x, y + h - 10, 10, 10), new Rectangle?(new Rectangle(0, value.Height - 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + 10, y + h - 10, w - 20, 10), new Rectangle?(new Rectangle(10, value.Height - 10, 10, 10)), c);
			sb.Draw(value, new Rectangle(x + w - 10, y + h - 10, 10, 10), new Rectangle?(new Rectangle(value.Width - 10, value.Height - 10, 10, 10)), c);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0040F224 File Offset: 0x0040D424
		public static string ReadEmbeddedResource(string path)
		{
			string text;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0040F280 File Offset: 0x0040D480
		public static void DrawSplicedPanel(SpriteBatch sb, Texture2D texture, int x, int y, int w, int h, int leftEnd, int rightEnd, int topEnd, int bottomEnd, Color c)
		{
			if (w < leftEnd + rightEnd)
			{
				w = leftEnd + rightEnd;
			}
			if (h < topEnd + bottomEnd)
			{
				h = topEnd + bottomEnd;
			}
			sb.Draw(texture, new Rectangle(x, y, leftEnd, topEnd), new Rectangle?(new Rectangle(0, 0, leftEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y, w - leftEnd - rightEnd, topEnd), new Rectangle?(new Rectangle(leftEnd, 0, texture.Width - leftEnd - rightEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y, topEnd, rightEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, 0, rightEnd, topEnd)), c);
			sb.Draw(texture, new Rectangle(x, y + topEnd, leftEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(0, topEnd, leftEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y + topEnd, w - leftEnd - rightEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(leftEnd, topEnd, texture.Width - leftEnd - rightEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y + topEnd, rightEnd, h - topEnd - bottomEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, topEnd, rightEnd, texture.Height - topEnd - bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x, y + h - bottomEnd, leftEnd, bottomEnd), new Rectangle?(new Rectangle(0, texture.Height - bottomEnd, leftEnd, bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + leftEnd, y + h - bottomEnd, w - leftEnd - rightEnd, bottomEnd), new Rectangle?(new Rectangle(leftEnd, texture.Height - bottomEnd, texture.Width - leftEnd - rightEnd, bottomEnd)), c);
			sb.Draw(texture, new Rectangle(x + w - rightEnd, y + h - bottomEnd, rightEnd, bottomEnd), new Rectangle?(new Rectangle(texture.Width - rightEnd, texture.Height - bottomEnd, rightEnd, bottomEnd)), c);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0040F4BD File Offset: 0x0040D6BD
		public static void DrawSettingsPanel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0040F4BD File Offset: 0x0040D6BD
		public static void DrawSettings2Panel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			Utils.DrawPanel(TextureAssets.SettingsPanel.Value, 2, 0, spriteBatch, position, width, color);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0040F4D4 File Offset: 0x0040D6D4
		public static void DrawPanel(Texture2D texture, int edgeWidth, int edgeShove, SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(texture, position, new Rectangle?(new Rectangle(0, 0, edgeWidth, texture.Height)), color);
			spriteBatch.Draw(texture, new Vector2(position.X + (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(edgeWidth + edgeShove, 0, texture.Width - (edgeWidth + edgeShove) * 2, texture.Height)), color, 0f, Vector2.Zero, new Vector2((width - (float)(edgeWidth * 2)) / (float)(texture.Width - (edgeWidth + edgeShove) * 2), 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, new Vector2(position.X + width - (float)edgeWidth, position.Y), new Rectangle?(new Rectangle(texture.Width - edgeWidth, 0, edgeWidth, texture.Height)), color);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0040F5AC File Offset: 0x0040D7AC
		public static void DrawRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			Utils.DrawLine(sb, start, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, start, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(start.X, end.Y), colorStart, colorEnd, width);
			Utils.DrawLine(sb, end, new Vector2(end.X, start.Y), colorStart, colorEnd, width);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0040F630 File Offset: 0x0040D830
		public static void DrawLaser(SpriteBatch sb, Texture2D tex, Vector2 start, Vector2 end, Vector2 scale, Utils.LaserLineFraming framing)
		{
			Vector2 vector = Vector2.Normalize(end - start);
			float num = (end - start).Length();
			float num2 = vector.ToRotation() - 1.5707964f;
			if (vector.HasNaNs())
			{
				return;
			}
			float num3;
			Rectangle rectangle;
			Vector2 vector2;
			Color color;
			framing(0, start, num, default(Rectangle), out num3, out rectangle, out vector2, out color);
			sb.Draw(tex, start, new Rectangle?(rectangle), color, num2, rectangle.Size() / 2f, scale, SpriteEffects.None, 0f);
			num -= num3 * scale.Y;
			Vector2 vector3 = start + vector * ((float)rectangle.Height - vector2.Y) * scale.Y;
			if (num > 0f)
			{
				float num4 = 0f;
				while (num4 + 1f < num)
				{
					framing(1, vector3, num - num4, rectangle, out num3, out rectangle, out vector2, out color);
					if (num - num4 < (float)rectangle.Height)
					{
						num3 *= (num - num4) / (float)rectangle.Height;
						rectangle.Height = (int)(num - num4);
					}
					sb.Draw(tex, vector3, new Rectangle?(rectangle), color, num2, vector2, scale, SpriteEffects.None, 0f);
					num4 += num3 * scale.Y;
					vector3 += vector * num3 * scale.Y;
				}
			}
			framing(2, vector3, num, default(Rectangle), out num3, out rectangle, out vector2, out color);
			sb.Draw(tex, vector3, new Rectangle?(rectangle), color, num2, vector2, scale, SpriteEffects.None, 0f);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0040F7D6 File Offset: 0x0040D9D6
		public static void DrawLine(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawLine(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)(end.X << 4), (float)(end.Y << 4)), color);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0040F810 File Offset: 0x0040DA10
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			float num = Vector2.Distance(start, end);
			Vector2 vector = (end - start) / num;
			Vector2 vector2 = start;
			Vector2 screenPosition = Main.screenPosition;
			float num2 = vector.ToRotation();
			for (float num3 = 0f; num3 <= num; num3 += 4f)
			{
				float num4 = num3 / num;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, vector2 - screenPosition, null, new Color(new Vector4(num4, num4, num4, 1f) * color.ToVector4()), num2, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
				vector2 = start + num3 * vector;
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0040F8C4 File Offset: 0x0040DAC4
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
		{
			float num = Vector2.Distance(start, end);
			float num2 = (end - start).ToRotation();
			int num3 = Math.Min(5, (int)num);
			for (int i = 0; i < num3; i++)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, Vector2.Lerp(start, end, (float)i / (float)num3) - Main.screenPosition, null, Color.Lerp(colorStart, colorEnd, ((float)i + 0.5f) / (float)num3), num2, Vector2.Zero, new Vector2(num / (float)num3 / 16f, width / 16f), SpriteEffects.None, 0f);
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0040F961 File Offset: 0x0040DB61
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRectForTilesInWorld(spriteBatch, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height), color);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0040F99A File Offset: 0x0040DB9A
		public static void DrawRectForTilesInWorld(SpriteBatch spriteBatch, Point start, Point end, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)(start.X << 4), (float)(start.Y << 4)), new Vector2((float)((end.X << 4) - 4), (float)((end.Y << 4) - 4)), color);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0040F9D5 File Offset: 0x0040DBD5
		public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			Utils.DrawRect(spriteBatch, new Vector2((float)rect.X, (float)rect.Y), new Vector2((float)(rect.X + rect.Width), (float)(rect.Y + rect.Height)), color);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0040FA14 File Offset: 0x0040DC14
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
		{
			Utils.DrawLine(spriteBatch, start, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, start, new Vector2(end.X, start.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(start.X, end.Y), color);
			Utils.DrawLine(spriteBatch, end, new Vector2(end.X, start.Y), color);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0040FA85 File Offset: 0x0040DC85
		public static void DrawRect(SpriteBatch spriteBatch, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, Color color)
		{
			Utils.DrawLine(spriteBatch, topLeft, topRight, color);
			Utils.DrawLine(spriteBatch, topRight, bottomRight, color);
			Utils.DrawLine(spriteBatch, bottomRight, bottomLeft, color);
			Utils.DrawLine(spriteBatch, bottomLeft, topLeft, color);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0040FAB4 File Offset: 0x0040DCB4
		public static void DrawSelectedCraftingBarIndicator(SpriteBatch spriteBatch, int craftX, int craftY)
		{
			int num = 16;
			Color ourFavoriteColor = Main.OurFavoriteColor;
			float num2 = 16f;
			for (float num3 = num2; num3 > 0f; num3 -= 1f)
			{
				float num4 = 1f - num3 / num2;
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(craftX - 16, craftY + num + (int)num3 * -1, 32, 2), ourFavoriteColor * (num4 * 0.6f));
			}
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(craftX - 16, craftY + num, 32, 4), ourFavoriteColor);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0040FB44 File Offset: 0x0040DD44
		public static void DrawCursorSingle(SpriteBatch sb, Color color, float rot = float.NaN, float scale = 1f, Vector2 manualPosition = default(Vector2), int cursorSlot = 0, int specialMode = 0)
		{
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			Vector2 zero = Vector2.Zero;
			Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (manualPosition != Vector2.Zero)
			{
				vector = manualPosition;
			}
			if (float.IsNaN(rot))
			{
				rot = 0f;
			}
			else
			{
				flag = true;
				rot -= 2.3561945f;
			}
			if (cursorSlot == 4 || cursorSlot == 5)
			{
				flag2 = false;
				zero = new Vector2(8f);
				if (flag && specialMode == 0)
				{
					float num = rot;
					if (num < 0f)
					{
						num += 6.2831855f;
					}
					for (float num2 = 0f; num2 < 4f; num2 += 1f)
					{
						if (Math.Abs(num - 1.5707964f * num2) <= 0.7853982f)
						{
							rot = 1.5707964f * num2;
							break;
						}
					}
				}
			}
			Vector2 vector2 = Vector2.One;
			if ((Main.ThickMouse && cursorSlot == 0) || cursorSlot == 1)
			{
				vector2 = Main.DrawThickCursor(cursorSlot == 1);
			}
			if (flag2)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, vector + vector2 + Vector2.One, null, color.MultiplyRGB(new Color(0.2f, 0.2f, 0.2f, 0.5f)), rot, zero, scale * 1.1f, SpriteEffects.None, 0f);
			}
			if (flag3)
			{
				sb.Draw(TextureAssets.Cursors[cursorSlot].Value, vector + vector2, null, color, rot, zero, scale, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0040FCCC File Offset: 0x0040DECC
		public static bool TryOperateInLock(object _lock, Action action)
		{
			if (!Monitor.TryEnter(_lock))
			{
				return false;
			}
			bool flag;
			try
			{
				action();
				flag = true;
			}
			finally
			{
				Monitor.Exit(_lock);
			}
			return flag;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0040FD08 File Offset: 0x0040DF08
		public static bool ParseCommandPrefix(string text, string prefix, out string remainder)
		{
			remainder = "";
			if (!text.StartsWith(prefix, true, CultureInfo.InvariantCulture))
			{
				return false;
			}
			if (text.Length == prefix.Length)
			{
				return true;
			}
			if (text[prefix.Length] != ' ')
			{
				return false;
			}
			remainder = text.Substring(prefix.Length + 1);
			return true;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0040FD60 File Offset: 0x0040DF60
		public static string TrimUserString(string s, int length)
		{
			if (s.Length <= length)
			{
				return s;
			}
			if (length > 0 && char.IsHighSurrogate(s[length - 1]))
			{
				length--;
			}
			return s.Substring(0, length);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0040FD8E File Offset: 0x0040DF8E
		public static string TrimLastCharacter(string s)
		{
			return Utils.TrimUserString(s, s.Length - 1);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0040FDA0 File Offset: 0x0040DFA0
		// Note: this type is marked as 'beforefieldinit'.
		static Utils()
		{
		}

		// Token: 0x04000ED7 RID: 3799
		public static readonly int MaxFloatInt = 16777216;

		// Token: 0x04000ED8 RID: 3800
		public const long MaxCoins = 9999999999L;

		// Token: 0x04000ED9 RID: 3801
		public static Dictionary<DynamicSpriteFont, float[]> charLengths = new Dictionary<DynamicSpriteFont, float[]>();

		// Token: 0x04000EDA RID: 3802
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x04000EDB RID: 3803
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x04000EDC RID: 3804
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x04000EDD RID: 3805
		private const ulong RANDOM_MASK = 281474976710655UL;

		// Token: 0x04000EDE RID: 3806
		private static readonly List<Point> _floodFillQueue1 = new List<Point>(2500);

		// Token: 0x04000EDF RID: 3807
		private static readonly List<Point> _floodFillQueue2 = new List<Point>(2500);

		// Token: 0x04000EE0 RID: 3808
		private static readonly BitSet2D _floodFillBitset = new BitSet2D();

		// Token: 0x02000642 RID: 1602
		// (Invoke) Token: 0x06003CB5 RID: 15541
		public delegate bool TileActionAttempt(int x, int y);

		// Token: 0x02000643 RID: 1603
		// (Invoke) Token: 0x06003CB9 RID: 15545
		public delegate void LaserLineFraming(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distanceCovered, out Rectangle frame, out Vector2 origin, out Color color);

		// Token: 0x02000644 RID: 1604
		// (Invoke) Token: 0x06003CBD RID: 15549
		public delegate Color ColorLerpMethod(float percent);

		// Token: 0x02000645 RID: 1605
		public class RandomTeleportationAttemptSettings
		{
			// Token: 0x06003CC0 RID: 15552 RVA: 0x0000357B File Offset: 0x0000177B
			public RandomTeleportationAttemptSettings()
			{
			}

			// Token: 0x04006563 RID: 25955
			public Vector2 teleporteeSize;

			// Token: 0x04006564 RID: 25956
			public Vector2 teleporteeVelocity;

			// Token: 0x04006565 RID: 25957
			public float teleporteeGravityDirection;

			// Token: 0x04006566 RID: 25958
			public bool mostlySolidFloor;

			// Token: 0x04006567 RID: 25959
			public bool avoidLava;

			// Token: 0x04006568 RID: 25960
			public bool avoidAnyLiquid;

			// Token: 0x04006569 RID: 25961
			public bool avoidHurtTiles;

			// Token: 0x0400656A RID: 25962
			public bool avoidWalls;

			// Token: 0x0400656B RID: 25963
			public int attemptsBeforeGivingUp;

			// Token: 0x0400656C RID: 25964
			public int maximumFallDistanceFromOrignalPoint;

			// Token: 0x0400656D RID: 25965
			public bool strictRange;

			// Token: 0x0400656E RID: 25966
			public int[] tilesToAvoid;

			// Token: 0x0400656F RID: 25967
			public int tilesToAvoidRange;

			// Token: 0x04006570 RID: 25968
			public bool allowSolidTopFloor;

			// Token: 0x04006571 RID: 25969
			public Func<Tile, int, int, bool> specializedConditions;
		}

		// Token: 0x02000646 RID: 1606
		public struct ChaseResults
		{
			// Token: 0x04006572 RID: 25970
			public bool InterceptionHappens;

			// Token: 0x04006573 RID: 25971
			public Vector2 InterceptionPosition;

			// Token: 0x04006574 RID: 25972
			public float InterceptionTime;

			// Token: 0x04006575 RID: 25973
			public Vector2 ChaserVelocity;
		}

		// Token: 0x02000647 RID: 1607
		[CompilerGenerated]
		private sealed class <>c__DisplayClass73_0
		{
			// Token: 0x06003CC1 RID: 15553 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass73_0()
			{
			}

			// Token: 0x06003CC2 RID: 15554 RVA: 0x0066E8BC File Offset: 0x0066CABC
			internal string <FormatWith>b__0(Match match)
			{
				if (match.Groups[1].Length != 0)
				{
					return "";
				}
				string text = match.Groups[2].ToString();
				PropertyDescriptor propertyDescriptor = this.properties.Find(text, false);
				if (propertyDescriptor == null)
				{
					return "";
				}
				return (propertyDescriptor.GetValue(this.obj) ?? "").ToString();
			}

			// Token: 0x04006576 RID: 25974
			public PropertyDescriptorCollection properties;

			// Token: 0x04006577 RID: 25975
			public object obj;
		}

		// Token: 0x02000648 RID: 1608
		[CompilerGenerated]
		private sealed class <>c__DisplayClass214_0
		{
			// Token: 0x06003CC3 RID: 15555 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass214_0()
			{
			}

			// Token: 0x06003CC4 RID: 15556 RVA: 0x0066E928 File Offset: 0x0066CB28
			internal bool <PlotTileLine>b__0(int x, int y)
			{
				return Utils.PlotLine(x + this.lineMinOffset.X, y + this.lineMinOffset.Y, x + this.lineMaxOffset.X, y + this.lineMaxOffset.Y, this.plot, false);
			}

			// Token: 0x04006578 RID: 25976
			public Point lineMinOffset;

			// Token: 0x04006579 RID: 25977
			public Point lineMaxOffset;

			// Token: 0x0400657A RID: 25978
			public Utils.TileActionAttempt plot;
		}

		// Token: 0x02000649 RID: 1609
		[CompilerGenerated]
		private sealed class <>c__DisplayClass215_0
		{
			// Token: 0x06003CC5 RID: 15557 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass215_0()
			{
			}

			// Token: 0x06003CC6 RID: 15558 RVA: 0x0066E978 File Offset: 0x0066CB78
			internal bool <PlotTileTale>b__0(int <p0>, int <p1>)
			{
				int num = this.length;
				this.length = num + 1;
				return true;
			}

			// Token: 0x06003CC7 RID: 15559 RVA: 0x0066E998 File Offset: 0x0066CB98
			internal bool <PlotTileTale>b__1(int x, int y)
			{
				double num = 1.0 - (double)this.curLength / (double)this.length;
				int num2 = this.curLength;
				this.curLength = num2 + 1;
				Point point = (this.start - this.perpOffset * this.halfWidth * num).ToTileCoordinates();
				Point point2 = (this.start + this.perpOffset * this.halfWidth * num).ToTileCoordinates();
				Point point3 = new Point(point.X - this.pointStart.X, point.Y - this.pointStart.Y);
				Point point4 = new Point(point2.X - this.pointStart.X, point2.Y - this.pointStart.Y);
				return Utils.PlotLine(x + point3.X, y + point3.Y, x + point4.X, y + point4.Y, this.plot, false);
			}

			// Token: 0x0400657B RID: 25979
			public int length;

			// Token: 0x0400657C RID: 25980
			public int curLength;

			// Token: 0x0400657D RID: 25981
			public Vector2D start;

			// Token: 0x0400657E RID: 25982
			public Vector2D perpOffset;

			// Token: 0x0400657F RID: 25983
			public double halfWidth;

			// Token: 0x04006580 RID: 25984
			public Point pointStart;

			// Token: 0x04006581 RID: 25985
			public Utils.TileActionAttempt plot;
		}
	}
}
