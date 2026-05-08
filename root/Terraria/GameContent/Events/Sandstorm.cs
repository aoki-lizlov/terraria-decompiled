using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FC RID: 1276
	public class Sandstorm
	{
		// Token: 0x060035BE RID: 13758 RVA: 0x0061CA9B File Offset: 0x0061AC9B
		private static bool HasSufficientWind()
		{
			return Math.Abs(Main.windSpeedCurrent) >= 0.6f;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0061CAB1 File Offset: 0x0061ACB1
		public static void WorldClear()
		{
			Sandstorm.Happening = false;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0061CABC File Offset: 0x0061ACBC
		public static void UpdateTime()
		{
			if (Main.netMode != 1)
			{
				if (Sandstorm.Happening)
				{
					if (Sandstorm.TimeLeft > 86400)
					{
						Sandstorm.TimeLeft = 0;
					}
					Sandstorm.TimeLeft -= Main.dayRate;
					if (!Sandstorm.HasSufficientWind())
					{
						Sandstorm.TimeLeft -= 15 * Main.dayRate;
					}
					if (Main.windSpeedCurrent == 0f)
					{
						Sandstorm.TimeLeft = 0;
					}
					if (Sandstorm.TimeLeft <= 0)
					{
						Sandstorm.StopSandstorm();
					}
				}
				else
				{
					int num = 21600;
					if (Main.hardMode)
					{
						num *= 2;
					}
					else
					{
						num *= 3;
					}
					if (Sandstorm.HasSufficientWind())
					{
						for (int i = 0; i < Main.dayRate; i++)
						{
							if (Main.rand.Next(num) == 0)
							{
								Sandstorm.StartSandstorm();
							}
						}
					}
				}
				if (Main.rand.Next(18000) == 0)
				{
					Sandstorm.ChangeSeverityIntentions();
				}
			}
			Sandstorm.UpdateSeverity();
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0061CB94 File Offset: 0x0061AD94
		private static void ChangeSeverityIntentions()
		{
			if (Sandstorm.Happening)
			{
				Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
			}
			else if (Main.rand.Next(3) == 0)
			{
				Sandstorm.IntendedSeverity = 0f;
			}
			else
			{
				Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
			}
			if (Main.netMode != 1)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x0061CC14 File Offset: 0x0061AE14
		private static void UpdateSeverity()
		{
			if (float.IsNaN(Sandstorm.Severity))
			{
				Sandstorm.Severity = 0f;
			}
			if (float.IsNaN(Sandstorm.IntendedSeverity))
			{
				Sandstorm.IntendedSeverity = 0f;
			}
			int num = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
			Sandstorm.Severity = MathHelper.Clamp(Sandstorm.Severity + 0.003f * (float)num, 0f, 1f);
			int num2 = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
			if (num != num2)
			{
				Sandstorm.Severity = Sandstorm.IntendedSeverity;
			}
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0061CC9F File Offset: 0x0061AE9F
		private static void StartSandstorm()
		{
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = Main.rand.Next(28800, 86401);
			Sandstorm.ChangeSeverityIntentions();
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0061CCC5 File Offset: 0x0061AEC5
		private static void StopSandstorm()
		{
			Sandstorm.Happening = false;
			Sandstorm.TimeLeft = 0;
			Sandstorm.ChangeSeverityIntentions();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0061CCD8 File Offset: 0x0061AED8
		public static bool ShowSandstormVisuals()
		{
			return Sandstorm.Happening && Main.SceneMetrics.ZoneSandstorm && SurfaceBackgroundID.Sets.IsDesertVariant[Main.bgStyle] && Main.bgDelay < 50;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0061CD08 File Offset: 0x0061AF08
		public static void EmitDust()
		{
			if (Main.gamePaused)
			{
				return;
			}
			int desertSandTileCount = Main.SceneMetrics.DesertSandTileCount;
			if (!Sandstorm.ShowSandstormVisuals())
			{
				return;
			}
			if (desertSandTileCount < 100)
			{
				return;
			}
			int num = 1;
			if (Main.rand.Next(num) != 0)
			{
				return;
			}
			int num2 = Math.Sign(Main.windSpeedCurrent);
			float num3 = Math.Abs(Main.windSpeedCurrent);
			if (num3 < 0.01f)
			{
				return;
			}
			float num4 = (float)num2 * MathHelper.Lerp(0.9f, 1f, num3);
			float num5 = 2000f / (float)desertSandTileCount;
			float num6 = 3f / num5;
			num6 = MathHelper.Clamp(num6, 0.77f, 1f);
			int num7 = (int)num5;
			float num8 = (float)Main.screenWidth / (float)Main.maxScreenW;
			int num9 = (int)(1000f * num8);
			float num10 = 20f * Sandstorm.Severity;
			float num11 = (float)num9 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num9 * 0.1f - (float)Dust.SandStormCount;
			if (num11 <= 0f)
			{
				return;
			}
			float num12 = (float)Main.screenWidth + 1000f;
			float num13 = (float)Main.screenHeight;
			WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
			weightedRandom.Add(new Color(200, 160, 20, 180), (double)(Main.SceneMetrics.GetTileCount(53) + Main.SceneMetrics.GetTileCount(396) + Main.SceneMetrics.GetTileCount(397)));
			weightedRandom.Add(new Color(103, 98, 122, 180), (double)(Main.SceneMetrics.GetTileCount(112) + Main.SceneMetrics.GetTileCount(400) + Main.SceneMetrics.GetTileCount(398)));
			weightedRandom.Add(new Color(135, 43, 34, 180), (double)(Main.SceneMetrics.GetTileCount(234) + Main.SceneMetrics.GetTileCount(401) + Main.SceneMetrics.GetTileCount(399)));
			weightedRandom.Add(new Color(213, 196, 197, 180), (double)(Main.SceneMetrics.GetTileCount(116) + Main.SceneMetrics.GetTileCount(403) + Main.SceneMetrics.GetTileCount(402)));
			float num14 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
			float num15 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
			float num16 = (num6 - 0.77f) / 0.23000002f;
			int num17 = (int)MathHelper.Lerp(1f, 10f, num16);
			int num18 = 0;
			while ((float)num18 < num10)
			{
				if (Main.rand.Next(num7 / 4) == 0)
				{
					Vector2 vector = new Vector2(Main.rand.NextFloat() * num12 - 500f, Main.rand.NextFloat() * -50f);
					if (Main.rand.Next(3) == 0 && num2 == 1)
					{
						vector.X = (float)(Main.rand.Next(500) - 500);
					}
					else if (Main.rand.Next(3) == 0 && num2 == -1)
					{
						vector.X = (float)(Main.rand.Next(500) + Main.screenWidth);
					}
					if (vector.X < 0f || vector.X > (float)Main.screenWidth)
					{
						vector.Y += Main.rand.NextFloat() * num13 * 0.9f;
					}
					vector += Main.screenPosition;
					int num19 = (int)vector.X / 16;
					int num20 = (int)vector.Y / 16;
					if (WorldGen.InWorld(num19, num20, 10) && Main.tile[num19, num20] != null && Main.tile[num19, num20].wall == 0)
					{
						for (int i = 0; i < 1; i++)
						{
							Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, 268, 0f, 0f, 0, default(Color), 1f)];
							dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;
							Dust dust2 = dust;
							dust2.velocity.Y = dust2.velocity.Y * dust.scale;
							Dust dust3 = dust;
							dust3.velocity.Y = dust3.velocity.Y * 0.35f;
							dust.velocity.X = num4 * 5f + Main.rand.NextFloat() * 1f;
							Dust dust4 = dust;
							dust4.velocity.X = dust4.velocity.X + num4 * num15 * 20f;
							dust.fadeIn += num15 * 0.2f;
							dust.velocity *= 1f + num14 * 0.5f;
							dust.color = weightedRandom;
							dust.velocity *= 1f + num14;
							dust.velocity *= num6;
							dust.scale = 0.9f;
							num11 -= 1f;
							if (num11 <= 0f)
							{
								break;
							}
							if (Main.rand.Next(num17) != 0)
							{
								i--;
								vector += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
								num19 = (int)vector.X / 16;
								num20 = (int)vector.Y / 16;
								if (WorldGen.InWorld(num19, num20, 10) && Main.tile[num19, num20] != null)
								{
									ushort wall = Main.tile[num19, num20].wall;
								}
							}
						}
						if (num11 <= 0f)
						{
							break;
						}
					}
				}
				num18++;
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x0000357B File Offset: 0x0000177B
		public Sandstorm()
		{
		}

		// Token: 0x04005AE0 RID: 23264
		private const int SANDSTORM_DURATION_MINIMUM = 28800;

		// Token: 0x04005AE1 RID: 23265
		private const int SANDSTORM_DURATION_MAXIMUM = 86400;

		// Token: 0x04005AE2 RID: 23266
		public static bool Happening;

		// Token: 0x04005AE3 RID: 23267
		public static int TimeLeft;

		// Token: 0x04005AE4 RID: 23268
		public static float Severity;

		// Token: 0x04005AE5 RID: 23269
		public static float IntendedSeverity;
	}
}
