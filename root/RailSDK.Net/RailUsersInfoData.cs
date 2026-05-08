using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200016A RID: 362
	public class RailUsersInfoData : EventBase
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00010F69 File Offset: 0x0000F169
		public RailUsersInfoData()
		{
		}

		// Token: 0x04000526 RID: 1318
		public List<PlayerPersonalInfo> user_info_list = new List<PlayerPersonalInfo>();
	}
}
