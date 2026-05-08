using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000031 RID: 49
	internal interface IResidue
	{
		// Token: 0x060001E7 RID: 487
		void Init(IPacket packet, int channels, ICodebook[] codebooks);

		// Token: 0x060001E8 RID: 488
		void Decode(IPacket packet, bool[] doNotDecodeChannel, int blockSize, float[][] buffer);
	}
}
