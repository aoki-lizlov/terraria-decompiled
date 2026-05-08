using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BA RID: 186
	[CallbackIdentity(1324)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x04000226 RID: 550
		public const int k_iCallback = 1324;

		// Token: 0x04000227 RID: 551
		public EResult m_eResult;

		// Token: 0x04000228 RID: 552
		public PublishedFileId_t m_nPublishedFileId;
	}
}
