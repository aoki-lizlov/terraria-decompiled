using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009C RID: 156
	[CallbackIdentity(4012)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x040001A9 RID: 425
		public const int k_iCallback = 4012;

		// Token: 0x040001AA RID: 426
		public int nID;
	}
}
