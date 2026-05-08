using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000041 RID: 65
	public interface IRailPlayerAchievement : IRailComponent
	{
		// Token: 0x060015CD RID: 5581
		RailID GetRailID();

		// Token: 0x060015CE RID: 5582
		RailResult AsyncRequestAchievement(string user_data);

		// Token: 0x060015CF RID: 5583
		RailResult HasAchieved(string name, out bool achieved);

		// Token: 0x060015D0 RID: 5584
		RailResult GetAchievementInfo(string name, out string achievement_info);

		// Token: 0x060015D1 RID: 5585
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value, string user_data);

		// Token: 0x060015D2 RID: 5586
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value);

		// Token: 0x060015D3 RID: 5587
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value);

		// Token: 0x060015D4 RID: 5588
		RailResult MakeAchievement(string name);

		// Token: 0x060015D5 RID: 5589
		RailResult CancelAchievement(string name);

		// Token: 0x060015D6 RID: 5590
		RailResult AsyncStoreAchievement(string user_data);

		// Token: 0x060015D7 RID: 5591
		RailResult ResetAllAchievements();

		// Token: 0x060015D8 RID: 5592
		RailResult GetAllAchievementsName(List<string> names);
	}
}
