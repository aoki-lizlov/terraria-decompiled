using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009D RID: 157
	[CallbackIdentity(4013)]
	[StructLayout(0, Pack = 4)]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x040001AB RID: 427
		public const int k_iCallback = 4013;

		// Token: 0x040001AC RID: 428
		public int nID;
	}
}
