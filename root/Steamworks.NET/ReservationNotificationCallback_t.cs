using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008B RID: 139
	[CallbackIdentity(5303)]
	[StructLayout(0, Pack = 4)]
	public struct ReservationNotificationCallback_t
	{
		// Token: 0x04000191 RID: 401
		public const int k_iCallback = 5303;

		// Token: 0x04000192 RID: 402
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x04000193 RID: 403
		public CSteamID m_steamIDJoiner;
	}
}
