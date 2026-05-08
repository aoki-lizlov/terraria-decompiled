using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x0200015D RID: 349
	public abstract class WorkshopSocialModule : ISocialModule
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00501C01 File Offset: 0x004FFE01
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x00501C09 File Offset: 0x004FFE09
		public WorkshopBranding Branding
		{
			[CompilerGenerated]
			get
			{
				return this.<Branding>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Branding>k__BackingField = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00501C12 File Offset: 0x004FFE12
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x00501C1A File Offset: 0x004FFE1A
		public AWorkshopProgressReporter ProgressReporter
		{
			[CompilerGenerated]
			get
			{
				return this.<ProgressReporter>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ProgressReporter>k__BackingField = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00501C23 File Offset: 0x004FFE23
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00501C2B File Offset: 0x004FFE2B
		public AWorkshopTagsCollection SupportedTags
		{
			[CompilerGenerated]
			get
			{
				return this.<SupportedTags>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SupportedTags>k__BackingField = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00501C34 File Offset: 0x004FFE34
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x00501C3C File Offset: 0x004FFE3C
		public WorkshopIssueReporter IssueReporter
		{
			[CompilerGenerated]
			get
			{
				return this.<IssueReporter>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IssueReporter>k__BackingField = value;
			}
		}

		// Token: 0x06001D68 RID: 7528
		public abstract void Initialize();

		// Token: 0x06001D69 RID: 7529
		public abstract void Shutdown();

		// Token: 0x06001D6A RID: 7530
		public abstract void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings);

		// Token: 0x06001D6B RID: 7531
		public abstract void PublishResourcePack(ResourcePack resourcePack, WorkshopItemPublishSettings settings);

		// Token: 0x06001D6C RID: 7532
		public abstract bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info);

		// Token: 0x06001D6D RID: 7533
		public abstract bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info);

		// Token: 0x06001D6E RID: 7534
		public abstract void LoadEarlyContent();

		// Token: 0x06001D6F RID: 7535
		public abstract List<string> GetListOfSubscribedResourcePackPaths();

		// Token: 0x06001D70 RID: 7536
		public abstract List<string> GetListOfSubscribedWorldPaths();

		// Token: 0x06001D71 RID: 7537
		public abstract bool TryGetPath(string pathEnd, out string fullPathFound);

		// Token: 0x06001D72 RID: 7538
		public abstract void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newDisplayName, Action onCompleted);

		// Token: 0x06001D73 RID: 7539 RVA: 0x0000357B File Offset: 0x0000177B
		protected WorkshopSocialModule()
		{
		}

		// Token: 0x04001651 RID: 5713
		[CompilerGenerated]
		private WorkshopBranding <Branding>k__BackingField;

		// Token: 0x04001652 RID: 5714
		[CompilerGenerated]
		private AWorkshopProgressReporter <ProgressReporter>k__BackingField;

		// Token: 0x04001653 RID: 5715
		[CompilerGenerated]
		private AWorkshopTagsCollection <SupportedTags>k__BackingField;

		// Token: 0x04001654 RID: 5716
		[CompilerGenerated]
		private WorkshopIssueReporter <IssueReporter>k__BackingField;
	}
}
