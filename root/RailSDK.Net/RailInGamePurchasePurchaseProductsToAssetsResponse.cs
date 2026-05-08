using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000D8 RID: 216
	public class RailInGamePurchasePurchaseProductsToAssetsResponse : EventBase
	{
		// Token: 0x0600172F RID: 5935 RVA: 0x00010C03 File Offset: 0x0000EE03
		public RailInGamePurchasePurchaseProductsToAssetsResponse()
		{
		}

		// Token: 0x04000255 RID: 597
		public string order_id;

		// Token: 0x04000256 RID: 598
		public List<RailAssetInfo> delivered_assets = new List<RailAssetInfo>();
	}
}
