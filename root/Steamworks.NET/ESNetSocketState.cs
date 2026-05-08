using System;

namespace Steamworks
{
	// Token: 0x02000125 RID: 293
	public enum ESNetSocketState
	{
		// Token: 0x04000687 RID: 1671
		k_ESNetSocketStateInvalid,
		// Token: 0x04000688 RID: 1672
		k_ESNetSocketStateConnected,
		// Token: 0x04000689 RID: 1673
		k_ESNetSocketStateInitiated = 10,
		// Token: 0x0400068A RID: 1674
		k_ESNetSocketStateLocalCandidatesFound,
		// Token: 0x0400068B RID: 1675
		k_ESNetSocketStateReceivedRemoteCandidates,
		// Token: 0x0400068C RID: 1676
		k_ESNetSocketStateChallengeHandshake = 15,
		// Token: 0x0400068D RID: 1677
		k_ESNetSocketStateDisconnecting = 21,
		// Token: 0x0400068E RID: 1678
		k_ESNetSocketStateLocalDisconnect,
		// Token: 0x0400068F RID: 1679
		k_ESNetSocketStateTimeoutDuringConnect,
		// Token: 0x04000690 RID: 1680
		k_ESNetSocketStateRemoteEndDisconnected,
		// Token: 0x04000691 RID: 1681
		k_ESNetSocketStateConnectionBroken
	}
}
