using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D0 RID: 208
	[CallbackIdentity(3411)]
	[StructLayout(0, Pack = 4)]
	public struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x0400027B RID: 635
		public const int k_iCallback = 3411;

		// Token: 0x0400027C RID: 636
		public EResult m_eResult;
	}
}
