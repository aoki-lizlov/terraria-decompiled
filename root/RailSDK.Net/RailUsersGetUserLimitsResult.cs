using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000169 RID: 361
	public class RailUsersGetUserLimitsResult : EventBase
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x00010F4B File Offset: 0x0000F14B
		public RailUsersGetUserLimitsResult()
		{
		}

		// Token: 0x04000524 RID: 1316
		public RailID user_id = new RailID();

		// Token: 0x04000525 RID: 1317
		public List<EnumRailUsersLimits> user_limits = new List<EnumRailUsersLimits>();
	}
}
