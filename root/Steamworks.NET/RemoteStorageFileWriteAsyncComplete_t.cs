using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C1 RID: 193
	[CallbackIdentity(1331)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		// Token: 0x04000244 RID: 580
		public const int k_iCallback = 1331;

		// Token: 0x04000245 RID: 581
		public EResult m_eResult;
	}
}
