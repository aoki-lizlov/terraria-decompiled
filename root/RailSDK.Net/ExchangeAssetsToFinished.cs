using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200004F RID: 79
	public class ExchangeAssetsToFinished : EventBase
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x0000F1A2 File Offset: 0x0000D3A2
		public ExchangeAssetsToFinished()
		{
		}

		// Token: 0x04000022 RID: 34
		public ulong exchange_to_asset_id;

		// Token: 0x04000023 RID: 35
		public RailProductItem to_product_info = new RailProductItem();

		// Token: 0x04000024 RID: 36
		public List<RailAssetItem> old_assets = new List<RailAssetItem>();
	}
}
