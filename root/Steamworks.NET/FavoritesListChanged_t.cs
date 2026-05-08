using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000077 RID: 119
	[CallbackIdentity(502)]
	[StructLayout(0, Pack = 4)]
	public struct FavoritesListChanged_t
	{
		// Token: 0x04000133 RID: 307
		public const int k_iCallback = 502;

		// Token: 0x04000134 RID: 308
		public uint m_nIP;

		// Token: 0x04000135 RID: 309
		public uint m_nQueryPort;

		// Token: 0x04000136 RID: 310
		public uint m_nConnPort;

		// Token: 0x04000137 RID: 311
		public uint m_nAppID;

		// Token: 0x04000138 RID: 312
		public uint m_nFlags;

		// Token: 0x04000139 RID: 313
		[MarshalAs(3)]
		public bool m_bAdd;

		// Token: 0x0400013A RID: 314
		public AccountID_t m_unAccountId;
	}
}
