using System;

namespace rail
{
	// Token: 0x0200013E RID: 318
	public interface IRailPlayerStats : IRailComponent
	{
		// Token: 0x060017EE RID: 6126
		RailID GetRailID();

		// Token: 0x060017EF RID: 6127
		RailResult AsyncRequestStats(string user_data);

		// Token: 0x060017F0 RID: 6128
		RailResult GetStatValue(string name, out int data);

		// Token: 0x060017F1 RID: 6129
		RailResult GetStatValue(string name, out double data);

		// Token: 0x060017F2 RID: 6130
		RailResult SetStatValue(string name, int data);

		// Token: 0x060017F3 RID: 6131
		RailResult SetStatValue(string name, double data);

		// Token: 0x060017F4 RID: 6132
		RailResult UpdateAverageStatValue(string name, double data);

		// Token: 0x060017F5 RID: 6133
		RailResult AsyncStoreStats(string user_data);

		// Token: 0x060017F6 RID: 6134
		RailResult ResetAllStats();
	}
}
