using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000164 RID: 356
	public abstract class AchievementsSocialModule : ISocialModule
	{
		// Token: 0x06001DA3 RID: 7587
		public abstract void Initialize();

		// Token: 0x06001DA4 RID: 7588
		public abstract void Shutdown();

		// Token: 0x06001DA5 RID: 7589
		public abstract byte[] GetEncryptionKey();

		// Token: 0x06001DA6 RID: 7590
		public abstract string GetSavePath();

		// Token: 0x06001DA7 RID: 7591
		public abstract void UpdateIntStat(string name, int value);

		// Token: 0x06001DA8 RID: 7592
		public abstract void UpdateFloatStat(string name, float value);

		// Token: 0x06001DA9 RID: 7593
		public abstract void CompleteAchievement(string name);

		// Token: 0x06001DAA RID: 7594
		public abstract bool IsAchievementCompleted(string name);

		// Token: 0x06001DAB RID: 7595
		public abstract void StoreStats();

		// Token: 0x06001DAC RID: 7596 RVA: 0x0000357B File Offset: 0x0000177B
		protected AchievementsSocialModule()
		{
		}
	}
}
