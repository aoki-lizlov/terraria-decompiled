using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000118 RID: 280
	public class GetRoomMetadataInfo : EventBase
	{
		// Token: 0x060017BC RID: 6076 RVA: 0x00010DDF File Offset: 0x0000EFDF
		public GetRoomMetadataInfo()
		{
		}

		// Token: 0x04000458 RID: 1112
		public List<RailKeyValue> key_value = new List<RailKeyValue>();

		// Token: 0x04000459 RID: 1113
		public ulong room_id;
	}
}
