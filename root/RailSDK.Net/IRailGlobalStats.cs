using System;

namespace rail
{
	// Token: 0x0200013D RID: 317
	public interface IRailGlobalStats : IRailComponent
	{
		// Token: 0x060017E9 RID: 6121
		RailResult AsyncRequestGlobalStats(string user_data);

		// Token: 0x060017EA RID: 6122
		RailResult GetGlobalStatValue(string name, out long data);

		// Token: 0x060017EB RID: 6123
		RailResult GetGlobalStatValue(string name, out double data);

		// Token: 0x060017EC RID: 6124
		RailResult GetGlobalStatValueHistory(string name, long[] global_stats_data, uint data_size, out int num_global_stats);

		// Token: 0x060017ED RID: 6125
		RailResult GetGlobalStatValueHistory(string name, double[] global_stats_data, uint data_size, out int num_global_stats);
	}
}
