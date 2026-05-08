using System;

namespace Steamworks
{
	// Token: 0x0200011E RID: 286
	[Flags]
	public enum EChatMemberStateChange
	{
		// Token: 0x04000662 RID: 1634
		k_EChatMemberStateChangeEntered = 1,
		// Token: 0x04000663 RID: 1635
		k_EChatMemberStateChangeLeft = 2,
		// Token: 0x04000664 RID: 1636
		k_EChatMemberStateChangeDisconnected = 4,
		// Token: 0x04000665 RID: 1637
		k_EChatMemberStateChangeKicked = 8,
		// Token: 0x04000666 RID: 1638
		k_EChatMemberStateChangeBanned = 16
	}
}
