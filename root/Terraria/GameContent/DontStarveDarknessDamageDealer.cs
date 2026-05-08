using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200024C RID: 588
	public class DontStarveDarknessDamageDealer
	{
		// Token: 0x0600230C RID: 8972 RVA: 0x0053C575 File Offset: 0x0053A775
		public static void Reset()
		{
			DontStarveDarknessDamageDealer.ResetTimer();
			DontStarveDarknessDamageDealer.saidMessage = false;
			DontStarveDarknessDamageDealer.lastFrameWasTooBright = true;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0053C588 File Offset: 0x0053A788
		private static void ResetTimer()
		{
			DontStarveDarknessDamageDealer.darknessTimer = -1;
			DontStarveDarknessDamageDealer.darknessHitTimer = 0;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0053C596 File Offset: 0x0053A796
		private static int GetDarknessDamagePerHit()
		{
			return 250;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0053C59D File Offset: 0x0053A79D
		private static int GetDarknessTimeBeforeStartingHits()
		{
			return 120;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0053C5A1 File Offset: 0x0053A7A1
		private static int GetDarknessTimeForMessage()
		{
			return 60;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0053C5A8 File Offset: 0x0053A7A8
		public static void Update(Player player)
		{
			if (player.dead || !FocusHelper.AllowDontStarveDarknessDamage || player.shimmering || Main.disableDontStarveDarknessDamage)
			{
				DontStarveDarknessDamageDealer.ResetTimer();
				return;
			}
			DontStarveDarknessDamageDealer.UpdateDarknessState(player);
			int darknessTimeBeforeStartingHits = DontStarveDarknessDamageDealer.GetDarknessTimeBeforeStartingHits();
			if (DontStarveDarknessDamageDealer.darknessTimer >= darknessTimeBeforeStartingHits)
			{
				DontStarveDarknessDamageDealer.darknessTimer = darknessTimeBeforeStartingHits;
				DontStarveDarknessDamageDealer.darknessHitTimer++;
				if (DontStarveDarknessDamageDealer.darknessHitTimer > 60 && !player.immune)
				{
					int darknessDamagePerHit = DontStarveDarknessDamageDealer.GetDarknessDamagePerHit();
					SoundEngine.PlaySound(SoundID.Item1, player.Center, 0f, 1f);
					player.Hurt(PlayerDeathReason.ByOther(17), darknessDamagePerHit, 0, false, false, false, -1, true);
					DontStarveDarknessDamageDealer.darknessHitTimer = 0;
				}
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0053C64C File Offset: 0x0053A84C
		private static void UpdateDarknessState(Player player)
		{
			if (DontStarveDarknessDamageDealer.IsPlayerSafe(player))
			{
				if (DontStarveDarknessDamageDealer.saidMessage)
				{
					if (!Main.getGoodWorld)
					{
						Main.NewText(Language.GetTextValue("Game.DarknessSafe"), 50, 200, 50);
					}
					DontStarveDarknessDamageDealer.saidMessage = false;
				}
				DontStarveDarknessDamageDealer.ResetTimer();
				return;
			}
			int darknessTimeForMessage = DontStarveDarknessDamageDealer.GetDarknessTimeForMessage();
			if (DontStarveDarknessDamageDealer.darknessTimer >= darknessTimeForMessage && !DontStarveDarknessDamageDealer.saidMessage)
			{
				if (!Main.getGoodWorld)
				{
					Main.NewText(Language.GetTextValue("Game.DarknessDanger"), 200, 50, 50);
				}
				DontStarveDarknessDamageDealer.saidMessage = true;
			}
			DontStarveDarknessDamageDealer.darknessTimer++;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0053C6DC File Offset: 0x0053A8DC
		private static bool IsPlayerSafe(Player player)
		{
			return Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16).ToVector3().Length() >= 0.1f;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x0000357B File Offset: 0x0000177B
		public DontStarveDarknessDamageDealer()
		{
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0053C726 File Offset: 0x0053A926
		// Note: this type is marked as 'beforefieldinit'.
		static DontStarveDarknessDamageDealer()
		{
		}

		// Token: 0x04004D46 RID: 19782
		public const int DARKNESS_HIT_TIMER_MAX_BEFORE_HIT = 60;

		// Token: 0x04004D47 RID: 19783
		public static int darknessTimer = -1;

		// Token: 0x04004D48 RID: 19784
		public static int darknessHitTimer = 0;

		// Token: 0x04004D49 RID: 19785
		public static bool saidMessage = false;

		// Token: 0x04004D4A RID: 19786
		public static bool lastFrameWasTooBright = true;
	}
}
