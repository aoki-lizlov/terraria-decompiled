using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002D RID: 45
	[CallbackIdentity(1030)]
	[StructLayout(0, Pack = 4)]
	public struct TimedTrialStatus_t
	{
		// Token: 0x04000010 RID: 16
		public const int k_iCallback = 1030;

		// Token: 0x04000011 RID: 17
		public AppId_t m_unAppID;

		// Token: 0x04000012 RID: 18
		[MarshalAs(3)]
		public bool m_bIsOffline;

		// Token: 0x04000013 RID: 19
		public uint m_unSecondsAllowed;

		// Token: 0x04000014 RID: 20
		public uint m_unSecondsPlayed;
	}
}
