using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200015A RID: 346
	public abstract class AWorkshopProgressReporter
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001D5B RID: 7515
		public abstract bool HasOngoingTasks { get; }

		// Token: 0x06001D5C RID: 7516
		public abstract bool TryGetProgress(out float progress);

		// Token: 0x06001D5D RID: 7517 RVA: 0x0000357B File Offset: 0x0000177B
		protected AWorkshopProgressReporter()
		{
		}
	}
}
