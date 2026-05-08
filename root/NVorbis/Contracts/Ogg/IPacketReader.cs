using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x02000039 RID: 57
	internal interface IPacketReader
	{
		// Token: 0x0600023C RID: 572
		Memory<byte> GetPacketData(int pagePacketIndex);

		// Token: 0x0600023D RID: 573
		void InvalidatePacketCache(IPacket packet);
	}
}
