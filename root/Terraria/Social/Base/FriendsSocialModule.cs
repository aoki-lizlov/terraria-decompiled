using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000162 RID: 354
	public abstract class FriendsSocialModule : ISocialModule
	{
		// Token: 0x06001D8E RID: 7566
		public abstract string GetUsername();

		// Token: 0x06001D8F RID: 7567
		public abstract void OpenJoinInterface();

		// Token: 0x06001D90 RID: 7568
		public abstract void Initialize();

		// Token: 0x06001D91 RID: 7569
		public abstract void Shutdown();

		// Token: 0x06001D92 RID: 7570 RVA: 0x0000357B File Offset: 0x0000177B
		protected FriendsSocialModule()
		{
		}
	}
}
