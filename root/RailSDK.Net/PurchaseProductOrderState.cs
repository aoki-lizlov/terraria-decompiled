using System;

namespace rail
{
	// Token: 0x020000D4 RID: 212
	public enum PurchaseProductOrderState
	{
		// Token: 0x04000249 RID: 585
		kPurchaseProductOrderStateInvalid,
		// Token: 0x0400024A RID: 586
		kPurchaseProductOrderStateCreateOrderOk = 100,
		// Token: 0x0400024B RID: 587
		kPurchaseProductOrderStatePayOk = 200,
		// Token: 0x0400024C RID: 588
		kPurchaseProductOrderStateDeliverOk = 300
	}
}
