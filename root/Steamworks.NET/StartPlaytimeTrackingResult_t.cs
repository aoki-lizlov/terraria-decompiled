using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CF RID: 207
	[CallbackIdentity(3410)]
	[StructLayout(0, Pack = 4)]
	public struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x04000279 RID: 633
		public const int k_iCallback = 3410;

		// Token: 0x0400027A RID: 634
		public EResult m_eResult;
	}
}
