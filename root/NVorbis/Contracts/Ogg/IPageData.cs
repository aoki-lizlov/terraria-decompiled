using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x0200003A RID: 58
	internal interface IPageData : IPageReader, IDisposable
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023E RID: 574
		long PageOffset { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023F RID: 575
		int StreamSerial { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000240 RID: 576
		int SequenceNumber { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000241 RID: 577
		PageFlags PageFlags { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000242 RID: 578
		long GranulePosition { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000243 RID: 579
		short PacketCount { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000244 RID: 580
		bool? IsResync { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000245 RID: 581
		bool IsContinued { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000246 RID: 582
		int PageOverhead { get; }

		// Token: 0x06000247 RID: 583
		Memory<byte>[] GetPackets();
	}
}
