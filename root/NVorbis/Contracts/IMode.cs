using System;

namespace NVorbis.Contracts
{
	// Token: 0x0200002D RID: 45
	internal interface IMode
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CC RID: 460
		int BlockSize { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001CD RID: 461
		float[][] Windows { get; }

		// Token: 0x060001CE RID: 462
		void Init(IPacket packet, int channels, int block0Size, int block1Size, IMapping[] mappings);

		// Token: 0x060001CF RID: 463
		bool Decode(IPacket packet, float[][] buffer, out int packetStartindex, out int packetValidLength, out int packetTotalLength);

		// Token: 0x060001D0 RID: 464
		int GetPacketSampleCount(IPacket packet, bool isLastInPage);
	}
}
