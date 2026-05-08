using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000030 RID: 48
	public interface IPacketProvider
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E1 RID: 481
		bool CanSeek { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E2 RID: 482
		int StreamSerial { get; }

		// Token: 0x060001E3 RID: 483
		IPacket GetNextPacket();

		// Token: 0x060001E4 RID: 484
		IPacket PeekNextPacket();

		// Token: 0x060001E5 RID: 485
		long SeekTo(long granulePos, int preRoll, GetPacketGranuleCount getPacketGranuleCount);

		// Token: 0x060001E6 RID: 486
		long GetGranuleCount();
	}
}
