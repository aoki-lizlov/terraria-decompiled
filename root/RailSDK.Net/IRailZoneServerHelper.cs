using System;

namespace rail
{
	// Token: 0x020001B0 RID: 432
	public interface IRailZoneServerHelper
	{
		// Token: 0x060018FE RID: 6398
		RailZoneID GetPlayerSelectedZoneID();

		// Token: 0x060018FF RID: 6399
		RailZoneID GetRootZoneID();

		// Token: 0x06001900 RID: 6400
		IRailZoneServer OpenZoneServer(RailZoneID zone_id, out RailResult result);

		// Token: 0x06001901 RID: 6401
		RailResult AsyncSwitchPlayerSelectedZone(RailZoneID zone_id);
	}
}
