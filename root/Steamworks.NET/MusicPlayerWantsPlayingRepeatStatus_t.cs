using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009E RID: 158
	[CallbackIdentity(4114)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x040001AD RID: 429
		public const int k_iCallback = 4114;

		// Token: 0x040001AE RID: 430
		public int m_nPlayingRepeatStatus;
	}
}
