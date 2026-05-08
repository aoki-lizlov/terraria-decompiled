using System;

namespace rail
{
	// Token: 0x02000040 RID: 64
	public interface IRailGlobalAchievement : IRailComponent
	{
		// Token: 0x060015CA RID: 5578
		RailResult AsyncRequestAchievement(string user_data);

		// Token: 0x060015CB RID: 5579
		RailResult GetGlobalAchievedPercent(string name, out double percent);

		// Token: 0x060015CC RID: 5580
		RailResult GetGlobalAchievedPercentDescending(int index, out string name, out double percent);
	}
}
