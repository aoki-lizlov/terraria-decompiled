using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009B RID: 155
	[CallbackIdentity(4011)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x040001A7 RID: 423
		public const int k_iCallback = 4011;

		// Token: 0x040001A8 RID: 424
		public float m_flNewVolume;
	}
}
