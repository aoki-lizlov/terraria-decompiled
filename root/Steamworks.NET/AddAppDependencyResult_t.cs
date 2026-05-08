using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D3 RID: 211
	[CallbackIdentity(3414)]
	[StructLayout(0, Pack = 4)]
	public struct AddAppDependencyResult_t
	{
		// Token: 0x04000285 RID: 645
		public const int k_iCallback = 3414;

		// Token: 0x04000286 RID: 646
		public EResult m_eResult;

		// Token: 0x04000287 RID: 647
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000288 RID: 648
		public AppId_t m_nAppID;
	}
}
