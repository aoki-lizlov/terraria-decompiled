using System;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004F7 RID: 1271
	public class CreditsRollEvent
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x00618304 File Offset: 0x00616504
		public static bool IsEventOngoing
		{
			get
			{
				return CreditsRollEvent._creditsRollRemainingTime > 0;
			}
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00618310 File Offset: 0x00616510
		public static void TryStartingCreditsRoll()
		{
			CreditsRollEvent._creditsRollRemainingTime = 28800;
			CreditsRollSky creditsRollSky = SkyManager.Instance["CreditsRoll"] as CreditsRollSky;
			if (creditsRollSky != null)
			{
				CreditsRollEvent._creditsRollRemainingTime = creditsRollSky.AmountOfTimeNeededForFullPlay;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(140, -1, -1, null, 0, (float)CreditsRollEvent._creditsRollRemainingTime, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00618374 File Offset: 0x00616574
		public static void SendCreditsRollRemainingTimeToPlayer(int playerIndex)
		{
			if (CreditsRollEvent._creditsRollRemainingTime == 0)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(140, playerIndex, -1, null, 0, (float)CreditsRollEvent._creditsRollRemainingTime, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x006183B2 File Offset: 0x006165B2
		public static void UpdateTime()
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(CreditsRollEvent._creditsRollRemainingTime - 1, 0, 28800);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x006183CB File Offset: 0x006165CB
		public static void Reset()
		{
			CreditsRollEvent._creditsRollRemainingTime = 0;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x006183D3 File Offset: 0x006165D3
		public static void SetRemainingTimeDirect(int time)
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(time, 0, 28800);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x0000357B File Offset: 0x0000177B
		public CreditsRollEvent()
		{
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static CreditsRollEvent()
		{
		}

		// Token: 0x04005AB9 RID: 23225
		private const int MAX_TIME_FOR_CREDITS_ROLL_IN_FRAMES = 28800;

		// Token: 0x04005ABA RID: 23226
		private static int _creditsRollRemainingTime;
	}
}
