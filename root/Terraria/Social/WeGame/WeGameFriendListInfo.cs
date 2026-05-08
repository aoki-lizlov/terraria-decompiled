using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200013D RID: 317
	[DataContract]
	public class WeGameFriendListInfo
	{
		// Token: 0x06001C8A RID: 7306 RVA: 0x0000357B File Offset: 0x0000177B
		public WeGameFriendListInfo()
		{
		}

		// Token: 0x040015E1 RID: 5601
		[DataMember]
		public List<RailFriendInfo> _friendList;
	}
}
