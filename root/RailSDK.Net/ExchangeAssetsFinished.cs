using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200004E RID: 78
	public class ExchangeAssetsFinished : EventBase
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x0000F184 File Offset: 0x0000D384
		public ExchangeAssetsFinished()
		{
		}

		// Token: 0x0400001F RID: 31
		public RailProductItem to_product_info = new RailProductItem();

		// Token: 0x04000020 RID: 32
		public List<RailAssetItem> old_assets = new List<RailAssetItem>();

		// Token: 0x04000021 RID: 33
		public ulong new_asset_id;
	}
}
