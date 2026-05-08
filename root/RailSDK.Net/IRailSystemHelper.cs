using System;

namespace rail
{
	// Token: 0x02000156 RID: 342
	public interface IRailSystemHelper
	{
		// Token: 0x06001831 RID: 6193
		RailResult SetTerminationTimeoutOwnershipExpired(int timeout_seconds);

		// Token: 0x06001832 RID: 6194
		RailSystemState GetPlatformSystemState();
	}
}
