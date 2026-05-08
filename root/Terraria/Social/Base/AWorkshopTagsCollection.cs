using System;
using System.Collections.Generic;

namespace Terraria.Social.Base
{
	// Token: 0x0200015F RID: 351
	public abstract class AWorkshopTagsCollection
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x00501C5B File Offset: 0x004FFE5B
		protected void AddWorldTag(string tagNameKey, string tagInternalName)
		{
			this.WorldTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00501C6F File Offset: 0x004FFE6F
		protected void AddResourcePackTag(string tagNameKey, string tagInternalName)
		{
			this.ResourcePackTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00501C83 File Offset: 0x004FFE83
		protected AWorkshopTagsCollection()
		{
		}

		// Token: 0x04001657 RID: 5719
		public readonly List<WorkshopTagOption> WorldTags = new List<WorkshopTagOption>();

		// Token: 0x04001658 RID: 5720
		public readonly List<WorkshopTagOption> ResourcePackTags = new List<WorkshopTagOption>();
	}
}
