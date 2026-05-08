using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public struct SteamNetworkingErrMsg
	{
		// Token: 0x04000AE5 RID: 2789
		[MarshalAs(30, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}
