using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008C RID: 140
	[CallbackIdentity(5304)]
	[StructLayout(0, Pack = 4)]
	public struct ChangeNumOpenSlotsCallback_t
	{
		// Token: 0x04000194 RID: 404
		public const int k_iCallback = 5304;

		// Token: 0x04000195 RID: 405
		public EResult m_eResult;
	}
}
