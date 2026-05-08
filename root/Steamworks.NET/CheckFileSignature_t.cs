using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F8 RID: 248
	[CallbackIdentity(705)]
	[StructLayout(0, Pack = 4)]
	public struct CheckFileSignature_t
	{
		// Token: 0x04000309 RID: 777
		public const int k_iCallback = 705;

		// Token: 0x0400030A RID: 778
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
