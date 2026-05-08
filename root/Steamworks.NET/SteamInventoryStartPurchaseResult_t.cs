using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000075 RID: 117
	[CallbackIdentity(4704)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInventoryStartPurchaseResult_t
	{
		// Token: 0x0400012C RID: 300
		public const int k_iCallback = 4704;

		// Token: 0x0400012D RID: 301
		public EResult m_result;

		// Token: 0x0400012E RID: 302
		public ulong m_ulOrderID;

		// Token: 0x0400012F RID: 303
		public ulong m_ulTransID;
	}
}
