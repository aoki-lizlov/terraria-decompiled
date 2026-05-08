using System;

namespace rail
{
	// Token: 0x020000DC RID: 220
	public class RailPurchaseProductInfo
	{
		// Token: 0x06001733 RID: 5939 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public RailPurchaseProductInfo()
		{
		}

		// Token: 0x0400025B RID: 603
		public string category;

		// Token: 0x0400025C RID: 604
		public float original_price;

		// Token: 0x0400025D RID: 605
		public string description;

		// Token: 0x0400025E RID: 606
		public RailDiscountInfo discount = new RailDiscountInfo();

		// Token: 0x0400025F RID: 607
		public bool is_purchasable;

		// Token: 0x04000260 RID: 608
		public string name;

		// Token: 0x04000261 RID: 609
		public string currency_type;

		// Token: 0x04000262 RID: 610
		public string product_thumbnail;

		// Token: 0x04000263 RID: 611
		public RailPurchaseProductExtraInfo extra_info = new RailPurchaseProductExtraInfo();

		// Token: 0x04000264 RID: 612
		public uint product_id;
	}
}
