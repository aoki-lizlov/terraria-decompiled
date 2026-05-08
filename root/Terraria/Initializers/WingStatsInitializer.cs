using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.Initializers
{
	// Token: 0x02000082 RID: 130
	public class WingStatsInitializer
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x004C8CB0 File Offset: 0x004C6EB0
		public static void Load()
		{
			WingStats[] array = new WingStats[ArmorIDs.Wing.Count];
			float num = 3f;
			float num2 = 6f;
			float num3 = 6.25f;
			float num4 = 6.5f;
			float num5 = 6.75f;
			float num6 = 7f;
			float num7 = 7.5f;
			float num8 = 8f;
			float num9 = 9f;
			int num10 = 25;
			int num11 = 100;
			int num12 = 130;
			int num13 = 150;
			int num14 = 160;
			int num15 = 170;
			int num16 = 180;
			int num17 = 150;
			array[46] = new WingStats(num10, num, 1f, false, -1f, 1f);
			array[1] = new WingStats(num11, num3, 1f, false, -1f, 1f);
			array[2] = new WingStats(num11, num3, 1f, false, -1f, 1f);
			array[25] = new WingStats(num12, num5, 1f, false, -1f, 1f);
			array[7] = new WingStats(num12, num5, 1f, false, -1f, 1f);
			array[6] = new WingStats(num12, num5, 1f, false, -1f, 1f);
			array[10] = new WingStats(num12, num5, 1f, false, -1f, 1f);
			array[4] = new WingStats(num13, num4, 1f, false, -1f, 1f);
			array[15] = new WingStats(num14, num7, 1f, false, -1f, 1f);
			array[5] = new WingStats(num14, num7, 1f, false, -1f, 1f);
			array[14] = new WingStats(num14, num7, 1f, false, -1f, 1f);
			array[9] = new WingStats(num14, num7, 1f, false, -1f, 1f);
			array[13] = new WingStats(num14, num7, 1f, false, -1f, 1f);
			array[11] = new WingStats(num15, num7, 1f, false, -1f, 1f);
			array[8] = new WingStats(num15, num7, 1f, false, -1f, 1f);
			array[27] = new WingStats(num15, num7, 1f, false, -1f, 1f);
			array[24] = new WingStats(num15, num7, 1f, false, -1f, 1f);
			array[22] = new WingStats(num15, num4, 1f, true, 10f, 10f);
			array[21] = new WingStats(num16, num7, 1f, false, -1f, 1f);
			array[20] = new WingStats(num16, num7, 1f, false, -1f, 1f);
			array[12] = new WingStats(num16, num7, 1f, false, -1f, 1f);
			array[23] = new WingStats(num16, num7, 1f, false, -1f, 1f);
			array[26] = new WingStats(num16, num8, 2f, false, -1f, 1f);
			array[45] = new WingStats(num16, num8, 4.5f, true, 16f, 16f);
			array[37] = new WingStats(num13, num6, 2.5f, true, 12f, 12f);
			array[44] = new WingStats(num13, num8, 2f, false, -1f, 1f);
			new WingStats(num13, num2, 2.5f, true, 12f, 12f);
			array[29] = new WingStats(num16, num9, 2.5f, false, -1f, 1f);
			array[32] = new WingStats(num16, num9, 2.5f, false, -1f, 1f);
			array[30] = new WingStats(num16, num4, 1.5f, true, 12f, 12f);
			array[31] = new WingStats(num16, num4, 1.5f, true, 12f, 12f);
			array[48] = new WingStats(num17, num6, 1f, false, -1f, 1f);
			array[49] = new WingStats(num17, num6, 1f, false, -1f, 1f);
			WingStats wingStats = new WingStats(num17, num6, 1f, false, -1f, 1f);
			array[3] = wingStats;
			array[16] = wingStats;
			array[17] = wingStats;
			array[18] = wingStats;
			array[19] = wingStats;
			array[28] = wingStats;
			array[33] = wingStats;
			array[34] = wingStats;
			array[35] = wingStats;
			array[36] = wingStats;
			array[38] = wingStats;
			array[39] = wingStats;
			array[40] = wingStats;
			array[42] = wingStats;
			array[41] = wingStats;
			array[43] = wingStats;
			array[47] = wingStats;
			array[50] = wingStats;
			array[51] = wingStats;
			ArmorIDs.Wing.Sets.Stats = array;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0000357B File Offset: 0x0000177B
		public WingStatsInitializer()
		{
		}
	}
}
