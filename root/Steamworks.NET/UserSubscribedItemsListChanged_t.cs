using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D7 RID: 215
	[CallbackIdentity(3418)]
	[StructLayout(0, Pack = 4)]
	public struct UserSubscribedItemsListChanged_t
	{
		// Token: 0x04000296 RID: 662
		public const int k_iCallback = 3418;

		// Token: 0x04000297 RID: 663
		public AppId_t m_nAppID;
	}
}
