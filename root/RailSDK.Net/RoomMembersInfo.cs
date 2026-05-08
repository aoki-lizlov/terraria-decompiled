using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200012A RID: 298
	public class RoomMembersInfo : EventBase
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x00010E5C File Offset: 0x0000F05C
		public RoomMembersInfo()
		{
		}

		// Token: 0x0400049A RID: 1178
		public List<MemberInfo> member_info = new List<MemberInfo>();

		// Token: 0x0400049B RID: 1179
		public ulong room_id;

		// Token: 0x0400049C RID: 1180
		public uint member_num;
	}
}
