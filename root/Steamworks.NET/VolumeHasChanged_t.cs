using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000090 RID: 144
	[CallbackIdentity(4002)]
	[StructLayout(0, Pack = 4)]
	public struct VolumeHasChanged_t
	{
		// Token: 0x04000199 RID: 409
		public const int k_iCallback = 4002;

		// Token: 0x0400019A RID: 410
		public float m_flNewVolume;
	}
}
