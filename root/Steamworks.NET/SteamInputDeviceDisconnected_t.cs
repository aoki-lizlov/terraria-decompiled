using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006E RID: 110
	[CallbackIdentity(2802)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInputDeviceDisconnected_t
	{
		// Token: 0x04000111 RID: 273
		public const int k_iCallback = 2802;

		// Token: 0x04000112 RID: 274
		public InputHandle_t m_ulDisconnectedDeviceHandle;
	}
}
