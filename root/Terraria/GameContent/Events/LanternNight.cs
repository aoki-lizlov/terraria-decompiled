using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004F8 RID: 1272
	public class LanternNight
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x006183E6 File Offset: 0x006165E6
		public static bool LanternsUp
		{
			get
			{
				return LanternNight.GenuineLanterns || LanternNight.ManualLanterns;
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x006183F8 File Offset: 0x006165F8
		public static void CheckMorning()
		{
			if (LanternNight.GenuineLanterns)
			{
				LanternNight.GenuineLanterns = false;
			}
			if (LanternNight.ManualLanterns)
			{
				LanternNight.ManualLanterns = false;
			}
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x00618427 File Offset: 0x00616627
		public static void CheckNight()
		{
			LanternNight.NaturalAttempt();
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x0061842E File Offset: 0x0061662E
		public static bool LanternsCanPersist()
		{
			return !Main.dayTime && LanternNight.LanternsCanStart();
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x0061843E File Offset: 0x0061663E
		public static bool LanternsCanStart()
		{
			return !WorldGen.spawnMeteor && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon && Main.invasionType == 0 && NPC.MoonLordCountdown == 0 && !LanternNight.BossIsActive();
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x00618474 File Offset: 0x00616674
		private static bool BossIsActive()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && (npc.boss || (npc.type >= 13 && npc.type <= 15)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x006184C0 File Offset: 0x006166C0
		private static void NaturalAttempt()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!LanternNight.LanternsCanStart())
			{
				return;
			}
			bool flag = false;
			if (LanternNight.LanternNightsOnCooldown > 0)
			{
				LanternNight.LanternNightsOnCooldown--;
			}
			if (LanternNight.LanternNightsOnCooldown == 0 && NPC.downedMoonlord && Main.rand.Next(14) == 0)
			{
				flag = true;
			}
			if (!flag && LanternNight.NextNightIsLanternNight)
			{
				LanternNight.NextNightIsLanternNight = false;
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			LanternNight.GenuineLanterns = true;
			LanternNight.LanternNightsOnCooldown = Main.rand.Next(5, 11);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x00618540 File Offset: 0x00616740
		public static void ToggleManualLanterns()
		{
			bool lanternsUp = LanternNight.LanternsUp;
			if (Main.netMode != 1)
			{
				LanternNight.ManualLanterns = !LanternNight.ManualLanterns;
			}
			if (lanternsUp != LanternNight.LanternsUp && Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x00618592 File Offset: 0x00616792
		public static void WorldClear()
		{
			LanternNight.ManualLanterns = false;
			LanternNight.GenuineLanterns = false;
			LanternNight.LanternNightsOnCooldown = 0;
			LanternNight._wasLanternNight = false;
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x006185AC File Offset: 0x006167AC
		public static void UpdateTime()
		{
			if (LanternNight.GenuineLanterns && !LanternNight.LanternsCanPersist())
			{
				LanternNight.GenuineLanterns = false;
			}
			if (LanternNight._wasLanternNight != LanternNight.LanternsUp)
			{
				if (Main.netMode != 2)
				{
					if (LanternNight.LanternsUp)
					{
						SkyManager.Instance.Activate("Lantern", default(Vector2), new object[0]);
					}
					else
					{
						SkyManager.Instance.Deactivate("Lantern", new object[0]);
					}
				}
				else
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			LanternNight._wasLanternNight = LanternNight.LanternsUp;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x0000357B File Offset: 0x0000177B
		public LanternNight()
		{
		}

		// Token: 0x04005ABB RID: 23227
		public static bool ManualLanterns;

		// Token: 0x04005ABC RID: 23228
		public static bool GenuineLanterns;

		// Token: 0x04005ABD RID: 23229
		public static bool NextNightIsLanternNight;

		// Token: 0x04005ABE RID: 23230
		public static int LanternNightsOnCooldown;

		// Token: 0x04005ABF RID: 23231
		private static bool _wasLanternNight;
	}
}
