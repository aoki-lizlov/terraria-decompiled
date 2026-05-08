using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.Social.Base
{
	// Token: 0x0200015C RID: 348
	public class WorkshopItemPublishSettings
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x00501BBC File Offset: 0x004FFDBC
		public string[] GetUsedTagsInternalNames()
		{
			return this.UsedTags.Select((WorkshopTagOption x) => x.InternalNameForAPIs).ToArray<string>();
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00501BED File Offset: 0x004FFDED
		public WorkshopItemPublishSettings()
		{
		}

		// Token: 0x0400164E RID: 5710
		public WorkshopTagOption[] UsedTags = new WorkshopTagOption[0];

		// Token: 0x0400164F RID: 5711
		public WorkshopItemPublicSettingId Publicity;

		// Token: 0x04001650 RID: 5712
		public string PreviewImagePath;

		// Token: 0x02000745 RID: 1861
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060040C4 RID: 16580 RVA: 0x0069ED55 File Offset: 0x0069CF55
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060040C5 RID: 16581 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060040C6 RID: 16582 RVA: 0x0069ED61 File Offset: 0x0069CF61
			internal string <GetUsedTagsInternalNames>b__3_0(WorkshopTagOption x)
			{
				return x.InternalNameForAPIs;
			}

			// Token: 0x040069CA RID: 27082
			public static readonly WorkshopItemPublishSettings.<>c <>9 = new WorkshopItemPublishSettings.<>c();

			// Token: 0x040069CB RID: 27083
			public static Func<WorkshopTagOption, string> <>9__3_0;
		}
	}
}
