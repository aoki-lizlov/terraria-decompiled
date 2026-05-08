using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D5 RID: 213
	[CallbackIdentity(3416)]
	[StructLayout(0, Pack = 4)]
	public struct GetAppDependenciesResult_t
	{
		// Token: 0x0400028D RID: 653
		public const int k_iCallback = 3416;

		// Token: 0x0400028E RID: 654
		public EResult m_eResult;

		// Token: 0x0400028F RID: 655
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000290 RID: 656
		[MarshalAs(30, SizeConst = 32)]
		public AppId_t[] m_rgAppIDs;

		// Token: 0x04000291 RID: 657
		public uint m_nNumAppDependencies;

		// Token: 0x04000292 RID: 658
		public uint m_nTotalNumAppDependencies;
	}
}
