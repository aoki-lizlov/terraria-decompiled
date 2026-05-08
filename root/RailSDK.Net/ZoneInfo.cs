using System;

namespace rail
{
	// Token: 0x0200012F RID: 303
	public class ZoneInfo
	{
		// Token: 0x060017D3 RID: 6099 RVA: 0x00002119 File Offset: 0x00000319
		public ZoneInfo()
		{
		}

		// Token: 0x040004A6 RID: 1190
		public EnumZoneStatus status;

		// Token: 0x040004A7 RID: 1191
		public string description;

		// Token: 0x040004A8 RID: 1192
		public string name;

		// Token: 0x040004A9 RID: 1193
		public ulong idc_id;

		// Token: 0x040004AA RID: 1194
		public uint country_code;

		// Token: 0x040004AB RID: 1195
		public ulong zone_id;
	}
}
