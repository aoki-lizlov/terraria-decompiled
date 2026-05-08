using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000DA RID: 218
	public class RailInGamePurchaseRequestAllPurchasableProductsResponse : EventBase
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x00010C29 File Offset: 0x0000EE29
		public RailInGamePurchaseRequestAllPurchasableProductsResponse()
		{
		}

		// Token: 0x04000258 RID: 600
		public List<RailPurchaseProductInfo> purchasable_products = new List<RailPurchaseProductInfo>();
	}
}
