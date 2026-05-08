using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F5 RID: 245
	[CallbackIdentity(702)]
	[StructLayout(0, Pack = 4)]
	public struct LowBatteryPower_t
	{
		// Token: 0x04000302 RID: 770
		public const int k_iCallback = 702;

		// Token: 0x04000303 RID: 771
		public byte m_nMinutesBatteryLeft;
	}
}
