using System;

namespace rail
{
	// Token: 0x020000F7 RID: 247
	public enum ChannelExceptionType
	{
		// Token: 0x040002A3 RID: 675
		kExceptionNone,
		// Token: 0x040002A4 RID: 676
		kExceptionLocalNetworkError,
		// Token: 0x040002A5 RID: 677
		kExceptionRelayAddressFailed,
		// Token: 0x040002A6 RID: 678
		kExceptionNegotiationRequestFailed,
		// Token: 0x040002A7 RID: 679
		kExceptionNegotiationResponseFailed,
		// Token: 0x040002A8 RID: 680
		kExceptionNegotiationResponseDataInvalid,
		// Token: 0x040002A9 RID: 681
		kExceptionNegotiationResponseTimeout,
		// Token: 0x040002AA RID: 682
		kExceptionRelayServerOverload,
		// Token: 0x040002AB RID: 683
		kExceptionRelayServerInternalError,
		// Token: 0x040002AC RID: 684
		kExceptionRelayChannelUserFull,
		// Token: 0x040002AD RID: 685
		kExceptionRelayChannelNotFound,
		// Token: 0x040002AE RID: 686
		kExceptionRelayChannelEndByServer
	}
}
