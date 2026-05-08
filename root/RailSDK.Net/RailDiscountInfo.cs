using System;

namespace rail
{
	// Token: 0x020000D5 RID: 213
	public class RailDiscountInfo
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x00002119 File Offset: 0x00000319
		public RailDiscountInfo()
		{
		}

		// Token: 0x0400024D RID: 589
		public PurchaseProductDiscountType type;

		// Token: 0x0400024E RID: 590
		public uint start_time;

		// Token: 0x0400024F RID: 591
		public float off;

		// Token: 0x04000250 RID: 592
		public float discount_price;

		// Token: 0x04000251 RID: 593
		public uint end_time;
	}
}
