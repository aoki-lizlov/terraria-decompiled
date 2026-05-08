using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000D1 RID: 209
	public interface IRailInGamePurchase
	{
		// Token: 0x06001726 RID: 5926
		RailResult AsyncRequestAllPurchasableProducts(string user_data);

		// Token: 0x06001727 RID: 5927
		RailResult AsyncRequestAllProducts(string user_data);

		// Token: 0x06001728 RID: 5928
		RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product);

		// Token: 0x06001729 RID: 5929
		RailResult AsyncPurchaseProducts(List<RailProductItem> cart_items, string user_data);

		// Token: 0x0600172A RID: 5930
		RailResult AsyncFinishOrder(string order_id, string user_data);

		// Token: 0x0600172B RID: 5931
		RailResult AsyncPurchaseProductsToAssets(List<RailProductItem> cart_items, string user_data);
	}
}
