using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200014F RID: 335
	public abstract class PlatformSocialModule : ISocialModule
	{
		// Token: 0x06001D20 RID: 7456
		public abstract void Initialize();

		// Token: 0x06001D21 RID: 7457
		public abstract void Shutdown();

		// Token: 0x06001D22 RID: 7458 RVA: 0x0000357B File Offset: 0x0000177B
		protected PlatformSocialModule()
		{
		}
	}
}
