using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009A RID: 154
	[CallbackIdentity(4110)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerWantsLooped_t
	{
		// Token: 0x040001A5 RID: 421
		public const int k_iCallback = 4110;

		// Token: 0x040001A6 RID: 422
		[MarshalAs(3)]
		public bool m_bLooped;
	}
}
