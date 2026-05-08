using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F9 RID: 249
	[CallbackIdentity(714)]
	[StructLayout(0, Pack = 4)]
	public struct GamepadTextInputDismissed_t
	{
		// Token: 0x0400030B RID: 779
		public const int k_iCallback = 714;

		// Token: 0x0400030C RID: 780
		[MarshalAs(3)]
		public bool m_bSubmitted;

		// Token: 0x0400030D RID: 781
		public uint m_unSubmittedText;

		// Token: 0x0400030E RID: 782
		public AppId_t m_unAppID;
	}
}
