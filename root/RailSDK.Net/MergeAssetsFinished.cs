using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000050 RID: 80
	public class MergeAssetsFinished : EventBase
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public MergeAssetsFinished()
		{
		}

		// Token: 0x04000025 RID: 37
		public List<RailAssetItem> source_assets = new List<RailAssetItem>();

		// Token: 0x04000026 RID: 38
		public ulong new_asset_id;
	}
}
