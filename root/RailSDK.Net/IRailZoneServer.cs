using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020001AF RID: 431
	public interface IRailZoneServer : IRailComponent
	{
		// Token: 0x060018F2 RID: 6386
		RailZoneID GetZoneID();

		// Token: 0x060018F3 RID: 6387
		RailResult GetZoneNameLanguages(List<string> languages);

		// Token: 0x060018F4 RID: 6388
		RailResult GetZoneName(string language_filter, out string zone_name);

		// Token: 0x060018F5 RID: 6389
		RailResult GetZoneDescriptionLanguages(List<string> languages);

		// Token: 0x060018F6 RID: 6390
		RailResult GetZoneDescription(string language_filter, out string zone_description);

		// Token: 0x060018F7 RID: 6391
		RailResult GetGameServerAddresses(List<string> server_addresses);

		// Token: 0x060018F8 RID: 6392
		RailResult GetZoneMetadatas(List<RailKeyValue> key_values);

		// Token: 0x060018F9 RID: 6393
		RailResult GetChildrenZoneIDs(List<RailZoneID> zone_ids);

		// Token: 0x060018FA RID: 6394
		bool IsZoneVisiable();

		// Token: 0x060018FB RID: 6395
		bool IsZoneJoinable();

		// Token: 0x060018FC RID: 6396
		uint GetZoneEnableStartTime();

		// Token: 0x060018FD RID: 6397
		uint GetZoneEnableEndTime();
	}
}
