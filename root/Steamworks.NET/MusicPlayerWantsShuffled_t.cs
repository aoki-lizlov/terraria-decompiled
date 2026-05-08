using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000099 RID: 153
	[CallbackIdentity(4109)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x040001A3 RID: 419
		public const int k_iCallback = 4109;

		// Token: 0x040001A4 RID: 420
		[MarshalAs(3)]
		public bool m_bShuffled;
	}
}
