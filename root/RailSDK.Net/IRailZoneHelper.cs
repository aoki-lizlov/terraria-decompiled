using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200010E RID: 270
	public interface IRailZoneHelper
	{
		// Token: 0x060017B7 RID: 6071
		RailResult AsyncGetZoneList(string user_data);

		// Token: 0x060017B8 RID: 6072
		RailResult AsyncGetRoomListInZone(ulong zone_id, uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<RoomInfoListFilter> filter, string user_data);
	}
}
