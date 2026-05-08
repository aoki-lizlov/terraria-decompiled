using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017D RID: 381
	[StructLayout(0, Pack = 4)]
	public struct SteamNetConnectionRealTimeStatus_t
	{
		// Token: 0x04000A34 RID: 2612
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A35 RID: 2613
		public int m_nPing;

		// Token: 0x04000A36 RID: 2614
		public float m_flConnectionQualityLocal;

		// Token: 0x04000A37 RID: 2615
		public float m_flConnectionQualityRemote;

		// Token: 0x04000A38 RID: 2616
		public float m_flOutPacketsPerSec;

		// Token: 0x04000A39 RID: 2617
		public float m_flOutBytesPerSec;

		// Token: 0x04000A3A RID: 2618
		public float m_flInPacketsPerSec;

		// Token: 0x04000A3B RID: 2619
		public float m_flInBytesPerSec;

		// Token: 0x04000A3C RID: 2620
		public int m_nSendRateBytesPerSecond;

		// Token: 0x04000A3D RID: 2621
		public int m_cbPendingUnreliable;

		// Token: 0x04000A3E RID: 2622
		public int m_cbPendingReliable;

		// Token: 0x04000A3F RID: 2623
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A40 RID: 2624
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A41 RID: 2625
		[MarshalAs(30, SizeConst = 16)]
		public uint[] reserved;
	}
}
