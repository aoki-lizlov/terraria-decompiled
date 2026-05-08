using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004E RID: 78
	[CallbackIdentity(210)]
	[StructLayout(0, Pack = 4)]
	public struct AssociateWithClanResult_t
	{
		// Token: 0x04000089 RID: 137
		public const int k_iCallback = 210;

		// Token: 0x0400008A RID: 138
		public EResult m_eResult;
	}
}
