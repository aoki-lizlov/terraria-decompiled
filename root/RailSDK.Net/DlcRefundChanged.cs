using System;

namespace rail
{
	// Token: 0x02000084 RID: 132
	public class DlcRefundChanged : EventBase
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x000108E6 File Offset: 0x0000EAE6
		public DlcRefundChanged()
		{
		}

		// Token: 0x040000B6 RID: 182
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000B7 RID: 183
		public EnumRailGameRefundState refund_state;
	}
}
