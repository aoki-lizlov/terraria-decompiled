using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017F RID: 383
	[StructLayout(0, Pack = 4)]
	public struct SteamNetworkPingLocation_t
	{
		// Token: 0x04000A48 RID: 2632
		[MarshalAs(30, SizeConst = 512)]
		public byte[] m_data;
	}
}
