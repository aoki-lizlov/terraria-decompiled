using System;

namespace Terraria.Achievements
{
	// Token: 0x020005E8 RID: 1512
	public interface IAchievementTracker
	{
		// Token: 0x06003B4D RID: 15181
		void ReportAs(string name);

		// Token: 0x06003B4E RID: 15182
		TrackerType GetTrackerType();

		// Token: 0x06003B4F RID: 15183
		void Load();

		// Token: 0x06003B50 RID: 15184
		void Clear();
	}
}
