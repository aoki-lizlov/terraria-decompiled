using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000117 RID: 279
	public class GetMemberMetadataInfo : EventBase
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public GetMemberMetadataInfo()
		{
		}

		// Token: 0x04000455 RID: 1109
		public List<RailKeyValue> key_value = new List<RailKeyValue>();

		// Token: 0x04000456 RID: 1110
		public ulong room_id;

		// Token: 0x04000457 RID: 1111
		public ulong member_id;
	}
}
