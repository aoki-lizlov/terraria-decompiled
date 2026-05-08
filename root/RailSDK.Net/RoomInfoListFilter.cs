using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000127 RID: 295
	public class RoomInfoListFilter
	{
		// Token: 0x060017CB RID: 6091 RVA: 0x00010E49 File Offset: 0x0000F049
		public RoomInfoListFilter()
		{
		}

		// Token: 0x0400048D RID: 1165
		public string room_name_contained;

		// Token: 0x0400048E RID: 1166
		public List<RoomInfoListFilterKey> key_filters = new List<RoomInfoListFilterKey>();

		// Token: 0x0400048F RID: 1167
		public EnumRailOptionalValue filter_password;

		// Token: 0x04000490 RID: 1168
		public EnumRailOptionalValue filter_friends_owned;

		// Token: 0x04000491 RID: 1169
		public uint available_slot_at_least;
	}
}
