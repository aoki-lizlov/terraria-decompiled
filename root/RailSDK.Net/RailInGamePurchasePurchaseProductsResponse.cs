using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000D7 RID: 215
	public class RailInGamePurchasePurchaseProductsResponse : EventBase
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public RailInGamePurchasePurchaseProductsResponse()
		{
		}

		// Token: 0x04000253 RID: 595
		public string order_id;

		// Token: 0x04000254 RID: 596
		public List<RailProductItem> delivered_products = new List<RailProductItem>();
	}
}
