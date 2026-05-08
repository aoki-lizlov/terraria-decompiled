using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017E RID: 382
	[StructLayout(0, Pack = 4)]
	public struct SteamNetConnectionRealTimeLaneStatus_t
	{
		// Token: 0x04000A42 RID: 2626
		public int m_cbPendingUnreliable;

		// Token: 0x04000A43 RID: 2627
		public int m_cbPendingReliable;

		// Token: 0x04000A44 RID: 2628
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A45 RID: 2629
		public int _reservePad1;

		// Token: 0x04000A46 RID: 2630
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A47 RID: 2631
		[MarshalAs(30, SizeConst = 10)]
		public uint[] reserved;
	}
}
