using System;

namespace NVorbis.Contracts
{
	// Token: 0x0200002B RID: 43
	internal interface IMapping
	{
		// Token: 0x060001C9 RID: 457
		void Init(IPacket packet, int channels, IFloor[] floors, IResidue[] residues, IMdct mdct);

		// Token: 0x060001CA RID: 458
		void DecodePacket(IPacket packet, int blockSize, int channels, float[][] buffer);
	}
}
