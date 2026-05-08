using System;

namespace rail
{
	// Token: 0x0200003F RID: 63
	public interface IRailAchievementHelper
	{
		// Token: 0x060015C8 RID: 5576
		IRailPlayerAchievement CreatePlayerAchievement(RailID player);

		// Token: 0x060015C9 RID: 5577
		IRailGlobalAchievement GetGlobalAchievement();
	}
}
