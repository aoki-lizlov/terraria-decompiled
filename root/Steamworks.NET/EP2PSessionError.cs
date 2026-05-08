using System;

namespace Steamworks
{
	// Token: 0x02000123 RID: 291
	public enum EP2PSessionError
	{
		// Token: 0x0400067B RID: 1659
		k_EP2PSessionErrorNone,
		// Token: 0x0400067C RID: 1660
		k_EP2PSessionErrorNoRightsToApp = 2,
		// Token: 0x0400067D RID: 1661
		k_EP2PSessionErrorTimeout = 4,
		// Token: 0x0400067E RID: 1662
		k_EP2PSessionErrorNotRunningApp_DELETED = 1,
		// Token: 0x0400067F RID: 1663
		k_EP2PSessionErrorDestinationNotLoggedIn_DELETED = 3,
		// Token: 0x04000680 RID: 1664
		k_EP2PSessionErrorMax = 5
	}
}
