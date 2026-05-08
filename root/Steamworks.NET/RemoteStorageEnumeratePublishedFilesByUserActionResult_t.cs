using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BE RID: 190
	[CallbackIdentity(1328)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x04000236 RID: 566
		public const int k_iCallback = 1328;

		// Token: 0x04000237 RID: 567
		public EResult m_eResult;

		// Token: 0x04000238 RID: 568
		public EWorkshopFileAction m_eAction;

		// Token: 0x04000239 RID: 569
		public int m_nResultsReturned;

		// Token: 0x0400023A RID: 570
		public int m_nTotalResultCount;

		// Token: 0x0400023B RID: 571
		[MarshalAs(30, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x0400023C RID: 572
		[MarshalAs(30, SizeConst = 50)]
		public uint[] m_rgRTimeUpdated;
	}
}
