using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006D RID: 109
	[CallbackIdentity(2801)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInputDeviceConnected_t
	{
		// Token: 0x0400010F RID: 271
		public const int k_iCallback = 2801;

		// Token: 0x04000110 RID: 272
		public InputHandle_t m_ulConnectedDeviceHandle;
	}
}
