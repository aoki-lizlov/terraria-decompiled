using System;

namespace rail
{
	// Token: 0x02000043 RID: 67
	public class PlayerAchievementStored : EventBase
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x0000F049 File Offset: 0x0000D249
		public PlayerAchievementStored()
		{
		}

		// Token: 0x04000009 RID: 9
		public bool group_achievement;

		// Token: 0x0400000A RID: 10
		public string achievement_name;

		// Token: 0x0400000B RID: 11
		public uint current_progress;

		// Token: 0x0400000C RID: 12
		public uint max_progress;
	}
}
