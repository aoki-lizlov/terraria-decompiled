using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003F RID: 63
	[CallbackIdentity(347)]
	[StructLayout(0, Pack = 4)]
	public struct SetPersonaNameResponse_t
	{
		// Token: 0x04000053 RID: 83
		public const int k_iCallback = 347;

		// Token: 0x04000054 RID: 84
		[MarshalAs(3)]
		public bool m_bSuccess;

		// Token: 0x04000055 RID: 85
		[MarshalAs(3)]
		public bool m_bLocalSuccess;

		// Token: 0x04000056 RID: 86
		public EResult m_result;
	}
}
