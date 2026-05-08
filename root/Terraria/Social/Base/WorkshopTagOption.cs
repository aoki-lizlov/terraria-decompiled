using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200015E RID: 350
	public class WorkshopTagOption
	{
		// Token: 0x06001D74 RID: 7540 RVA: 0x00501C45 File Offset: 0x004FFE45
		public WorkshopTagOption(string nameKey, string internalName)
		{
			this.NameKey = nameKey;
			this.InternalNameForAPIs = internalName;
		}

		// Token: 0x04001655 RID: 5717
		public readonly string NameKey;

		// Token: 0x04001656 RID: 5718
		public readonly string InternalNameForAPIs;
	}
}
