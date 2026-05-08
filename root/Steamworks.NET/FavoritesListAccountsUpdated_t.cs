using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000081 RID: 129
	[CallbackIdentity(516)]
	[StructLayout(0, Pack = 4)]
	public struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x04000160 RID: 352
		public const int k_iCallback = 516;

		// Token: 0x04000161 RID: 353
		public EResult m_eResult;
	}
}
