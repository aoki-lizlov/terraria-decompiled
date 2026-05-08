using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E6 RID: 230
	[CallbackIdentity(167)]
	[StructLayout(0, Pack = 4)]
	public struct DurationControl_t
	{
		// Token: 0x040002C5 RID: 709
		public const int k_iCallback = 167;

		// Token: 0x040002C6 RID: 710
		public EResult m_eResult;

		// Token: 0x040002C7 RID: 711
		public AppId_t m_appid;

		// Token: 0x040002C8 RID: 712
		[MarshalAs(3)]
		public bool m_bApplicable;

		// Token: 0x040002C9 RID: 713
		public int m_csecsLast5h;

		// Token: 0x040002CA RID: 714
		public EDurationControlProgress m_progress;

		// Token: 0x040002CB RID: 715
		public EDurationControlNotification m_notification;

		// Token: 0x040002CC RID: 716
		public int m_csecsToday;

		// Token: 0x040002CD RID: 717
		public int m_csecsRemaining;
	}
}
