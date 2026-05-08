using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DC RID: 220
	[CallbackIdentity(113)]
	[StructLayout(0, Pack = 4)]
	public struct ClientGameServerDeny_t
	{
		// Token: 0x040002A5 RID: 677
		public const int k_iCallback = 113;

		// Token: 0x040002A6 RID: 678
		public uint m_uAppID;

		// Token: 0x040002A7 RID: 679
		public uint m_unGameServerIP;

		// Token: 0x040002A8 RID: 680
		public ushort m_usGameServerPort;

		// Token: 0x040002A9 RID: 681
		public ushort m_bSecure;

		// Token: 0x040002AA RID: 682
		public uint m_uReason;
	}
}
