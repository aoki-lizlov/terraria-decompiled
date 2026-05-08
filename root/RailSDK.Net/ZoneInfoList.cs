using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000130 RID: 304
	public class ZoneInfoList : EventBase
	{
		// Token: 0x060017D4 RID: 6100 RVA: 0x00010E82 File Offset: 0x0000F082
		public ZoneInfoList()
		{
		}

		// Token: 0x040004AC RID: 1196
		public List<ZoneInfo> zone_info = new List<ZoneInfo>();
	}
}
