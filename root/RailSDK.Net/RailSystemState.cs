using System;

namespace rail
{
	// Token: 0x0200015B RID: 347
	public enum RailSystemState
	{
		// Token: 0x040004F8 RID: 1272
		kSystemStateUnknown,
		// Token: 0x040004F9 RID: 1273
		kSystemStatePlatformOnline,
		// Token: 0x040004FA RID: 1274
		kSystemStatePlatformOffline,
		// Token: 0x040004FB RID: 1275
		kSystemStatePlatformExit,
		// Token: 0x040004FC RID: 1276
		kSystemStatePlayerOwnershipExpired = 20,
		// Token: 0x040004FD RID: 1277
		kSystemStatePlayerOwnershipActivated
	}
}
