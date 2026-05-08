using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x0200003C RID: 60
	internal interface IStreamPageReader
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600024E RID: 590
		IPacketProvider PacketProvider { get; }

		// Token: 0x0600024F RID: 591
		void AddPage();

		// Token: 0x06000250 RID: 592
		Memory<byte>[] GetPagePackets(int pageIndex);

		// Token: 0x06000251 RID: 593
		int FindPage(long granulePos);

		// Token: 0x06000252 RID: 594
		bool GetPage(int pageIndex, out long granulePos, out bool isResync, out bool isContinuation, out bool isContinued, out int packetCount, out int pageOverhead);

		// Token: 0x06000253 RID: 595
		void SetEndOfStream();

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000254 RID: 596
		int PageCount { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000255 RID: 597
		bool HasAllPages { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000256 RID: 598
		long? MaxGranulePosition { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000257 RID: 599
		int FirstDataPageIndex { get; }
	}
}
