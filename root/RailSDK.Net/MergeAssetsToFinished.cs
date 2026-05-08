using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000051 RID: 81
	public class MergeAssetsToFinished : EventBase
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x0000F1D3 File Offset: 0x0000D3D3
		public MergeAssetsToFinished()
		{
		}

		// Token: 0x04000027 RID: 39
		public ulong merge_to_asset_id;

		// Token: 0x04000028 RID: 40
		public List<RailAssetItem> source_assets = new List<RailAssetItem>();
	}
}
