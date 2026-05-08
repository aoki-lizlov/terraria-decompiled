using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D4 RID: 212
	[CallbackIdentity(3415)]
	[StructLayout(0, Pack = 4)]
	public struct RemoveAppDependencyResult_t
	{
		// Token: 0x04000289 RID: 649
		public const int k_iCallback = 3415;

		// Token: 0x0400028A RID: 650
		public EResult m_eResult;

		// Token: 0x0400028B RID: 651
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400028C RID: 652
		public AppId_t m_nAppID;
	}
}
