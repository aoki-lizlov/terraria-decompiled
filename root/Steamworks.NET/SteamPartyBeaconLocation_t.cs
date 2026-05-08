using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000175 RID: 373
	[StructLayout(0, Pack = 4)]
	public struct SteamPartyBeaconLocation_t
	{
		// Token: 0x040009F5 RID: 2549
		public ESteamPartyBeaconLocationType m_eType;

		// Token: 0x040009F6 RID: 2550
		public ulong m_ulLocationID;
	}
}
