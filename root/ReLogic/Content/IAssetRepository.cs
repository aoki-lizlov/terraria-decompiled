using System;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x02000097 RID: 151
	public interface IAssetRepository : IDisposable
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600037B RID: 891
		int PendingAssets { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600037C RID: 892
		int TotalAssets { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600037D RID: 893
		int LoadedAssets { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600037E RID: 894
		// (set) Token: 0x0600037F RID: 895
		AssetValueUpdated AssetValueUpdatedHandler { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x06000381 RID: 897
		FailedToLoadAssetCustomAction AssetLoadFailHandler { get; set; }

		// Token: 0x06000382 RID: 898
		void SetSources(IEnumerable<IContentSource> sources, AssetRequestMode mode = AssetRequestMode.ImmediateLoad);

		// Token: 0x06000383 RID: 899
		Asset<T> Request<T>(string assetName, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) where T : class;

		// Token: 0x06000384 RID: 900
		void TransferCompletedAssets();

		// Token: 0x06000385 RID: 901
		void EnableAssetWatcher();

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		AssetWatcherValueUpdated AssetWatcherValueUpdatedHandler { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000389 RID: 905
		AssetWatcherUpdateFailed AssetWatcherUpdateFailedHandler { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600038A RID: 906
		// (set) Token: 0x0600038B RID: 907
		ContentFileUpdated ContentFileUpdatedHandler { get; set; }
	}
}
