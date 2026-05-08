using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000530 RID: 1328
	public interface IRoomCheckFeedback_Scoring
	{
		// Token: 0x06003705 RID: 14085
		void BeginScoring();

		// Token: 0x06003706 RID: 14086
		void ReportScore(int x, int y, int score);

		// Token: 0x06003707 RID: 14087
		void SetAsHighScore(int x, int y, int score);

		// Token: 0x06003708 RID: 14088
		void EndScoring();
	}
}
