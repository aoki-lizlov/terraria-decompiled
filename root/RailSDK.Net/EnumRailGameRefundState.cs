using System;

namespace rail
{
	// Token: 0x0200006F RID: 111
	public enum EnumRailGameRefundState
	{
		// Token: 0x0400008D RID: 141
		kRailGameRefundStateUnknown,
		// Token: 0x0400008E RID: 142
		kRailGameRefundStateApplyReceived = 1000,
		// Token: 0x0400008F RID: 143
		kRailGameRefundStateUserCancelApply = 1100,
		// Token: 0x04000090 RID: 144
		kRailGameRefundStateAdminCancelApply,
		// Token: 0x04000091 RID: 145
		kRailGameRefundStateRefundSuccess = 1200,
		// Token: 0x04000092 RID: 146
		kRailGameRefundStateRefundFailed
	}
}
