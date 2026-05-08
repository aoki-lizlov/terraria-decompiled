using System;

namespace rail
{
	// Token: 0x0200012B RID: 299
	public class RoomOptions
	{
		// Token: 0x060017CF RID: 6095 RVA: 0x00002119 File Offset: 0x00000319
		public RoomOptions()
		{
		}

		// Token: 0x0400049D RID: 1181
		public uint max_members;

		// Token: 0x0400049E RID: 1182
		public string password;

		// Token: 0x0400049F RID: 1183
		public EnumRoomType type;

		// Token: 0x040004A0 RID: 1184
		public ulong zone_id;

		// Token: 0x040004A1 RID: 1185
		public bool enable_team_voice;
	}
}
