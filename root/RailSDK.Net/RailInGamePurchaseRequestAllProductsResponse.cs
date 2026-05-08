using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000D9 RID: 217
	public class RailInGamePurchaseRequestAllProductsResponse : EventBase
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00010C16 File Offset: 0x0000EE16
		public RailInGamePurchaseRequestAllProductsResponse()
		{
		}

		// Token: 0x04000257 RID: 599
		public List<RailPurchaseProductInfo> all_products = new List<RailPurchaseProductInfo>();
	}
}
