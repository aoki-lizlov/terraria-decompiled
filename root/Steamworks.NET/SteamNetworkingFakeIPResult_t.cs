using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FF RID: 255
	[CallbackIdentity(1223)]
	[StructLayout(0, Pack = 4)]
	public struct SteamNetworkingFakeIPResult_t
	{
		// Token: 0x0400031A RID: 794
		public const int k_iCallback = 1223;

		// Token: 0x0400031B RID: 795
		public EResult m_eResult;

		// Token: 0x0400031C RID: 796
		public SteamNetworkingIdentity m_identity;

		// Token: 0x0400031D RID: 797
		public uint m_unIP;

		// Token: 0x0400031E RID: 798
		[MarshalAs(30, SizeConst = 8)]
		public ushort[] m_unPorts;
	}
}
