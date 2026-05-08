using System;

namespace rail
{
	// Token: 0x02000106 RID: 262
	public class PlayerPersonalInfo
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		public PlayerPersonalInfo()
		{
		}

		// Token: 0x040002DA RID: 730
		public RailResult error_code;

		// Token: 0x040002DB RID: 731
		public string avatar_url;

		// Token: 0x040002DC RID: 732
		public uint rail_level;

		// Token: 0x040002DD RID: 733
		public RailID rail_id = new RailID();

		// Token: 0x040002DE RID: 734
		public string rail_name;

		// Token: 0x040002DF RID: 735
		public string email_address;
	}
}
